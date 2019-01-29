using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;

namespace SkillEngine.SkillImpl.Football
{
    public class FootballBlurBuff : BuffBase
    {
        #region .ctor
        public FootballBlurBuff(ISkill skill, EnumBlurType buffCode, EnumBlurBuffCode blurCode)
            : base(skill, EnumBuffType.BlurState, new int[] { (int)buffCode })
        {
            this.BlurCode = blurCode;
        }
        #endregion

        #region Data
        public EnumBlurBuffCode BlurCode
        {
            get;
            set;
        }
        public override bool DebuffFlag
        {
            get
            {
                return true;
            }
        }
        public override bool ValuedFlag
        {
            get
            {
                return this.BlurCode != EnumBlurBuffCode.None;
            }
        }
        public override bool InvalidFlag
        {
            get
            {
                return this.TimeEnd == 0
                    || this.TimeEnd > 0 && this.TimeEnd < this._skill.Context.MatchRound;
            }
        }
        #endregion

        #region BuffBase
        protected override bool PlusCore(IBuff srcBuff)
        {
            var blurBuff = srcBuff as FootballBlurBuff;
            if (null == blurBuff)
                return false;
            this.BlurCode = blurBuff.BlurCode;
            this.Rate = blurBuff.Rate;
            this.Point = blurBuff.Point;
            this.Percent = blurBuff.Percent;
            this.Times = blurBuff.Times;
            this.TimeEnd = blurBuff.TimeEnd;
            return true;
        }
        protected override bool CoverCore(IBuff srcBuff)
        {
            var blurBuff = srcBuff as FootballBlurBuff;
            if (null == blurBuff)
                return false;
            this.BlurCode = blurBuff.BlurCode;
            this.Rate = blurBuff.Rate;
            this.Point = blurBuff.Point;
            this.Percent = blurBuff.Percent;
            this.Times = blurBuff.Times;
            this.TimeEnd = blurBuff.TimeEnd;
            return true;
        }
        public override bool Clear()
        {
            this.BlurCode = EnumBlurBuffCode.None;
            this.Rate = 0;
            this.Point = 0;
            this.Percent = 0;
            return true;
        }
        public override IBuff Clone(params int[] buffId)
        {
            var buff = new FootballBlurBuff(this._skill, (EnumBlurType)buffId[0], this.BlurCode);
            buff.CopyValue(this);
            return buff;
        }
        #endregion
    }

    public class FootballDisableBuff : FootballBlurBuff, IDisableBuff
    {
        #region .ctor
        public FootballDisableBuff(ISkill skill, EnumBlurBuffCode blurCode)
            : base(skill, EnumBlurType.BanMan, blurCode)
        {
        }
        #endregion

        #region Data
        public override bool InvalidFlag
        {
            get
            {
                if (this.BlurCode == EnumBlurBuffCode.Disable)
                    return false;
                return this.TimeEnd == 0
                    || this.TimeEnd > 0 && this.TimeEnd <= this._skill.Context.MatchRound;
            }
        }
        #endregion

        protected override bool PlusCore(IBuff srcBuff)
        {
            if (this.BlurCode == EnumBlurBuffCode.Disable)
                return true;
            var blurBuff = srcBuff as FootballDisableBuff;
            if (null == blurBuff)
                return false;
            this.BlurCode = blurBuff.BlurCode;
            this.Rate = blurBuff.Rate;
            this.Point = blurBuff.Point;
            this.Percent = blurBuff.Percent;
            this.Times = blurBuff.Times;
            this.TimeEnd = blurBuff.TimeEnd;
            return true;
        }

        public void ReEnable()
        {
            if (BlurCode == EnumBlurBuffCode.Injure)
            {
                if (this.TimeEnd > 0 && this.TimeEnd <= this._skill.Context.MatchRound)
                    this.TimeEnd = (int)EnumBuffLast.TillDeadBall;
            }
        }
        public override IBuff Clone(params int[] buffId)
        {
            var buff = new FootballDisableBuff(this._skill, this.BlurCode);
            buff.CopyValue(this);
            return buff;
        }
    }
}
