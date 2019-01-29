using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public class PlayerMotionStateFilter : ValueSetFilterBase<int>, IPlayerFilter, ITrigger
    {
        public PlayerMotionStateFilter(int[] values, EnumDoneStateFlag stateType)
            : base(values)
        {
            this.StateType = stateType;
            this.Side = EnumOwnSide.Own;
        }

        #region IPlayerFilter
        /// <summary>
        /// 0-当前动作; 1-最近失败；2-最近成功;3-动作结束
        /// </summary>
        public EnumDoneStateFlag StateType
        {
            get;
            set;
        }
        public EnumOwnSide Side
        {
            get;
            set;
        }

        public bool Check(ISkillManager srcManager, ISkillPlayer srcPlayer, ISkillPlayer dstPlayer)
        {
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
        public bool Trigger(ISkill srcSkill, ISkillPlayer caster)
        {
            if (null == caster)
                return false;
            return CheckCore(caster);
        }
        #endregion

        bool CheckCore(ISkillPlayer dstPlayer)
        {
            if (null != dstPlayer && this.Side == EnumOwnSide.Opp)
                dstPlayer = dstPlayer.OppSkillPlayer;
            if (null == dstPlayer)
                return false;
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
            return CheckValue(doingFlag ? dstPlayer.DoingState : dstPlayer.DoneState);
        }
    }
}
