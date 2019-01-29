using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model.TranOut;

namespace Games.NB.Match.AI.MatchStates
{
    public class DefaultBallState : IMatchState
    {
        #region Singleton
        static readonly DefaultBallState s_instance = new DefaultBallState();
        public static DefaultBallState Instance
        {
            get { return s_instance; }
        }
        #endregion

        public BallMoveReport SaveRpt(IMatch match)
        {
            var state = CreateStateRpt(match);
            FillStateRpt(match, state);
            var rpt = new BallMoveReport();
            rpt.AsRound = match.Status.Round;
            rpt.StateData = state;
            return rpt;
        }
        protected virtual BallStateReport CreateStateRpt(IMatch match)
        {
            return new BallStateReport();
        }
        protected void FillStateRpt(IMatch match, BallStateReport rpt)
        {
            var ball = match.Football;
            rpt.Current = ball.Current;
            rpt.Angle = 0;
            rpt.State = (byte)match.Status.BreakState;
            rpt.TurnFlag = Convert.ToByte(match.Football.TurnFlag);
            rpt.FoulState = (byte)match.Status.FoulState;
        }
    }
}
