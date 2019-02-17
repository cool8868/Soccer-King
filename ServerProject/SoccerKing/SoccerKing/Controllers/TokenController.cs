using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoccerKing.Common;
using SoccerKing.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoccerKing.Controllers
{
	[Route("api/[controller]")]
	public class TokenController : ControllerBase
	{
		private readonly soccerkingContext _context;


		public TokenController(soccerkingContext context)
		{
			_context = context;
		}

		[HttpGet("{userid}")]
		public IActionResult GetToken(string userId)
		{
			Users user = _context.Users.Find(userId);
			if (user == null)
				return BadRequest();
			//if (IsValidUserAndPasswordCombination(username, password))
			return new ObjectResult(GenerateToken(userId));
			//return BadRequest();
		}

		[HttpPost]
		public IActionResult Create(string userId)
		{
			//Log l = new Log();
			//l.Content = userId;
			//_context.Log.Add(l);
			//_context.SaveChanges();
			Users user = _context.Users.Find(userId);
			if (user == null)
				return BadRequest();
			//if (IsValidUserAndPasswordCombination(username, password))
			return new ObjectResult(GenerateToken(userId));
			//return BadRequest();
		}

		private bool IsValidUserAndPasswordCombination(string username, string password)
		{
			return !string.IsNullOrEmpty(username) && username == password;
		}

		private string GenerateToken(string userId)
		{
			var claims = new Claim[]
			{
				new Claim(ClaimTypes.Name, userId),
				new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
				new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(2)).ToUnixTimeSeconds().ToString()),
			};

			var token = new JwtSecurityToken(
				new JwtHeader(new SigningCredentials(
					key: new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes("BellaMyDaughterBabaLoveYou")),
											 algorithm: SecurityAlgorithms.HmacSha256)),
				new JwtPayload(claims));

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
