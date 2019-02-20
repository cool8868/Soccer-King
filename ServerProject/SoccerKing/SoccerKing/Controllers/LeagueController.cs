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
	public class LeagueController : ControllerBase
	{
		private readonly soccerkingContext _context;

		private const int NcpNameCount = 312;
		private const int LeagueTeamNum = 32;//冠军联赛有多少支队伍
		private const int LeagueTeamLimit = 32;//冠军联赛有多少支队伍
		private const int CreateLeagueCostDiamonds = 60;//创建联赛的花费

		public LeagueController(soccerkingContext context)
		{
			_context = context;			
		}

		/// <summary>
		/// 玩家付费创建联赛Id
		/// </summary>
		/// <param name="idx">联赛类别</param>
		/// <returns>新创建的联赛id</returns>
		[Authorize]
		[HttpGet("c/{id}")]
		public long Create(int idx)
		{
			//付钱
			Users u = _context.Users.Find(User.Identity.Name);
			if (u == null || String.IsNullOrEmpty(u.OpenId))
				return 0L;

			if (u.Diamond < CreateLeagueCostDiamonds)
				return 0L;

			u.Diamond -= CreateLeagueCostDiamonds;
			_context.SaveChanges();

			return CreateEmptyLeague();
		}

		//[HttpGet("testSP/")]
		//public int TestSp()
		//{
		//	string openId = "successopenid";
		//	int cash = 1;
		//	string sql = "call CostCash('"+ openId + "',"+ cash + ")";
		//	int result = _context.Database.ExecuteSqlCommand(sql);
		//	return result;
		//}


		/// <summary>
		/// 玩家报名参加某联赛
		/// </summary>
		/// <param name="id">联赛Id</param>
		/// <returns>是否报名成功</returns>
		[Authorize]
		[HttpGet("b/{id}")]
		public IActionResult Baoming(int id)
		{
			League l = _context.League.Find(id);
			if (l == null || l.Id == 0L)
				return BadRequest();

			if (l.TeamLimit == l.CurrentTeams)
				return BadRequest();

			l.CurrentTeams++;
			Leaguememebers lm = new Leaguememebers();
			lm.UserId = User.Identity.Name;
			lm.Draw = 0;
			lm.Goals = 0;
			lm.LeagueId = l.Id;
			lm.Lose = 0;
			lm.Losts = 0;
			lm.Rowtime = DateTime.Now;
			lm.Score = 0;
			lm.Status = 0;
			lm.Win = 0;
			lm.Cash = 0;
			_context.Leaguememebers.Add(lm);
			_context.SaveChanges();
			return Ok();
		}

		/// <summary>
		/// 获取最新的10个联赛
		/// </summary>
		/// <returns>最新的10个联赛</returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<League>>> GetTop10()
		{
			return await _context.League.FromSql("select * from league order by id desc limit 0,10;").ToListAsync();
		}

		


		/// <summary>
		/// 创建联赛，判断当前需不需要创建新的联赛
		/// </summary>
		protected void CreateLeague()
		{
			League l = _context.League.FromSql("select * from League order by id desc limit 0,1;").FirstOrDefault();
			if (l == null || l.Id == 0)
			{
				//创建联赛
				CreateEmptyLeague();
			}

			if (l.StartDate != null)
			{
				//如果没有当天创建的联赛，则需要创建新联赛
				if (l.StartDate.Value.AddDays(1) > DateTime.Now)
				{
					CreateEmptyLeague();
				}
			}
			//如果满了，也需要创建联赛
			if (l.CurrentTeams >= l.TeamLimit)
			{
				CreateEmptyLeague();
			}
		}

		/// <summary>
		/// 创建空白联赛，供玩家加入
		/// </summary>
		/// <returns>新创建的联赛id</returns>
		[HttpGet("create/")]
		public long CreateEmptyLeague()
		{
			League l = new League();
			l.Level = 1;
			l.StartDate = DateTime.Now;
			l.EndDate = DateTime.Now.AddDays(1);
			l.Status = 0;			
			l.TeamLimit = LeagueTeamLimit;
			l.CurrentTeams = 0;
			_context.League.Add(l);
			_context.SaveChanges();
			return l.Id;
		}

		
		/// <summary>
		/// 创建玩家的联赛&联赛赛程（新玩家通过剧情新手引导后调用）
		/// </summary>
		/// <param name="user">玩家</param>		
		protected void CreateUserLeague(Users user)
		{
			long leagueId = CreateEmptyLeague();

			List<NpcName> listName = _context.NpcName.ToList();
			List<string> userIdList = new List<string>();
			int nameIndex = RandomHelper.GetInt32(1, NcpNameCount - 33);//防止数组越界
			DateTime now = DateTime.Now;

			//leagueMember表数据填充
			for (int i = 0; i < 32; i++)
			{
				Leaguememebers lm = new Leaguememebers();
				if (i == 0)
				{
					lm.UserId = user.OpenId;
				}
				else
				{
					lm.UserId = listName[nameIndex + i].Account;
				}
				lm.Draw = 0;
				lm.Goals = 0;
				lm.LeagueId = leagueId;
				lm.Lose = 0;
				lm.Losts = 0;
				lm.Rowtime = now;
				lm.Score = 0;
				lm.Status = 0;
				lm.Win = 0;
				lm.Cash = 0;
				_context.Leaguememebers.Add(lm);
				userIdList.Add(lm.UserId);
			}
			_context.SaveChanges();
		}


		/// <summary>
		/// 联赛赛程对阵表
		/// </summary>
		/// <param name="leagueId">联赛ID</param>
		/// <param name="userIdList">玩家Id列表</param>
		protected void CreateLeagueMatch(long leagueId, List<string> userIdList)
		{
			for (int i = 0; i < LeagueTeamNum - 1; i++)
			{
				LeagueMatch lm = new LeagueMatch();
				
			}
		}

	}
}
