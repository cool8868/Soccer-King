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
	[ApiController]
	public class LeagueMatchController : ControllerBase
	{
		private readonly soccerkingContext _context;

		public static List<LeagueSaiCheng> listSaiCheng = new List<LeagueSaiCheng>();

		public LeagueMatchController(soccerkingContext context)
		{
			_context = context;
			listSaiCheng = context.LeagueSaiCheng.ToList();
		}

		/// <summary>
		/// 计算某个联赛的比赛
		/// 创建联赛后，从第一个玩家进入比赛开始，每4小时进行一轮比赛
		/// </summary>
		/// <param name="leagueId">联赛Id</param>
		protected async Task CalculateMatchAsync(long leagueId)
		{			
			League l = _context.League.Find(leagueId);

			//判断联赛是否有人报名,没人报名则不用计算
			if (l.CurrentTeams == 0)
				return;

			//判断联赛当前应该打到第几轮//实际打了几轮
			int round = DateTime.Now.Subtract(l.StartDate).Hours / 4;
			if (round <= l.CurrentRound)
				return;
			List<Leaguememebers> listMembers = _context.Leaguememebers.Where(b => b.LeagueId == leagueId).OrderBy(b => b.Id).ToList();
			if(listSaiCheng.Count == 0)
				listSaiCheng = _context.LeagueSaiCheng.ToList();
			//没打的比赛进行计算
			for (int i = l.CurrentRound; i < round; i++)
			{
				int nr = i + 1;
				for (int r = 0; r < 10; r++)//每一轮10场比赛
				{
					int homePower = listMembers[listSaiCheng[i * 10 + r].A].MyPower ;
					int awayPower = listMembers[listSaiCheng[i * 10 + r].B].MyPower ;

					LeagueMatch match = new LeagueMatch();
					match.LeagueId = leagueId;
					match.Round = nr;
					match.Rowtime = DateTime.Now;
					match.Status = 0;
					match.AwayGoals = 0;
					match.AwayId = listMembers[listSaiCheng[i * 10 + r].B].UserId;
					match.HomeGoals = 0;
					match.HomeId = listMembers[listSaiCheng[i * 10 + r].A].UserId;
					_context.LeagueMatch.Add(match);

					//更新主客队进球，失球，胜负记录
					listMembers[listSaiCheng[i * 10 + r].A].Goals += match.HomeGoals;
					listMembers[listSaiCheng[i * 10 + r].A].Losts += match.AwayGoals;
					listMembers[listSaiCheng[i * 10 + r].A].Draw ++;

					listMembers[listSaiCheng[i * 10 + r].B].Goals += match.AwayGoals;
					listMembers[listSaiCheng[i * 10 + r].B].Losts += match.HomeGoals;
					listMembers[listSaiCheng[i * 10 + r].B].Draw++;
				}
			}
			//更新实际轮次数据
			l.CurrentRound = (sbyte)round;

			//保存赛事结果到数据库
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.Message);
			}			
		}

		protected void CalculateResult(Leaguememebers home, Leaguememebers away)
		{

		}

	}
}
