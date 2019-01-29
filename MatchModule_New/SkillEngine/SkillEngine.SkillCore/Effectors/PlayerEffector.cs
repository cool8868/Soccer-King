using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public class PlayerEffector : EffectorBase<ISkillPlayer>
    {
        #region .ctor
        public PlayerEffector(PlayerLocator locator, List<IEffect> effects)
            : base(locator, effects)
        { }
        public PlayerEffector(ISkill srcSkill, PlayerLocator locator, List<IBoostEffect> booster, List<INormalEffect> buffFeeder, List<ISpecEffect> specFeeder)
            : base(srcSkill, locator, booster, buffFeeder, specFeeder)
        { }
        #endregion

        #region Facade
        protected override bool EffectTarget(INormalEffect effect, ISkill srcSkill, ISkillPlayer caster, List<ISkillPlayer> targets)
        {
            return effect.EffectPlayers(srcSkill, caster, targets);
        }
        protected override bool EffectTarget(IBoostEffect effect, ISkill srcSkill, ISkillPlayer caster, List<ISkillPlayer> targets)
        {
            return effect.EffectPlayers(srcSkill, caster, targets);
        }
        protected override bool EffectTarget(ISpecEffect effect, ISkill srcSkill, ISkillPlayer caster, List<ISkillPlayer> targets)
        {
            return effect.EffectPlayers(srcSkill, caster, targets);
        }
        protected override bool UnEffectTarget(INormalEffect effect, ISkill srcSkill, ISkillPlayer caster, List<ISkillPlayer> targets)
        {
            if (null != effect.SrcModelSetting && null != caster)
                effect.RemoveShowModel(srcSkill, caster, false);
            foreach (var target in targets)
            {
                foreach (int buffId in effect.BuffId)
                {
                    target.RemoveBuff(buffId, srcSkill.InnerId);
                }
                effect.RemoveShowModel(srcSkill, target, true);
            }
            return true;
        }
        protected override bool UnEffectTarget(IBoostEffect effect, ISkill srcSkill, ISkillPlayer caster, List<ISkillPlayer> targets)
        {
            if (null != effect.SrcModelSetting && null != caster)
                effect.RemoveShowModel(srcSkill, caster, false);
            foreach (var target in targets)
            {
                target.RemoveBoost(effect);
                effect.RemoveShowModel(srcSkill, target, true);
            }
            return true;
        }
        protected override bool RevokeEffect(INormalEffect effect, ISkill srcSkill, ISkillPlayer caster, List<ISkillPlayer> targets)
        {
            foreach (var target in targets)
            {
                foreach (int buffId in effect.BuffId)
                {
                    target.ForceSyncBuff(buffId, true);
                }
            }
            return true;
        }
        protected override byte[] GetClipTargets(ISkillPlayer caster)
        {
            var targets = GetTargets(caster);
            if (null == targets || targets.Count == 0)
                return null;
            int cnt = targets.Count;
            var array = new byte[cnt];
            for (int i = 0; i < cnt; i++)
            {
                array[i] = targets[i].SkillClientId;
            }
            return array;
        }
        public override IEffector Clone(ISkill srcSkill)
        {
            var effctor = new PlayerEffector(srcSkill, (PlayerLocator)this._locator, this._booster, this._buffFeeder, this._specFeeder);
            effctor.ClipSetting = ClipSetting;
            return effctor;
        }
        #endregion
    }
}
