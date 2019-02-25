using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Freesignplayers
    {
        public long LeagueId { get; set; }
        public string PlayerIdArr { get; set; }
        public sbyte Status { get; set; }
        //public long RowVersion { get; set; }
    }
}
