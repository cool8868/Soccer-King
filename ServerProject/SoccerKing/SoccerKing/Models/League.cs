using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class League
    {
        public long Id { get; set; }
        public int Level { get; set; }
        public sbyte CurrentRound { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CurrentTeams { get; set; }
        public int TeamLimit { get; set; }
        public sbyte Status { get; set; }
        //public long RowVersion { get; set; }
    }
}
