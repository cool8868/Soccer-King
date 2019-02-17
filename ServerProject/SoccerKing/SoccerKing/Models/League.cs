using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class League
    {
        public long Id { get; set; }
        public int? Level { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public sbyte Status { get; set; }
        public DateTime Rowtime { get; set; }
    }
}
