/********************************************************************************
 * 文件名：ShootState
 * 创建人：
 * 创建时间：2009-11-18 13:47:09
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.AI.States.Shoot;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Common.Random;
using Games.NB.Match.Base.Structs;

namespace Games.NB.Match.AI.States
{
    /// <summary>
    /// Represents the shoot action state.
    /// </summary>
    [Singleton]
    public class ShootState : State
    {
        /// <summary>
        /// Initializes the <see cref="ShootState"/>.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(VolleyShootState.Instance);
            this.StateChain.Add(DefaultShootState.Instance);
            this.StateChain.Add(HoldBallState.Instance);

            this.StateCondition.Add(VolleyShootState.Instance, ValidateShootToVolleyShoot);
            this.StateCondition.Add(DefaultShootState.Instance, ValidateShootToDefaultShoot);
            this.StateCondition.Add(HoldBallState.Instance, ValidateShootToHoldBall);
        }

        /// <summary>
        /// Enters the <see cref="ShootState"/>
        /// </summary>
        /// <param name="player"></param>
        public override void Enter(IPlayer player)
        {
            player.Status.DecideEnd();
        }

        protected override PlayerStateReport CreateStateRpt(IPlayer player)
        {
            if (ReportAsset.RPTVerNo <= 1)
            {
                var rpt = new PlayerShootStateReport();
                rpt.GoalIndex = player.Status.ShootStatus.ShootTargetIndex;
                rpt.GoalX = player.Status.ShootStatus.ShootTarget.X;
                rpt.GoalY = player.Status.ShootStatus.ShootTarget.Y;
                return rpt;
            }
            var rpt2 = new PlayerShootStateReportV2();
            rpt2.GoalIndex = player.Status.ShootStatus.ShootTargetIndex;
            rpt2.GoalX = player.Status.ShootStatus.ShootTarget.X;
            rpt2.GoalY = player.Status.ShootStatus.ShootTarget.Y;
            rpt2.SuccFlag = player.Status.ShootStatus.SuccFlag > 0 ? 1 : 0;
            rpt2.RawSuccRate = player.Status.ShootStatus.RawSuccRate;
            rpt2.NewSuccRate = player.Status.ShootStatus.NewSuccRate;
            return rpt2;
        }
      
        /// <summary>
        /// Outputs the <see cref="ShootState"/>'s name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ShootState";
        }

        /// <summary>
        /// Represents the instance of the <see cref="ShootState"/>.
        /// </summary>
        public static ShootState Instance
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

            if (!player.Status.BallDistanceZero)
            {
                return HoldBallState.Instance;
            }

            this.CheckSubState(player);
            double strength=player.PropCore[PlayerProperty.Strength];
            if (strength > 100)
            {
                if (player.Match.RandomPercent() < 0.2 * strength)
                {
                    return VolleyShootState.Instance;
                }
            }
            return DefaultShootState.Instance;
        }

        #region 倒挂金钩
        protected void CheckSubState(IPlayer player)
        {
            if (null == player.Status.PassStatus.PassFrom
                || !player.Match.Football.IsInAir)
                return;
            int round = player.Match.Status.Round;
            var subState = player.Status.SubState;
            if (subState.GetSubState(round) != EnumSubState.LongPassAccepted)
                return;
            subState.SetSubState(EnumSubState.BicycleShot, round);
            //const double dist = 16;
            //var defs = player.Status.DefenceStatus.Defenders;
            //if (null == defs)
            //    defs = player.Manager.Opponent.Players;
            //bool hitFlag = true;
            //foreach (var def in defs)
            //{
            //    if (player.Current.SimpleDistance(def.Current) < dist)
            //    {
            //        hitFlag = false;
            //        break;
            //    }
            //}
            //if (hitFlag)
            //    subState.SetSubState(EnumSubState.BicycleShot, round);
        }
        #endregion

        #region encapsulation

        private static readonly ShootState _instance = new ShootState();

        /// <summary>
        /// Initializes a new instance of the <see cref="ShootState"/>.
        /// </summary>
        protected ShootState()
        {
            this.Stopable = false;
        }

        private static bool ValidateShootToVolleyShoot(IPlayer player, IState state)
        {
            double strength=player.PropCore[PlayerProperty.Strength];
            if (strength < 100)
            {
                return false;
            }

            return player.Match.RandomPercent() < 0.2 * strength;
        }

        private static bool ValidateShootToDefaultShoot(IPlayer player, IState state)
        {
            if (!player.Status.Hasball)
            {
                return false;
            }

            if (player.Status.BallDistance > 0)
            {
                return false;
            }

            return true;
        }

        private static bool ValidateShootToHoldBall(IPlayer player, IState state)
        {
            if (!player.Status.Hasball)
            {
                return true;
            }

            if (player.Status.BallDistance > 0)
            {
                return true;
            }

            return !player.Status.NeedRedecide;
        }

        #endregion

    }
}