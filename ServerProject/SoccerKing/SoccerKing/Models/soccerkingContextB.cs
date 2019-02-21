using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SoccerKing.Models
{
    public partial class soccerkingContext : DbContext
    {
		
	}

	public interface IRowVersion
	{
		byte[] RowVersion { get; set; }
	}
}
