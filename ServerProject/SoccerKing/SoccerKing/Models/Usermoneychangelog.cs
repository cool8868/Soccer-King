using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Usermoneychangelog
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long CashChangeNum { get; set; }
        public long DiamondChangeNum { get; set; }
        public long CashBefore { get; set; }
        public long DiamondBefore { get; set; }
        public DateTime Rowtime { get; set; }
    }
}
