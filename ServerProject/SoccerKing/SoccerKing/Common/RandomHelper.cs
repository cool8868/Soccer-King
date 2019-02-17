using System;
using System.Collections.Generic;


namespace SoccerKing.Common
{
	/// <summary>
	/// 随机数生成器
	/// </summary>
	public static class RandomHelper
	{
		private static Random _random = new Random(Seed);
		private static readonly object _locker = new object();

		/// <summary>
		/// 随机数种子
		/// </summary>
		public static int Seed => Guid.NewGuid().GetHashCode();

		/// <summary>
		/// 获取随机数(包含min，但不包含max)
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static int GetInt32WithoutMax(int min, int max)
		{
			if (min == max) return min;
			if (min > max)
			{
				var tmp = max;
				max = min;
				min = tmp;
			}

			lock (_locker)
			{
				return _random.Next(min, max);
			}
		}

		/// <summary>
		/// 获取随机数（包含min和max）
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		/// <returns></returns>
		public static int GetInt32(int min, int max)
		{
			if (min == max) return min;
			if (min > max)
			{
				var tmp = max;
				max = min;
				min = tmp;
			}

			lock (_locker)
			{
				return _random.Next(min, max + 1);
			}
		}

		/// <summary>
		/// 获取随机Boolean型
		/// </summary>
		/// <returns></returns>
		public static bool GetBoolean()
		{
			return GetInt32(0, 1) > 0;
		}

		/// <summary>
		/// 获取0 - 100随机值
		/// </summary>
		/// <returns></returns>
		public static int GetPercentage()
		{
			return GetInt32WithoutMax(0, 100);
		}

		public static bool CheckPercentagePow(int rate)
		{
			if (rate < 1)
				return false;
			if (rate >= 10000)
				return true;
			var random = GetPercentagePow();
			return random < rate;
		}

		public static bool CheckPercentage(int rate)
		{
			if (rate < 1)
				return false;
			if (rate >= 100)
				return true;
			var random = GetPercentage();
			return random < rate;
		}

		public static bool CheckPercentage(double rate)
		{
			if (rate < 1)
				return false;
			var random = GetPercentage();
			return random < rate;
		}

		/// <summary>
		/// 获取0 - 10000随机值
		/// </summary>
		/// <returns></returns>
		public static int GetPercentagePow()
		{
			return GetInt32WithoutMax(0, 10000);
		}

		/// <summary>
		/// 获取随机排列的数组
		/// </summary>
		/// <typeparam name="T">数组的类型</typeparam>
		/// <param name="list">数组</param>
		/// <returns>随机排列后的数组</returns>
		public static List<T> GetRandomTestSortList<T>(List<T> list)
		{
			List<Int32> indexList = new List<int>(list.Count);
			for (Int32 i = 0; i < list.Count; i++)
			{
				indexList.Add(i);
			}

			List<Int32> randomIndex = new List<int>(list.Count);
			while (indexList.Count > 0)
			{
				var index = indexList[GetInt32(0, indexList.Count - 1)];
				randomIndex.Add(index);

				indexList.Remove(index);
			}

			List<T> newList = new List<T>(list.Count);
			foreach (Int32 index in randomIndex)
			{
				newList.Add(list[index]);
			}

			return newList;
		}
	}
}
