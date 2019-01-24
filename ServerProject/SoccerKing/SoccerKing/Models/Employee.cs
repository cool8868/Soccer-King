using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Employee
    {
        public int Idx { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public sbyte Age { get; set; }
        public int Lv { get; set; }
        public sbyte? Type { get; set; }
        public sbyte Status { get; set; }
        public DateTime Rowtime { get; set; }
    }
}
