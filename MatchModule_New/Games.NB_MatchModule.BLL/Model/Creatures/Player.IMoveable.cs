/********************************************************************************
 * 文件名：PlayerMoveable
 * 创建人：
 * 创建时间：2009-12-18 21:59:43
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：This class implemented the interface of the <see cref="IMoveable"/>.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using Games.NB.Match.AI.States;
using Games.NB.Match.Base;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.BLL.Rules;
using Games.NB.Match.Log;

namespace Games.NB.Match.BLL.Model.Creatures
{
    /// <summary>
    /// Represents the player entity.
    /// This class implemented the interface of the <see cref="IMoveable"/>.
    /// </summary>
    public sealed partial class Player
    {

        #region IMoveable 

        /// <summary>
        /// Represents the player's current <see cref="Coordinate"/>.
        /// </summary>
        public Coordinate Current
        {
            get { return this._status.Current; }
        }

        /// <summary>
        /// Represents the player's destination.
        /// </summary>
        public Coordinate Destination
        {
            get { return this._status.Destination; }
        }

        /// <summary>
        /// Represents the current object's speed.
        /// </summary>
        public double Speed
        {
            get { return this._status.Speed; }
        }

        public int Angle
        {
            get { return this._status.Angle; }
        }

        /// <summary>
        /// Represents the current object's acceleration.
        /// </summary>
        public double Acceleration
        {
            get { return this._status.Acceleration; }
        }

        /// <summary>
        /// Moves current object.
        /// 球员移动
        /// </summary>
        public void Move()
        {
            // 判定当前球员是否移动出了可移动区域
            //ValidateOutofRegion();
            ValidateOutofRegionX();

            #region 球员旋转
            var angle = _status.Angle;
            Rotate(_status.Destination);

            if (Math.Abs(_status.Angle - angle) > 90)
            {
                DecreaseSpeed();    // decrease the speed while the player rotate large than 90 degree
                if (_status.IsAttackSide)
                    return;
            }
            #endregion

            #region 门将的后退
            if (_input.AsPosition == Position.Goalkeeper)
            {
                if (this.Side == Side.Home)
                {
                    _status.IsBackward = (_status.Destination.X < _status.Current.X);
                }
                else
                {
                    _status.IsBackward = (_status.Destination.X > _status.Current.X);
                }

                if (_status.Hasball) // 如果门将往球移动，则为站起状态
                {
                    _status.IsStantUp = false;
                }
            }
            #endregion

            #region 球员移动
            if (Status.DestinationDistanceZero)
            {
                Redecide();
                return;
            }

            // 当前回合可移动的距离
            double totalDistance = Status.DestinationDistance;
            double topSpeed = PlayerRule.GetSpeed(this, Speed + Acceleration * Defines.Match.ROUND_TIME);
            double passedDistance = (Speed + topSpeed) / 2 * Defines.Match.ROUND_TIME;
            passedDistance = Math.Max(0.51, passedDistance);
            if (passedDistance > totalDistance)
            {
                this._status.Current = this._status.Destination;
                Coordinate newCoor;
                if (this._status.Current.Regulate(out newCoor))
                {
                    this._status.Destination = this._status.Current = newCoor;
                    Redecide();
                }
                return;
            }

            // 将移动距离映射至坐标系上
            var changeX = Math.Abs(this._status.Destination.X - this._status.Current.X) * passedDistance / totalDistance;
            var changeY = Math.Abs(this._status.Destination.Y - this._status.Current.Y) * passedDistance / totalDistance;

            // 计算移动后坐标
            var x = _status.Current.X;
            var y = _status.Current.Y;
            x += (this._status.Destination.X > this._status.Current.X) ? changeX : -changeX;
            y += (this._status.Destination.Y > this._status.Current.Y) ? changeY : -changeY;

            // 防止守门员离开禁区 同时守门员持球时可以离开禁区 （解决门将无法接传球失误的球 2010-6-21 ）
            if (this._input.AsPosition == Position.Goalkeeper && _status.Hasball == false)
            {
                var region = (Side == Base.Enum.Side.Home) ? _match.Pitch.HomePenaltyRegion : _match.Pitch.AwayPenaltyRegion;
                if (x < region.Start.X) x = region.Start.X;
                if (x > region.End.X) x = region.End.X;
                if (y < region.Start.Y) y = region.Start.Y;
                if (y > region.End.Y) y = region.End.Y;
            }

            // 修正最后坐标&速度
            this._status.Current = new Coordinate(x, y).Regulate();
            this._status.Speed = topSpeed;
            #endregion

        }

        /// <summary>
        /// Moves the object to the target.
        /// </summary>
        /// <param name="target">Represents the target <see cref="Coordinate"/></param>
        public void MoveTo(Coordinate target)
        {
            this._status.Current = new Coordinate(target.X, target.Y);
        }

        /// <summary>
        /// Moves the object to the target.        
        /// </summary>
        /// <param name="x">Represents the X-axis coordinate.</param>
        /// <param name="y">Represents the Y-axis coordinate.</param>
        public void MoveTo(double x, double y)
        {
            this._status.Current = new Coordinate(x, y);
        }

        /// <summary>
        /// Resets current object.
        /// 球员重置位置
        /// </summary>
        public void Reset()
        {
            this._status.Reset();
        }

        /// <summary>
        /// 将球员的速度降低为当前速度的一半
        /// </summary>
        public void DecreaseSpeed()
        {
            _status.Speed = _status.Speed / 2;
        }

        #endregion

        #region 限定区域

        /// <summary>
        /// Validates whether the player has moved out of his moving region.
        /// 判断球员是否已经移动超出了可移动区域
        /// </summary>
        private void ValidateOutofRegion()
        {
            // 持球人不考虑可移动区域
            if (_status.Hasball)
            {
                return;
            }

            // 当有防守人时可以移动出区域
            //if (_status.DefenceStatus.DefenceTarget != null)
            //{
            //    return;
            //}

            var region = _status.MoveRegion;

            if (this.Status.IsAttackSide) // 进攻方 
            {
                if (this.Side == Base.Enum.Side.Home)
                {
                    region.End.X += 30;
                }
                else
                {
                    region.Start.X -= 30;
                }
            }
            else // 防守方
            {
                if (this.Side == Base.Enum.Side.Home)
                {
                    region.Start.X -= 30;
                }
                else
                {
                    region.End.X += 30;
                }
            }

            if (_status.Current.X < region.Start.X ||
                _status.Current.X > region.End.X ||
                _status.Current.Y < region.Start.Y ||
                _status.Current.Y > region.End.Y)
            { // out of the move region

                double x = _status.Current.X;
                double y = _status.Current.Y;

                if (_status.Current.X < region.Start.X)
                {
                    x = region.Start.X + 5;
                }

                if (_status.Current.X > region.End.X)
                {
                    x = region.End.X - 5;
                }

                if (_status.Current.Y < region.Start.Y)
                {
                    y = region.Start.Y + 5;
                }

                if (_status.Current.Y > region.End.Y)
                {
                    y = region.End.Y - 5;
                }

                // 更改球员的移动目标为可移动范围内
                SetTarget(x, y);
            }
        }
        /// <summary>
        /// 11人制限定区域
        /// </summary>
        private void ValidateOutofRegionX()
        {
            // 持球人不受限制
            if (_status.Hasball)
                return;
            //盯防持球人不受限制
            if (null != _status.DefenceStatus.DefenceTarget
                && _status.DefenceStatus.DefenceTarget.Status.Hasball)
                return;
            double maxX = Defines.Pitch.MAX_WIDTH;
            double maxY = Defines.Pitch.MAX_HEIGHT;
            var pStart = _status.MoveRegion.Start;
            var pEnd = _status.MoveRegion.End;
            double topX = -1;
            bool isHome = this.Manager.Side == Base.Enum.Side.Home;
            bool isGK = this._input.AsPosition == Position.Goalkeeper;
            bool isAtk = this.Status.IsAttackSide;
            double curX = _status.Current.X;
            double curY = _status.Current.Y;
            if (!isGK)
            {
                double maxEx = 180;
                double minEx = 180;
                if (isAtk) // 进攻方 
                {
                    if (this._input.AsPosition == Position.Fullback)
                    {
                        maxEx = 120;
                        minEx = 90;
                    }
                    if (null != this.Manager.Opponent.Status.LastPlayer)
                        topX = this.Manager.Opponent.Status.LastPlayer.Status.Current.X;
                    //边路可往前突破60,不可越位
                    bool isSide = this._status.IsWinger;
                    if (isHome)
                    {
                        if (topX < 0)
                            pEnd.X = pEnd.X + (isSide ? maxEx : minEx);
                        else
                            pEnd.X = Math.Min(pEnd.X + (isSide ? maxEx : minEx), topX);
                    }
                    else
                    {
                        if (topX < 0)
                            pStart.X = pStart.X - (isSide ? maxEx : minEx);
                        else
                            pStart.X = Math.Max(pStart.X - (isSide ? maxEx : minEx), topX);
                    }
                    if (isSide)
                    {
                        if (pStart.Y <= 2)
                            pStart.Y = 2;
                        if (pEnd.Y >= maxY - 2)
                            pEnd.Y = maxY - 2;
                    }
                }
                else // 防守方
                {
                    minEx = 30;
                    bool isFore = this._input.AsPosition == Position.Forward;
                    if (isFore && null != this.Manager.Opponent.Status.LastPlayer)
                        topX = this.Manager.Opponent.Status.LastPlayer.Status.Current.X;
                    if (isHome && curX >= topX + 10 && _status.Destination.X < topX + 10
                        || !isHome && curX <= topX - 10 && _status.Destination.X > topX - 10)
                        return;
                    //前锋可往前突破对方后卫+10
                    if (isHome)
                    {
                        if (isFore && topX > 0)
                        {
                            //pEnd.X = Math.Max(pEnd.X, topX + 10);
                            pEnd.X = topX + 10;
                        }
                        pStart.X = Math.Min(pStart.X - minEx, topX);
                    }
                    else
                    {
                        if (isFore && topX > 0)
                        {
                            //pStart.X = Math.Min(pStart.X, topX - 10);
                            pStart.X = topX - 10;
                        }
                        pEnd.X = Math.Max(pEnd.X + minEx, topX);
                    }
                }
            }
            if (curX < pStart.X || curX > pEnd.X
                || curY < pStart.Y || curY > pEnd.Y)
            {
                //防止球员摆动
                double ballX = this.Match.Football.Current.X;
                double ballY = this.Match.Football.Current.Y;
                double offX = 0;
                double offY = 0;
                double mindif = 2;
                double dif = 0;
                if (this.Status.IsAttackSide
                    || this._input.AsPosition == Position.Forward)
                {
                    dif = Math.Abs(curX - ballX);
                    dif =dif <= 15 ? 8 : 10;
                    if (curX * 2 <= pStart.X + pEnd.X)
                        offX = mindif + ballX / maxX * dif;
                    else
                        offX = -(mindif + (maxX - ballX) / maxX * dif);
                    if (curY * 2 <= pStart.Y + pEnd.Y)
                        offY = mindif + ballY / maxY * dif;
                    else
                        offY = -(mindif + (maxY - ballY) / maxY * dif);
                }
                else
                {
                    dif = Math.Abs(curX - ballX);
                    dif = dif <= 20? 8 : 10;
                    if (ballX >= curX)
                        offX = mindif + (pEnd.X - curX) / (pEnd.X - pStart.X) * dif;
                    else
                        offX = -(mindif + (curX - pStart.X) / (pEnd.X - pStart.X) * dif);
                    if (ballY >= curY)
                        offY = mindif + (pEnd.Y - curY) / (pEnd.Y - pStart.Y) * dif;
                    else
                        offY = -(mindif + (curY - pStart.Y) / (pEnd.Y - pStart.Y) * dif);
                }
                // 更改球员的移动目标为可移动范围内
                if (curX < pStart.X)
                {
                    curX = pStart.X + 4;
                    curY = curY + offY;
                }
                if (curX > pEnd.X)
                {
                    curX = pEnd.X - 4;
                    curY = curY + offY;
                }
                if (curY < pStart.Y)
                {
                    curX = curX + offX;
                    curY = pStart.Y + 4;
                }
                if (curY > pEnd.Y)
                {
                    curX = curX + offX;
                    curY = pEnd.Y - 4;
                }

                SetTarget(curX, curY);
            }
        }
        #endregion
    }
}
