using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using SoccerKing.Common;
using SoccerKing.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoccerKing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CatchDataController : ControllerBase
	{
		private readonly soccerkingContext _context;
		private static HtmlParser htmlParser = new HtmlParser();

		public CatchDataController(soccerkingContext context)
		{
			_context = context;
			//PlayerCount = context.Dicplayers.Count();
		}

		#region 调整赛程
		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<LeagueSaiCheng>>> Catch()
		//{
		//	List<LeagueMatchAnPai> list = _context.LeagueMatchAnPai.ToList();
		//	foreach (LeagueMatchAnPai l in list)
		//	{
		//		LeagueSaiCheng c = new LeagueSaiCheng();
		//		c.Round = l.Round;
		//		#region switch

		//		switch (l.A)
		//		{
		//			case "阿森纳":
		//				c.A = 1;
		//				break;
		//			case "莱切斯特":
		//				c.A = 2;
		//				break;
		//			case "曼联":
		//				c.A = 3;
		//				break;
		//			case "西汉姆联":
		//				c.A = 4;
		//				break;
		//			case "纽卡斯尔":
		//				c.A = 5;
		//				break;
		//			case "热刺":
		//				c.A = 6;
		//				break;
		//			case "布赖顿":
		//				c.A = 7;
		//				break;
		//			case "曼城":
		//				c.A = 8;
		//				break;
		//			case "南安普敦":
		//				c.A = 9;
		//				break;
		//			case "斯旺西":
		//				c.A = 10;
		//				break;
		//			case "西布罗姆":
		//				c.A = 11;
		//				break;
		//			case "伯恩茅斯":
		//				c.A = 12;
		//				break;
		//			case "埃弗顿":
		//				c.A = 13;
		//				break;
		//			case "斯托克城":
		//				c.A = 14;
		//				break;
		//			case "水晶宫":
		//				c.A = 15;
		//				break;
		//			case "哈德斯":
		//				c.A = 16;
		//				break;
		//			case "切尔西":
		//				c.A = 17;
		//				break;
		//			case "伯恩利":
		//				c.A = 18;
		//				break;
		//			case "沃特福德":
		//				c.A = 19;
		//				break;
		//			case "利物浦":
		//				c.A = 20;
		//				break;
		//			default:break;
		//		}
		//		switch (l.B)
		//		{
		//			case "阿森纳":
		//				c.B = 1;
		//				break;
		//			case "莱切斯特":
		//				c.B = 2;
		//				break;
		//			case "曼联":
		//				c.B = 3;
		//				break;
		//			case "西汉姆联":
		//				c.B = 4;
		//				break;
		//			case "纽卡斯尔":
		//				c.B = 5;
		//				break;
		//			case "热刺":
		//				c.B = 6;
		//				break;
		//			case "布赖顿":
		//				c.B = 7;
		//				break;
		//			case "曼城":
		//				c.B = 8;
		//				break;
		//			case "南安普敦":
		//				c.B = 9;
		//				break;
		//			case "斯旺西":
		//				c.B = 10;
		//				break;
		//			case "西布罗姆":
		//				c.B = 11;
		//				break;
		//			case "伯恩茅斯":
		//				c.B = 12;
		//				break;
		//			case "埃弗顿":
		//				c.B = 13;
		//				break;
		//			case "斯托克城":
		//				c.B = 14;
		//				break;
		//			case "水晶宫":
		//				c.B = 15;
		//				break;
		//			case "哈德斯":
		//				c.B = 16;
		//				break;
		//			case "切尔西":
		//				c.B = 17;
		//				break;
		//			case "伯恩利":
		//				c.B = 18;
		//				break;
		//			case "沃特福德":
		//				c.B = 19;
		//				break;
		//			case "利物浦":
		//				c.B = 20;
		//				break;
		//			default: break;
		//		}
		//		#endregion

		//		_context.LeagueSaiCheng.Add(c);
		//	}
		//	await _context.SaveChangesAsync();
		//	return NoContent();
		//}
		#endregion

		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<LeagueMatchAnPai>>> Catch()
		//{

		//	try
		//	{
		//		// 设置配置以支持文档加载
		//		var config = Configuration.Default.WithDefaultLoader();
		//		for (int r = 1; r <= 38; r++)
		//		{
		//			// 地址
		//			var address = "http://saishi.zgzcw.com/summary/liansaiAjax.action?source_league_id=36&currentRound=" + r + "&season=2017-2018";
		//			// 请求
		//			var document = BrowsingContext.New(config).OpenAsync(address);
		//			// 根据class获取html元素
		//			var cells = document.Result.QuerySelectorAll("a");


		//			for (int i = 0, length = cells.Length; i < length; i += 5)
		//			{
		//				if (cells[i].ClassName == "oyx")
		//					continue;
		//				LeagueMatchAnPai lmap = new LeagueMatchAnPai();
		//				lmap.Round = r;
		//				lmap.A = cells[i].InnerHtml;
		//				lmap.B = cells[i + 1].InnerHtml;
		//				_context.LeagueMatchAnPai.Add(lmap);
		//			}
		//		}
		//		await _context.SaveChangesAsync();
		//	}
		//	catch (Exception ex)
		//	{
		//	}

		//	return await _context.LeagueMatchAnPai.ToListAsync();
		//}

	}
}
