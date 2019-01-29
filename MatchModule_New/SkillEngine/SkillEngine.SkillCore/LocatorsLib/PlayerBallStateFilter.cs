using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;


namespace SkillEngine.SkillCore
{
    public class PlayerBallStateFilter:IPlayerFilter,ITrigger
    {
        #region Data
        public EnumBallSide BallSide
        {
            get;
            set;
        }
        public EnumBallState BallState
        {
            get;
            set;
        }
        #endregion

        #region IPlayerFilter
        public bool Check(ISkillManager srcManager, ISkillPlayer srcPlayer, ISkillPlayer dstPlayer)
        {
            return CheckCore(dstPlayer, srcManager);
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
            if (null == caster)
                return CheckCore(caster, srcSkill.Owner as ISkillManager);
            return CheckCore(caster, caster.SkillManager);
        }
        #endregion

        bool CheckCore(ISkillPlayer dstPlayer, ISkillManager srcManager)
        {
            if (this.BallSide == EnumBallSide.None && this.BallState == EnumBallState.None)
                return true;
            var holder = srcManager.SkillMatch.SkillBallHandler;
            if (null == holder || null == srcManager)
                return false;
            bool atkFlag = srcManager.SkillSide == holder.SkillSide;
            bool holdFlag = null != dstPlayer && dstPlayer.SkillHoldBall;
            switch (this.BallSide)
            {
                case EnumBallSide.Atk:
                    if (!atkFlag)
                        return false;
                    break;
                case EnumBallSide.Def:
                    if (atkFlag)
                        return false;
                    break;
            }
            switch (this.BallState)
            {
                case EnumBallState.HoldBall:
                    if (!holdFlag)
                        return false;
                    break;
                case EnumBallState.OffBall:
                    if (holdFlag)
                        return false;
                    break;
            }
            return true;
        }
    }
}
