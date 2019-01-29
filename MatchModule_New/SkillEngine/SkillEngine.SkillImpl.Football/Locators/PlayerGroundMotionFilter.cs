using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillBase.Enum.Football;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Enum;

namespace SkillEngine.SkillImpl.Football
{
    public class PlayerGroundMotionFilter : ValueSetFilterBase<int>, IPlayerFilter, ITrigger
    {
        public PlayerGroundMotionFilter(int[] values, EnumDoneStateFlag stateType)
            : base(values)
        {
            this.StateType = stateType;
            this.SeekType = EnumMotionSeekType.OwnPlayer;
            this.GroundType = EnumGroundType.All;
        }

        #region Data
        public EnumMotionSeekType SeekType
        {
            get;
            set;
        }

        public EnumGroundType GroundType
        {
            get;
            set;
        }

        /// <summary>
        /// 0-当前动作; 1-最近失败；2-最近成功;3-动作结束
        /// </summary>
        public EnumDoneStateFlag StateType
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
            return CheckCore(dstPlayer);
        }
        protected override bool InnerEquals(int x, int y)
        {
            return x == y;
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
                    if (CheckCore(player))
                        return true;
                }
                if (this.SeekType == EnumMotionSeekType.BothTeam)
                {
                    players = InnerSeekMulti(srcManager.OppSkillManager);
                    if (null == players)
                        return false;
                    foreach (var player in players)
                    {
                        if (CheckCore(player))
                            return true;
                    }
                }
                return false;
            }
            var dstPlayer = InnerSeek(srcManager, caster);
            if (null == dstPlayer)
                return false;
            return CheckCore(dstPlayer);
        }
        #endregion

        protected virtual bool CheckCore(ISkillPlayer dstPlayer)
        {
            bool doingFlag = false;
            switch (this.StateType)
            {
                case EnumDoneStateFlag.None:
                    doingFlag = true;
                    break;
                case EnumDoneStateFlag.Over:
                    if (dstPlayer.DoneStateFlag == EnumDoneStateFlag.None)
                        return false;
                    break;
                case EnumDoneStateFlag.Fail:
                case EnumDoneStateFlag.Succ:
                    if (dstPlayer.DoneStateFlag != this.StateType)
                        return false;
                    break;
            }
            if (!CheckValue(doingFlag ? dstPlayer.DoingState : dstPlayer.DoneState))
                return false;
            if (this.GroundType == EnumGroundType.All)
                return true;
            bool homeFlag = dstPlayer.SkillManager.SkillSide == EnumMatchSide.Home;
            if (this.GroundType == EnumGroundType.Opp)
                homeFlag = !homeFlag;
            Region area = homeFlag ? FootballPitchDefines.HOMERegion : FootballPitchDefines.AwayRegion;
            return area.IsCoordinateInRegion(((IPlayer)dstPlayer).Status.Current);
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
        ISkillPlayer InnerSeek(ISkillManager srcManager,ISkillPlayer srcPlayer)
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
    }
}
