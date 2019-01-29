/*****************************************************************************
 * 文件名：Football
 * 
 * 创建人：
 * 
 * 创建时间：2009-11-5 13:25:52
 * 
 * 功能说明：Represents the football entity which will emulate the real football moving.
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using Games.NB.Match.AI.States.Idle;
using Games.NB.Match.Base;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Log;
using Games.NB.Match.AI.States;
using Games.NB.Match.BLL.Rules.FreeKickRules;

namespace Games.NB.Match.BLL.Model
{

    /// <summary>
    /// Represents the football entity which will emulate the real football moving.
    /// </summary>
    [Serializable]
    class Football : BusinessBase, IFootball
    {
        #region Cache
        private Coordinate _current;
        private Coordinate _destination;
        private readonly Coordinate _default = Coordinate.Parse(Defines.Position.BALL_DEFAULT);
        private byte _angle;
        private double _speed;
        private double _acceleration;
        private double _cosA;
        private double _sinA;

        private byte _hideTime;
        private bool _isInAir;
        private bool _recalculate;
        private bool _isPassDestination;
        private bool _forceInAir;
        private bool _isOutBorder;
        private bool _isCorner;
        private int _turnCount;
        private IPlayer _lastTouchMan;
        private IMatch _match;
        #endregion

        #region .ctor
        public Football(IMatch match)
        {
            this._match = match;
            _acceleration = Defines.Pitch.BALL_ACCELERATION;
        }
        #endregion

        #region IMoveable
        /// <summary>
        /// Represents the current <see cref="Coordinate"/>.
        /// </summary>
        public Coordinate Current
        {
            get { return _current; }
        }
        /// <summary>
        /// Represents the destination <see cref="Coordinate"/>.
        /// </summary>
        public Coordinate Destination
        {
            get { return _destination; }
        }
        /// <summary>
        /// Represents the football's speed.
        /// </summary>
        public double Speed
        {
            get { return _speed; }
        }
        
        public int Angle
        {
            get { return this._angle; }
        }
        /// <summary>
        /// Represents the football's acceleration.
        /// </summary>
        public double Acceleration
        {
            get { return _acceleration; }
        }
        /// <summary>
        /// Creature moving.
        /// 物体根据自身的趋势移动
        /// </summary>
        public void Move()
        {
            if (Speed <= 0) // 防止球反向移动
            {
                ValidateOutBorder();
                return;
            }
            var d = _current.Distance(_destination); // 防止被除数为0
            if (d == 0 && _recalculate)
            {
                ValidateOutBorder();
                return;
            }

            if (_recalculate)
            {
                _cosA = (_destination.X - _current.X) / d;
                _sinA = (_destination.Y - _current.Y) / d;

                this._recalculate = false;
            }

            var vx = Speed * _cosA;
            var vy = Speed * _sinA;

            var ax = Acceleration * _cosA;
            var ay = Acceleration * _sinA;

            var t = Defines.Match.ROUND_TIME;

            var x = _current.X + vx * t + 0.5 * ax * Math.Pow(t, 2);
            var y = _current.Y + vy * t + 0.5 * ay * Math.Pow(t, 2);

            var nextDistance = _current.SimpleDistance(new Coordinate(x, y));
            if (nextDistance > d * d)
            {
                _isPassDestination = true;
                _isInAir = false;
            }
            _current.X = x;
            _current.Y = y;

            _speed = Speed + Acceleration * t;
            ValidateOutBorder();
        }
        public void MoveTo(Coordinate target)
        {
            _current.X = target.X;
            _current.Y = target.Y;

            _destination.X = target.X;
            _destination.Y = target.Y;
            _speed = 0;
            _isInAir = false;
            
        }
        public void MoveTo(double x, double y)
        {
            _current.X = x;
            _current.Y = y;

            _destination.X = x;
            _destination.Y = y;
            _speed = 0;
            _isInAir = false;
            
        }
        public void Reset()
        {
            _current = _default;
            _destination = _default;
            _speed = 0;
            _isCorner = false;
            _isOutBorder = false;
            _acceleration = Defines.Pitch.BALL_ACCELERATION;
        }
        public void DecreaseSpeed()
        {
 
        }
        #endregion

        #region Data
        /// <summary>
        /// Represents the football's visible.
        /// 表示了球的可见性
        /// </summary>
        public bool Visible
        {
            get
            {
                return (_hideTime == 0);
            }
        }
        /// <summary>
        /// Represents whether the ball is in the air.
        /// 表示了球是否在空中
        /// </summary>
        public bool IsInAir
        {
            get { return _isInAir; }
        }

        /// <summary>
        /// Represents whether the ball has passed the destination.
        /// 是否通过了目标点（惯性移动）
        /// </summary>
        public bool IsPassDestination
        {
            get { return _isPassDestination; }
        }
        /// <summary>
        /// 是否出界
        /// </summary>
        public bool IsOutBorder 
        {
            get
            {
                return this._isOutBorder;
            }
        }
        /// <summary>
        /// 是否角球
        /// </summary>
        public bool IsCorner
        {
            get
            {
                return this._isCorner;
            }
        }
        public int TurnCount
        {
            get { return _turnCount; }
        }
        public bool TurnFlag
        {
            get;
            set;
        }
        /// <summary>
        /// Represents the last touch ball <see cref="IPlayer"/>.
        /// 表示了最后接触球的球员
        /// </summary>
        public IPlayer LastTouchMan
        {
            get { return _lastTouchMan; }
        }
        #endregion

        #region Func
        /// <summary>
        /// Force the next pass is in air.        
        /// </summary>
        public void ForceInAir()
        {
            _forceInAir = true;
        }
        /// <summary>
        /// Kick the ball to a <see cref="Coordinate"/>.
        /// 把球踢往某一点
        /// </summary>
        /// <param name="coordinate">Represents the target <see cref="Coordinate"/>.</param>
        /// <param name="speed">Represents the ball's start speed.</param>
        /// <param name="lastMan">Represents the last touch ball <see cref="IPlayer"/></param>
        public void Kick(Coordinate destination, double speed, IPlayer lastMan)
        {
            TurnFlag = true;
            ++_turnCount;
            _lastTouchMan = lastMan;
            if (_forceInAir)
            {
                _forceInAir = false;
                Kick(destination, speed, 45, lastMan);
                return;
            }

            _destination = destination;
            _speed = speed;
            _recalculate = true;
            _angle = 0;
            _isInAir = false;
            _isPassDestination = false;
        }
        /// <summary>
        /// Kick the ball to a <see cref="Coordinate"/>, and the ball will in the air all the time.
        /// 将球踢往某一点（球是在空中的）
        /// </summary>
        /// <param name="coordinate">Represents the target <see cref="Coordinate"/>.</param>
        /// <param name="speed">Represents the ball's start speed.</param>
        /// <param name="angle">Represents the ball's start angle.</param>
        /// <param name="lastMan">Represents the last touch ball <see cref="IPlayer"/>.</param>
        public void Kick(Coordinate destination, double speed, byte angle, IPlayer lastMan)
        {
            _lastTouchMan = lastMan;

            //speed *= 2;
            Kick(destination, speed, lastMan);
            _isInAir = true;
            _angle = angle;
        }
        #endregion
    
        #region ValidateOutBorder
        /// <summary>
        /// Validates whether the football is out of the ground border.
        /// 判断足球是否出了边界
        /// </summary>
        private void ValidateOutBorder()
        {
            if (_lastTouchMan == null)
            {
                //LogHelper.Insert("Validate Out the border with null last touch man.round:"+ _match.Status.Round, LogType.Error);
                return;
            }

            _isCorner = false;
            _isOutBorder = false;
            #region 门球
            if (_current.X < 0 &&_lastTouchMan.Side == Side.Away)
            {
                 _match.Status.Break(EnumMatchBreakState.GoalKick);
                 //_match.GKOpenball(_match.HomeManager);
                 _match.GKOpenball(_lastTouchMan.Manager.Opponent);
                 return;
            }
            else if (_current.X > Defines.Pitch.MAX_WIDTH && _lastTouchMan.Side == Side.Home)
            {
                _match.Status.Break(EnumMatchBreakState.GoalKick);
                //_match.GKOpenball(_match.AwayManager);
                _match.GKOpenball(_lastTouchMan.Manager.Opponent);
                return;
            }
            #endregion

            #region 角球
            _isCorner=_current.X < 0||_current.X > Defines.Pitch.MAX_WIDTH;
            if (_isCorner)
            {
                _match.Status.Break(EnumMatchBreakState.CornerKick);
                MoveTo(_current.X < 0 ? 0 : Defines.Pitch.MAX_WIDTH, _current.Y);
                _match.OutBorder(_lastTouchMan.Manager);
                return;
            }
            #endregion

            #region 边线球
            _isOutBorder = _current.Y < 0 || _current.Y > Defines.Pitch.MAX_HEIGHT;
            if (_isOutBorder)
            {
                _match.Status.Break(EnumMatchBreakState.HandThrow);
                MoveTo(_current.X, _current.Y < 0 ? 0 : Defines.Pitch.MAX_HEIGHT);
                _match.OutBorder(_lastTouchMan.Manager);
                return;
            }
            #endregion
        }

        #endregion
    }
}
