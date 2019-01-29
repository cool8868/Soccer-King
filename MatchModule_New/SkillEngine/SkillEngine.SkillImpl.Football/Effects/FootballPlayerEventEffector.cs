using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;

namespace SkillEngine.SkillImpl.Football
{
    public class FootballPlayerEventEffector : PlayerEffector
    {
        #region .ctor
        public FootballPlayerEventEffector(PlayerLocator locator, List<IEffect> effects)
            : base(locator, effects)
        { }
        public FootballPlayerEventEffector(ISkill srcSkill, PlayerLocator locator, List<IBoostEffect> booster, List<INormalEffect> buffFeeder, List<ISpecEffect> specFeeder)
            : base(srcSkill, locator, booster, buffFeeder, specFeeder)
        { }
        #endregion

        protected override bool UnEffectTarget(INormalEffect effect, ISkill srcSkill, ISkillPlayer caster, List<ISkillPlayer> targets)
        {
            base.UnEffectTarget(effect, srcSkill, caster, targets);
            var eventEffect = effect as FootballEventPropPlusEffect;
            if (null != eventEffect)
                eventEffect.UnEffecttPlayers(srcSkill, caster, targets);
            return true;
        }
        protected override bool RevokeEffect(INormalEffect effect, ISkill srcSkill, ISkillPlayer caster, List<ISkillPlayer> targets)
        {
            base.RevokeEffect(effect, srcSkill, caster, targets);
            var eventEffect = effect as FootballEventPropPlusEffect;
            if (null != eventEffect)
                eventEffect.UnEffecttPlayers(srcSkill, caster, targets);
            return true;
        }

        public override IEffector Clone(ISkill srcSkill)
        {
            var effctor = new FootballPlayerEventEffector(srcSkill, (PlayerLocator)this._locator, this._booster, this._buffFeeder, this._specFeeder);
            effctor.ClipSetting = ClipSetting;
            return effctor;
        }
    }
}
