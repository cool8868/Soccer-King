using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Story
    {
        public int Idx { get; set; }
        public string Sub { get; set; }
        public string Who { get; set; }
        public string Scene { get; set; }
        public string Lines { get; set; }
        public int Nid { get; set; }
    }
}
