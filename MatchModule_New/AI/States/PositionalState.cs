/********************************************************************************
 * 文件名：PositionalState
 * 创建人：
 * 创建时间：2009-11-19 14:41:09
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.AI.Decides.Factory;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.States
{
    /// <summary>
    /// Represents the Positional State.
    /// 表示了寻位状态
    /// </summary>
    [Singleton]
    public class PositionalState : State
    {
        /// <summary>
        /// Outputs the <see cref="PositionalState"/>'s name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "PositionalState";
        }

        /// <summary>
        /// Initializes the <see cref="PositionalState"/>.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(OffBallState.Instance);

            this.StateCondition.Add(OffBallState.Instance, ValidatePositionalToOffBall);
        }

        /// <summary>
        /// Enters the <see cref="PositionalState"/>.
        /// </summary>
        /// <param name="player"></param>
        public override void Enter(IPlayer player)
        {
            player.SetTarget(PositionalDecideFactory.Create(player.Input.AsPosition).DecideTarget(player));
        }

        /// <summary>
        /// Exits the <see cref="PositionalState"/>.
        /// </summary>
        /// <param name="player"></param>
        public override void Exit(IPlayer player)
        {
            player.Status.DecideEnd();
        }

        /// <summary>
        /// Represents the instance of the <see cref="PositionalState"/>.
        /// </summary>
        public static PositionalState Instance
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
            return ChaceState.Instance;
        }

        #region encapsulation

        private static readonly PositionalState _instance = new PositionalState();

        private PositionalState()
        {
            this.Stopable = false;
        }

        private static bool ValidatePositionalToOffBall(IPlayer player, IState preview)
        {
            return true;
        }

        #endregion
    }
}
