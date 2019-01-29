using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public abstract class PropPlusEffect : EffectBase, INormalEffect
    {
        #region .ctor
        protected PropPlusEffect(int[] buffId,
            bool mainFlag, bool pureFlag, bool debuffFlag)
            : base(EnumBuffType.PropPlus, buffId, mainFlag, pureFlag, debuffFlag)
        { }
        #endregion

        #region Facade
        public bool EffectManager(ISkill srcSkill, ISkillPlayer caster, IList<ISkillManager> dstManagers)
        {
            if (null == dstManagers || dstManagers.Count == 0)
                return false;
            int last;
            int point, percent;
            int buffId = this.BuffId[0];
            bool pureFlag = this.PureFlag;
            GetAmp(srcSkill, caster, buffId, pureFlag, out last, out point, out percent);
            bool rtnVal = false;
            foreach (var target in dstManagers)
            {
                rtnVal |= AddBuff(srcSkill, caster, target, buffId, pureFlag, last, point, percent);
            }
            if (rtnVal)
                this.AddSrcShowModel(srcSkill, caster, last);
            return rtnVal;
        }
        public bool EffectPlayers(ISkill srcSkill, ISkillPlayer caster, IList<ISkillPlayer> dstPlayers)
        {
            if (null == dstPlayers || dstPlayers.Count == 0)
                return false;
            int last;
            int point, percent;
            int buffId = this.BuffId[0];
            bool pureFlag = this.PureFlag;
            GetAmp(srcSkill, caster, buffId, pureFlag, out last, out point, out percent);
            bool rtnVal = false;
            foreach (var target in dstPlayers)
            {
                rtnVal |= AddBuff(srcSkill, caster, target, buffId, pureFlag, last, point, percent);
            }
            if (rtnVal)
                this.AddSrcShowModel(srcSkill, caster, last);
            return rtnVal;
        }
        #endregion

        #region Tools
        protected void GetAmp(ISkill srcSkill, ISkillPlayer caster, int buffId, bool pureFlag, out int last, out int point, out int percent)
        {
            //ISkillOwner owner = caster ?? srcSkill.Owner;
            BoostUtil.GetAmpValue(out point, out percent, this.Point, this.Percent, srcSkill, ValueBuffUnits(buffId));
            last = this.Last;
            if (last > 0)
                BoostUtil.GetAmpLast(out last, last, srcSkill, LastBuffUnits(buffId));
            if (pureFlag || this.Last <= 0)
                last = srcSkill.Context.GetBuffLast(srcSkill, caster, last);
        }
        protected virtual bool AddBuff(ISkill srcSkill, ISkillPlayer caster, ISkillOwner target, int buffId, bool pureFlag, int last, int point, int percent)
        {
            if (!pureFlag)
            {
                if (BoostUtil.IfAntiDebuff(this, srcSkill, target, true, (int)EnumBoostRootType.AntiPropDebuff))
                    return true;
                BoostUtil.GetEaseValue(out point, out percent, point, percent, target, ValueBuffUnits(buffId));
                if (this.Last > 0)
                {
                    BoostUtil.GetEaseLast(out last, last, target, LastBuffUnits(buffId));
                    last = srcSkill.Context.GetBuffLast(srcSkill, caster, last);
                }
            }
            if (!target.AddBuff(InnerCreatePropBuff(srcSkill, point, percent, (short)last)))
                return false;
            this.AddTgtShowModel(srcSkill, target, last);
            return true;
        }
        int[] LastBuffUnits(int buffId)
        {
            if (this.DebuffFlag)
                return new int[] { (int)EnumBoostRootType.PropDebuffLast, buffId };
            else
                return new int[] { (int)EnumBoostRootType.PropBuffLast, buffId };
        }
        int[] ValueBuffUnits(int buffId)
        {
            if (this.DebuffFlag)
                return new int[] { (int)EnumBoostRootType.PropDebuffValue, buffId };
            else
                return new int[] { (int)EnumBoostRootType.PropBuffValue, buffId };
        }
        #endregion

        protected abstract IBuff InnerCreatePropBuff(ISkill srcSkill, int point, int percent, short last);
        public abstract INormalEffect Clone();

    }
}
