using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class LeagueMatch
    {
        public long Id { get; set; }
        public long LeagueId { get; set; }
        public string HomeId { get; set; }
        public string AwayId { get; set; }
        public int Round { get; set; }
        public sbyte HomeGoals { get; set; }
        public sbyte AwayGoals { get; set; }
        public sbyte Status { get; set; }
        public DateTime Rowtime { get; set; }
    }
}
