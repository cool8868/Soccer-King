using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Enum;

namespace SkillEngine.SkillImpl.Football
{
    public class FootballPlayerMotionSeeker : PlayerSeekerBase
    {
        #region Cache
        #endregion

        #region .ctor
        #endregion

        #region Data
        public EnumMotionSeekType SeekType
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
            if (null == srcPlayer)
            {
                switch (SeekType)
                {
                    case EnumMotionSeekType.OwnTeam:
                    case EnumMotionSeekType.OppTeam:
                    case EnumMotionSeekType.OwnBallHandler:
                    case EnumMotionSeekType.OppBallHandler:
                    case EnumMotionSeekType.OwnGoalKeeper:
                    case EnumMotionSeekType.OppGoalKeeper:
                        break;
                    default:
                        return rst;
                }
            }
            ISkillPlayer tmp = null;
            switch (SeekType)
            {
                case EnumMotionSeekType.OwnPlayer:
                    rst.Add(srcPlayer);
                    break;
                case EnumMotionSeekType.OppPlayer:
                    tmp = srcPlayer.OppSkillPlayer;
                    if (null != tmp)
                        rst.Add(tmp);
                    break;
                case EnumMotionSeekType.PairingPlayer:
                    tmp = srcPlayer.SkillPairingPlayer;
                    if (null != tmp)
                        rst.Add(tmp);
                    break;
                case EnumMotionSeekType.OppPairingPlayer:
                    tmp = srcPlayer.SkillPairingPlayer;
                    if (null != tmp)
                        tmp = tmp.OppSkillPlayer;
                    if (null != tmp)
                        rst.Add(tmp);
                    break;
                case EnumMotionSeekType.PairedPlayer:
                    tmp = srcPlayer.SkillPairedPlayer;
                    if (null != tmp)
                        rst.Add(tmp);
                    break;
                case EnumMotionSeekType.AssistPlayer:
                    tmp = srcPlayer.SkillAssistPlayer;
                    if (null != tmp)
                        rst.Add(tmp);
                    break;
                case EnumMotionSeekType.OwnMates:
                    foreach (var item in srcManager.SkillPlayerList)
                    {
                        if (item == srcPlayer || item.Disable)
                            continue;
                        rst.Add(item);
                    }
                    break;
                case EnumMotionSeekType.OwnTeam:
                    foreach (var item in srcManager.SkillPlayerList)
                    {
                        if (item.Disable)
                            continue;
                        rst.Add(item);
                    }
                    break;
                case EnumMotionSeekType.OppTeam:
                    foreach (var item in srcManager.OppSkillManager.SkillPlayerList)
                    {
                        if (item.Disable)
                            continue;
                        rst.Add(item);
                    }
                    break;
                case EnumMotionSeekType.OwnBallHandler:
                    tmp = srcManager.SkillMatch.SkillBallHandler;
                    if (null != tmp & tmp.SkillManager == srcManager)
                        rst.Add(tmp);
                    break;
                case EnumMotionSeekType.OppBallHandler:
                    tmp = srcManager.SkillMatch.SkillBallHandler;
                    if (null != tmp & tmp.SkillManager != srcManager)
                        rst.Add(tmp);
                    break;
                case EnumMotionSeekType.OwnGoalKeeper:
                    var list = ((IManager)srcManager).GetPlayersByPosition(Position.Goalkeeper);
                    if (null != list && list.Count > 0)
                        rst.Add(list[0]);
                    break;
                case EnumMotionSeekType.OppGoalKeeper:
                    list = ((IManager)srcManager.OppSkillManager).GetPlayersByPosition(Position.Goalkeeper);
                    if (null != list && list.Count > 0)
                        rst.Add(list[0]);
                    break;
            }
            return rst;
        }
       
    }
}
