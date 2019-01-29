/********************************************************************************
 * 文件名：InterruptionState
 * 创建人：
 * 创建时间：2009-12-30 13:14:09
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.States.Defence
{
    /// <summary>
    /// Represents the interruption state.
    /// </summary>
    [Singleton]
    public sealed class InterruptionState : DefenceState
    {

        /// <summary>
        /// Initialize current <see cref="IState"/>.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(DefenceState.Instance);
            this.StateCondition.Add(DefenceState.Instance, ValidateInterruptionToDefence);
        }

        /// <summary>
        /// Return the <see cref="IState"/>'s name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "InterruptionState";
        }

        /// <summary>
        /// Represents the instance of current singleton class.
        /// </summary>
        public new static InterruptionState Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Enters the <see cref="InterruptionState"/>.
        /// </summary>
        /// <param name="player"></param>
        public override void Enter(IPlayer player)
        {
        }

        /// <summary>
        /// Player to interrupt ball.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        public override void Action(IPlayer player)
        {
            player.InterruptionBall();
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
                return HoldBallState.Instance;
            }
            else
            {
                return OffBallState.Instance;
            }
        }

        #region encapsulation

        private static readonly InterruptionState _instance = new InterruptionState();

        private InterruptionState()
        {
            this.Stopable = true;
        }

        private static bool ValidateInterruptionToDefence(IPlayer player, IState preview)
        {
            return true;
        }

        #endregion
    }
}
