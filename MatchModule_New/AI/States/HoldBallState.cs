/********************************************************************************
 * 文件名：HoldBallState
 * 创建人：
 * 创建时间：2009-11-18 13:36:10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Common.Random;

namespace Games.NB.Match.AI.States
{
    /// <summary>
    /// Represents the player who have the ball.
    /// This class implemented the Singleton pattern.
    /// </summary>
    /// <example>HoldBallState.Instance</example>
    [Singleton]
    public class HoldBallState : State
    {
        /// <summary>
        /// Represents the <see cref="HoldBallState"/>'s instance.
        /// </summary>
        public static HoldBallState Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Return the <see cref="HoldBallState"/>'s name.
        /// </summary>
        /// <returns><see cref="HoldBallState"/>'s name.</returns>
        public override string ToString()
        {
            return "HoldBallState";
        }

        /// <summary>
        /// Initialize the <see cref="HoldBallState"/>.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(ActionState.Instance);
            this.StateChain.Add(ShootState.Instance);
            this.StateChain.Add(PassState.Instance);
            this.StateChain.Add(DribbleState.Instance);
            //this.StateChain.Add(StopballState.Instance);

            this.StateCondition.Add(PassState.Instance, ValidateHoldBallToPass);
            this.StateCondition.Add(ActionState.Instance, ValidateHoldBallToAction);
            this.StateCondition.Add(DribbleState.Instance, ValidateHoldBallToDribble);
            this.StateCondition.Add(ShootState.Instance, ValidateHoldBallToShoot);
            // this.StateCondition.Add(StopballState.Instance, ValidateHoldBallToStopBall);
        }

        /// <summary>
        /// While enter the <see cref="HoldBallState"/>, to validate the ball distance.
        /// if the ball distance is larget than zero, the player will decide to chace ball.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        public override void Enter(IPlayer player)
        {
            IMatch match = player.Match;

            // while the ball distance is larger than zero, the player will decide to chace ball
            if (player.Status.Hasball && !player.Status.BallDistanceZero)
            {
                if (match.Football.Speed < 18)
                {
                    player.SetTargetBall(true);
                }
                else
                {
                    if (match.Football.IsPassDestination) // 球已经滚动过了目标点
                    {
                        player.SetTargetBall(true);
                    }
                    else
                    {
                        player.SetTargetBall(false);
                    }
                }
            }
        }

        /// <summary>
        /// Decides the nex <see cref="IState"/>
        /// </summary>
        /// <param name="player">Represents the current <see cref="IPlayer"/>.</param>
        /// <param name="preview">Represents the preview <see cref="IState"/></param>
        /// <returns></returns>
        public override IState QuickDecide(IPlayer player, IState preview)
        {
            var match = player.Match;
            if (player.Status.Hasball == false)
            {
                return OffBallState.Instance;
            }
            else
            {
                if (player.Status.Holdball == false)
                {
                    return ChaceState.Instance;
                }

                if (player.Status.NeedRedecide == false)
                {
                    return ActionState.Instance;
                }

                #region 处理传球来源
                int round = player.Match.Status.Round;
                var subState= player.Status.SubState;
                EnumSubState subStateVal = subState.GetSubState(round);
                if (subStateVal == EnumSubState.LongPassAccepting
                    || subStateVal == EnumSubState.ShortPassAccepting)
                {
                    var passFrom = player.Status.PassStatus.PassFrom;
                    if (null == passFrom)
                        subState.SetSubState(EnumSubState.None, round);
                    else
                    {
                        if (subStateVal == EnumSubState.LongPassAccepting)
                        {
                            passFrom.Status.SetDoneState(AI.States.Pass.LongPassState.Instance, SkillEngine.Extern.Enum.EnumDoneStateFlag.Succ);
                            subState.SetSubState(EnumSubState.LongPassAccepted, round + 1);
                        }
                        else if (subStateVal == EnumSubState.ShortPassAccepting)
                        {
                            passFrom.Status.SetDoneState(AI.States.Pass.ShortPassState.Instance, SkillEngine.Extern.Enum.EnumDoneStateFlag.Succ);
                            subState.SetSubState(EnumSubState.None, round);
                        }
                    }
                }
                #endregion

                #region 守门员传球
                if (player.Input.AsPosition == Position.Goalkeeper)
                {
                    return PassState.Instance;
                }
                #endregion

                #region 验证射门
                var shootRegion = (player.Side == Side.Home) ?
                    player.Match.Pitch.AwayShootRegion :
                    player.Match.Pitch.HomeShootRegion;

                if (shootRegion.IsCoordinateInRegion(player.Current))
                {
                    if (subStateVal != EnumSubState.HeadBall)
                        player.AddFinishingBuff(1);
                    return ShootState.Instance;
                }

              
                // while the player is not in the shooting area, can't to shoot
                if (subStateVal == EnumSubState.HeadBall && player.PropCore.CanHeadShoot(player.Manager.Opponent.Side)
                    || subStateVal != EnumSubState.HeadBall && player.PropCore.CanShoot(player.Manager.Opponent.Side))
                {
                    if (match.RandomPercent() < player.PropCore.ShootChooseRate())
                    {
                        return ShootState.Instance;
                    }
                }
                #endregion

                if (subStateVal != EnumSubState.HeadBall)
                {
                    #region 下底传中
                    if (player.Status.IsForceCross)
                    {
                        if (null != player.DecideCrossTarget())
                        {
                            player.DecideEnd();
                            return Pass.LongPassState.Instance;
                        }
                    }
                    #endregion

                    #region 单刀球
                    if (Decides.Utility.IfSolo(player))
                    {
                        player.SetTarget(Decides.Utility.GetSoloPosition(player));
                        return Dribble.DefaultDribbleState.Instance;
                    }
                    #endregion
                }

                #region 验证传球
                //region = (player.Side == Side.Home) ? player.Match.Pitch.AwayForcePassRegion : player.Match.Pitch.HomeForcePassRegion;
                //if (region.IsCoordinateInRegion(player.Current))
                //{
                //    if (RandomHelper.GetPercentage() < 40)
                //    {
                //        return PassState.Instance;
                //    }
                //}
                double rawPassRate = 0;
                if (player.Input.AsPosition == Position.Midfielder || player.Input.AsPosition == Position.Forward)
                {
                    if (player.PropCore[PlayerProperty.Dribble] - player.PropCore[PlayerProperty.Passing] >= 18)
                    {
                        rawPassRate = 30;
                    }
                }
                double passRate = player.PropCore.PassChooseRate(rawPassRate);
                double dribbleRate = player.PropCore.DribbleChooseRate();
                if (dribbleRate >= 100 && passRate < dribbleRate)
                    passRate = 0;
                else if (dribbleRate < 100 && passRate < 100)
                    passRate = (3 * passRate + 2 * (100 - dribbleRate)) / 5;
                if (passRate >= 100 || passRate > 0 && match.RandomPercent() <= passRate)
                {
                    if (subStateVal == EnumSubState.HeadBall)
                    {
                        if (null != player.DecideShortPassTarget())
                            return Pass.ShortPassState.Instance;
                        return DribbleState.Instance;
                    }
                    return PassState.Instance;
                }
                #endregion

                return DribbleState.Instance;
            }
        }

        #region encapsulation

        private static readonly HoldBallState _instance = new HoldBallState();  // represents the instance of the 

        /// <summary>
        /// Initialize a new <see cref="HoldBallState"/>.
        /// </summary>
        private HoldBallState()
        {
            this.Stopable = false;
        }

        /// <summary>
        /// Validate the <see cref="HoldBallState"/> to <see cref="ActionState"/>.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        /// <param name="preview">Preview <see cref="IState"/></param>
        /// <returns></returns>
        private static bool ValidateHoldBallToAction(IPlayer player, IState preview)
        {
            if (!player.Status.Hasball)
            {
                return true;
            }

            if (player.Status.NeedRedecide)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate the <see cref="HoldBallState"/> to <see cref="PassState"/>.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        /// <param name="preview">Preview <see cref="IState"/></param>
        /// <returns>bool</returns>
        private static bool ValidateHoldBallToPass(IPlayer player, IState preview)
        {
            if (!player.Status.Hasball)
            {
                return false;
            }

            if (!player.Status.NeedRedecide)
            {
                return false;
            }

            if (!player.Status.Holdball)
            {
                return false;
            }

            if (player.Input.AsPosition == Position.Goalkeeper)
            {
                return true;
            }           

            Region region = (player.Side == Side.Home) ? player.Match.Pitch.AwayForcePassRegion : player.Match.Pitch.HomeForcePassRegion;
            if (region.IsCoordinateInRegion(player.Current))
            {
                return true;
            }

            return (player.Match.RandomPercent() < player.PropCore[PlayerProperty.PassChooseRate]);
        }

        /// <summary>
        /// Validate the <see cref="HoldBallState"/> to <see cref="DribbleState"/>.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        /// <param name="preview">Preview <see cref="IState"/></param>
        /// <returns>bool</returns>
        private static bool ValidateHoldBallToDribble(IPlayer player, IState preview)
        {
            if (!player.Status.NeedRedecide)
            {
                return false;
            }

            return true;
            //return DribbleConditionFactory.Create(player.Property.Position).Decide(player, preview);
        }

        /// <summary>
        /// Validate the <see cref="HoldBallState"/> to <see cref="ShootState"/>.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        /// <param name="preview">Preview <see cref="IState"/></param>
        /// <returns>bool</returns>
        private static bool ValidateHoldBallToShoot(IPlayer player, IState preview)
        {
            // while the ball handler is no need to decide, can't to the shoot
            if (!player.Status.NeedRedecide)
            {
                return false;
            }

            var region = (player.Side == Side.Home) ? player.Match.Pitch.AwayShootRegion : player.Match.Pitch.HomeShootRegion;
            if (region.IsCoordinateInRegion(player.Current))
            {
                player.AddFinishingBuff(1);
                return true;
            }

            // while the player is not in the shooting area, can't to shoot
            if (!player.Status.ShootStatus.ShootRegion.IsCoordinateInRegion(player.Current))
            {
                return false;
            }
            
            // while the player is in the shooting area, still need a percentage to decide shoot. 1-((射门属性/400))1/4 
            return (player.Match.RandomPercent() < (1 - Math.Pow(player.PropCore[PlayerProperty.Shooting] / 400, 0.25)) * 100); 
            // return (RandomHelper.GetPercentage() < (1 - Math.Pow(player.CurrProperty[PlayerProperty.Shooting] / 400, 0.25)) * 100);            
        }

        private static bool ValidateHoldBallToStopBall(IPlayer player, IState preview)
        {
            if (player.Status.BallDistance < 2)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}