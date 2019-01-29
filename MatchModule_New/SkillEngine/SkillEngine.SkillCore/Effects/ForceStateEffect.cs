using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public abstract class ForceStateEffect : EffectBase, INormalEffect
    {
        #region .ctor
        protected ForceStateEffect(int forceType, int forceState, bool mainFlag, bool debuffFlag)
            : base(EnumBuffType.Spec, new int[] { forceType }, mainFlag, true, debuffFlag)
        {
            this.ForceState = forceState;
        }
        #endregion

        #region Data
        public int ForceState
        {
            get;
            set;
        }
        #endregion

        #region Facade
        public bool EffectManager(ISkill srcSkill, ISkillPlayer caster, IList<ISkillManager> dstManagers)
        {
            return true;
        }
        public bool EffectPlayers(ISkill srcSkill, ISkillPlayer caster, IList<ISkillPlayer> dstPlayers)
        {
            if (null == dstPlayers || dstPlayers.Count == 0)
                return false;
            short last = srcSkill.Context.GetBuffLast(srcSkill, caster, this.Last);
            bool rtnVal = false;
            foreach (var target in dstPlayers)
            {
                rtnVal |= AddBuff(srcSkill, target, last);
            }
            if (rtnVal)
                this.AddSrcShowModel(srcSkill, caster, last);
            return rtnVal;
        }
        protected virtual bool AddBuff(ISkill srcSkill, ISkillPlayer target, short last)
        {
            if (this.Last == 0)
            {
                int maxRate = SkillDefines.MAXStorePercent;
                if (this.Rate <= 0 || this.Rate < maxRate && srcSkill.Context.RandomPercent(maxRate) > this.Rate)
                    return false;
                if (!target.ForceState(this.ForceState))
                    return false;
            }
            else
            {
                if (!target.AddBuff(InnerCreateForceBuff(srcSkill, last)))
                    return false;
            }
            this.AddTgtShowModel(srcSkill, target, last);
            return true;
        }
        #endregion

        protected abstract IBuff InnerCreateForceBuff(ISkill srcSkill, short last);
        public abstract INormalEffect Clone();
    }
}
