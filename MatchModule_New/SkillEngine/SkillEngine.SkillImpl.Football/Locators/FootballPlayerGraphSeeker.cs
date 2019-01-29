using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Interface;

namespace SkillEngine.SkillImpl.Football
{
    public class FootballPlayerGraphSeeker : PlayerSeekerBase
    {
        #region .ctor
        #endregion

        #region Data
        public EnumOwnSide Side
        {
            get;
            set;
        }
        public EnumGraphSeekType SeekType
        {
            get;
            set;
        }
        public int MinSpan
        {
            get;
            set;
        }
        public int MaxSpan
        {
            get;
            set;
        }
        #endregion

        protected override List<ISkillPlayer> InnerSeek(ISkill srcSkill, ISkillManager srcManager, ISkillPlayer srcPlayer)
        {
            var rst = new List<ISkillPlayer>();
            if (null == srcManager)
                return rst;
            var players = Side == EnumOwnSide.Own ? srcManager.SkillPlayerList : srcManager.OppSkillManager.SkillPlayerList;
            bool homeFlag = srcManager.SkillSide == EnumMatchSide.Home;
            switch (SeekType)
            {
                case EnumGraphSeekType.OppGoal:
                case EnumGraphSeekType.OppBackLine:
                case EnumGraphSeekType.OppGoalArea:
                case EnumGraphSeekType.OppRegion:
                    homeFlag = !homeFlag;
                    break;
                case EnumGraphSeekType.PlayerPoint:
                    if (null == srcPlayer)
                        return rst;
                    break;
            }
            double min, max, val;
            min = max = val = 0;

            switch (SeekType)
            {
                case EnumGraphSeekType.OwnGoal:
                case EnumGraphSeekType.OppGoal:
                    Coordinate goal = homeFlag ? FootballPitchDefines.HOMEGoal : FootballPitchDefines.AWAYGoal;
                    min = Math.Pow(MinSpan, 2);
                    max = Math.Pow(MaxSpan, 2);
                    foreach (var player in players)
                    {
                        val = ((IPlayer)player).Status.Current.SimpleDistance(goal);
                        if (min <= val && val <= max)
                            rst.Add(player);
                    }
                    break;
                case EnumGraphSeekType.OwnBackLine:
                case EnumGraphSeekType.OppBackLine:
                    double line = homeFlag ? FootballPitchDefines.HOMEBackLine : FootballPitchDefines.AWAYBackLine;
                    foreach (var player in players)
                    {
                        val = Math.Abs(((IPlayer)player).Status.Current.X - line);
                        if (MinSpan <= val && val <= MaxSpan)
                            rst.Add(player);
                    }
                    break;
                case EnumGraphSeekType.OwnRegion:
                case EnumGraphSeekType.OppRegion:
                    bool inArea = MaxSpan <= 0;
                    Region area = homeFlag ? FootballPitchDefines.HOMERegion : FootballPitchDefines.AwayRegion;
                    foreach (var player in players)
                    {
                        if (inArea == area.IsCoordinateInRegion(((IPlayer)player).Status.Current))
                            rst.Add(player);
                    }
                    break;
                case EnumGraphSeekType.PlayerPoint:
                    min = Math.Pow(MinSpan, 2);
                    max = Math.Pow(MaxSpan, 2);
                    Coordinate coord = ((IPlayer)srcPlayer).Status.Current;
                    foreach (var player in players)
                    {
                        val = ((IPlayer)player).Status.Current.SimpleDistance(coord);
                        if (min <= val && val <= max)
                            rst.Add(player);
                    }
                    break;
            }
            return rst;
        }
    }

    public static class FootballPitchDefines
    {
        public const int HOMEBackLine = 0;
        public const int AWAYBackLine = 210;
        public static readonly Coordinate HOMEGoal = new Coordinate(0, 68);
        public static readonly Coordinate AWAYGoal = new Coordinate(210, 68);
        public static readonly Region HOMERegion = new Region(0, 0, 105, 136);
        public static readonly Region AwayRegion = new Region(105, 0, 210, 136);
    }
}
