using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Freesignplayers
    {
        public long Id { get; set; }
        public long? LeagueId { get; set; }
        public int? PlayerId { get; set; }
        public sbyte? Status { get; set; }
        public DateTime RowVersion { get; set; }
    }
}
