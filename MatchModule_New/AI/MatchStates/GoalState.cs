using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model.TranOut;

namespace Games.NB.Match.AI.MatchStates
{
    public class GoalState : DefaultBallState
    {
        #region Singleton
        static readonly GoalState s_instance = new GoalState();
        public new static GoalState Instance
        {
            get { return s_instance; }
        }
        #endregion

        protected override BallStateReport CreateStateRpt(IMatch match)
        {
            var rpt = new GoalStateReport();
            rpt.HomeScore = (byte)match.HomeScore;
            rpt.AwayScore = (byte)match.AwayScore;
            return rpt;
        }
    }
}
