using System;
using Games.NB.Match.AI.Actions;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.States
{
    /// <summary>
    /// 跑动状态
    /// </summary>
    [Singleton]
    public class ChaceState : State
    {
        /// <summary>
        /// Represents the instance of the <see cref="ChaceState"/> class.
        /// </summary>
        public static ChaceState Instance
        {
            get { return _state; }
        }

        /// <summary>
        /// Outputs the ChaceState's name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ChaceState";
        }

        /// <summary>
        /// Initializes the new instance of the <see cref="ChaceState"/> class.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(this);
            this.StateChain.Add(IdleState.Instance);
            this.StateChain.Add(ActionState.Instance);

            this.StateCondition.Add(ActionState.Instance, ValidateChaceToAction);
            this.StateCondition.Add(this, ValidateChaceToChace);
            this.StateCondition.Add(IdleState.Instance, ValidateChaceToIdle);
        }

        /// <summary>
        /// Chace State action.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        public override void Action(IPlayer player)
        {
            ChaceAction.Instance.Action(player);
        }

        /// <summary>
        /// Decides the nex <see cref="IState"/>
        /// </summary>
        /// <param name="player">Represents the current <see cref="IPlayer"/>.</param>
        /// <param name="preview">Represents the preview <see cref="IState"/></param>
        /// <returns></returns>
        public override IState QuickDecide(IPlayer player, IState preview)
        {
            if (player.Status.NeedRedecide)
            {
                if (player.Status.IsAttackSide == false) // 防守方
                {
                    if (player.Input.AsPosition == Base.Enum.Position.Goalkeeper)
                    {
                        return OffBallState.Instance;
                    }

                    if (player.Status.DefenceStatus.DefenceTarget == null) // 没有防守对象
                    {
                        return PositionalState.Instance;
                    }
                }
                else // 进攻方
                {
                    if (player.Status.Hasball == false)
                    {
                        var ballHandler=player.Match.Status.BallHandler;
                        if (player.Input.AsPosition == Base.Enum.Position.Goalkeeper
                            && null != ballHandler
                            && ballHandler.Status.State is Shoot.RebelShootState)
                            return OffBallState.Instance;
                        return PositionalState.Instance;
                    }
                }
                return ActionState.Instance;
            }
            if (player.Status.DestinationDistanceZero)
                return IdleState.Instance;
            var playerPos = player.Status.Current;
            if (!player.Status.ActiveRegion.IsCoordinateInRegion(playerPos))
                return ChaceState.Instance;
            var ballPos = player.Match.Football.Current;
            if (Math.Abs(playerPos.X - ballPos.X) <= 40 || Math.Abs(playerPos.Y - ballPos.Y) <= 30)
                return ChaceState.Instance;
            return WalkState.Instance;
        }

        #region encapsulation

        private static readonly ChaceState _state = new ChaceState();

        private ChaceState()
        {
            this.Stopable = true;
        }

        private static bool ValidateChaceToAction(IPlayer player, IState preview)
        {
            return player.Status.NeedRedecide;
        }

        private static bool ValidateChaceToChace(IPlayer player, IState preview)
        {
            if (player.Status.NeedRedecide)
            {
                return false;
            }

            if (player.Status.DestinationDistance == 0)
            {
                return false;
            }

            return true;
        }

        private static bool ValidateChaceToIdle(IPlayer player, IState preview)
        {
            if (player.Status.NeedRedecide)
            {
                return false;
            }

            if (player.Status.DestinationDistance > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}