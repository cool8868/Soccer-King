/********************************************************************************
 * 文件名：GKHoldBallState
 * 创建人：
 * 创建时间：4/24/2010 8:16:48 PM
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.States.Idle
{

    /// <summary>
    /// Represents the goalkeeper hold ball state.
    /// 表示了守门员持球不动的动作
    /// </summary>
    [Singleton]
    public class GKHoldBallState : IdleState
    {

        /// <summary>
        /// Represents the instance of the <see cref="GKHoldBallState"/> instance.
        /// 表示了<see cref="GKHoldBallState"/>的对象实例
        /// </summary>
        public new static GKHoldBallState Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Initializes the data of the <see cref="GKHoldBallState"/> class.
        /// 初始化<see cref="GKHoldBallState"/>类的数据
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(ActionState.Instance);

            this.StateCondition.Add(ActionState.Instance, ValidateGKHoldToAction);
        }

        /// <summary>
        /// Outputs the <see cref="GKHoldBallState"/>'s name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "GKHoldBallState";
        }

        /// <summary>
        /// Decides the nex <see cref="IState"/>
        /// </summary>
        /// <param name="player">Represents the current <see cref="IPlayer"/>.</param>
        /// <param name="preview">Represents the preview <see cref="IState"/></param>
        /// <returns></returns>
        public override IState QuickDecide(IPlayer player, IState preview)
        {
            return ActionState.Instance;
        }

        #region encapsulation

        private static readonly GKHoldBallState _instance = new GKHoldBallState();

        /// <summary>
        /// 初始化守门员持球状态
        /// </summary>
        protected GKHoldBallState()
        {
            this.TimeLast = 2;
        }

        private static bool ValidateGKHoldToAction(IPlayer player, IState preview)
        {
            return true;
        }

        #endregion
    }
}
