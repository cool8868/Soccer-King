/********************************************************************************
 * 文件名：IdleState
 * 创建人：
 * 创建时间：2009-11-18 11:18:02
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.AI.Actions;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Common.Collections;

namespace Games.NB.Match.AI.States
{
    /// <summary>
    /// Represents the idle state.
    /// </summary>
    [Singleton]
    public class IdleState : State
    {
        /// <summary>
        /// Represents the instance of the <see cref="IdleState"/> class.
        /// </summary>
        public static IdleState Instance
        {
            get { return _state; }
        }

        /// <summary>
        /// Outputs the <see cref="IdleState"/>'s name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "IdleState";
        }

        /// <summary>
        /// Initializes the new instance of the <see cref="IdleState"/> class.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(ChaceState.Instance);
            this.StateChain.Add(ActionState.Instance);
            this.StateChain.Add(this);

            this.StateCondition.Add(this, ValidateIdleToIdle);
            this.StateCondition.Add(ChaceState.Instance, ValidateIdleToChace);
            this.StateCondition.Add(ActionState.Instance, ValidateIdleToAction);
        }

        /// <summary>
        /// Action.
        /// </summary>
        /// <param name="player"></param>
        public override void Action(IPlayer player)
        {
            Singleton<IdleAction>.Instance.Action(player);
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
                if (player.Current == player.Destination)
                {
                    return IdleState.Instance;
                }
                else
                {
                    return ChaceState.Instance;
                }
            }
        }

        #region encapsulation

        private static readonly IdleState _state = new IdleState();

        /// <summary>
        /// Initializes the IdleState.
        /// </summary>
        protected IdleState()
        {
            this.Stopable = true;
        }

        private static bool ValidateIdleToIdle(IPlayer player, IState preview)
        {
            if (player.Status.NeedRedecide)
            {
                return false;
            }

            return player.Current == player.Destination;
        }

        private static bool ValidateIdleToChace(IPlayer player, IState preview)
        {
            if (player.Status.NeedRedecide)
            {
                return false;
            }

            return player.Current != player.Destination;
        }

        private static bool ValidateIdleToAction(IPlayer player, IState preview)
        {
            return player.Status.NeedRedecide;
        }

        #endregion
    }
}