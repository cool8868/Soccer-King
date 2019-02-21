using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class LeagueMatchAnPai
    {
        public int Id { get; set; }
        public int? Round { get; set; }
        public string A { get; set; }
        public string B { get; set; }
    }
}
