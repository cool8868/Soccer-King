using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.States.Defence
{
    /// <summary>
    /// 头球争顶状态
    /// </summary>
    public sealed class HeadingDuelState : DefenceState
    {
         #region Singleton
        static readonly HeadingDuelState s_instance = new HeadingDuelState();
        public static HeadingDuelState Instance
        {
            get { return s_instance; }
        }
        private HeadingDuelState()
        {
            this.Stopable = true;
        }
        #endregion

        public override void Initialize()
        {
        }
        public override void Enter(IPlayer player)
        {
        }
        public override string ToString()
        {
            return "HeadingDuelState";
        }
        public override void Action(IPlayer player)
        {
            player.HeadingBall();
        }
        public override IState QuickDecide(IPlayer player, IState preview)
        {
            if (player.Status.Hasball)
            {
                player.Redecide();
                return HoldBallState.Instance;
            }
            else
            {
                return OffBallState.Instance;
            }
        }
    }
}
