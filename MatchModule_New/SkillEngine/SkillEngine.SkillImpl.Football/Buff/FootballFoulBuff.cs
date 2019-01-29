using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.Extern.Enum.Football;

namespace SkillEngine.SkillImpl.Football
{
    public class FootballFoulBuff : BuffBase
    {
        #region .ctor
        public FootballFoulBuff(ISkill skill, EnumFoulBuffCode buffCode, EnumFoulState foulType)
            : base(skill, EnumBuffType.Foul, new int[] { (int)buffCode })
        {
            this.FoulType = foulType;
        }
        #endregion

        #region Data
        public EnumFoulState FoulType
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
        #endregion

        #region BuffBase
        protected override bool PlusCore(IBuff srcBuff)
        {
            var foulBuff = srcBuff as FootballFoulBuff;
            if (null == foulBuff)
                return false;
            this.FoulType = foulBuff.FoulType;
            this.Rate += foulBuff.Rate;
            this.Point += foulBuff.Point;
            this.Percent += foulBuff.Percent;
            this.Times = foulBuff.Times;
            this.TimeEnd = foulBuff.TimeEnd;
            return true;
        }
        protected override bool CoverCore(IBuff srcBuff)
        {
            var foulBuff = srcBuff as FootballFoulBuff;
            if (null == foulBuff)
                return false;
            this.FoulType = foulBuff.FoulType;
            this.Rate = foulBuff.Rate;
            this.Point = foulBuff.Point;
            this.Percent = foulBuff.Percent;
            this.Times = foulBuff.Times;
            this.TimeEnd = foulBuff.TimeEnd;
            return true;
        }
        public override bool Clear()
        {
            this.FoulType = EnumFoulState.None;
            this.Rate = 0;
            this.Point = 0;
            this.Percent = 0;
            return true;
        }
        public override IBuff Clone(params int[] buffId)
        {
            var buff = new FootballFoulBuff(this._skill, (EnumFoulBuffCode)buffId[0], this.FoulType);
            buff.CopyValue(this);
            return buff;
        }
        #endregion
    }
}
