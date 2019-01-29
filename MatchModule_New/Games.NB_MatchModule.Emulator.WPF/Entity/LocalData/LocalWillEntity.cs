using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NB.Match.Emulator.WPF.Entity.LocalData
{
    public class LocalWillEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int WillRank { get; set; }

        public string WillRankStr { get; set; }

        public bool IsSelect { get; set; }
    }
}
