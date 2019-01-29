/********************************************************************************
 * 文件名：ActionState
 * 创建人：
 * 创建时间：2009-11-18 13:28:35
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.States
{
    /// <summary>
    /// Represents the action state.
    /// 行动状态
    /// </summary>
    [Singleton]
    public class ActionState : State
    {
        /// <summary>
        /// Represents the instance of the <see cref="ActionState"/> class.
        /// </summary>
        public static ActionState Instance { get { return _instance; } }

        /// <summary>
        /// Initializes the new instance of the <see cref="ActionState"/> class.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(IdleState.Instance);
            this.StateChain.Add(ChaceState.Instance);
            this.StateChain.Add(HoldBallState.Instance);
            this.StateChain.Add(OffBallState.Instance);

            this.StateCondition.Add(IdleState.Instance, ValidateActionToIdle);
            this.StateCondition.Add(ChaceState.Instance, ValidateActionToChace);
            this.StateCondition.Add(HoldBallState.Instance, ValidateActionToHoldBall);
            this.StateCondition.Add(OffBallState.Instance, ValidateActionToOffBall);
        }

        public override void Enter(IPlayer player)
        {
            if (player.Input.AsPosition == Base.Enum.Position.Goalkeeper)
            {
                var distance = player.Current.SimpleDistance(player.Match.Football.Current);

                if (distance <= 4)
                {
                    player.Status.Hasball = true;
                }
            }
        }

        /// <summary>
        /// Overrides the ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ActionState";
        }

        /// <summary>
        /// Decides the nex <see cref="IState"/>
        /// </summary>
        /// <param name="player">Represents the current <see cref="IPlayer"/>.</param>
        /// <param name="preview">Represents the preview <see cref="IState"/></param>
        /// <returns></returns>
        public override IState QuickDecide(IPlayer player, IState preview)
        {
            if (player.Status.NeedRedecide)
            {
                if (player.Status.Hasball)
                {
                    return HoldBallState.Instance;
                }
                else
                {
                    return OffBallState.Instance;
                }
            }
            else
            {
                if (player.Current != player.Destination)
                {
                    return ChaceState.Instance;
                }
                else
                {
                    return IdleState.Instance;
                }
            }
        }

        #region encapsulation

        private static readonly ActionState _instance = new ActionState();

        private ActionState()
        {
            this.Stopable = false;
        }

        private static bool ValidateActionToIdle(IPlayer player, IState preview)
        {
            if (player.Status.NeedRedecide)
            {
                return false;
            }

            return player.Current == player.Destination;
        }

        private static bool ValidateActionToChace(IPlayer player, IState preview)
        {
            if (player.Status.NeedRedecide)
            {
                return false;
            }

            return player.Current != player.Destination;
        }

        private static bool ValidateActionToHoldBall(IPlayer player, IState preview)
        {
            if (!player.Status.NeedRedecide)
            {
                return false;
            }

            return player.Status.Hasball;
        }

        private static bool ValidateActionToOffBall(IPlayer player, IState preview)
        {
            if (!player.Status.NeedRedecide)
            {
                return false;
            }

            return !player.Status.Hasball;
        }

        #endregion
    }
}