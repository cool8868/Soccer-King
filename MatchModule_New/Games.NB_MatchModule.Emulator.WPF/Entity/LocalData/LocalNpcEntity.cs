using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NB.Match.Emulator.WPF.Entity.LocalData
{
    public class LocalNpcEntity
    {
        public int Type { get; set; }

        public Guid NpcId { get; set; }

        public string Name { get; set; }

        public int Kpi { get; set; }

        public string Stage { get; set; }
    }
}
