using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.Extern.Enum;
using SkillEngine.Extern.Enum.Football;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;
using SkillEngine.SkillImpl.Football;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Enum;

namespace Games.NB.Match.BLL.Model.Creatures
{
    public partial class Player
    {
        #region Cache
        readonly BoostCore boostCore;
        readonly BuffCore buffCore;
        readonly SpecBuffCore specBuffCoe;
        int _disableState;
        int _disableCycle;
        int _disableRound;
        #endregion

        #region Data
        public EnumMatchSide SkillSide
        {
            get { return (EnumMatchSide)this.Side; }
        }
        public ISkillManager SkillManager
        {
            get { return _manager; }
        }
        public ISkillPlayer SkillPairingPlayer
        {
            get { return _status.PassStatus.PassTarget; }
        }
        public ISkillPlayer SkillPairedPlayer
        {
            get { return _status.PassStatus.PassFrom; }
        }
        public ISkillPlayer SkillAssistPlayer
        {
            get { return null; }
        }
        public ISkillPlayer OppSkillPlayer
        {
            get
            {
                IPlayer target = null;
                if (_input.AsPosition == Base.Enum.Position.Goalkeeper)
                {
                    target = _match.Status.BallHandler;
                    if (!(target.Status.State is AI.States.ShootState))
                        target = null;
                }
                else if (_status.State is AI.States.ShootState)
                    target = _manager.Opponent.GetPlayersByPosition(Base.Enum.Position.Goalkeeper)[0];
                else
                    target = _status.DefenceStatus.DefenceTarget;
                if (null != target)
                    return target;
                return _status.DefenceStatus.Defender;
            }
        }
        public int DoingState
        {
            get { return _status.State.ClientId; }
        }
        public int DoneState
        {
            get { return _status.DoneState.ClientId; }
        }
        public EnumDoneStateFlag DoneStateFlag
        {
            get { return _status.DoneStateFlag; }
        }
        public int SkillPlayerId
        {
            get { return _input.Pid; }
        }
        public byte SkillClientId
        {
            get { return _clientId; }
        }
        public int SkillPosition
        {
            get { return _input.Position; }
        }
        public int SkillColour
        {
            get { return _input.Color; }
        }
        public bool SkillHoldBall
        {
            get { return _status.Hasball && _status.BallDistanceZero; }
        }
        public bool Disable
        {
            get { return Status.FoulStatus.IsRedCard || DisableState != 0; }
        }
        public int DisableState
        {
            get
            {
                switch (_disableState)
                {
                    case (int)EnumBlurBuffCode.Injure:
                        if (_disableCycle < _match.Status.CycleCount
                         && _match.Status.Round <= _disableRound)
                            _disableCycle = _match.Status.CycleCount;
                        if (_disableCycle < _match.Status.CycleCount)
                        {
                            _disableState = 0;
                            _disableRound = 0;
                            SkillCore.AddShowModel(null, 0, 0);
                            _status.Reset();
                        }
                        break;
                    case (int)EnumBlurBuffCode.Reborn:
                        if (_disableRound <= _match.Status.Round)
                        {
                            _disableState = 0;
                            _disableRound = 0;
                            SkillCore.AddShowModel(null, (short)EnumSkillModel.Reborn, 1);
                            _status.Reset();
                        }
                        break;
                    case (int)EnumBlurBuffCode.FalldownThenInjure:
                        if (_disableRound <= _match.Status.Round)
                        {
                            _disableState = (int)EnumBlurBuffCode.Injure;
                            _disableCycle = _match.Status.CycleCount;
                            _disableRound = _match.Status.Round + 2;
                            SkillCore.AddShowModel(null, (short)EnumSkillModel.Injure, 1);
                        }
                        break;
                }
                return _disableState;
            }
            set
            {
                _disableState = value;
                if (_disableState <= 0)
                {
                    _disableCycle = 0;
                    _disableRound = 0;
                    return;
                }
                _disableCycle = _match.Status.CycleCount;
                //if (_status.Hasball)
                //    _match.Status.IsNoBallHandler = true;
                switch (_disableState)
                {
                    case (int)EnumBlurBuffCode.Injure:
                        _disableRound = _match.Status.Round + 2;
                        return;
                    case (int)EnumBlurBuffCode.Reborn:
                        _disableRound = _match.Status.Round + 5;
                        return;
                    case (int)EnumBlurBuffCode.FalldownThenInjure:
                        _disableRound = _match.Status.Round + 2;
                        return;
                }
            }
        }
        public bool SkillEnable
        {
            get
            {
                if (Disable)
                    return false;
                IBuff buff = null;
                return !this.TryGetBuff((int)EnumBlurType.LockMotion, ref buff);
            }
        }
        public bool SkillLock
        {
            get
            {
                if (Disable)
                    return true;
                IBuff buff = null;
                if (!this.TryGetBuff((int)EnumBlurType.LockMotion, ref buff))
                    return false;
                var blurBuff = buff as FootballBlurBuff;
                if (null == blurBuff)
                    return false;
                return blurBuff.BlurCode != EnumBlurBuffCode.Inertia;
            }
        }
        public int SkillLockState
        {
            get
            {
                IBuff buff = null;
                if (!this.TryGetBuff((int)EnumBlurType.LockMotion, ref buff))
                    return 0;
                var blurBuff = buff as FootballBlurBuff;
                if (null == blurBuff)
                    return 0;
                return (int)blurBuff.BlurCode;
            }
        }
        public int SkillLimitState
        {
            get
            {
                IBuff buff = null;
                if (!this.TryGetBuff((int)EnumBlurType.SpecMotion, ref buff))
                    return 0;
                var blurBuff = buff as FootballBlurBuff;
                if (null == blurBuff)
                    return 0;
                return (int)blurBuff.BlurCode;
            }
        }
        public int InnerId
        {
            get { return _clientId; }
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
            return GetStatInt(statType);
        }
        public int GetStatInt(int statType)
        {
            switch (statType)
            {
                case (int)EnumPlayerStat.Goals:
                    return _status.StatStatus.Goals;
                case (int)EnumPlayerStat.ShootTimes:
                    return _status.StatStatus.ShootTimes;
                case (int)EnumPlayerStat.PassTimes:
                    return _status.StatStatus.PassTimes;
                case (int)EnumPlayerStat.SuccPassTimes:
                    return _status.StatStatus.SuccPassTimes;
                case (int)EnumPlayerStat.ThroughTimes:
                    return _status.StatStatus.ThroughTimes;
                case (int)EnumPlayerStat.SuccThroughTimes:
                    return _status.StatStatus.SuccThroughTimes;
                case (int)EnumPlayerStat.StealTimes:
                    return _status.StatStatus.StealTimes;
                case (int)EnumPlayerStat.SuccStealTimes:
                    return _status.StatStatus.SuccStealTimes;
                case (int)EnumPlayerStat.DiveTimes:
                    return _status.StatStatus.DiveTimes;
                case (int)EnumPlayerStat.SuccDiveTimes:
                    return _status.StatStatus.SuccDiveTimes;
            }
            return 0;
        }
        public byte SkillFoulState
        {
            get
            {
                if (this.DisableState != 0)
                    return Games.NB.Match.Base.Defines.FoulLevel.INJURED;
                return _status.FoulStatus.FoulLevel;
            }
        }
        public void SkillFoul(int foulType)
        {
            this.Foul((byte)foulType);
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
