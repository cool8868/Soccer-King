/********************************************************************************
 * 文件名：PassState
 * 创建人：
 * 创建时间：2009-11-18 13:49:16
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.AI.States.Pass;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Common.Random;
using Games.NB.Match.Base.Enum;

namespace Games.NB.Match.AI.States
{
    /// <summary>
    /// Represents the PassState.
    /// </summary>
    [Singleton]
    public class PassState : State
    {
        /// <summary>
        /// Initializes the new instance of the <see cref="PassState"/> class.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(LongPassState.Instance);
            this.StateChain.Add(ShortPassState.Instance);
            this.StateChain.Add(HoldBallState.Instance);

            this.StateCondition.Add(HoldBallState.Instance, ValidatePassToHoldBall);
            this.StateCondition.Add(ShortPassState.Instance, ValidatePassToShortPass);
            this.StateCondition.Add(LongPassState.Instance, ValidatePassToLongPass);
        }

        /// <summary>
        /// Enters the <see cref="PassState"/> class.
        /// </summary>
        /// <param name="player"></param>
        public override void Enter(IPlayer player)
        {
            if (player.Status.Hasball)
            {
                player.DecideEnd();
                player.DecidePassTarget();
            }
        }

        /// <summary>
        /// Outputs the <see cref="PassState"/>'s name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "PassState";
        }

        /// <summary>
        /// Represents the instance of the <see cref="PassState"/> class.
        /// </summary>
        public static PassState Instance
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
            var match = player.Match;
            if (player.Status.Hasball == false)
            {
                return OffBallState.Instance;
            }
            else
            {
                if (!player.Status.Holdball)
                {
                    return HoldBallState.Instance;
                }
                if (player.Status.PassStatus.PassTarget == null)
                {
                    return DribbleState.Instance;
                }
                else
                {
                    IPlayer target = player.Status.PassStatus.PassTarget;

                    if (player.Current.SimpleDistance(player.Status.PassStatus.PassTarget.Current) <= Defines.Player.SHORT_PASS_MAX_RANGEPow)
                    {
                        this.CheckSubState(player);
                        return ShortPassState.Instance;
                    }

                    if (match.RandomPercent() < Defines.Player.LONG_PASS_PERCENTAGE) // 长传概率
                    {
                        return LongPassState.Instance;
                    }
                    else
                    {
                        return ShortPassState.Instance;
                    }
                }
            }
        }

        #region 头球传球
        protected void CheckSubState(IPlayer player)
        {
            if (null == player.Status.PassStatus.PassFrom
                || !player.Match.Football.IsInAir)
                return;
            int round = player.Match.Status.Round;
            var subState = player.Status.SubState;
            if (subState.GetSubState(round) != EnumSubState.LongPassAccepted)
                return;
            subState.SetSubState(EnumSubState.HeadPass, round);
        }
        #endregion

        #region encapsulation

        private static readonly PassState _instance = new PassState();

        /// <summary>
        /// Initializes the new instance of the <see cref="PassState"/>.
        /// </summary>
        protected PassState()
        {
            this.Stopable = false;
        }

        private static bool ValidatePassToHoldBall(IPlayer player, IState preview)
        {
            return true;
        }

        private static bool ValidatePassToShortPass(IPlayer player, IState preview)
        {
            if (!player.Status.Hasball)
            {
                return false;
            }

            if (player.Status.PassStatus.PassTarget == null)
            {
                player.Redecide();
                return false;
            }

            if (!player.Status.Holdball)
            {
                return false;
            }

            return true;
        }

        private static bool ValidatePassToLongPass(IPlayer player, IState preview)
        {
            if (!player.Status.Hasball)
            {
                return false;
            }

            if (player.Status.PassStatus.PassTarget == null)
            {
                player.Redecide();
                return false;
            }

            if (!player.Status.Holdball)
            {
                return false;
            }

            if (player.Status.PassStatus.PassTarget.Input.AsPosition == Base.Enum.Position.Goalkeeper ||
                player.Status.PassStatus.PassTarget.Input.AsPosition == Base.Enum.Position.Fullback)
            {
                return false;
            }

            if (player.Current.SimpleDistance(player.Status.PassStatus.PassTarget.Current) <= 2500)
            {
                return false;
            }

            return player.Match.RandomPercent() < Defines.Player.LONG_PASS_PERCENTAGE;
        }

        #endregion
    }
}