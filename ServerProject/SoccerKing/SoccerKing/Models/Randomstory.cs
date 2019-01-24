using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Randomstory
    {
        public int Id { get; set; }
        public string Line { get; set; }
        public string Op1 { get; set; }
        public string Op2 { get; set; }
        public sbyte Pro1 { get; set; }
        public sbyte Pro2 { get; set; }
        public int Num1 { get; set; }
        public int Num2 { get; set; }
    }
}
