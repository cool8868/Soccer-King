using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model.TranOut;

namespace Games.NB.Match.AI.MatchStates
{
    public class AirBallState : DefaultBallState
    {
        #region Singleton
        static readonly AirBallState s_instance = new AirBallState();
        public new static AirBallState Instance
        {
            get { return s_instance; }
        }
        #endregion

        protected override BallStateReport CreateStateRpt(IMatch match)
        {
            var rpt = new AirBallStateReport();
            rpt.Destination = match.Football.Destination;
            return rpt;
        }
    }
}
