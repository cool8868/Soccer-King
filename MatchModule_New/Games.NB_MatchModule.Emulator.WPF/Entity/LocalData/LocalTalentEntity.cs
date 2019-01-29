using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NB.Match.Emulator.WPF.Entity.LocalData
{
    public class LocalTalentEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int DriveType { get; set; }

        public string DriveTypeStr { get; set; }

        public bool IsSelect { get; set; }
    }
}
