using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class ZhanShu
    {
        public string UserId { get; set; }
        public sbyte A1 { get; set; }
        public sbyte A2 { get; set; }
        public sbyte A3 { get; set; }
        public sbyte A4 { get; set; }
        public sbyte D1 { get; set; }
        public sbyte D2 { get; set; }
        public sbyte D3 { get; set; }
        public sbyte D4 { get; set; }
        public sbyte Status { get; set; }
        public DateTime Rowtime { get; set; }
    }
}
