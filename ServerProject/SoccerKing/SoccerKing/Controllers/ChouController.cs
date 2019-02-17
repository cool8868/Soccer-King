using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
	public class ChouController : ControllerBase
	{
		private readonly soccerkingContext _context;
		public const int PlayerCount = 3142;//球员总数量
		public const int PlayerFCount = 1735;//前锋总数量
		public const int PlayerMCount = 256;//中场总数量
		public const int PlayerDCount = 885;//后卫总数量
		public const int PlayerGKCount = 266;//门将总数量
		public const int InitTeamCount = 121;//初始球队模板数量，新创建的球队，从这里随机取一个

		public ChouController(soccerkingContext context)
		{
			_context = context;
			//PlayerCount = context.Dicplayers.Count();
		}


		/// <summary>
		/// 抽卡
		/// </summary>
		/// <param name="num">需要抽取的数量</param>
		/// <returns>抽取的球员卡</returns>
		[Authorize]
		[HttpGet("{num}")]
		public List<Userplayers> Get(int num)
		{
			if (num < 1)
			{
				return null ;
			}

			List<int> pidList = ChouCard(num);
			if (pidList == null || pidList.Count == 0)
			{
				return null;
			}

			List<Userplayers> listPlayers = new List<Userplayers>();
			for (int i = 0; i < num; i++)
			{
				Dicplayers dp = _context.Dicplayers.Find(pidList[i]);
				if (dp == null)
				{
					continue;
				}

				Userplayers up = new Userplayers();
				up.Id = 0;
				up.Age = (sbyte)RandomHelper.GetInt32(16,26);
				up.Jx = 0;
				up.Lv = 1;
				up.Pz = RandomHelper.GetInt32(1, 5);
				up.Name = dp.名字;				
				up.Pid = dp.Id;
				up.Rowtime = DateTime.Now;
				up.Status = 0;
				up.Type = (sbyte)RandomHelper.GetInt32(0, 2);
				up.Uid = User.Identity.Name;				
				//up.P = dp;
				listPlayers.Add(up);
			}
			SaveData(listPlayers);
			return listPlayers;
		}

		/// <summary>
		/// 创建球队，给这名玩家创建11名球员
		/// </summary>
		/// <returns></returns>
		//[Authorize]
		[HttpGet("c/")]
		public List<Userplayers> CreateTeam()
		{
			int teamId = RandomHelper.GetInt32(1, InitTeamCount);
			Teamplayersrandom tr = _context.Teamplayersrandom.Find(teamId);
			if (tr == null || tr.Id == 0)
				return null;
			string[] playerIdArr = tr.PlayerIdString.Split(',');

			List<Userplayers> listPlayers = new List<Userplayers>();
			for (int i = 0; i < 11; i++)
			{
				Dicplayers dp = _context.Dicplayers.Find(Convert.ToInt32(playerIdArr[i]));
				if (dp == null)
				{
					return null;
				}

				Userplayers up = new Userplayers();
				up.Id = 0;
				up.Age = 16;
				up.Jx = 0;
				up.Lv = 1;
				up.Pz = 5;
				up.Name = dp.名字;
				up.Pid = dp.Id;
				up.Rowtime = DateTime.Now;
				up.Status = 0;
				up.Type = (sbyte)RandomHelper.GetInt32(0, 2);
				//up.Uid = User.Identity.Name;
				up.Uid = "successopenid";
				listPlayers.Add(up);
			}
			SaveData(listPlayers);
			return listPlayers;
		}

		/// <summary>
		/// 抽卡,指定品质
		/// </summary>
		/// <param name="num">需要抽取的数量&品质</param>
		/// <returns>抽取的球员卡</returns>
		[Authorize]
		[HttpGet("p/{num&pz}")]
		public List<Userplayers> GetByPz(string numpz)
		{
			if (numpz.IndexOf('&') < 1)
			{
				return null;
			}

			int num = 0;
			int pz = 1;
			string[] strArr = numpz.Split('&');
			if (strArr.Length != 2)
			{
				return null;
			}

			if (!int.TryParse(strArr[0], out num))
				return null;			
			if (!int.TryParse(strArr[1], out pz))
				return null;

			if (num < 1)
			{
				return null;
			}

			List<int> pidList = ChouCard(num);
			if (pidList == null || pidList.Count == 0)
			{
				return null;
			}

			List<Userplayers> listPlayers = new List<Userplayers>();
			for (int i = 0; i < num; i++)
			{
				Dicplayers dp = _context.Dicplayers.Find(pidList[i]);
				if (dp == null)
				{
					continue;
				}

				Userplayers up = new Userplayers();
				up.Id = 0;
				up.Age = (sbyte)RandomHelper.GetInt32(16, 26);
				up.Jx = 0;
				up.Lv = 1;
				up.Pz = pz;
				up.Name = dp.名字;
				up.Pid = dp.Id;
				up.Rowtime = DateTime.Now;
				up.Status = 0;
				up.Type = (sbyte)RandomHelper.GetInt32(0, 2);
				up.Uid = User.Identity.Name;
				//up.P = dp;
				listPlayers.Add(up);
			}
			SaveData(listPlayers);
			return listPlayers;
		}

		/// <summary>
		/// 抽卡,指定位置(0门将，1后卫，2中场，3前锋)和品质
		/// </summary>
		/// <param name="num">需要抽取的数量&位置&品质（不指定品质则为0）</param>
		/// <returns>抽取的球员卡</returns>
		[Authorize]
		[HttpGet("w/{num&wz&pz}")]
		public List<Userplayers> GetByWeizhi(string numpz)
		{
			if (numpz.IndexOf('&') < 1)
			{
				return null;
			}

			int num = 0;
			int pz = 0;
			int wz = 0;
			string[] strArr = numpz.Split('&');
			if (strArr.Length != 3)
			{
				return null;
			}
			if (!int.TryParse(strArr[0], out num))
				return null;
			if (!int.TryParse(strArr[1], out wz))
				return null;
			if (!int.TryParse(strArr[2], out pz))
				return null;


			if (num < 1)
			{
				return null;
			}

			List<int> pidList = ChouCardByWeizhi(num,wz);
			if (pidList == null || pidList.Count == 0)
			{
				return null;
			}

			List<Userplayers> listPlayers = new List<Userplayers>();
			for (int i = 0; i < num; i++)
			{
				Dicplayers dp = _context.Dicplayers.Find(pidList[i]);
				if (dp == null)
				{
					continue;
				}

				Userplayers up = new Userplayers();
				up.Id = 0;
				up.Age = (sbyte)RandomHelper.GetInt32(16, 26);
				up.Jx = 0;
				up.Lv = 1;
				up.Pz = pz == 0 ? RandomHelper.GetInt32(1, 5) : pz;//如果品质参数为0，则需要随机品质
				up.Name = dp.名字;
				up.Pid = dp.Id;
				up.Rowtime = DateTime.Now;
				up.Status = 0;
				up.Type = (sbyte)RandomHelper.GetInt32(0, 2);
				up.Uid = User.Identity.Name;
				//up.P = dp;
				listPlayers.Add(up);
			}
			SaveData(listPlayers);
			return listPlayers;
		}
		/// <summary>
		/// 生成系统球队模板，新玩家创建球队从其中随机其一
		/// </summary>
		/// <param name="num"></param>
		[HttpGet("g/{num}")]
		public void Generate(int num)
		{
			List<Userplayers> list = new List<Userplayers>();
			
			for (int pid = 1; pid < 12; pid++)
			{
				//11个巨星，每个球星搭配10个队友
				Dicplayers dpJuxing = _context.Dicplayers.Find(pid);
				if (dpJuxing == null)
				{
					return;
				}

				#region 各位置数量
				//442阵型，各位置球员数量
				int fcount = 2;//前锋数量
				int mcount = 4;
				int dcount = 4;
				int gkcount = 1;
				switch (dpJuxing.大位置)
				{
					case 0:
						gkcount--;
						break;
					case 1:
						dcount--;
						break;
					case 2:
						mcount--;
						break;
					case 3:
						fcount--;
						break;
					default: break;
				}
				#endregion

				#region 循环生成
				for (int i = 0; i < num; i++)
				{
					List<int> pidList = new List<int>();//球员id拼接字符串
					pidList.Add(dpJuxing.Id);					
					
					//生成前锋
					for (int j = 0; j < fcount; j++)
					{
						int fid = RandomHelper.GetInt32(1, PlayerFCount);
						if (pidList.IndexOf(fid) >= 0)//检查是否有重复球员
						{
							for (int k = 0; k < 100; k++)//最多循环100次，还有相同的直接return
							{
								fid = RandomHelper.GetInt32(1, PlayerFCount);
								if (pidList.IndexOf(fid) < 0)
									break;
							}
							if (pidList.IndexOf(fid) >= 0)
							{
								return;
							}
						}
						pidList.Add(fid);
					}
					for (int j = 0; j < mcount; j++)//生成中场
					{
						int fid = RandomHelper.GetInt32(1, PlayerMCount);
						if (pidList.IndexOf(fid) >= 0)//检查是否有重复球员
						{
							for (int k = 0; k < 100; k++)//最多循环100次，还有相同的直接return
							{
								fid = RandomHelper.GetInt32(1, PlayerMCount);
								if (pidList.IndexOf(fid) < 0)
									break;
							}
							if (pidList.IndexOf(fid) >= 0)
							{
								return;
							}
						}
						pidList.Add(fid);
					}
					for (int j = 0; j < dcount; j++)//生成后卫
					{
						int fid = RandomHelper.GetInt32(1, PlayerDCount);
						if (pidList.IndexOf(fid) >= 0)//检查是否有重复球员
						{
							for (int k = 0; k < 100; k++)//最多循环100次，还有相同的直接return
							{
								fid = RandomHelper.GetInt32(1, PlayerDCount);
								if (pidList.IndexOf(fid) < 0)
									break;
							}
							if (pidList.IndexOf(fid) >= 0)
							{
								return;
							}
						}
						pidList.Add(fid);
					}
					if (gkcount > 0)//生成门将
					{
						int fid = RandomHelper.GetInt32(1, PlayerGKCount);
						if (pidList.IndexOf(fid) >= 0)//检查是否有重复球员
						{
							for (int k = 0; k < 100; k++)//最多循环100次，还有相同的直接return
							{
								fid = RandomHelper.GetInt32(1, PlayerGKCount);
								if (pidList.IndexOf(fid) < 0)
									break;
							}
							if (pidList.IndexOf(fid) >= 0)
							{
								return;
							}
						}
						pidList.Add(fid);
					}

					//拼接字符串
					StringBuilder pidStr = new StringBuilder();
					for (int li = 0; li < 11; li++)
					{
						pidStr.Append(pidList[li]);
						if (li < 10)
							pidStr.Append(',');
					}
					string pzStr = "5,5,5,5,5,5,5,5,5,5,5";
					Teamplayersrandom tpr = new Teamplayersrandom();
					tpr.Id = 0;
					tpr.PlayerIdString = pidStr.ToString();
					tpr.PzString = pzStr;
					_context.Teamplayersrandom.Add(tpr);
				}
				#endregion
			}

			_context.SaveChanges();
		}

		/// <summary>
		/// 把抽取到的球员卡保存到数据库
		/// </summary>
		/// <param name="listPlayers">抽取到的球员列表</param>
		private void SaveData(List<Userplayers> listPlayers)
		{
			foreach (Userplayers up in listPlayers)
			{
				//up.P = null;
				_context.Userplayers.Add(up);
			}
			_context.SaveChanges();
		}
		/// <summary>
		/// 总球员库里抽卡
		/// </summary>
		/// <param name="num">需要抽取的数量</param>
		/// <returns>抽取的球员卡</returns>
		private List<int> ChouCard(int num)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < num; i++)
			{
				int pid = RandomHelper.GetInt32(1, PlayerCount);
				list.Add(pid);
			}
			return list;
		}
		/// <summary>
		/// 根据位置进行抽卡(0门将，1后卫，2中场，3前锋)
		/// </summary>
		/// <param name="num">所需要的数量</param>
		/// <returns>抽取的球员卡</returns>
		private List<int> ChouCardByWeizhi(int num,int weizhi)
		{
			if (weizhi > 3 || weizhi < 0)
				return null;
			if (num < 0 || num > 100)
				return null;
			List<int> list = new List<int>();			
			
			for (int i = 0; i < num; i++)
			{
				int randomId = 0;
				int playerId = 0;
				switch (weizhi)
				{
					case 0:
						randomId = RandomHelper.GetInt32(1, PlayerGKCount);
						playerId = _context.Dicplayersgkrelation.Find(randomId).PlayerId;
						break;
					case 1:
						randomId = RandomHelper.GetInt32(1, PlayerDCount);
						playerId = _context.Dicplayersdrelation.Find(randomId).PlayerId;
						break;
					case 2:
						randomId = RandomHelper.GetInt32(1, PlayerMCount);
						playerId = _context.Dicplayersmrelation.Find(randomId).PlayerId;
						break;
					case 3:
						randomId = RandomHelper.GetInt32(1, PlayerFCount);
						playerId = _context.Dicplayersfrelation.Find(randomId).PlayerId;
						break;
					default: break;
				}

				list.Add(playerId);

			}
			return list;
		}


	}
}
