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
	public class LeagueController : ControllerBase
	{
		private readonly soccerkingContext _context;
		

		public LeagueController(soccerkingContext context)
		{
			_context = context;
			//PlayerCount = context.Dicplayers.Count();
		}

		/// <summary>
		/// 创建玩家的联赛&联赛赛程（新玩家通过剧情新手引导后调用）
		/// </summary>
		/// <param name="user">玩家</param>
		public void CreateUserLeague(Users user)
		{
			League l = new League();
			l.Level = user.
		}

	}
}
