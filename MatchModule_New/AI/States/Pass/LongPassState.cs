/********************************************************************************
 * 文件名：LongPassState
 * 创建人：
 * 创建时间：2009-12-16 17:47:35
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

namespace Games.NB.Match.AI.States.Pass
{
    /// <summary>
    /// Represents the long pass action.
    /// </summary>
    [Singleton]
    public class LongPassState : PassState
    {
        /// <summary>
        /// Initializes the new instance of the <see cref="LongPassState"/> class.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(PassState.Instance);

            this.StateCondition.Add(PassState.Instance, ValidateLongPassToPass);
        }

        /// <summary>
        /// Represents the instance of the <see cref="LongPassState"/> class.
        /// </summary>
        public new static LongPassState Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// LongPass Action.
        /// </summary>
        /// <param name="player"></param>
        public override void Action(IPlayer player)
        {
            player.LongPass();
        }

        /// <summary>
        /// Outputs the <see cref="LongPassState"/>'s name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "LongPassState";
        }

        /// <summary>
        /// Enters the <see cref="LongPassState"/>.
        /// </summary>
        /// <param name="player"></param>
        public override void Enter(IPlayer player)
        {
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

        private static readonly LongPassState _instance = new LongPassState();

        private LongPassState()
        {
            this.Stopable = true;
        }

        private static bool ValidateLongPassToPass(IPlayer player, IState preview)
        {
            return true;
        }

        #endregion
    }
}
