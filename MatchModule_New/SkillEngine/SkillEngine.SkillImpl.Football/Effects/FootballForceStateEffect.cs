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
using Games.NB.Match.Base.Model;

namespace SkillEngine.SkillImpl.Football
{
    public class FootballForceStateEffect : ForceStateEffect
    {
        #region .ctor
        public FootballForceStateEffect(EnumForceState forceState, bool mainFlag, bool debuffFlag)
            : base((int)EnumSpecEffect.ForceState, (int)forceState, mainFlag, debuffFlag)
        { }
        #endregion

        protected override IBuff InnerCreateForceBuff(ISkill srcSkill, short last)
        {
            if (last > 0)
                last += srcSkill.Context.MatchRound;
            return new ForceStateBuff(srcSkill, (EnumForceState)this.ForceState)
            {
                Rate = this.Rate,
                TimeEnd = (short)last,
            };
        }

        public override INormalEffect Clone()
        {
            var effect = new FootballForceStateEffect((EnumForceState)this.ForceState, this.MainFlag, this.DebuffFlag);
            effect.CopyValue(this);
            return effect;
        }
    }
}
