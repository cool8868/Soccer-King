/********************************************************************************
 * 文件名：DiveBallState
 * 创建人：
 * 创建时间：2009-12-20 17:18:19
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
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.AI.States.Idle;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.Base.Structs;

namespace Games.NB.Match.AI.States
{
    /// <summary>
    /// Represents the goal keeper's dive ball state.
    /// </summary>
    [Singleton]
    public sealed class DiveBallState : State
    {

        /// <summary>
        /// Initialize the current state.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(GKHoldBallState.Instance);
            this.StateCondition.Add(GKHoldBallState.Instance, ValidateDiveBallToGKHoldBall);
        }

        /// <summary>
        /// 扑救
        /// </summary>
        /// <param name="player"></param>
        public override void Action(IPlayer player)
        {
            player.DiveBall();
        }

        /// <summary>
        /// 扑救的实例对象
        /// </summary>
        public static DiveBallState Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "DiveBallState";
        }

        protected override PlayerStateReport CreateStateRpt(IPlayer player)
        {
            if (ReportAsset.RPTVerNo <= 1)
            {
                var rpt = new PlayerDiveStateReport();
                rpt.DiveDirection = player.Status.DiveStatus.DiveDirection;
                return rpt;
            }
            var rpt2 = new PlayerDiveStateReportV2();
            rpt2.DiveDirection = player.Status.DiveStatus.DiveDirection;
            return rpt2;
        }
       
        /// <summary>
        /// Decides the nex <see cref="IState"/>
        /// </summary>
        /// <param name="player">Represents the current <see cref="IPlayer"/>.</param>
        /// <param name="preview">Represents the preview <see cref="IState"/></param>
        /// <returns></returns>
        public override IState QuickDecide(IPlayer player, IState preview)
        {
            if (!player.Match.Status.IsNoBallHandler && player.Status.Holdball)
                player.Status.SubState.SetSubState(EnumSubState.KeepBall, player.Match.Status.Round + 2);
            return GKHoldBallState.Instance;
        }

        #region encapsulation

        private readonly static DiveBallState _instance = new DiveBallState();

        private DiveBallState()
        {
            this.Stopable = true;
        }

        private static bool ValidateDiveBallToGKHoldBall(IPlayer player, IState preview)
        {
            return true;
        }

        #endregion
    }
}
