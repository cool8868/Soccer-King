using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NB.Match.Emulator.WPF.Entity
{
    public class UnitTestEntity
    {
        public UnitTestEntity(int unitIndex, int matchIndex, long cost, string result)
        {
            Cost = cost;
            MatchIndex = matchIndex;
            ThreadIndex = unitIndex;
            ScoreStr = result;
        }

        public int ThreadIndex { get; set; }

        public int MatchIndex { get; set; }

        public string ScoreStr { get; set; }

        public long Cost { get; set; }

    }
}
