using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Users
    {
        public string OpenId { get; set; }
        public string Nick { get; set; }
        public string UnionId { get; set; }
        public long Cash { get; set; }
        public long Fans { get; set; }
        public sbyte Status { get; set; }
        public DateTime RowTime { get; set; }
    }
}
