using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;

namespace SkillEngine.SkillCore
{
    public partial class Skill
    {
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
            this._owner.TryGetSkillCD(ref buff);
            return this.boostCore.TryGetSkillCD(ref buff);
        }
        public bool TryGetSkillRate(ref IBoostBuff buff)
        {
            this._owner.TryGetSkillRate(ref buff);
            return this.boostCore.TryGetSkillRate(ref buff);
        }
        public bool TryGetPureBuff(ref IBoostBuff buff)
        {
            this._owner.TryGetPureBuff(ref buff);
            return this.boostCore.TryGetPureBuff(ref buff);
        }
        public bool TryGetAntiDebuff(ref IBoostBuff buff)
        {
            this._owner.TryGetAntiDebuff(ref buff);
            return this.boostCore.TryGetAntiDebuff(ref buff);
        }
        public bool TryGetAmpLast(ref IBoostBuff buff, params int[] buffIds)
        {
            this._owner.TryGetAmpLast(ref buff, buffIds);
            return this.boostCore.TryGetAmpLast(ref buff, buffIds);
        }
        public bool TryGetAmpRate(ref IBoostBuff buff, params int[] buffIds)
        {
            this._owner.TryGetAmpRate(ref buff, buffIds);
            return this.boostCore.TryGetAmpRate(ref buff, buffIds);
        }
        public bool TryGetAmpValue(ref IBoostBuff buff, params int[] buffIds)
        {
            this._owner.TryGetAmpValue(ref buff, buffIds);
            return this.boostCore.TryGetAmpValue(ref buff, buffIds);
        }
        public bool TryGetEaseLast(ref IBoostBuff buff, params int[] buffIds)
        {
            this._owner.TryGetEaseLast(ref buff, buffIds);
            return this.boostCore.TryGetEaseLast(ref buff, buffIds);
        }
        public bool TryGetEaseRate(ref IBoostBuff buff, params int[] buffIds)
        {
            this._owner.TryGetEaseRate(ref buff, buffIds);
            return this.boostCore.TryGetEaseRate(ref buff, buffIds);
        }
        public bool TryGetEaseValue(ref IBoostBuff buff, params int[] buffIds)
        {
            this._owner.TryGetEaseValue(ref buff, buffIds);
            return this.boostCore.TryGetEaseValue(ref buff, buffIds);
        }
        public bool TryGetAntiRate(ref IBoostBuff buff, params int[] buffIds)
        {
            this._owner.TryGetAntiRate(ref buff, buffIds);
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
            return false;
        }
        public bool ForceSyncBoost(bool forceFlag)
        {
            return this.boostCore.ForceSyncBoost(forceFlag);
        }
        #endregion

    }
}
