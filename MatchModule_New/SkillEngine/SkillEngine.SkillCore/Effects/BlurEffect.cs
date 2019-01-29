using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public abstract class   BlurEffect : EffectBase, INormalEffect
    {
        #region .ctor
        protected BlurEffect(EnumBlurType blurType, int blurCode, EnumBuffExec execType,
            bool mainFlag, bool pureFlag)
            : base(EnumBuffType.BlurState, new int[] { (int)blurType }, mainFlag, pureFlag, true)
        {
            this.BlurCode = blurCode;
            this.ExecType = execType;
        }
        #endregion

        #region Data
        public int BlurCode
        {
            get;
            set;
        }
        public EnumBuffExec ExecType
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
            int last;
            int rate;
            bool pureFlag = BoostUtil.IfPureBuff(this, srcSkill, caster);
            GetAmp(srcSkill, caster, pureFlag, out last, out rate);
            bool rtnVal = false;
            foreach (var target in dstPlayers)
            {
                rtnVal |= AddBuff(srcSkill, caster, target, pureFlag, last, rate);
            }
            if (rtnVal)
                this.AddSrcShowModel(srcSkill, caster, last);
            return rtnVal;
        }
        #endregion

        #region Tools
        protected void GetAmp(ISkill srcSkill, ISkillPlayer caster, bool pureFlag, out int last, out int rate)
        {
            //ISkillOwner owner = caster ?? srcSkill.Owner;
            BoostUtil.GetAmpRate(out rate, this.Rate, srcSkill, (int)EnumBoostRootType.BlurRate, this.BlurCode);
            last = this.Last;
            if (last > 0)
                BoostUtil.GetAmpLast(out last, last, srcSkill, (int)EnumBoostRootType.BlurLast, this.BlurCode);
            if (pureFlag || this.Last <= 0)
                last = srcSkill.Context.GetBuffLast(srcSkill, caster, last);
        }
        protected virtual bool AddBuff(ISkill srcSkill, ISkillPlayer caster, ISkillPlayer target, bool pureFlag, int last, int rate)
        {
            if (!pureFlag)
            {
                if (BoostUtil.IfAntiDebuff(this, srcSkill, target, true, (int)EnumBoostRootType.AntiBlur, this.BlurCode))
                    return true;
                BoostUtil.GetEaseRate(out rate, rate, target, (int)EnumBoostRootType.BlurRate, this.BlurCode);
                if (this.Last > 0)
                {
                    BoostUtil.GetEaseLast(out last, last, target, (int)EnumBoostRootType.BlurLast, this.BlurCode);
                    last = srcSkill.Context.GetBuffLast(srcSkill, caster, last);
                }
            }
            if (!BuffUtil.ExecRate(out rate, rate, srcSkill.Context, this.ExecType))
                return false;
            if (rate <= 0)
                return true;
            int buffId = this.BuffId[0];
            if (buffId == (int)EnumBlurType.BanMan)
            {
                target.DisableState = (int)this.BlurCode;
            }
            else
            {
                if (buffId == (int)EnumBlurType.LockMotion)
                    target.RemoveBuff(buffId, 0);
                if (!target.AddBuff(InnerCreateBuff(srcSkill, target, (short)last, rate)))
                    return false;
            }
            this.AddTgtShowModel(srcSkill, target, last);
            return true;
        }
        #endregion
        protected abstract IBuff InnerCreateBuff(ISkill srcSkill, ISkillPlayer target, short last, int rate);
        public abstract INormalEffect Clone();

    }
}
