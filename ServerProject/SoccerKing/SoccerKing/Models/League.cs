using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerKing.Models
{
    public partial class League
    {
        public long Id { get; set; }
        public int? Level { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CurrentTeams { get; set; }
        public int? TeamLimit { get; set; }
        public sbyte Status { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		[ConcurrencyCheck]
		[Column("rowversion", TypeName = "bigint")]
		public virtual long RowVersion { get; set; }
    }
}
