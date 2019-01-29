using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public abstract class FactPropPlusEffect : EffectBase, INormalEffect
    {
        #region .ctor
        protected FactPropPlusEffect(int[] buffId,
            bool mainFlag, bool pureFlag, bool debuffFlag)
            : base(EnumBuffType.PropPlus, buffId, mainFlag, pureFlag, debuffFlag)
        {
            this.FactType = EnumBuffFact.ColourQty;
        }
        #endregion

        #region Data
        public abstract EnumBuffExec ExecType
        {
            get;
        }
        public EnumOwnSide Side
        {
            get;
            set;
        }
        public EnumBuffFact FactType
        {
            get;
            set;
        }
        public int[] Values
        {
            get;
            set;
        }
        #endregion

        #region Facade
        public bool EffectManager(ISkill srcSkill, ISkillPlayer caster, IList<ISkillManager> dstManagers)
        {
            if (null == dstManagers || dstManagers.Count == 0)
                return false;
            bool pureFlag = this.PureFlag;
            bool rtnVal = false;
            double multiFact, addFact;
            GetBuffFact(srcSkill, caster, out multiFact, out addFact);
            foreach (var target in dstManagers)
            {
                rtnVal |= AddBuff(srcSkill, caster, target, pureFlag, multiFact, addFact);
            }
            if (rtnVal)
                this.AddSrcShowModel(srcSkill, caster, this.Last);
            return rtnVal;
        }
        public bool EffectPlayers(ISkill srcSkill, ISkillPlayer caster, IList<ISkillPlayer> dstPlayers)
        {
            if (null == dstPlayers || dstPlayers.Count == 0)
                return false;
            bool pureFlag = this.PureFlag;
            bool rtnVal = false;
            double multiFact, addFact;
            GetBuffFact(srcSkill, caster, out multiFact, out addFact);
            foreach (var target in dstPlayers)
            {
                rtnVal |= AddBuff(srcSkill, caster, target, pureFlag, multiFact, addFact);
            }
            if (rtnVal)
                this.AddSrcShowModel(srcSkill, caster, this.Last);
            return rtnVal;
        }
        #endregion

        #region Tools
        protected virtual bool AddBuff(ISkill srcSkill, ISkillPlayer caster, ISkillOwner target, bool pureFlag, double multiFact, double addFact)
        {
            if (!pureFlag)
            {
                if (BoostUtil.IfAntiDebuff(this, srcSkill, target, true, (int)EnumBoostRootType.AntiPropDebuff))
                    return true;
            }
            if (this.ExecType == EnumBuffExec.MustHit)
            {
                int rate = this.Rate;
                int maxRate = SkillDefines.MAXStorePercent;
                if (rate >= 0 && rate < maxRate)
                {
                    if (srcSkill.Context.RandomPercent(maxRate) > rate)
                        return false;
                }
            }
            target.AddBuff(InnerCreatePropBuff(srcSkill, caster, multiFact, addFact));
            this.AddTgtShowModel(srcSkill, target, this.Last);
            return true;
        }
        #endregion

        protected abstract IBuff InnerCreatePropBuff(ISkill srcSkill, ISkillPlayer caster, double multiFact, double addFact);
        protected abstract void GetBuffFact(ISkill srcSkill, ISkillPlayer caster, out double multiFact, out double addFact);
        public abstract INormalEffect Clone();
    }
}
