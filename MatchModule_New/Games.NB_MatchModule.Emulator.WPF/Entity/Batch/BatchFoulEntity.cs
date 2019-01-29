using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Emulator.WPF.Entity.Batch
{
    public class BatchFoulEntity
    {
        public void AddData(BatchFoulEntity entity)
        {
            FoulTimes += entity.FoulTimes;
            YellowTimes += entity.YellowTimes;
            RedTimes += entity.RedTimes;
        }

        public void CalAvg(int totalCount)
        {
            FoulTimes = FoulTimes / totalCount;
            YellowTimes = YellowTimes / totalCount;
            RedTimes = RedTimes / totalCount;
            
        }

        public int FoulTimes { get; set; }

        public int YellowTimes { get; set; }

        public int RedTimes { get; set; }

        
    }
}
