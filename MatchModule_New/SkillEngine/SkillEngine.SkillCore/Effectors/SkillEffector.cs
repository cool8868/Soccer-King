using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public class SkillEffector : EffectorBase<ISkill>
    {
        #region .ctor
        public SkillEffector(SkillLocator locator, List<IEffect> effects)
            : base(locator, effects)
        { }
        public SkillEffector(ISkill srcSkill, SkillLocator locator, List<IBoostEffect> booster, List<INormalEffect> buffFeeder, List<ISpecEffect> specFeeder)
            : base(srcSkill, locator, booster, buffFeeder, specFeeder)
        { }
        #endregion

        #region Data
        public List<IEffector> PlugEffectors
        {
            get;
            set;
        }
        #endregion

        #region Facade
        protected override bool EffectCore(ISkill srcSkill, ISkillPlayer caster, byte undoFlag, bool redoFlag)
        {
            bool rtnVal = false;
            if (this._effects.Count > 0)
                rtnVal |= base.EffectCore(srcSkill, caster, undoFlag, redoFlag);
            var targets = GetTargets(caster);
            if (null == targets || targets.Count == 0 || null == PlugEffectors || PlugEffectors.Count == 0)
                return rtnVal;
            foreach (var target in targets)
            {
                foreach (var effctor in PlugEffectors)
                {
                    target.AddEffector(effctor.Clone(target));
                }
            }
            return rtnVal;
        }
        protected override bool EffectTarget(INormalEffect effect, ISkill srcSkill, ISkillPlayer caster, List<ISkill> targets)
        {
            return effect.EffectSkills(srcSkill, caster, targets);
        }
        protected override bool EffectTarget(IBoostEffect effect, ISkill srcSkill, ISkillPlayer caster, List<ISkill> targets)
        {
            return effect.EffectSkills(srcSkill, caster, targets);
        }
        protected override bool EffectTarget(ISpecEffect effect, ISkill srcSkill, ISkillPlayer caster, List<ISkill> targets)
        {
            return effect.EffectSkills(srcSkill, caster, targets);
        }
        protected override bool UnEffectTarget(INormalEffect effect, ISkill srcSkill, ISkillPlayer caster, List<ISkill> targets)
        {
            if (null != effect.SrcModelSetting && null != caster)
                effect.RemoveShowModel(srcSkill, caster, false);
            foreach (var target in targets)
            {
                effect.UnEffectSkills(srcSkill, caster, target);
            }
            return true;
        }
        protected override bool RevokeEffect(INormalEffect effect, ISkill srcSkill, ISkillPlayer caster, List<ISkill> targets)
        {
            return this.UnEffectTarget(effect, srcSkill, caster, targets);
        }
        protected override byte[] GetClipTargets(ISkillPlayer caster)
        {
            var targets = GetTargets(caster);
            if (null == targets || targets.Count == 0)
                return null;
            var dic = new Dictionary<byte, byte>();
            ISkillPlayer player = null;
            foreach (var item in targets)
            {
                player = item.Owner as ISkillPlayer;
                if (null == player)
                    continue;
                dic[player.SkillClientId] = 0;
            }
            return dic.Keys.ToArray();
        }
        public override IEffector Clone(ISkill srcSkill)
        {
            var effctor = new SkillEffector(srcSkill, (SkillLocator)this._locator, this._booster, this._buffFeeder, this._specFeeder);
            effctor.ClipSetting = this.ClipSetting;
            effctor.PlugEffectors = this.PlugEffectors;
            return effctor;
        }
        #endregion

    }
}
