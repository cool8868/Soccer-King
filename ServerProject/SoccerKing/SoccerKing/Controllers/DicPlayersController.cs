using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerKing.Common;
using SoccerKing.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoccerKing.Controllers
{
	[Route("api/[controller]")]
	public class DicPlayersController : ControllerBase
	{
		private readonly soccerkingContext _context;
		

		public DicPlayersController(soccerkingContext context)
		{
			_context = context;
			//PlayerCount = context.Dicplayers.Count();
		}
		// GET api/<controller>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Dicplayers>> Get(int id)
		{
			return await _context.Dicplayers.FindAsync(id);
		}

	}
}
