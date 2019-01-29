using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.Emulator.WPF.Entity.Statistics
{
    public class StatisticsManagerEntity
    {
        private IManager _manager;
        private IMatch _match;
        public int MinRound=-1;
        public StatisticsManagerEntity(IManager manager)
        {
            this.Name = manager.Input.Name;
            Players = new Dictionary<int, List<StatisticsPlayerEntity>>();
        }

        public void AddProcess(int round, IManager manager)
        {
            if (MinRound == -1)
                MinRound = round;
            if(Players.ContainsKey(round))
                return;
            var list = new List<StatisticsPlayerEntity>(11);
            foreach (var player in manager.Players)
            {
                list.Add(new StatisticsPlayerEntity(player));
            }
            Players.Add(round,list);
        }

        public void SetTotal(BatchManagerEntity batchManagerEntity)
        {
            ShootTimes = batchManagerEntity.TotalShoot.ShootTimes;
            GoalTimes = batchManagerEntity.TotalShoot.GoalTimes;
            DiveTimes = batchManagerEntity.GoalKeep.Times;
            DiveSuccTimes = batchManagerEntity.GoalKeep.SuccTimes;
            DiveFailTimes = batchManagerEntity.GoalKeep.FailTimes;
            PassTimes = batchManagerEntity.TotalPass.Times;
            PassSuccTimes = batchManagerEntity.TotalPass.SuccTimes;
        }

        public string Name { get; set; }

        public int ShootTimes { get; set; }

        public int GoalTimes { get; set; }

        public int DiveTimes { get; set; }

        public int DiveSuccTimes { get; set; }

        public int DiveFailTimes { get; set; }

        public int PassTimes { get; set; }

        public int PassSuccTimes { get; set; }

        public int RebelShootTimes { get; set; }

        public int RebelSuccTimes { get; set; }

        public Dictionary<int,List<StatisticsPlayerEntity>> Players { get; set; } 
    }
}
