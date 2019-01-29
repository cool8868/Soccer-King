using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NB.Match.Emulator.WPF.Entity.LocalData
{
    public class LocalSuitEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SuitType { get; set; }

        public string SuitTypeStr { get; set; }

        public bool IsSelect { get; set; }
    }
}
