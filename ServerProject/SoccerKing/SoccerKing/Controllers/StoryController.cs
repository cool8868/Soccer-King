using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerKing.Models;

namespace SoccerKing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StoryController : ControllerBase
    {
		private readonly soccerkingContext _context;

		public StoryController(soccerkingContext context)
		{
			_context = context;
		}
		// GET: api/<controller>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Story>>> GetAll()
		{
			return await _context.Story.ToListAsync();
		}

		// GET api/<controller>/5
		[HttpGet("{idx}")]
		public async Task<ActionResult<Story>> Get(int idx)
		{
			return await _context.Story.FindAsync(idx);
		}
	}
}