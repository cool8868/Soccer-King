using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Rowtime { get; set; }
    }
}
