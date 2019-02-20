using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerKing.Models
{
	public class SignPlayer
	{
		/// <summary>
		/// 球员Id
		/// </summary>
		public int PlayerId { get; set; }
		/// <summary>
		/// 还价次数，降价负数，涨价正数，不变为0；还价降低签约成功率，涨价提高签约成功率
		/// </summary>
		public int PriceChanges { get; set; }
		/// <summary>
		/// 违约金改动次数，降低违约金为负数，增加违约金为正数，不变为0；增加违约金会降低签约成功率
		/// </summary>
		public int WeiYueJinChanges { get; set; }
	}
}
