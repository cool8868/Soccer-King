using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Leaguememebers
    {
        public long Id { get; set; }
        public long LeagueId { get; set; }
        public string UserId { get; set; }
        public long Cash { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lose { get; set; }
        public int Score { get; set; }
        public int? Goals { get; set; }
        public int? Losts { get; set; }
        public sbyte? Status { get; set; }
        public DateTime? Rowtime { get; set; }
    }
}
