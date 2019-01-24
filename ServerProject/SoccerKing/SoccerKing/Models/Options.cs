using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Options
    {
        public int Id { get; set; }
        public int Sid { get; set; }
        public string Ops { get; set; }
        public sbyte Pro { get; set; }
        public int Num { get; set; }
    }
}
