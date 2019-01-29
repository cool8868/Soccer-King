using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;

namespace SkillEngine.SkillImpl.Football
{
    public class FootballPropBuff : BuffBase
    {
        #region .ctor
        public FootballPropBuff(ISkill skill, int[] buffId)
            : base(skill, EnumBuffType.PropPlus, buffId)
        {
        }
        #endregion

        #region Data
        public override bool DebuffFlag
        {
            get
            {
                return this.Point < 0 || this.Percent < 0;
            }
        }
        public override bool ValuedFlag
        {
            get
            {
                return this.Point != 0 || this.Percent != 0;
            }
        }
        #endregion

        public override IBuff Clone(params int[] buffId)
        {
            var buff = new FootballPropBuff(this._skill, buffId);
            buff.CopyValue(this);
            return buff;
        }
    }
}
