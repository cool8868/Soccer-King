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
	public class UserPlayersController : ControllerBase
	{
		private readonly soccerkingContext _context;

		public UserPlayersController(soccerkingContext context)
		{
			_context = context;
		}

		// GET api/<controller>/5
		[HttpGet("{idx}")]
		public async Task<ActionResult<Userplayers>> Get(int idx)
		{
			return await _context.Userplayers.FindAsync(idx);
		}

		// GET api/<controller>/5
		[HttpGet("u/{uid}")]		
		public async Task<ActionResult<IEnumerable<Userplayers>>> GetByUid(string uid)
		{
			return await _context.Userplayers.Include(b => b.P).Where(b => b.Uid == uid).ToListAsync();
		}

		// POST: api/Todo
		[HttpPost]
		public async Task<ActionResult<Users>> PostUserplayers(Userplayers userplayers)
		{
			_context.Userplayers.Add(userplayers);
			await _context.SaveChangesAsync();

			return CreatedAtAction("Get", new { id = userplayers.Id }, userplayers);
		}

		// PUT: api/Todo/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutUserplayers(int id, Userplayers userplayers)
		{
			if (id != userplayers.Id)
			{
				return BadRequest();
			}

			_context.Entry(userplayers).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// DELETE: api/Todo/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Userplayers>> DeleteUserplayers(int id)
		{
			var userplayers = await _context.Userplayers.FindAsync(id);
			if (userplayers == null)
			{
				return NotFound();
			}

			_context.Userplayers.Remove(userplayers);
			await _context.SaveChangesAsync();

			return NoContent();
		}

	}
}
