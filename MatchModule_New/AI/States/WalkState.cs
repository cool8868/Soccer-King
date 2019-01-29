using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.AI.Actions;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.States
{
    /// <summary>
    /// 走动状态
    /// </summary>
    [Singleton]
    public class WalkState : State
    {
        #region Singleton
        static readonly WalkState s_instance = new WalkState();
        public static WalkState Instance
        {
            get { return s_instance; }
        }
        private WalkState()
        {
            this.Stopable = true;
        }
        #endregion

        public override void Initialize()
        {
           
        }
        public override string ToString()
        {
            return "WalkState";
        }
        public override void Action(IPlayer player)
        {
            player.Move();
        }
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
    }
}
