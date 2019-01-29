/********************************************************************************
 * 文件名：Player.IShortPass.cs
 * 创建人：
 * 创建时间：2009-12-18 22:36:05
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：This partial class implemented the interface of the <see cref="IPass"/>.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Drawing;
using Games.NB.Match.AI.Decides.Factory;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Interface.Player;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Model;
using Games.NB.Match.BLL.Rules;
using Games.NB.Match.Common.Random;
using Games.NB.Match.Log;

namespace Games.NB.Match.BLL.Model.Creatures
{

    /// <summary>
    /// Represents the player entity.
    /// This partial class implemented the interface of the <see cref="IShortPass"/>.
    /// </summary>
    public sealed partial class Player
    {
        #region IPass Members

        /// <summary>
        /// Decide a target to pass.
        /// 选择传递目标
        /// </summary>
        public void DecidePassTarget()
        {
            if (_input.AsPosition == Position.Goalkeeper)
            {
                _status.PassStatus.PassTarget = PassTargetDecideRule.GoalKickDecide(this);
                return;
            }
            var region = (Side == Base.Enum.Side.Home) ? _match.Pitch.AwayForcePassRegion : _match.Pitch.HomeForcePassRegion;
            if (region.IsCoordinateInRegion(Current))
            {
                this._status.PassStatus.PassTarget = PassTargetDecideRule.ForcePassDecide(this);
            }
            else
            {
                this._status.PassStatus.PassTarget = PassTargetDecideRule.Decide(this);
            }
        }
        public IPlayer DecideShortPassTarget()
        {
            var target = PassTargetDecideRule.PassClosest(this);
            if (null != target)
                this._status.PassStatus.PassTarget = target;
            return target;
        }

        public IPlayer DecideCrossTarget()
        {
            var target = PassTargetDecideRule.WingCrossDecide(this);
            if (null != target)
                this._status.PassStatus.PassTarget = target;
            return target;
        }
        /// <summary>
        /// Player to pass the ball to passtarget.
        /// </summary>      
        public void ShortPass()
        {
            _status.PassStatus.IsPassFail = false;
            if (!this._status.Hasball || !this._status.Holdball)
                return;

            var target = GetTarget();
            var speed = FootballRule.GetPassSpeed(_status.Current, target);
            Rotate(target);
            this.Manager.Match.Football.Kick(target, speed, this);
            this.Status.PassStatus.PassTarget.Status.SubState.SetSubState(EnumSubState.ShortPassAccepting, 0);
            InternalPass();
        }

        /// <summary>
        /// Player to action a long pass.
        /// </summary>
        public void LongPass()
        {
            _status.PassStatus.IsPassFail = false;

            if (!this._status.Hasball || !this._status.BallDistanceZero)
                return;

            var target = GetTarget();
            var speed = FootballRule.GetPassSpeed(_status.Current, target);
            var angle = _match.RandomByte(20, 30);
            Rotate(target);
            this.Manager.Match.Football.Kick(target, speed, angle, this);
            this.Status.PassStatus.PassTarget.Status.SubState.SetSubState(EnumSubState.LongPassAccepting, 0);
            InternalPass();
        }

        /// <summary>
        /// Validate a <see cref="Coordinate"/> enable to pass.        
        /// 判定坐标是否可以传(回传线)
        /// </summary>
        /// <param name="c"><see cref="Coordinate"/></param>
        /// <returns>Enable to pass.</returns>
        public bool IsCoordinateEnablePass(Coordinate c)
        {
            if (Side == Side.Home)
            {
                return c.X > (_status.Current.X - Defines.Player.PASS_BACK_RANGE);
            }
            else
            {
                return c.X < (_status.Current.X + Defines.Player.PASS_BACK_RANGE);
            }

        }

        /// <summary>
        /// Internal pass logic.
        /// 内部传球逻辑
        /// </summary>
        private void InternalPass()
        {
            IPlayer receiver = _status.PassStatus.PassTarget;
            receiver.Status.PassStatus.PassFrom = this;
            receiver.Status.Hasball = true;

            this._status.Redecide();
            receiver.Status.Redecide();
        }

        /// <summary>
        /// 获取传球的目标点
        /// </summary>
        private Coordinate GetTarget()
        {
            Coordinate target = _status.PassStatus.PassTarget.Current;
            bool isHome = Side == Base.Enum.Side.Home;
            if (target.Y < Defines.Pitch.PASS_PROTECTED_LINE || target.Y > Defines.Pitch.MAX_HEIGHT - Defines.Pitch.PASS_PROTECTED_LINE)
            {
                target = new Coordinate(target.X + (isHome ? 10 : -10), target.Y);
            }
            else
            {
                target = AI.Decides.Utility.GetPointWithRange(_status.PassStatus.PassTarget, _status.PassStatus.PassTarget.Destination, Defines.Player.PASS_AHEAD_RANGE);
                //Coordinate destTarget = _status.PassStatus.PassTarget.Destination;
                //double difFromX = this.Current.X - target.X;
                //double difFromY = this.Current.Y - target.Y;
                //double difDestX = destTarget.X - target.X;
                //double difDestY = destTarget.Y - target.Y;
                //if ((difFromX > 0 && difDestX > 0 || difFromX < 0 && difDestX < 0)
                //    && (difFromY > 0 && difDestY > 0 || difFromY < 0 && difDestY < 0))
                //    target = new Coordinate(target.X + (isHome ? 10 : -10), target.Y);
                //else
                //    target = AI.Decides.Utility.GetPointWithRange(_status.PassStatus.PassTarget, _status.PassStatus.PassTarget.Destination, Defines.Player.PASS_AHEAD_RANGE);
            }
            var pro = 50 + 0.25 * _propCore[PlayerProperty.Passing];
            if (pro > 90)
            {
                pro = 90;
            }
            #region Old
            //var pro = 50 + 0.2 * _propCore[PlayerProperty.Passing];
            //if (pro > 90)
            //{
            //    pro = 90;
            //}
            #endregion
            // 守门员不会传球失误
            if (_input.AsPosition != Position.Goalkeeper)
            {
                // 传球失误
                if (_match.RandomPercent() >= pro)
                {
                    target = PlayerRule.GetPassOffset(_match, this, target);
                    _status.PassStatus.IsPassFail = true;
                }
            }
            return FixOffSide(target);
        }

        #region 获取越位点
        Coordinate FixOffSide(Coordinate target)
        {
            bool isHome = _manager.Side == Base.Enum.Side.Home;
            double topX = _manager.Opponent.Status.LastPlayer.Current.X;
            if (isHome)
            {
                if (target.X > topX)
                    target.X = topX - 2;
            }
            else
            {
                if (target.X < topX)
                    target.X = topX + 2;
            }
            return target;
        }
        #endregion

        #endregion

    }
}
