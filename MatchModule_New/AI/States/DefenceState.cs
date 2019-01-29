/********************************************************************************
 * 文件名：DefenceState
 * 创建人：
 * 创建时间：2009-12-30 13:26:52
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.AI.States.Defence;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Common.Random;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Model.TranOut;

namespace Games.NB.Match.AI.States
{
    /// <summary>
    /// Represents the defence state class.
    /// 表示了防守状态
    /// </summary>
    [Singleton]
    public class DefenceState : State
    {
        /// <summary>
        /// Initialize current <see cref="State"/>.        
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(SlideTackleState.Instance);
            this.StateChain.Add(InterruptionState.Instance);
            this.StateChain.Add(OffBallState.Instance);

            this.StateCondition.Add(InterruptionState.Instance, ValidateDefenceToInterruption);
            this.StateCondition.Add(OffBallState.Instance, ValidateDefenceToOffBall);
            this.StateCondition.Add(SlideTackleState.Instance, ValidateDefenceToSlideTackle);
        }

        /// <summary>
        /// Enters the <see cref="DefenceState"/> clas.
        /// </summary>
        /// <param name="player"></param>
        public override void Enter(IPlayer player)
        {
            player.Status.DecideEnd();
        }

        /// <summary>
        /// Outputs the <see cref="State"/>'s name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "DefenceState";
        }

        /// <summary>
        /// Represents the instance of the <see cref="DefenceState"/> class.
        /// </summary>
        public static DefenceState Instance
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
            if (player.Status.Hasball)
            {
                return HoldBallState.Instance;
            }
            else
            {
                if (preview is DefenceState)
                {
                    return OffBallState.Instance;
                }
                else
                {
                    if (player.Match.RandomPercent() < 50)
                    {
                        return SlideTackleState.Instance;
                    }
                    else
                    {
                        return InterruptionState.Instance;
                    }
                }
            }
        }
        protected override PlayerStateReport CreateStateRpt(IPlayer player)
        {
            if (ReportAsset.RPTVerNo <= 1
                || this is Defence.HeadingDuelState
                || player.Status.DefenceStatus.SuccFlag < 0)
            {
                return base.CreateStateRpt(player);
            }
            var rpt2 = new PlayerStealStateReportV2();
            rpt2.SuccFlag = player.Status.DefenceStatus.SuccFlag > 0 ? 1 : 0;
            rpt2.RawSuccRate = player.Status.DefenceStatus.RawSuccRate;
            rpt2.NewSuccRate = player.Status.DefenceStatus.NewSuccRate;
            return rpt2;
        }

        #region encapsulation

        private static readonly DefenceState _instance = new DefenceState();

        /// <summary>
        /// 防守状态
        /// </summary>
        protected DefenceState()
        {
            this.Stopable = false;
        }

        private static bool ValidateDefenceToInterruption(IPlayer player, IState preview)
        {
            if (player.Status.Hasball)
            {
                return false;
            }

            if (preview == SlideTackleState.Instance || preview == InterruptionState.Instance)
            {
                return false;
            }

            return true;
        }

        private static bool ValidateDefenceToOffBall(IPlayer player, IState preview)
        {
            return true;
        }

        private static bool ValidateDefenceToSlideTackle(IPlayer player, IState preview)
        {
            if (player.Status.Hasball)
            {
                return false;
            }

            if (preview == SlideTackleState.Instance || preview == InterruptionState.Instance)
            {
                return false;
            }

            return player.Match.RandomPercent() < 50;
        }

        #endregion
    }

}
