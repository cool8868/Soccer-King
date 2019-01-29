/********************************************************************************
 * 文件名：DribbleState
 * 创建人：
 * 创建时间：2009-12-4 10:10:20
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.AI.Decides;
using Games.NB.Match.AI.States.Dribble;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.States
{
    /// <summary>
    /// 带球状态
    /// </summary>
    [Singleton]
    public class DribbleState : State
    {
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "DribbleState";
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(HoldBallState.Instance);
            this.StateChain.Add(DefaultDribbleState.Instance);

            this.StateCondition.Add(DefaultDribbleState.Instance, ValidateDribbleToDefaultDribble);
            this.StateCondition.Add(HoldBallState.Instance, ValidateDribbleToHoldBall);
        }

        /// <summary>
        /// Player to make a action.
        /// </summary>
        /// <param name="player"></param>
        public override void Action(IPlayer player)
        {
            player.DribbleBall();
        }

        /// <summary>
        /// Enters the <see cref="DribbleState"/>.
        /// </summary>
        /// <param name="player"></param>
        public override void Enter(IPlayer player)
        {
            player.Status.DecideEnd();
            player.SetTarget(HoldBallPositionalDecide.GetTarget(player));
        }

        /// <summary>
        /// Represents the instance of the <see cref="DribbleState"/> class.
        /// </summary>
        public static DribbleState Instance
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
            if (player.Status.Hasball == false)
            {
                return OffBallState.Instance;
            }
            else
            {
                if (player.Status.Holdball == false)
                {
                    return HoldBallState.Instance;
                }
                else
                {
                    return DefaultDribbleState.Instance;
                }
            }
        }

        #region encapsulation

        private static readonly DribbleState _instance = new DribbleState();

        /// <summary>
        /// Initializes a new instance of the <see cref="DribbleState"/>.
        /// </summary>
        protected DribbleState()
        {
            this.Stopable = false;
        }

        private static bool ValidateDribbleToDefaultDribble(IPlayer player, IState preview)
        {
            if (player.Status.BallDistance > 0)
            {
                return false;
            }

            if (preview == DefaultDribbleState.Instance)
            {
                return false;
            }

            return true;
        }

        private static bool ValidateDribbleToHoldBall(IPlayer player, IState preview)
        {
            if (!player.Status.Hasball) return true;

            if (preview == DefaultDribbleState.Instance)
            {
                player.Redecide();
                return true;
            }

            return false;
        }

        #endregion
    }
}