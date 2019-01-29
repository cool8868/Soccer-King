/********************************************************************************
 * 文件名：SlideTackleState
 * 创建人：
 * 创建时间：2009-12-30 16:03:19
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

namespace Games.NB.Match.AI.States.Defence
{
    /// <summary>
    /// Represents the slide tackle state.
    /// </summary>
    [Singleton]
    public sealed class SlideTackleState : DefenceState
    {
        /// <summary>
        /// Initialize current <see cref="IState"/>.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(DefenceState.Instance);
            this.StateCondition.Add(DefenceState.Instance, ValidateSlideTackleToDefence);
        }

        /// <summary>
        /// Outputs the <see cref="SlideTackleState"/>'s name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "SlideTackleState";
        }

        /// <summary>
        /// Represents the instance of the <see cref="SlideTackleState"/>.
        /// </summary>
        public new static SlideTackleState Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Enters the <see cref="SlideTackleState"/>.
        /// </summary>
        /// <param name="player"></param>
        public override void Enter(IPlayer player)
        {
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <param name="player"></param>
        public override void Action(IPlayer player)
        {
            player.SlideTackleBall();
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

        private static readonly SlideTackleState _instance = new SlideTackleState();

        private SlideTackleState()
        {
            this.Stopable = true;
        }

        private static bool ValidateSlideTackleToDefence(IPlayer player, IState preview)
        {
            return true;
        }

        #endregion
    }
}
