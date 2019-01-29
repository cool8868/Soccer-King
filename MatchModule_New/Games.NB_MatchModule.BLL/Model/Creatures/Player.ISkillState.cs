using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.AI;
using Games.NB.Match.AI.States;
using Games.NB.Match.AI.States.Shoot;
using Games.NB.Match.AI.States.Pass;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Interface;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillImpl.Football;

namespace Games.NB.Match.BLL.Model.Creatures
{
    public partial class Player
    {
        public bool ForceState(int forceState)
        {
            if (forceState == 0 || _input.AsPosition == Base.Enum.Position.Goalkeeper)
                return false;
            switch (forceState)
            {
                case (int)EnumForceState.ShootState:
                    if (!_status.Holdball || !_propCore.CanShoot(_manager.Opponent.Side))
                        return false;
                    _status.State = ShootState.Instance;
                    break;
                case (int)EnumForceState.UltraShootState:
                    if (!_status.Holdball || _input.AsPosition == Base.Enum.Position.Goalkeeper)
                        return false;
                    _status.State = ShootState.Instance;
                    break;
                case (int)EnumForceState.PassState:
                    if (!_status.Holdball)
                        return false;
                    _status.State = PassState.Instance;
                    break;
                case (int)EnumForceState.DribbleState:
                    if (!_status.Holdball)
                        return false;
                    _status.State = DribbleState.Instance;
                    break;
                case (int)EnumForceState.DefenceState:
                    if (_status.IsAttackSide || _match.Football.IsInAir || !_propCore.CanSteal())
                        return false;
                    var target = _match.Status.BallHandler;
                    if (null == target || target.Status.State is ShootState || target.Status.State is PassState)
                        return false;
                    _status.DefenceStatus.DefenceTarget = target;
                    _status.State = DefenceState.Instance;
                    this.Rotate(_match.Football.Destination);
                    break;
                case (int)EnumForceState.UltraDefenceState:
                    if (_status.IsAttackSide || _match.Football.IsInAir)
                        return false;
                    target = _match.Status.BallHandler;
                    if (null == target || target.Status.State is ShootState || target.Status.State is PassState)
                        return false;
                    _status.DefenceStatus.DefenceTarget = target;
                    _status.State = DefenceState.Instance;
                    this.Rotate(_match.Football.Destination);
                    break;
                default:
                    return false;
            }
            int loopi = 0;
            do
            {
                var state = _status.State;
                state.Enter(this);
                _status.State = state.QuickDecide(this, state);
                if (loopi == 0 && _status.State.Stopable)
                    return true;
                if (loopi >= 5)
                    _status.State = IdleState.Instance;
            }
            while (!_status.State.Stopable && ++loopi < 10);
            return false;
        }
        public bool SkillDecide()
        {
            switch (this.SkillLimitState)
            {
                case (int)EnumBlurBuffCode.Rebel:
                    return toRebel();
                case (int)EnumBlurBuffCode.Inertia:
                    return toInertia();
                default:
                    break;
            }
            IState preState = _status.State;
            SkillEngine.SkillImpl.SkillFacade.TriggerPlayerSkills(this, 1);
            if (preState != _status.State)
                return true;
            IBuff buff = null;
            if (!this.TryGetBuff((int)EnumSpecEffect.ForceState, ref buff))
                return false;
            var forceBuff = buff as ForceStateBuff;
            if (null == forceBuff)
                return false;
            int maxRate = SkillDefines.MAXStorePercent;
            int rate = forceBuff.Rate;
            if (rate <= 0 || rate < maxRate && _match.RandomPercent(maxRate) > rate)
                return false;
            return this.ForceState((int)forceBuff.ForceState);
        }
        public bool SkillAction()
        {
            switch (this.SkillLockState)
            {
                case (int)EnumBlurBuffCode.Stand:
                case (int)EnumBlurBuffCode.Falldown:
                case (int)EnumBlurBuffCode.Stun:
                    _status.ForceState(IdleState.Instance);
                    _status.State.Action(this);
                    return true;
                case (int)EnumBlurBuffCode.Puzzle:
                    return doPuzzle();
                case (int)EnumBlurBuffCode.Inertia:
                    return doInertia();
                default:
                    break;
            }
            switch (this.SkillLimitState)
            {
                case (int)EnumBlurBuffCode.Inertia:
                    return doInertia();
                default:
                    return false;
            }
        }
      
        #region Native
        bool doPuzzle()
        {
            const byte offset = Defines.Player.PUZZLE_RANGE;
            var x = _match.RandomInt32(Convert.ToInt32(_status.Current.X - offset), Convert.ToInt32(_status.Current.X + offset));
            var y = _match.RandomInt32(Convert.ToInt32(_status.Current.Y - offset), Convert.ToInt32(_status.Current.Y + offset));
            x = Math.Max(0, Math.Min(Defines.Pitch.MAX_WIDTH, x));
            y = Math.Max(0, Math.Min(Defines.Pitch.MAX_HEIGHT, y));
            this.SetTarget(x, y);
            _status.ForceState(ChaceState.Instance);
            _status.State.Action(this);
            return true;
        }
        bool doInertia()
        {
            if (_status.DestinationDistanceZero)
            {
                _status.State = IdleState.Instance;
            }
            else
            {
                var x = _status.Current.X + Math.Cos(_status.Angle * Math.PI / 180) * 50;
                var y = _status.Current.Y + Math.Sin(_status.Angle * Math.PI / 180) * 50;
                x = Math.Max(0, Math.Min(Defines.Pitch.MAX_WIDTH, x));
                y = Math.Max(0, Math.Min(Defines.Pitch.MAX_HEIGHT, y));
                this.SetTarget(x, y);
                this.DecreaseSpeed();
                _status.ForceState(ChaceState.Instance);
            }
            _status.State.Action(this);
            return true;
        }
        bool toInertia()
        {
            if (_status.DestinationDistanceZero)
            {
                _status.State = IdleState.Instance;
            }
            else
            {
                _status.State = ChaceState.Instance;
            }
            return true;
        }
        bool toRebel()
        {
            if (!_status.Hasball)
            {
                if (_input.AsPosition != Base.Enum.Position.Goalkeeper && null == _status.DefenceStatus.DefenceTarget)
                    return false;
                _status.ForceState(PositionalState.Instance);
                return true;
            }
            if (!_status.Holdball)
            {
                _status.ForceState(HoldBallState.Instance);
                return true;
            }
            bool rebelFlag = false;
            var shootRegion = (Side == Base.Enum.Side.Home) ? _match.Pitch.HomeShootRegion : _match.Pitch.AwayShootRegion;
            if (shootRegion.IsCoordinateInRegion(_status.Current))
            {
                this.AddFinishingBuff(1);
                rebelFlag = true;
            }
            else
            {
                if (_propCore.CanShoot(_manager.Side))
                    rebelFlag = true;
            }
            if (rebelFlag)
            {
                _status.ForceState(RebelShootState.Instance);
                return true;
            }
            if (null == _status.DefenceStatus.Defender)
                _status.DefenceStatus.RefreshDefenders();
            if (null != _status.DefenceStatus.HelpDefender)
                _status.PassStatus.PassTarget = _status.DefenceStatus.HelpDefender;
            else
                _status.PassStatus.PassTarget = _status.DefenceStatus.Defender;
            if (null != _status.PassStatus.PassTarget)
                _status.ForceState(ShortPassState.Instance);
            else
                _status.ForceState(IdleState.Instance);
            return true;
        }
        #endregion
    }
}
