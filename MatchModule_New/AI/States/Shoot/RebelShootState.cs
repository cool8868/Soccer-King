using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.AI.States.Shoot
{
    public sealed class RebelShootState : DefaultShootState
    {
        #region Singleton
        private static readonly RebelShootState _instance = new RebelShootState();
        private RebelShootState()
        {
            this.Stopable = true;
        }
        public new static RebelShootState Instance
        {
            get { return _instance; }
        }
        #endregion

        public override string ToString()
        {
            return "RebelShootState";
        }
        public override void Action(Base.Interface.IPlayer player)
        {
            player.RebelShoot();
        }
    }
}
