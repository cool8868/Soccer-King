using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

		/// <summary>
		/// 获取某球员
		/// </summary>
		/// <param name="idx">球员Id</param>
		/// <returns>球员</returns>
		// GET api/<controller>/5
		[HttpGet("{idx}")]
		public async Task<ActionResult<Userplayers>> Get(int idx)
		{
			return await _context.Userplayers.FindAsync(idx);
		}

		/// <summary>
		/// 获取某玩家旗下的所有球员
		/// </summary>
		/// <param name="uid">玩家Id</param>
		/// <returns>玩家旗下的所有球员</returns>
		[HttpGet("u/{uid}")]		
		public async Task<ActionResult<IEnumerable<Userplayers>>> GetByUid(string uid)
		{
			return await _context.Userplayers.Include(b => b.P).Where(b => b.Uid == uid).ToListAsync();
		}

		

		// PUT: api/Todo/5
		//[HttpPut("{id}")]
		//[Authorize]
		//public async Task<IActionResult> PutUserplayers(int id, Userplayers userplayers)
		//{
		//	if (id != userplayers.Id)
		//	{
		//		return BadRequest();
		//	}

		//	_context.Entry(userplayers).State = EntityState.Modified;
		//	await _context.SaveChangesAsync();

		//	return NoContent();
		//}

		/// <summary>
		/// 解雇球员
		/// </summary>
		/// <param name="id">球员Id</param>
		/// <returns>是否成功</returns>
		// DELETE: api/Todo/5
		[HttpDelete("{id}")]
		[Authorize]
		public async Task<ActionResult<Userplayers>> DeleteUserplayers(int id)
		{
			var userplayers = await _context.Userplayers.FindAsync(id);
			if (userplayers == null)
			{
				return NotFound(id);
			}

			_context.Userplayers.Remove(userplayers);
			await _context.SaveChangesAsync();

			return Ok();
		}

	}
}
