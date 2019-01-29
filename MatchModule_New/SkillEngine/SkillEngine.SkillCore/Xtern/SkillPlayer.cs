using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.Extern.Enum;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;


namespace SkillEngine.SkillCore.Xtern
{
    public class SkillPlayer : ISkillPlayer
    {
        #region Cache
        readonly BoostCore boostCore;
        readonly BuffCore buffCore;
        readonly SpecBuffCore specBuffCoe;
        #endregion

        #region .ctor
        public SkillPlayer(ISkillMatch match)
        {
            this.boostCore = new BoostCore(match);
            this.buffCore = new BuffCore(match);
            this.specBuffCoe = new SpecBuffCore();
        }
        #endregion

        #region Data
        public Extern.Enum.EnumMatchSide SkillSide
        {
            get;
            set;
        }
        public ISkillManager SkillManager
        {
            get;
            set;
        }
        public ISkillPlayer SkillPairingPlayer
        {
            get;
            set;
        }
        public ISkillPlayer SkillPairedPlayer
        {
            get;
            set;
        }
        public ISkillPlayer SkillAssistPlayer
        {
            get;
            set;
        }
        public ISkillPlayer OppSkillPlayer
        {
            get;
            set;
        }
        public int DoingState
        {
            get;
            set;
        }
        public int DoneState
        {
            get;
            set;
        }
        public EnumDoneStateFlag DoneStateFlag
        {
            get;
            set;
        }
        public int SkillPlayerId
        {
            get;
            set;
        }
        public byte SkillClientId
        {
            get;
            set;
        }
        public int SkillPosition
        {
            get;
            set;
        }
        public int SkillColour
        {
            get;
            set;
        }
        public bool SkillHoldBall
        {
            get;
            set;
        }
        public bool Disable
        {
            get { return DisableState != 0; }
        }
        public int DisableState
        {
            get;
            set;
        }
        public bool SkillEnable
        {
            get { return DisableState == 0 && SkillLockState == 0; }
        }
        public bool SkillLock
        {
            get;
            set;
        }
        public int SkillLockState
        {
            get;
            set;
        }
        public int SkillLimitState
        {
            get;
            set;
        }
        public int InnerId
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
            this.SkillManager.TryGetSkillCD(ref buff);
            return this.boostCore.TryGetSkillCD(ref buff);
        }
        public bool TryGetSkillRate(ref IBoostBuff buff)
        {
            this.SkillManager.TryGetSkillRate(ref buff);
            return this.boostCore.TryGetSkillRate(ref buff);
        }
        public bool TryGetPureBuff(ref IBoostBuff buff)
        {
            this.SkillManager.TryGetPureBuff(ref buff);
            return this.boostCore.TryGetPureBuff(ref buff);
        }
        public bool TryGetAntiDebuff(ref IBoostBuff buff)
        {
            this.SkillManager.TryGetAntiDebuff(ref buff);
            return this.boostCore.TryGetAntiDebuff(ref buff);
        }
        public bool TryGetAmpLast(ref IBoostBuff buff, params int[] buffIds)
        {
            this.SkillManager.TryGetAmpLast(ref buff, buffIds);
            return this.boostCore.TryGetAmpLast(ref buff, buffIds);
        }
        public bool TryGetAmpRate(ref IBoostBuff buff, params int[] buffIds)
        {
            this.SkillManager.TryGetAmpRate(ref buff, buffIds);
            return this.boostCore.TryGetAmpRate(ref buff, buffIds);
        }
        public bool TryGetAmpValue(ref IBoostBuff buff, params int[] buffIds)
        {
            this.SkillManager.TryGetAmpValue(ref buff, buffIds);
            return this.boostCore.TryGetAmpValue(ref buff, buffIds);
        }
        public bool TryGetEaseLast(ref IBoostBuff buff, params int[] buffIds)
        {
            this.SkillManager.TryGetEaseLast(ref buff, buffIds);
            return this.boostCore.TryGetEaseLast(ref buff, buffIds);
        }
        public bool TryGetEaseRate(ref IBoostBuff buff, params int[] buffIds)
        {
            this.SkillManager.TryGetEaseRate(ref buff, buffIds);
            return this.boostCore.TryGetEaseRate(ref buff, buffIds);
        }
        public bool TryGetEaseValue(ref IBoostBuff buff, params int[] buffIds)
        {
            this.SkillManager.TryGetEaseValue(ref buff, buffIds);
            return this.boostCore.TryGetEaseValue(ref buff, buffIds);
        }
        public bool TryGetAntiRate(ref IBoostBuff buff, params int[] buffIds)
        {
            this.SkillManager.TryGetAntiRate(ref buff, buffIds);
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
            this.SkillManager.TryGetBuff(buffId, ref buff);
            return this.buffCore.TryGetBuff(buffId, ref buff);
        }
        public bool TryPickBuff(int buffId, out IBuff buff)
        {
            this.SkillManager.TryPickBuff(buffId, out buff);
            if (null != buff)
                return true;
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
        public byte SkillFoulState
        {
            get { throw new NotImplementedException(); }
        }
        public void SkillFoul(int foulType)
        {
            throw new NotImplementedException();
        }
        public bool ForceState(int forceState)
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

        #region Event
        public event EventHandler<FoulEventArgs> FoulEvent;
        public event EventHandler<BlurEventArgs> BlurEvent;
        public ISkill FoulSrcSkill
        {
            get;
            set;
        }
        public ISkill BlurSrcSkill
        {
            get;
            set;
        }
        public void RaiseFoulEvent(FoulEventArgs e)
        {
            if (null == this.FoulEvent)
                return;
            this.FoulEvent(this, e);
        }
        public void RaiseBlurEvent(BlurEventArgs e)
        {
            if (null == this.BlurEvent)
                return;
            this.BlurEvent(this, e);
        }
        #endregion
       
    }
}
