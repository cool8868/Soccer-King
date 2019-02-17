using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Talent
    {
        public int Id { get; set; }
        public int UserPlayerId { get; set; }
        public sbyte Tid { get; set; }
        public sbyte Lv { get; set; }
    }
}
