using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Emulator.WPF.Entity.Batch;

namespace Games.NB.Match.Emulator.WPF.Entity.Statistics
{
    public class StatisticsMatchEntity
    {
        private IMatch _match;
        public StatisticsMatchEntity(IMatch match)
        {
            _match = match;
            HomeManager = new StatisticsManagerEntity(match.HomeManager);
            AwayManager = new StatisticsManagerEntity(match.AwayManager);
            StatisticsBallResults = new List<StatisticsBallEntity>();
        }

        public int TotalRound { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public StatisticsManagerEntity HomeManager { get; set; }

        public StatisticsManagerEntity AwayManager { get; set; }

        public List<StatisticsBallEntity> StatisticsBallResults { get; set; }

        public void SetTotal()
        {
            BatchMatchEntity batchMatchEntity = new BatchMatchEntity(_match.Input.MatchId,0,0,_match.Report);
            SetTotal(batchMatchEntity);
        }

        public void SetTotal(BatchMatchEntity batchMatchEntity)
        {
            HomeScore = _match.HomeScore;
            AwayScore = _match.AwayScore;
            TotalRound = _match.Status.TotalRound;
            HomeManager.SetTotal(batchMatchEntity.HomeManager);
            AwayManager.SetTotal(batchMatchEntity.AwayManager);
            int endRound = 0;
            for (int i = 0; i < _match.Report.BallResults.Count;i++ )
            {
                var ballResult = _match.Report.BallResults[i];
                if (ballResult.AsRound > endRound)
                {
                    if (ballResult.StateData.State != 0 || ballResult.ClassId>1)
                    {
                        var entity = new StatisticsBallEntity(_match,i, ballResult);
                        if (entity.EndRound > 0)
                            endRound = entity.EndRound;
                        StatisticsBallResults.Add(entity);
                    }
                }
            }
        }

        public void AddProcess()
        {
            if (_match.Status.Round > 0)
            {
                HomeManager.AddProcess(_match.Status.Round, _match.Managers[0]);
                AwayManager.AddProcess(_match.Status.Round, _match.Managers[1]);
            }
        }
    }
}
