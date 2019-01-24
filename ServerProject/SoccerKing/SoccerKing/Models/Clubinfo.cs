using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Clubinfo
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string Content { get; set; }
        public sbyte Status { get; set; }
        public DateTime Rowtime { get; set; }
    }
}
