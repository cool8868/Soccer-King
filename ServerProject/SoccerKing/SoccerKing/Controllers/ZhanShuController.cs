using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerKing.Common;
using SoccerKing.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoccerKing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ZhanShuController : ControllerBase
	{
		private readonly soccerkingContext _context;
		
		public ZhanShuController(soccerkingContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult<ZhanShu>> Get()
		{
			return await _context.ZhanShu.FindAsync(User.Identity.Name);
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult> PostCoach(ZhanShu zs)
		{
			if (zs.UserId != User.Identity.Name)
				return BadRequest();
			_context.ZhanShu.Update(zs);
			await _context.SaveChangesAsync();

			return Ok();
		}

	}
}
