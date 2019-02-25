using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Playercontract
    {
        public long Id { get; set; }
        public int PlayerId { get; set; }
        public string UserId { get; set; }
        public long Salary { get; set; }
        public sbyte Years { get; set; }
        public long Weiyujin { get; set; }
        public sbyte Status { get; set; }
        public long RowVersion { get; set; }
    }
}
