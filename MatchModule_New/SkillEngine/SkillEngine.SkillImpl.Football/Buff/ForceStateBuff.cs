using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;

namespace SkillEngine.SkillImpl.Football
{
    public class ForceStateBuff : BuffBase
    {
        #region .ctor
        public ForceStateBuff(ISkill skill, EnumForceState forceState)
            : base(skill, EnumBuffType.Spec, new int[] { (int)EnumSpecEffect.ForceState })
        {
            this.ForceState = forceState;
        }
        #endregion

        #region Data
        public EnumForceState ForceState
        {
            get;
            set;
        }
        public override bool DebuffFlag
        {
            get
            {
                return false;
            }
        }
        public override bool ValuedFlag
        {
            get
            {
                return this.ForceState != EnumForceState.None;
            }
        }
        #endregion

        #region
        protected override bool PlusCore(IBuff srcBuff)
        {
            var buff = srcBuff as ForceStateBuff;
            if (null == buff)
                return false;
            this.ForceState = buff.ForceState;
            this.Rate = buff.Rate;
            this.Point = buff.Point;
            this.Percent = buff.Percent;
            this.Times = buff.Times;
            this.TimeEnd = buff.TimeEnd;
            return true;
        }
        protected override bool CoverCore(IBuff srcBuff)
        {
            var buff = srcBuff as ForceStateBuff;
            if (null == buff)
                return false;
            this.ForceState = buff.ForceState;
            this.Rate = buff.Rate;
            this.Point = buff.Point;
            this.Percent = buff.Percent;
            this.Times = buff.Times;
            this.TimeEnd = buff.TimeEnd;
            return true;
        }
        public override bool Clear()
        {
            this.ForceState = EnumForceState.None;
            this.Rate = 0;
            this.Point = 0;
            this.Percent = 0;
            return true;
        }
        public override IBuff Clone(params int[] buffId)
        {
            var buff = new ForceStateBuff(this._skill, this.ForceState);
            buff.CopyValue(this);
            return buff;
        }
        #endregion
    }
}
