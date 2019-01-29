/********************************************************************************
 * 文件名：OffBallState
 * 创建人：
 * 创建时间：2009-11-18 13:38:17
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the player dosen't have football state.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Common.Random;
using SkillEngine.SkillBase.Enum.Football;

namespace Games.NB.Match.AI.States
{
    /// <summary>
    /// Represents the player who dosen't have the ball.
    /// This class implemented the Singleton pattern.
    /// </summary>
    /// <example>OffBallState.Instance</example>
    [Singleton]
    public class OffBallState : State
    {
        /// <summary>
        /// Initialize current state.
        /// </summary>
        public override void Initialize()
        {
            // initialize the state chain.
            this.StateChain.Add(StopballState.Instance);
            this.StateChain.Add(DiveBallState.Instance);
            this.StateChain.Add(DefenceState.Instance);
            this.StateChain.Add(ActionState.Instance);
            this.StateChain.Add(PositionalState.Instance);

            // initialize the state conditions.
            this.StateCondition.Add(PositionalState.Instance, ValidateOffBallToPositional);
            this.StateCondition.Add(ActionState.Instance, ValidateOffBallToAction);
            this.StateCondition.Add(StopballState.Instance, ValidateOffBallToStopball);
            this.StateCondition.Add(DiveBallState.Instance, ValidateOffBallToDiveBall);
            this.StateCondition.Add(DefenceState.Instance, ValidateOffBallToDefence);
        }

        /// <summary>
        /// Return the <see cref="OffBallState"/>'s name.
        /// </summary>
        /// <returns>state name</returns>
        public override string ToString()
        {
            return "OffBallState";
        }

        /// <summary>
        /// Represents the instance of the <see cref="OffBallState"/>.
        /// </summary>
        public static OffBallState Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Decides the nex <see cref="IState"/>
        /// </summary>
        /// <param name="player">Represents the current <see cref="IPlayer"/>.</param>
        /// <param name="preview">Represents the preview <see cref="IState"/></param>
        /// <returns></returns>
        public override IState QuickDecide(IPlayer player, IState preview)
        {
            if (player.Status.Hasball)
            {
                player.Redecide();
                return HoldBallState.Instance;
            }
            else
            {
                IPlayer ballHandler = player.Match.Status.BallHandler;
                if (ballHandler == null)
                {
                    return PositionalState.Instance;
                }
                if (player.Input.AsPosition == Position.Goalkeeper) // 门将
                {
                    #region 扑救状态
                    if (ballHandler.Status.State is Shoot.RebelShootState)
                    {
                        if (player.Match.Status.BallHandler.Side == player.Side)
                            return DiveBallState.Instance;
                    }
                    else if (ballHandler.Status.State is ShootState)
                    {
                        if (player.Match.Status.BallHandler.Side != player.Side)
                            return DiveBallState.Instance;
                    }
                    #endregion
                }
                if (player.Status.IsAttackSide)
                {
                    return PositionalState.Instance;
                }
                if (null == player.Status.DefenceStatus.DefenceTarget 
                    || ballHandler.Status.State is ShootState || ballHandler.Status.State is PassState)
                    return PositionalState.Instance;
                bool airFlag = player.Match.Football.IsInAir;
                if (airFlag)
                {
                    if (player.PropCore.CanHeading(ballHandler))
                        return Defence.HeadingDuelState.Instance;
                }
                else if (player.PropCore.CanSteal())
                {
                    double stealRate = !ballHandler.SkillEnable ? 100 : player.PropCore.StealChooseRate();
                    if (stealRate < 100)
                    {
                        double maxStealRate = 0;
                        double dif = player.Status.BallDistancePow;
                        if (dif <= 2)
                            maxStealRate = 100;
                        else if (dif <= 25)
                            maxStealRate = 30;
                        else
                            maxStealRate = 10;
                        stealRate = Math.Max(maxStealRate, stealRate);
                    }
                    if (stealRate >= 100 || player.Match.RandomPercent() < stealRate)
                    {
                        return DefenceState.Instance;
                    }
                }
                return PositionalState.Instance;
            }
        }

        
        #region encapsulation

        private static readonly OffBallState _instance = new OffBallState();    // internal instance

        /// <summary>
        /// Initialize the <see cref="OffBallState"/>.
        /// </summary>
        private OffBallState()
        {
            this.Stopable = false;
        }

        /// <summary>
        /// Validate the <see cref="OffBallState"/> to <see cref="PositionalState"/>.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        /// <param name="preview">Preview <see cref="IState"/></param>
        /// <returns></returns>
        private static bool ValidateOffBallToPositional(IPlayer player, IState preview)
        {
            // while any other state can't to enter, so the PositionalState is the default one.
            return true;
        }

        /// <summary>
        /// Validate the <see cref="OffBallState"/> to <see cref="ActionState"/>.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        /// <param name="preview">Preview <see cref="IState"/></param>
        /// <returns>bool</returns>
        private static bool ValidateOffBallToAction(IPlayer player, IState preview)
        {
            if (player.Status.Hasball)
            {
                player.Redecide();
                return true;
            }

            return player.Status.NeedRedecide == false;
        }

        /// <summary>
        /// Validate the <see cref="OffBallState"/> to <see cref="StopballState"/>.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        /// <param name="preview">Preview <see cref="IState"/></param>
        /// <returns>bool</returns>
        private static bool ValidateOffBallToStopball(IPlayer player, IState preview)
        {
            return false;

            //// goal keeper can't to stop ball.
            //if (player.Input.AsPosition == Position.Goalkeeper) {
            //    return false;
            //}

            //if (player.Status.Hasball) {
            //    return false;
            //}

            //if (!player.Status.NeedRedecide) {
            //    return false;
            //}

            //if (!player.Status.IsAttackSide) {
            //    return false;
            //}

            //return player.Status.BallDistance < 2;
        }

        /// <summary>
        /// Validate <see cref="OffBallState"/> to <see cref="DiveBallState"/>.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        /// <param name="preview">Preview <see cref="IState"/></param>
        /// <returns>bool</returns>
        private static bool ValidateOffBallToDiveBall(IPlayer player, IState preview)
        {
            if (player.Input.AsPosition != Position.Goalkeeper)
            { // only the goal keeper can dive ball
                return false;
            }

            if (player.Match.Status.BallHandler == null)
            { // no ball handler can't dive
                return false;
            }

            // while the ball handler not make the shoot action, the goal keeper will not dive the ball.
            if (!(player.Match.Status.BallHandler.Status.State is ShootState))
            {
                return false;
            }

            // while the shooter is our side, it's also not necessary.
            if (player.Match.Status.BallHandler.Side == player.Side)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate the <see cref="OffBallState"/> to <see cref="DefenceState"/>.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        /// <param name="preview">Preview <see cref="IState"/></param>
        /// <returns>bool</returns>
        private static bool ValidateOffBallToDefence(IPlayer player, IState preview)
        {
            // goal keeper can't to make a defence, because the goal keeper can't interrup and slide tackle.
            if (player.Input.AsPosition == Position.Goalkeeper)
            {
                return false;
            }

            if (player.Status.Hasball)
            {
                return false;
            }

            if (!player.Status.NeedRedecide)
            {
                return false;
            }

            if (player.Status.IsAttackSide)
            {
                return false;
            }

            if (player.Status.DefenceStatus.DefenceTarget == null)
            {
                return false;
            }

            if (player.Match.Status.BallHandler.Status.State is ShootState ||
                player.Match.Status.BallHandler.Status.State is PassState)
            {
                return false;
            }

            if (player.Match.Football.IsInAir)
            {
                return false;
            }

            int ballDistance = player.Status.BallDistance; // 球距离
            if (ballDistance > Defines.Player.DEFENCE_RANGE)
            { // 如果离球过远不判定         
                return false;
            }

            //// 当持球人离自己的球过远（比如传球中途可以断球）
            //if (!player.Match.Status.BallHandler.Status.Holdball) // while the ball handler is not hold ball.
            //{ 
            //    if (player.Status.BallDistance <= 2)
            //    {
            //        return true;
            //    }
            //}

            // 起脚率
            double probability = Math.Pow(player.PropCore[PlayerProperty.Aggression] / 100, 2) * 100 * 0.05;
            if (probability > 20)
            {
                probability = 20;
            }

            return player.Match.RandomPercent() < probability;
        }

        #endregion
    }
}