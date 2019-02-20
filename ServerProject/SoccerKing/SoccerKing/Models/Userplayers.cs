using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Userplayers
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public int? Pid { get; set; }
        public string Name { get; set; }
        public long? SignPrice { get; set; }
        public long? WeiYueJin { get; set; }
        public int? Lv { get; set; }
        public int? Pz { get; set; }
        public sbyte? Jx { get; set; }
        public sbyte? Age { get; set; }
        public sbyte? Type { get; set; }
        public sbyte? Status { get; set; }
        public DateTime? Rowtime { get; set; }

        public virtual Dicplayers P { get; set; }
    }
}
