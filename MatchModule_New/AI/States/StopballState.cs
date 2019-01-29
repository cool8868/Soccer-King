/********************************************************************************
 * 文件名：StopballState
 * 创建人：
 * 创建时间：2009-12-10 13:10:51
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the stopball state.
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
    /// Represents the stopball state.
    /// This class implemented the Singleton pattern.
    /// </summary>
    /// <example>StopballState.Instance</example>
    [Singleton]
    public class StopballState : State
    {

        /// <summary>
        /// Initialize the state chain and state condition.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(HoldBallState.Instance);

            this.StateCondition.Add(HoldBallState.Instance, ValidateStopballToHoldBall);
        }

        /// <summary>
        /// Represents the instance of the <see cref="StopballState"/>.
        /// </summary>
        public static StopballState Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "StopballState";
        }

        /// <summary>
        /// Stopball Action
        /// </summary>
        /// <param name="player"></param>
        public override void Action(IPlayer player)
        {
            Singleton<StopballAction>.Instance.Action(player);
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

        private static readonly StopballState _instance = new StopballState();

        /// <summary>
        /// Avoid to create the instance of the <see cref="StopballState"/>.
        /// </summary>
        private StopballState()
        {
            this.Stopable = true;
        }

        private static bool ValidateStopballToHoldBall(IPlayer player, IState preview)
        {
            return true;
        }

        #endregion
    }

  
}
