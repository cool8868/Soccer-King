using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerKing.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoccerKing.Controllers
{
	[Route("api/[controller]")]
	public class CoachController : ControllerBase
	{
		private readonly soccerkingContext _context;

		public CoachController(soccerkingContext context)
		{
			_context = context;
		}

		// GET api/<controller>/5
		[HttpGet("{idx}")]
		public async Task<ActionResult<Coach>> Get(int idx)
		{
			return await _context.Coach.FindAsync(idx);
		}

		// GET api/<controller>/5
		[HttpGet("u/{uid}")]
		public async Task<ActionResult<IEnumerable<Coach>>> GetByUid(string uid)
		{
			return await _context.Coach.Where(b => b.Uid == uid).ToListAsync();
		}

		// POST: api/Todo
		[HttpPost]
		public async Task<ActionResult<Users>> PostCoach(Coach Coach)
		{
			_context.Coach.Add(Coach);
			await _context.SaveChangesAsync();

			return CreatedAtAction("Get", new { id = Coach.Id }, Coach);
		}

		// PUT: api/Todo/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCoach(int id, Coach Coach)
		{
			if (id != Coach.Id)
			{
				return BadRequest();
			}

			_context.Entry(Coach).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// DELETE: api/Todo/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Coach>> DeleteCoach(int id)
		{
			var Coach = await _context.Coach.FindAsync(id);
			if (Coach == null)
			{
				return NotFound();
			}

			_context.Coach.Remove(Coach);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
