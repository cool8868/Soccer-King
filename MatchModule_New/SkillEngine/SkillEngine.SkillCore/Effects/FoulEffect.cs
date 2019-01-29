using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public abstract class FoulEffect : EffectBase, INormalEffect
    {
        #region .ctor
        protected FoulEffect(int foulCode, byte foulType, //EnumBuffExec execType,
            bool mainFlag, bool pureFlag)
            : base(EnumBuffType.Foul, new int[] { foulCode }, mainFlag, pureFlag, true)
        {
            this.FoulType = foulType;
            //this.ExecType = execType;
        }
        #endregion

        #region Data
        public byte FoulType
        {
            get;
            set;
        }
        //public EnumBuffExec ExecType
        //{
        //    get;
        //    set;
        //}
        #endregion

        #region Facade
        public bool EffectManager(ISkill srcSkill, ISkillPlayer caster, IList<ISkillManager> dstManagers)
        {
            return true;
        }
        public virtual bool EffectPlayers(ISkill srcSkill, ISkillPlayer caster, IList<ISkillPlayer> dstPlayers)
        {
            if (null == dstPlayers || dstPlayers.Count == 0)
                return false;
            int rate;
            int buffId = this.BuffId[0];
            GetAmp(srcSkill, caster, buffId, out rate);
            short last = srcSkill.Context.GetBuffLast(srcSkill, caster, this.Last);
            bool rtnVal = false;
            foreach (var dstPlayer in dstPlayers)
            {
                rtnVal |= FoulPlayer(srcSkill, caster, dstPlayer, buffId, rate, 1);
            }
            if (rtnVal)
                this.AddSrcShowModel(srcSkill, caster, last);
            return rtnVal;
        }
        #endregion

        #region Tools
        protected virtual void GetAmp(ISkill srcSkill, ISkillPlayer caster, int buffId, out int rate)
        {
            BoostUtil.GetAmpRate(out rate, this.Rate, srcSkill, (int)EnumBoostRootType.FoulRate, buffId);
        }
        protected bool FoulPlayer(ISkill srcSkill, ISkillPlayer caster, ISkillPlayer target, int buffId, int rate, int depth)
        {
            int foulType = this.FoulType;
            if (null == target)
                return false;
            if (null == caster || caster == target)
                caster = target.OppSkillPlayer;
            if (null == caster)
                return false;
            if (!this.PureFlag)
            {
                //反弹犯规
                if (depth > 0 && BoostUtil.IfAntiDebuff(this, srcSkill, target, (int)EnumBoostRootType.TurnFoul))
                    return FoulPlayer(srcSkill, target, caster, buffId, rate, --depth);
                //免疫犯规
                if (BoostUtil.IfAntiDebuff(this, srcSkill, target, (int)EnumBoostRootType.AntiFoul))
                    return true;
                //降低犯规概率
                BoostUtil.GetEaseRate(out rate, rate, target, (int)EnumBoostRootType.FoulRate, buffId);
            }
            if (srcSkill.Context.RandomPercent(SkillDefines.MAXStorePercent) > rate)
                return true;
            //一定概率降低犯规等级
            int point, percent;
            if (BoostUtil.GetEaseValue(out point, out percent, target, (int)EnumBoostRootType.FoulValue))
            {
                if (srcSkill.Context.RandomPercent(SkillDefines.MAXStorePercent) <= percent)
                    foulType -= point;
            }
            if (foulType >= 0)
                InnerFoul(srcSkill, target, foulType);
            return true;
        }
        #endregion
        protected abstract bool InnerFoul(ISkill srcSkill, ISkillPlayer target, int foulType);
        public abstract INormalEffect Clone();
    }
}
