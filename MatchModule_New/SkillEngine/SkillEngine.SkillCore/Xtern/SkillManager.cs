using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore.Xtern
{
    public class SkillManager : ISkillManager
    {
        #region Cache
        readonly BoostCore boostCore;
        readonly BuffCore buffCore;
        readonly SpecBuffCore specBuffCoe;
        #endregion

        #region .ctor
        public SkillManager(ISkillMatch match)
        {
            this.boostCore = new BoostCore(match);
            this.buffCore = new BuffCore(match);
            this.specBuffCoe = new SpecBuffCore();
        }
        #endregion

        #region Data
        public ISkillMatch SkillMatch
        {
            get;
            set;
        }
        public ISkillManager OppSkillManager
        {
            get;
            set;
        }
        public List<ISkillPlayer> SkillPlayerList
        {
            get;
            set;
        }
        public ISkill RootSkill
        {
            get;
            set;
        }
        public Extern.Enum.EnumMatchSide SkillSide
        {
            get;
            set;
        }
        public string SkillTaticId
        {
            get;
            set;
        }
        public int InnerId
        {
            get;
            set;
        }
        public Guid SkillMid
        {
            get;
            set;
        }
        #endregion

        #region IWaitBuff
        public bool WaitingFlag
        {
            get { return this.boostCore.WaitingFlag; }
        }
        public bool SetWaitBuffEnd(EnumBuffLast lastType)
        {
            return this.boostCore.SetWaitBuffEnd(lastType);
        }
        #endregion

        #region IBoostCore
        public bool TryGetSkillCD(ref IBoostBuff buff)
        {
            return this.boostCore.TryGetSkillCD(ref buff);
        }
        public bool TryGetSkillRate(ref IBoostBuff buff)
        {
            return this.boostCore.TryGetSkillRate(ref buff);
        }
        public bool TryGetPureBuff(ref IBoostBuff buff)
        {
            return this.boostCore.TryGetPureBuff(ref buff);
        }
        public bool TryGetAntiDebuff(ref IBoostBuff buff)
        {
            return this.boostCore.TryGetAntiDebuff(ref buff);
        }
        public bool TryGetAmpLast(ref IBoostBuff buff, params int[] buffIds)
        {
            return this.boostCore.TryGetAmpLast(ref buff, buffIds);
        }
        public bool TryGetAmpRate(ref IBoostBuff buff, params int[] buffIds)
        {
            return this.boostCore.TryGetAmpRate(ref buff, buffIds);
        }
        public bool TryGetAmpValue(ref IBoostBuff buff, params int[] buffIds)
        {
            return this.boostCore.TryGetAmpValue(ref buff, buffIds);
        }
        public bool TryGetEaseLast(ref IBoostBuff buff, params int[] buffIds)
        {
            return this.boostCore.TryGetEaseLast(ref buff, buffIds);
        }
        public bool TryGetEaseRate(ref IBoostBuff buff, params int[] buffIds)
        {
            return this.boostCore.TryGetEaseRate(ref buff, buffIds);
        }
        public bool TryGetEaseValue(ref IBoostBuff buff, params int[] buffIds)
        {
            return this.boostCore.TryGetEaseValue(ref buff, buffIds);
        }
        public bool TryGetAntiRate(ref IBoostBuff buff, params int[] buffIds)
        {
            return this.boostCore.TryGetAntiRate(ref buff, buffIds);
        }
        public bool AddBoost(IBoostEffect inBoost)
        {
            return this.boostCore.AddBoost(inBoost);
        }
        public bool RemoveBoost(IBoostEffect inBoost)
        {
            return this.boostCore.RemoveBoost(inBoost);
        }
        public bool AddBoostToSkill(ISkill inSkill)
        {
            return this.boostCore.AddBoostToSkill(inSkill);
        }
        public bool ForceSyncBoost(bool forceFlag)
        {
            return this.boostCore.ForceSyncBoost(forceFlag);
        }
        #endregion

        #region IBuffCore
        public bool TryGetBuff(int buffId, ref IBuff buff)
        {
            return this.buffCore.TryGetBuff(buffId, ref buff);
        }
        public bool TryPickBuff(int buffId, out IBuff buff)
        {
            return this.buffCore.TryPickBuff(buffId, out buff);
        }
        public bool RemoveBuff(int buffId, int skillId)
        {
            return this.buffCore.RemoveBuff(buffId, skillId);
        }
        public bool ForceSyncBuff(int buffId,bool forceFlag)
        {
            return this.buffCore.ForceSyncBuff(buffId, forceFlag);
        }
        public bool AddBuff(IBuff inBuff)
        {
            return this.buffCore.AddBuff(inBuff);
        }
        public bool SetPickBuffEnd(int buffId, int skillId)
        {
            return this.buffCore.SetPickBuffEnd(buffId, skillId);
        }
        #endregion

        #region ISpecBuffCore
        public bool AddSpecBuff(ISpecEffect inSpec)
        {
            return this.specBuffCoe.AddSpecBuff(inSpec);
        }
        public bool TryPickSpecBuff(int inTiming, out ISpecEffect outSpec)
        {
            return this.specBuffCoe.TryPickSpecBuff(inTiming, out outSpec);
        }
        #endregion

        #region 球场行为
        public object GetStatObj(int statType)
        {
            throw new NotImplementedException();
        }
        public int GetStatInt(int statType)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 技能
        public ISkillCore SkillCore
        {
            get;
            set;
        }
        #endregion
    }
}
