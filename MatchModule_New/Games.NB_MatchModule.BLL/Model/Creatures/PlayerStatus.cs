/********************************************************************************
 * 文件名：PlayerStatus
 * 创建人：
 * 创建时间：2009-12-11 11:28:52
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using Games.NB.Match.AI;
using Games.NB.Match.AI.States;
using Games.NB.Match.Base;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Interface.Player.Status;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Log;
using Games.NB.Match.BLL.Rules;
using Games.NB.Match.Frame;
using SkillEngine.Extern.Enum;

namespace Games.NB.Match.BLL.Model.Creatures
{
    /// <summary>
    /// Represents the player's status.
    /// </summary>
    sealed class PlayerStatus : BusinessBase, IPlayerStatus
    {
        #region Cache
        private readonly IPlayer _player;
        private readonly IPassStatus _passStatus;
        private readonly IShootStatus _shootStatus = new ShootStatus();
        private readonly IDefenceStatus _defenceStatus;
        private readonly IFoulStatus _foulStatus = new FoulStatus();
        private readonly IDiveStatus _diveStatus = new DiveStatus();
        private readonly IModelStatus _modelStatus = new ModelStatus();
        private readonly IPlayerStatStatus _statStatus = new PlayerStatStatus();
        private bool _hasBall;
        private bool _needRedecide;
        private int _ballDistance;
        private double _ballDistancePow;
        private double _destDistance;
        private double _destDistancePow;
        private Coordinate _lastDestination;
        const int s_initDistance = -9;
        private Region _xPlusActiveRegion;
        private Region _xMinusActiveRegion;
        #endregion

        #region ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerStatus"/> class.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        /// <param name="coordinate">Represents the player's default coordinate.</param>
        public PlayerStatus(IPlayer player, Coordinate curPoint, Coordinate halfDefault)
        {
            _player = player;
            _defenceStatus = new DefenceStatus(_player);
            _passStatus = new PassStatus(_player);
            SubState = new SubStateObj();

            var side = player.Side;
            var position = player.Input.AsPosition;
            if (position == Position.Goalkeeper)
            {
                this.MoveRegion = PlayerRule.GetGoalKeeperMoveRegion(position, side);
            }
            else
            {
                this.MoveRegion = MoveRegionCache.GetMoveRegion(curPoint);
                if (side == Side.Away)
                    this.MoveRegion = this.MoveRegion.Mirror();
            }
            this._xPlusActiveRegion = PlayerRule.GetActiveRegion(true, this.MoveRegion);
            this._xMinusActiveRegion = PlayerRule.GetActiveRegion(false, this.MoveRegion);
            if (side == Side.Away)
            {
                curPoint = curPoint.Mirror();
                halfDefault = halfDefault.Mirror();
            }
            this.Default = curPoint;
            this.HalfDefault = halfDefault;
            this.Acceleration = PlayerRule.GetAcceleration(player.PropCore[PlayerProperty.Speed, 0, -1, false], player.PropCore[PlayerProperty.Acceleration, 0, -1, false]);
            this.MaxSpeed = PlayerRule.GetMaxSpeed(player.PropCore[PlayerProperty.Speed, 0, -1, false]);
            //this.Speed = PlayerRule.GetSpeed(player, this.MaxSpeed);
            this.Reset();
            this.Redecide();
        }
        #endregion

        #region Data
        public bool Enable
        {
            get { return (!_foulStatus.IsInjured && !_foulStatus.IsRedCard); }
        }
        /// <summary>
        /// Represents the player has hold ball?
        /// (Only the player is has ball and ball distance less than 2)
        /// </summary>
        public bool Holdball
        {
            get
            {
                if (_hasBall == false)
                    return false;
                return BallDistancePow <= 16;
            }
        }

        /// <summary>
        /// Represents the play has ball?
        /// </summary>
        public bool Hasball
        {
            get
            {
                return _hasBall;
            }
            set
            {
                if (value)
                {
                    if (_player.Disable)
                    {
                        this._player.Match.Status.IsNoBallHandler = true;
                        return;
                    }

                    if (this._player.Match.Status.BallHandler != null)
                    {
                        this._player.Match.Status.BallHandler.Status.Hasball = false;
                    }

                    this._player.Match.Status.BallHandler = _player;
                }

                this._hasBall = value;
            }
        }
        /// <summary>
        /// Represents the player's current coordinate.
        /// </summary>
        public Coordinate Current { get; set; }

        /// <summary>
        /// Represents the player's default coordiante.
        /// </summary>
        public Coordinate Default { get; set; }

        /// <summary>
        /// 己方半场的默认坐标
        /// </summary>
        public Coordinate HalfDefault { get; set; }

        /// <summary>
        /// Represents the player's destination coordinate.
        /// </summary>
        public Coordinate Destination { get; set; }
        /// <summary>
        /// Represents the player's current angle.
        /// <remarks>
        /// In this system, towards right means 0° and towards left means 180°        
        /// </remarks>
        /// </summary>
        public int Angle { get; set; }

        /// <summary>
        /// Represents the player's current speed.
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// Represents the player's current max speed.
        /// </summary>
        public double MaxSpeed { get; set; }
        /// <summary>
        /// Represents the player's current accleration.
        /// </summary>
        public double Acceleration { get; set; }
        public byte ClientState 
        {
            get
            {
                byte state = this.State.ClientId;
                EnumSubState subState = this.SubState.GetSubState(this._player.Match.Status.Round);
                if ((int)subState > 0)
                    return (byte)subState;
                EnumClientState clientState = EnumClientState.Idle;
                switch ((EnumAIState)state)
                {
                    case EnumAIState.Chace:
                        clientState = EnumClientState.Chace;
                        break;
                    case EnumAIState.DiveBall:
                        clientState = EnumClientState.DiveBall;
                        break;
                    case EnumAIState.Idle:
                    case EnumAIState.GKHoldBall:
                        clientState = EnumClientState.Idle;
                        break;
                    case EnumAIState.Stopball:
                        clientState = EnumClientState.Stopball;
                        break;
                    case EnumAIState.Interruption:
                        clientState = EnumClientState.Interruption;
                        break;
                    case EnumAIState.SlideTackle:
                        clientState = EnumClientState.SlideTackle;
                        break;
                    case EnumAIState.DefaultDribble:
                        if (subState == EnumSubState.HeadBall)
                            clientState = EnumClientState.HeadStop;
                        else
                            clientState = EnumClientState.DefaultDribble;
                        break;
                    case EnumAIState.LongPass:
                        clientState = EnumClientState.LongPass;
                        break;
                    case EnumAIState.ShortPass:
                        if (subState == EnumSubState.HeadBall)
                            clientState = EnumClientState.HeadPass;
                        else
                            clientState = EnumClientState.ShortPass;
                        break;
                    case EnumAIState.DefaultShoot:
                         if (subState == EnumSubState.HeadBall)
                            clientState = EnumClientState.HeadShoot;
                        else
                            clientState = EnumClientState.Shoot;
                        break;
                    case EnumAIState.VolleyShoot:
                        if (subState == EnumSubState.HeadBall)
                            clientState = EnumClientState.HeadShoot;
                        else
                            clientState = EnumClientState.VolleyShoot;
                        break;
                    case EnumAIState.FreekickShoot:
                        clientState = EnumClientState.Shoot;
                        break;
                    case EnumAIState.BreakThrough:
                        clientState = EnumClientState.BreakThrough;
                        break;
                    case EnumAIState.Walk:
                        clientState = EnumClientState.Walk;
                        break;
                    case EnumAIState.RebelShoot:
                        clientState = EnumClientState.RebelShoot;
                        break;
                    case EnumAIState.HeadingDuel:
                        clientState = EnumClientState.HeadingDuel;
                        break;
                }
                return (byte)clientState;
            }
        }
        public IState PreState { get; set; }
        public IState State { get; set; }
        public IState DoneState { get; private set; }
        public EnumDoneStateFlag DoneStateFlag { get; private set; }
        public ISubState SubState { get; set; }
        public void ForceState(IState state)
        {
            this.State = state;
            this.DoneState = IdleState.Instance;
            this.DoneStateFlag = EnumDoneStateFlag.None;
        }
        public void SetDoneState(IState doneState, EnumDoneStateFlag doneStateFlag)
        {
            this.DoneState = doneState;
            this.DoneStateFlag = doneStateFlag;
            if (doneStateFlag == EnumDoneStateFlag.None)
                return;
            bool succFlag = doneStateFlag == EnumDoneStateFlag.Succ;
            var mStat = _player.Manager;
            switch ((EnumAIState)doneState.ClientId)
            {
                case EnumAIState.DefaultShoot:
                case EnumAIState.VolleyShoot:
                case EnumAIState.FreekickShoot:
                case EnumAIState.RebelShoot:
                    _statStatus.ShootTimes++;
                    mStat.Status.StatStatus.ShootTimes++;
                    if (succFlag)
                    {
                        _statStatus.Goals++;
                    }
                    return;
                case EnumAIState.ShortPass:
                case EnumAIState.LongPass:
                    _statStatus.PassTimes++;
                    mStat.Status.StatStatus.PassTimes++;
                    if (succFlag)
                    {
                        _statStatus.SuccPassTimes++;
                        mStat.Status.StatStatus.SuccPassTimes++;
                    }
                    return;
                case EnumAIState.DefaultDribble:
                case EnumAIState.BreakThrough:
                    _statStatus.ThroughTimes++;
                    mStat.Status.StatStatus.ThroughTimes++;
                    if (succFlag)
                    {
                        _statStatus.SuccThroughTimes++;
                        mStat.Status.StatStatus.SuccThroughTimes++;
                    }
                    return;
                case EnumAIState.Interruption:
                case EnumAIState.SlideTackle:
                    _statStatus.StealTimes++;
                    mStat.Status.StatStatus.StealTimes++;
                    if (succFlag)
                    {
                        _statStatus.SuccStealTimes++;
                        mStat.Status.StatStatus.SuccStealTimes++;
                    }
                    return;
                case EnumAIState.DiveBall:
                    _statStatus.DiveTimes++;
                    mStat.Status.StatStatus.DiveTimes++;
                    if (succFlag)
                    {
                        _statStatus.SuccDiveTimes++;
                        mStat.Status.StatStatus.SuccDiveTimes++;
                    }
                    return;

            }
        }
        public int ActionLast { get; set; }
        /// <summary>
        /// Represents the player's current <see cref="Zone"/> in the pitch.
        /// </summary>
        public Zone Zone
        {
            get
            {
                return _player.GetTargetZone(this.Current);
            }
        }
        public bool IsAttackSide
        {
            get 
            {
                var ballHandler=_player.Match.Status.BallHandler;
                if (null == ballHandler)
                    return false;
                return _player.Manager.Side == ballHandler.Manager.Side; 
            }
        }
        public bool IsWinger
        {
            get
            {
                return this.Current.Y <= 38 || this.Current.Y >= 98;
            }
        }
        public bool IsForceCross
        {
            get
            {
                if (!this.IsWinger)
                    return false;
                if (this._player.Side == Side.Home && Current.X >= 175
                    || this._player.Side == Side.Away && Current.X <= 35)
                    return true;
                return false;
            }
        }
        /// <summary>
        /// Represents whether the player is backward moving.
        /// 表示了球员是否向后移动
        /// </summary>
        public bool IsBackward { get; set; }

        /// <summary>
        /// Represents whether the goal kepper is stand up.        
        /// </summary>
        public bool IsStantUp { get; set; }
        #endregion

        #region Decide
        /// <summary>
        /// Represents the current player need to redecide.
        /// </summary>
        public bool NeedRedecide
        {
            get { return _needRedecide; }
        }
        /// <summary>
        /// Redecide.
        /// </summary>
        public void Redecide()
        {
            // LogHelper.Insert("[Redecide]Notify the player to redecide. [Player]" + _player.BuildinAttribute.FamilyName + ", " + _player.Side);
            _needRedecide = true;
        }

        /// <summary>
        /// Decide is end.
        /// </summary>
        public void DecideEnd()
        {
            // LogHelper.Insert("[End]Notify the player is decided end. [Player]" + _player.BuildinAttribute.FamilyName + ", " + _player.Side);
            _needRedecide = false;
        }
        #endregion

        #region Range
        /// <summary>
        /// Represents the player's width.
        /// 表示了球员的宽度
        /// </summary>
        public byte Width
        {
            get { return Defines.Player.PLAYER_WIDTH; }
        }
        /// <summary>
        /// Represents a distance between player's edge and the ball.
        /// </summary>
        public int BallDistance
        {
            get
            {
                if (_ballDistance <= s_initDistance)
                    _ballDistance = (int)(Math.Sqrt(BallDistancePow) - Width);
                return _ballDistance;
            }
        }
        public bool BallDistanceZero
        {
            get { return BallDistancePow < 9; }
        }
        public double BallDistancePow
        {
            get
            {
                if (_ballDistancePow <= s_initDistance)
                    _ballDistancePow = Current.SimpleDistance(_player.Match.Football.Current);
                return _ballDistancePow;
            }
        }

        /// <summary>
        /// Represents a distance between current coordinate and the destination coordinate.
        /// </summary>
        public double DestinationDistance
        {
            get
            {
                if (_destDistance <= s_initDistance || Destination != _lastDestination)
                    _destDistance = Math.Sqrt(DestinationDistancePow);
                return _destDistance;
            }
        }
        public double DestinationDistancePow
        {
            get
            {
                if (_destDistancePow <= s_initDistance || Destination != _lastDestination)
                {
                    _destDistancePow = Current.SimpleDistance(Destination);
                    _lastDestination = Destination;
                }
                return _destDistancePow;
            }
        }
        public bool DestinationDistanceZero
        {
            get { return Current == Destination; }
        }
        /// <summary>
        /// Represents the player's move region.
        /// 表示了球员的可移动区域
        /// </summary>
        public Region MoveRegion { get; set; }
        /// <summary>
        /// 球员的活跃区域
        /// </summary>
        public Region ActiveRegion
        {
            get
            {
                bool homeFlag = _player.Side == Base.Enum.Side.Home;
                bool atkFlag = this.IsAttackSide;
                return homeFlag ^ atkFlag ? this._xMinusActiveRegion : this._xPlusActiveRegion;
            }

        }
        #endregion

        #region Func
        public void ChangeSide()
        {
            this.MoveRegion = this.MoveRegion.Mirror();
            this._xPlusActiveRegion = PlayerRule.GetActiveRegion(true, this.MoveRegion);
            this._xMinusActiveRegion = PlayerRule.GetActiveRegion(false, this.MoveRegion);
            this.Default = this.Default.Mirror();
            this.HalfDefault = this.HalfDefault.Mirror();
            this.Destination = this.Current = this.HalfDefault;
        }
        public void Reset()
        {
            this.Current = this.HalfDefault;
            this.Destination = this.HalfDefault;
            this.ForceState(IdleState.Instance);
            this.Speed = 0;
            this.Angle = (_player.Side == Side.Home) ? 0 : 180;
        }
        public void RoundInit()
        {
            _destDistance = _destDistancePow = _ballDistancePow = _ballDistance = s_initDistance;
            _passStatus.PassTarget = null;
            _defenceStatus.DefenceTarget = null;
            _defenceStatus.RefreshDefenders();
        }
        #endregion

        #region SubStatus
        /// <summary>
        /// Represents the pass state's status.
        /// </summary>
        public IPassStatus PassStatus
        {
            get { return this._passStatus; }
        }
        /// <summary>
        /// Represents the shoot state's status.
        /// </summary>
        public IShootStatus ShootStatus
        {
            get { return this._shootStatus; }
        }
        /// <summary>
        /// Represents the defence state's status.
        /// </summary>
        public IDefenceStatus DefenceStatus
        {
            get { return this._defenceStatus; }
        }
        /// <summary>
        /// Represents the player's foul informations.
        /// </summary>
        public IFoulStatus FoulStatus
        {
            get { return _foulStatus; }
        }
        public IDiveStatus DiveStatus
        {
            get { return _diveStatus; }
        }
        public IModelStatus ModelStatus
        {
            get { return _modelStatus; }
        }
        public IPlayerStatStatus StatStatus
        {
            get { return _statStatus; }
        }
        #endregion


    }

    class PassStatus : IPassStatus
    {
        public PassStatus(IPlayer player)
        {
            this._ball = player.Match.Football;
        }
        private IFootball _ball;
        private IPlayer _passTarget;
        private IPlayer _passFrom;
        private int _passFromTurn;

        public IPlayer PassTarget
        {
            get { return _passTarget; }
            set { _passTarget = value; }
        }

        public IPlayer PassFrom
        {
            get
            {
                if (null == this._passFrom || _ball.TurnCount <= _passFromTurn)
                    return _passFrom;
                _passFrom = null;
                return null;
            }
            set
            {
                this._passFrom = value;
                this._passFromTurn = _ball.TurnCount + 1;
            }
        }

        public bool IsPassFail { get; set; }
       
    }

    class ShootStatus : IShootStatus
    {
        public ShootTarget ShootTarget { get; set; }
        public int ShootTargetIndex { get; set; }
        public int ShootSpeed { get; set; }
        public Region ShootRegion { get; set; }

        int _rawSuccRate;
        int _newSuccRate;
        public int SuccFlag { get; set; }
        public int RawSuccRate 
        {
            get { return _rawSuccRate; }
            set { _rawSuccRate = Math.Min(100, value); }
        }
        public int NewSuccRate
        {
            get { return _newSuccRate; }
            set { _newSuccRate = Math.Min(100, value); }
        }
    }

    class DefenceStatus : IDefenceStatus
    {
        #region Cache
        private readonly IPlayer _player;
        private readonly IPlayer[] _defenders = new IPlayer[2];
        private IPlayer _defenceTarget;
        double _defenderDistancePow;
        double _helpDefenderDistancePow;
        #endregion

        int _rawSuccRate;
        int _newSuccRate;
        public int SuccFlag { get; set; }
        public int RawSuccRate
        {
            get { return _rawSuccRate; }
            set { _rawSuccRate = Math.Min(100, value); }
        }
        public int NewSuccRate
        {
            get { return _newSuccRate; }
            set { _newSuccRate = Math.Min(100, value); }
        }

        public DefenceStatus(IPlayer player)
        {
            this._player = player;
        }
        public IPlayer DefenceTarget
        {
            get { return this._defenceTarget; }
            set { this._defenceTarget = value; }
        }
        public IPlayer[] Defenders
        {
            get { return this._defenders; }
        }
        public IPlayer Defender
        {
            get { return _defenders[0]; }
            set { _defenders[0] = value; }
        }
        public double DefenderDistancePow
        {
            get { return _defenderDistancePow; }
        }
        public IPlayer HelpDefender
        {
            get { return _defenders[1]; }
            set { _defenders[1] = value; }
        }
        public double HelpDefenderDistancePow
        {
            get { return _helpDefenderDistancePow; }
        }
        public void RefreshDefenders(bool forceFlag = false)
        {
            if (_player.Input.AsPosition == Position.Goalkeeper)
                return;
            const int maxRange = 256 * 256;
            const int defRange = 30;
            bool targetFlag = _player == _player.Match.Status.BallHandler;
            var players = _player.Manager.Opponent.Players;
            int idxMin = -1;
            int idxMin2 = -1;
            double min = maxRange;
            double min2 = 0d;
            double dif = 0d;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Disable)
                    continue;
                if (players[i].Input.AsPosition == Position.Goalkeeper)
                    continue;
                if (!targetFlag && !forceFlag)
                {
                    if (Math.Abs(_player.Current.X - players[i].Current.X) >= defRange
                        || Math.Abs(_player.Current.Y - players[i].Current.Y) >= defRange)
                        continue;
                }
                dif = _player.Current.SimpleDistance(players[i].Current);
                if (dif < min)
                {
                    if (min2 == 0)
                    {
                        idxMin2 = i;
                        min2 = dif;
                    }
                    else
                    {
                        idxMin2 = idxMin;
                        min2 = min;
                    }
                    idxMin = i;
                    min = dif;
                }
            }
            if (idxMin < 0)
            {
                _defenders[0] = null;
                _defenders[1] = null;
                _defenderDistancePow = maxRange;
                _helpDefenderDistancePow = maxRange;
                return;
            }
            if (!targetFlag && !forceFlag && min >= 225)
                _defenders[0] = null;
            else
                _defenders[0] = players[idxMin];
            _defenders[1] = players[idxMin2];
            _defenderDistancePow = min;
            _helpDefenderDistancePow = min2;
            if (!targetFlag)
                return;
            _defenders[0].Status.DefenceStatus.DefenceTarget = this._player;
            if (_helpDefenderDistancePow >= Defines.Player.HELPDefenceAllowRangePow
                || _player.Match.RandomPercent() < _player.Manager.Status.HelpDefenseRate)
                _defenders[1].Status.DefenceStatus.DefenceTarget = this._player;
        }
    }

    class FoulStatus : IFoulStatus
    {

        /// <summary>
        /// Represents the player's foul level.
        /// <example>        
        ///  0 - Normal
        ///  1 - Yellow Card
        ///  2 - Red Card
        ///  3 - Injured
        /// </example>
        /// </summary>
        public byte FoulLevel { get; set; }

        /// <summary>
        /// Whether the player is yellow card.
        /// </summary>
        public bool IsYellowCard
        {
            get { return FoulLevel == Defines.FoulLevel.YELLOW_CARD; }
        }

        /// <summary>
        /// Whether the player is red card.
        /// </summary>
        public bool IsRedCard
        {
            get { return FoulLevel == Defines.FoulLevel.RED_CARD; }
        }

        /// <summary>
        /// Whether the player is injured.
        /// </summary>
        public bool IsInjured
        {
            get { return FoulLevel == Defines.FoulLevel.INJURED; }
        }
    }

    class DiveStatus : IDiveStatus
    {
        public byte DiveDirection { get; set; }
    }

    class ModelStatus : IModelStatus
    {
        public byte Mid { get; set; }
        public int RemainTime { get; set; }
        public bool IsHoldBall { get; set; }
    }

 

}
