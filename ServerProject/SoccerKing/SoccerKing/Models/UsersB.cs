using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerKing.Models
{
    public partial class Users
    {
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		[ConcurrencyCheck]
		[Column("rowversion", TypeName = "bigint")]
		public virtual long RowVersion { get; set; }
	}
}
