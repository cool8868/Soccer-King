using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.Emulator.WPF.Mgrs;

namespace Games.NB.Match.Emulator.WPF.Entity.Batch
{
    public class BatchMatchEntity
    {
        public BatchMatchEntity(Guid matchId, int id, long cost, MatchReport match)
        {
            MatchId = matchId;
            Id = id;
            Cost = cost;
            Round = EmulatorHelper.GetMaxRound(match);
            HomeManager = new BatchManagerEntity(match,Round, true);
            AwayManager = new BatchManagerEntity(match, Round, false);
            HomeScore = match.HomeScore;
            AwayScore = match.AwayScore;
        }

        public Guid MatchId { get; set; }

        public int Id { get; set; }

        public long Cost { get; set; }

        public int Round { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public string ScoreStr { get { return HomeManager.Score + ":" + AwayManager.Score; } }

        public BatchManagerEntity HomeManager { get; set; }

        public BatchManagerEntity AwayManager { get; set; }
    }
}
