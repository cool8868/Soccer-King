/********************************************************************************
 * 文件名：Player
 * 创建人：
 * 创建时间：5/10/2010 3:56:07 PM
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.AI.States;
using Games.NB.Match.AI.States.Dribble;
using Games.NB.Match.AI.States.Defence;
using Games.NB.Match.AI.Decides;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Common.Random;
using Games.NB.Match.Log;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.Extern.Enum;

namespace Games.NB.Match.BLL.Model.Creatures
{
    /// <summary>
    /// Represents the player entity.
    /// This partial class implemented the interface of the <see cref="IDefence"/>.
    /// </summary>
    partial class Player
    {
        /// <summary>
        /// 头球争顶
        /// </summary>
        public void HeadingBall()
        {
            _status.DefenceStatus.SuccFlag = -1;
            if (this.Status.IsAttackSide)
            {
                return;
            }
            var ballHandler = _match.Status.BallHandler;
            int round = _match.Status.Round;
            IPlayer passFrom = null;
            var subState = ballHandler.Status.SubState;
            var subStateVal = subState.GetSubState(round);
            if (subStateVal == EnumSubState.LongPassAccepting || subStateVal == EnumSubState.LongPassAccepted)
                passFrom = ballHandler.Status.PassStatus.PassFrom;
            if (null == passFrom)
                return;
            Coordinate ballPoint = _match.Football.Current;
            _match.Football.MoveTo(ballPoint);
            double bounce = Math.Max(1, _propCore[PlayerProperty.Bounce]);
            double opBounce = Math.Max(1, ballHandler.PropCore[PlayerProperty.Bounce]);
            double headingRate = (_input.Height - ballHandler.Input.Height) / 2d + bounce / opBounce * 20d + 20;
            if (headingRate >= 100 || headingRate > 0 && _match.RandomPercent() <= headingRate)
            {
                this.Rotate(ballPoint);
                this.MoveTo(ballPoint);
                ballHandler.MoveTo(Utility.GetPointWithRange(ballHandler, ballPoint, 5));
                _status.Hasball = true;
                ballHandler.AddInertiaBuff(2);
                _status.SubState.SetSubState(EnumSubState.HeadBall, round + 1);
                subState.SetSubState(EnumSubState.HeadingDuel, round);
                _status.SetDoneState(HeadingDuelState.Instance, EnumDoneStateFlag.Succ);
                ballHandler.Status.SetDoneState(HeadingDuelState.Instance, EnumDoneStateFlag.Fail);
                passFrom.Status.SetDoneState(AI.States.Pass.LongPassState.Instance, EnumDoneStateFlag.Fail);
                
            }
            else
            {
                ballHandler.Rotate(ballPoint);
                ballHandler.MoveTo(ballPoint);
                this.MoveTo(Utility.GetPointWithRange(this, ballPoint, 5));
                this.AddInertiaBuff(2);
                _status.SubState.SetSubState(EnumSubState.HeadingDuel, round);
                subState.SetSubState(EnumSubState.HeadBall, round + 1);
                _status.SetDoneState(HeadingDuelState.Instance, EnumDoneStateFlag.Fail);
                ballHandler.Status.SetDoneState(HeadingDuelState.Instance, EnumDoneStateFlag.Succ);
            }
        }
        /// <summary>
        /// Interrupts a ball.
        /// 抢断一个球
        /// </summary>
        public void InterruptionBall()
        {
            _status.DefenceStatus.SuccFlag = -1;
            if (this.Status.IsAttackSide)
            {
                this.Status.ForceState(AI.States.ChaceState.Instance);
                return;
            }
            InternalDefence(AI.States.Defence.InterruptionState.Instance);
            Rotate(_match.Football.Current);
            MoveTo(_match.Football.Current);
        }

        /// <summary>
        /// Slides tackle a ball.
        /// 铲球
        /// </summary>
        public void SlideTackleBall()
        {
            _status.DefenceStatus.SuccFlag = -1;
            if (this.Status.IsAttackSide)
            {
                this.Status.ForceState(AI.States.ChaceState.Instance);
                return;
            }
            InternalDefence(AI.States.Defence.SlideTackleState.Instance);
            Rotate(_match.Football.Current);
            MoveTo(_match.Football.Current);
        }

        #region encapsulation
        /// <summary>
        /// Internal logic of the defence.
        /// 防守逻辑
        /// </summary>
        private void InternalDefence(IState doneState)
        {
            var ballHandler = _match.Status.BallHandler;
            bool dribbleFlag = ballHandler.Status.State is AI.States.DribbleState;
            double interception = _propCore[PlayerProperty.Interception];
            double stealRate = 0d;
            double holdRate = 0d;
            IPlayer passFrom = null;
            ISubState subState = null;
            EnumSubState subStateVal = EnumSubState.None;
            if (!dribbleFlag)
            {
                subState = ballHandler.Status.SubState;
                subStateVal = subState.GetSubState(_match.Status.Round);
                if (subStateVal == EnumSubState.ShortPassAccepting || subStateVal == EnumSubState.LongPassAccepting)
                    passFrom = ballHandler.Status.PassStatus.PassFrom;
            }
            if (!ballHandler.SkillEnable)
            {
                stealRate = 100;
                holdRate = 0;
                _status.DefenceStatus.RawSuccRate = (int)Math.Round(holdRate, 0);
                _status.DefenceStatus.NewSuccRate = (int)Math.Round(stealRate, 0);
            }
            else
            {
                if (null != passFrom)
                {
                    double passing = passFrom.PropCore[PlayerProperty.Passing];
                    stealRate = Math.Pow(interception / passing, 2) * 20 + 10;
                    holdRate = 100 - stealRate;
                    if (holdRate > 0)
                        holdRate = passFrom.PropCore.GetActionRate(EnumBuffCode.PassSuccRate, holdRate);
                }
                if (stealRate == 0)
                {
                    double dribble = ballHandler.PropCore[PlayerProperty.Dribble];
                    //stealRate = Math.Pow(interception / dribble, 2) * 20 + 10; Old
                    stealRate = Math.Pow(interception / dribble, 2) * 40 + 20;
                    holdRate = 100 - stealRate;
                    if (holdRate > 0)
                        holdRate = ballHandler.PropCore.GetActionRate(EnumBuffCode.DribbleSuccRate, holdRate);
                }
                stealRate = _propCore.GetActionRate(EnumBuffCode.StealSuccRate, stealRate);
                _status.DefenceStatus.RawSuccRate = (int)Math.Round(holdRate, 0);
                _status.DefenceStatus.NewSuccRate = (int)Math.Round(stealRate, 0);
                if (holdRate >= 100 && stealRate < holdRate)
                    stealRate = 0;
                else if (holdRate < 100 && stealRate < 100)
                    stealRate = (3 * stealRate + 2 * (100 - holdRate)) / 5;
            }
            //_status.DefenceStatus.NewSuccRate = (int)stealRate;
            if (stealRate >= 100 || stealRate > 0 && _match.RandomPercent() <= stealRate)
            {
                if (dribbleFlag)
                {
                    double turnStealRate = ballHandler.PropCore[(int)EnumBuffCode.TurnStealRate, -1, -1, true];
                    if (turnStealRate > 0 && _match.RandomPercent() < turnStealRate)
                        ballHandler.AddForceStateBuff((int)EnumForceState.DefenceState, 2);
                    else if (_match.RandomPercent() < (_propCore[PlayerProperty.Strength] - ballHandler.PropCore[PlayerProperty.Strength]) * 2)
                        ballHandler.AddFallDownBuff(4);
                    else
                        AddTargetInertia(ballHandler);
                }
                _status.Hasball = true;
                _match.Football.Kick(_status.Destination, 15, this); // 断球后，球滚动
                _match.Football.MoveTo(_match.Football.Current);
                _status.SetDoneState(doneState, EnumDoneStateFlag.Succ);
                _status.DefenceStatus.SuccFlag = 1;
                if (dribbleFlag)
                {
                    ballHandler.Status.SetDoneState(AI.States.Dribble.BreakThroughState.Instance, EnumDoneStateFlag.Fail);
                }
                else
                {
                    if (null != passFrom)
                    {
                        if (subStateVal == EnumSubState.ShortPassAccepting)
                            passFrom.Status.SetDoneState(AI.States.Pass.ShortPassState.Instance, EnumDoneStateFlag.Fail);
                        else
                            passFrom.Status.SetDoneState(AI.States.Pass.LongPassState.Instance, EnumDoneStateFlag.Fail);
                        subState.SetSubState(EnumSubState.None, 0);
                    }
                }
            }
            else
            {
                AddTargetInertia(this);
                _status.SetDoneState(doneState, EnumDoneStateFlag.Fail);
                _status.DefenceStatus.SuccFlag = 0;
                if (dribbleFlag)
                {
                    ballHandler.Status.SetDoneState(AI.States.Dribble.BreakThroughState.Instance, EnumDoneStateFlag.Succ);
                }
            }
        }

        /// <summary>
        /// Adds inertia effect to the target player.
        /// 添加惯性效果至目标
        /// </summary>
        /// <param name="target">Represents the target <see cref="IPlayer"/></param>
        private static void AddTargetInertia(IPlayer target)
        {
            double probability = 100 - target.PropCore[PlayerProperty.Balance] * 0.4;
            if (probability < 20)
            {
                probability = 20;
            }

            if (target.Match.RandomPercent() < probability)
            {
                int last = 8;
                last -= Convert.ToInt32(target.PropCore[PlayerProperty.Balance] * last / 200);
                if (last <= 0)
                    last = 1;
                target.AddInertiaBuff(last);
            }
            else
            {
                target.AddInertiaBuff(1);
            }
        }

        #region Old
        ///// <summary>
        ///// Adds inertia effect to the target player.
        ///// 添加惯性效果至目标
        ///// </summary>
        ///// <param name="target">Represents the target <see cref="IPlayer"/></param>
        //private static void AddTargetInertia(IPlayer target)
        //{
        //    double probability = 100 - target.PropCore[PlayerProperty.Balance] * 0.4;
        //    if (probability < 20)
        //    {
        //        probability = 20;
        //    }

        //    if (target.Match.RandomPercent() < probability)
        //    {
        //        target.AddInertiaBuff(4);
        //    }
        //    else
        //    {
        //        target.AddInertiaBuff(1);
        //    }
        //}
        #endregion

        #endregion
    }
}
