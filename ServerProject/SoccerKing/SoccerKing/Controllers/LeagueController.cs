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

		//[HttpGet("testRV/{id}")]
		//public int testRV(long id)
		//{
		//	League l = _context.League.Find(id);
		//	l.CurrentTeams++;
		//	//_context.League.Update(l);
		//	try
		//	{
		//		return _context.SaveChanges();
		//	}
		//	catch
		//	{
		//		return 0;
		//	}
		//}

		[HttpGet("testEmpty/{Id}")]
		public long TestSp(long Id)
		{
			//LogHelper.Instance.Error("====================================================");
			//LogHelper.Instance.Error("====================================================");

			if (_context.League.Find(Id) == null)
				return -1;
			return _context.League.Find(Id).Id;
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
		public IActionResult Baoming(long id)
		{
			League l = _context.League.Find(id);
			if (l == null || l.Id == 0L)
				return BadRequest();
			//留4个AI给玩家虐
			if (l.TeamLimit > (l.CurrentTeams-4))
				return Ok(1);

			Leaguememebers ai = _context.Leaguememebers.Where(b => b.LeagueId == id && b.Status == 1).Single();
			if (ai == null)
			{
				return Ok(2);
			}
			if (l.CurrentTeams == 0)//从第一个玩家进入联赛开始，算正式启动比赛
			{
				l.StartDate = DateTime.Now;
			}
			l.CurrentTeams++;
			string userId = User.Identity.Name;
			if (_context.Leaguememebers.Where(b=>b.LeagueId == id && b.UserId == userId) != null)
			{
				return Ok(0);
			}
			ai.UserId = userId;
			ai.Status = 0;//ai成为真实玩家后，状态要重置为0
			
			try
			{
				_context.SaveChanges();
			}
			catch(Exception ex)
			{
				LogHelper.Instance.Error(ex.Message);
				return Ok(3);
			}
			return Ok(0);
		}

		/// <summary>
		/// 获取最新的10个联赛
		/// </summary>
		/// <returns>最新的10个联赛</returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<League>>> GetTop10()
		{
			//return await _context.League.FromSql("select * from league order by id desc limit 0,10;").ToListAsync();			
			return await _context.League.OrderByDescending(b => b.Id).Take(10).ToListAsync();
		}

		


		/// <summary>
		/// 创建联赛，判断当前需不需要创建新的联赛
		/// </summary>
		protected void CreateLeague()
		{			
			League l = _context.League.OrderByDescending(b => b.Id).Single();
			if (l == null || l.Id == 0)
			{
				//创建联赛
				CreateEmptyLeague();
			}

			if (l.StartDate != null)
			{
				//如果没有4小时内创建的联赛，则需要创建新联赛
				if (l.StartDate.AddHours(4) < DateTime.Now)
				{
					CreateEmptyLeague();
				}
			}
			//如果满了，也需要创建联赛,留4个AI
			if (l.CurrentTeams >= (l.TeamLimit-4))
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

			//添加20个AI球队，后续玩家进入，替换这些AI
			AddAIIntoLeague(l.Id);

			return l.Id;
		}

		

		/// <summary>
		/// 给新创建的联赛添加20个AI球队
		/// </summary>
		/// <param name="leagueId">联赛Id</param>
		private void AddAIIntoLeague(long leagueId)
		{
			int rid = RandomHelper.GetInt32(0, 179);
			DateTime now = DateTime.Now;
			List<NpcName> npcList = _context.NpcName.ToList();
			for (int i = 0; i < 20; i++)
			{
				Leaguememebers lm = new Leaguememebers();
				lm.Draw = 0;
				lm.Goals = 0;
				lm.LeagueId = leagueId;
				lm.Lose = 0;
				lm.Losts = 0;
				lm.Rowtime = now;
				lm.Score = 0;
				lm.Status = 1;//AI的Status字段比较特别，为1
				lm.Win = 0;
				lm.Cash = 0;
				lm.UserId = npcList[rid + i].Account;
				_context.Leaguememebers.Add(lm);
			}
			_context.SaveChangesAsync();
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
