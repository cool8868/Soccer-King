using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Userplayers
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public int? Lv { get; set; }
        public sbyte? Age { get; set; }
        public string Position { get; set; }
        public int? Shoot { get; set; }
        public int? Pass { get; set; }
        public int? Defence { get; set; }
        public int? Gate { get; set; }
        public int? MaxPower { get; set; }
        public sbyte? Type { get; set; }
        public sbyte? Status { get; set; }
        public DateTime? Rowtime { get; set; }
    }
}
