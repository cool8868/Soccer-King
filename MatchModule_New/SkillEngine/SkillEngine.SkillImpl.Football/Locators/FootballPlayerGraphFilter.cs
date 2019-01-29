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
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Interface;

namespace SkillEngine.SkillImpl.Football
{
    public class FootballPlayerGraphFilter : IPlayerFilter, ITrigger
    {
        #region Data
        public EnumMotionSeekType SeekType
        {
            get;
            set;
        }
        public EnumGraphSeekType GraphType
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

        #region IPlayerFilter
        public bool Check(ISkillManager srcManager, ISkillPlayer srcPlayer, ISkillPlayer dstPlayer)
        {
            dstPlayer = InnerSeek(srcManager, dstPlayer);
            if (null == dstPlayer)
                return false;
            return CheckCore(srcManager, dstPlayer);
        }
        #endregion

        #region ITrigger
        public bool Repeat
        {
            get;
            set;
        }
        public bool Recycle
        {
            get;
            set;
        }
        public bool Delay
        {
            get;
            set;
        }
        public bool Trigger(ISkill srcSkill, ISkillPlayer caster)
        {
            var srcManager = null == caster ? srcSkill.Owner as ISkillManager : caster.SkillManager;
            var players = InnerSeekMulti(srcManager);
            if (null != players)
            {
                foreach (var player in players)
                {
                    if (CheckCore(srcManager, player))
                        return true;
                }
                if (this.SeekType == EnumMotionSeekType.BothTeam)
                {
                    players = InnerSeekMulti(srcManager.OppSkillManager);
                    if (null == players)
                        return false;
                    foreach (var player in players)
                    {
                        if (CheckCore(srcManager, player))
                            return true;
                    }
                }
                return false;
            }
            var dstPlayer = InnerSeek(srcManager, caster);
            if (null == dstPlayer)
                return false;
            return CheckCore(srcManager, dstPlayer);
        }
        #endregion

        #region Tools
        bool CheckCore(ISkillManager srcManager, ISkillPlayer srcPlayer)
        {
            if (null == srcManager)
                return false;
            bool homeFlag = srcManager.SkillSide == EnumMatchSide.Home;
            switch (GraphType)
            {
                case EnumGraphSeekType.OppGoal:
                case EnumGraphSeekType.OppBackLine:
                case EnumGraphSeekType.OppGoalArea:
                case EnumGraphSeekType.OppRegion:
                    homeFlag = !homeFlag;
                    break;
                case EnumGraphSeekType.PlayerPoint:
                    if (null == srcPlayer)
                        return false;
                    break;
            }
            double min, max, val;
            min = max = val = 0;
            switch (GraphType)
            {
                case EnumGraphSeekType.OwnGoal:
                case EnumGraphSeekType.OppGoal:
                    Coordinate goal = homeFlag ? FootballPitchDefines.HOMEGoal : FootballPitchDefines.AWAYGoal;
                    min = Math.Pow(MinSpan, 2);
                    max = Math.Pow(MaxSpan, 2);
                    val = ((IPlayer)srcPlayer).Status.Current.SimpleDistance(goal);
                    if (min <= val && val <= max)
                        return true;
                    return false;
                case EnumGraphSeekType.OwnBackLine:
                case EnumGraphSeekType.OppBackLine:
                    double line = homeFlag ? FootballPitchDefines.HOMEBackLine : FootballPitchDefines.AWAYBackLine;
                    val = Math.Abs(((IPlayer)srcPlayer).Status.Current.X - line);
                    if (MinSpan <= val && val <= MaxSpan)
                        return true;
                    return false;
                case EnumGraphSeekType.OwnRegion:
                case EnumGraphSeekType.OppRegion:
                    bool inArea = MaxSpan <= 0;
                    Region area = homeFlag ? FootballPitchDefines.HOMERegion : FootballPitchDefines.AwayRegion;
                    if (inArea == area.IsCoordinateInRegion(((IPlayer)srcPlayer).Status.Current))
                        return true;
                    return false;
            }
            return false;
        }
        List<ISkillPlayer> InnerSeekMulti(ISkillManager srcManager)
        {
            switch (this.SeekType)
            {
                case EnumMotionSeekType.OwnTeam:
                    return srcManager.SkillPlayerList;
                case EnumMotionSeekType.OppTeam:
                    return srcManager.OppSkillManager.SkillPlayerList;
                case EnumMotionSeekType.BothTeam:
                    return srcManager.SkillPlayerList;
                default:
                    return null;
            }
        }
        ISkillPlayer InnerSeek(ISkillManager srcManager, ISkillPlayer srcPlayer)
        {
            if (null == srcManager)
                return null;
            if (null == srcPlayer)
            {
                switch (SeekType)
                {
                    case EnumMotionSeekType.OwnBallHandler:
                    case EnumMotionSeekType.OppBallHandler:
                    case EnumMotionSeekType.OwnGoalKeeper:
                    case EnumMotionSeekType.OppGoalKeeper:
                        break;
                    default:
                        return null;
                }
            }
            ISkillPlayer tmp = null;
            switch (SeekType)
            {
                case EnumMotionSeekType.OwnPlayer:
                    return srcPlayer;
                case EnumMotionSeekType.OppPlayer:
                    return srcPlayer.OppSkillPlayer;
                case EnumMotionSeekType.OppParaPlayer:
                    foreach (var item in srcManager.OppSkillManager.SkillPlayerList)
                    {
                        if (item.SkillPosition == srcPlayer.SkillPosition)
                        {
                            return item;
                        }
                    }
                    break;
                case EnumMotionSeekType.PairingPlayer:
                    return srcPlayer.SkillPairingPlayer;
                case EnumMotionSeekType.OppPairingPlayer:
                    tmp = srcPlayer.SkillPairingPlayer;
                    if (null == tmp)
                        return null;
                    return tmp.OppSkillPlayer;
                case EnumMotionSeekType.PairedPlayer:
                    return srcPlayer.SkillPairedPlayer;
                case EnumMotionSeekType.AssistPlayer:
                    return srcPlayer.SkillAssistPlayer;

                case EnumMotionSeekType.OwnBallHandler:
                    tmp = srcManager.SkillMatch.SkillBallHandler;
                    if (null != tmp & tmp.SkillManager == srcManager)
                        return tmp;
                    break;
                case EnumMotionSeekType.OppBallHandler:
                    tmp = srcManager.SkillMatch.SkillBallHandler;
                    if (null != tmp & tmp.SkillManager != srcManager)
                        return tmp;
                    break;
                case EnumMotionSeekType.OwnGoalKeeper:
                    var list = ((IManager)srcManager).GetPlayersByPosition(Position.Goalkeeper);
                    if (null != list && list.Count > 0)
                        return list[0];
                    break;
                case EnumMotionSeekType.OppGoalKeeper:
                    list = ((IManager)srcManager.OppSkillManager).GetPlayersByPosition(Position.Goalkeeper);
                    if (null != list && list.Count > 0)
                        return list[0];
                    break;
            }
            return null;

        }
        #endregion
    }
}
