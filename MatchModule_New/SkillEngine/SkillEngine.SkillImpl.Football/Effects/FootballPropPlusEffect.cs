using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;

namespace SkillEngine.SkillImpl.Football
{
    public class FootballPropPlusEffect : PropPlusEffect
    {
        #region .ctor
        public FootballPropPlusEffect(int[] buffId,
            bool mainFlag, bool pureFlag, bool debuffFlag)
            : base(buffId, mainFlag, pureFlag, debuffFlag)
        { }
        #endregion

        protected override IBuff InnerCreatePropBuff(ISkill srcSkill, int point, int percent, short last)
        {
            if (this.Last == 0 && this.BuffId[0] == (int)EnumBuffCode.PassSuccRate)
                last = 8;
            if (last > 0)
                last += srcSkill.Context.MatchRound;
            return new FootballPropBuff(srcSkill, this.BuffId)
            {
                Rate = SkillDefines.MAXStorePercent,
                Point = point,
                Percent = percent,
                TimeEnd = last,
                Times = this.Repeat,
            };
        }

        public override INormalEffect Clone()
        {
            var effect = new FootballPropPlusEffect(this.BuffId, this.MainFlag, this.PureFlag, this.DebuffFlag);
            effect.CopyValue(this);
            return effect;
        }

    }
}
