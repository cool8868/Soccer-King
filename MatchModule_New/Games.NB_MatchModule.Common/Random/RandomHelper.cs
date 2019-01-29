/*****************************************************************************
 * 文件名：RandomHelper
 * 
 * 创建人：
 * 
 * 创建时间：2009-11-12 11:24:43
 * 
 * 功能说明：
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
using System;
using System.Collections.Generic;

namespace Games.NB.Match.Common.Random
{
    /// <summary>
    /// 随机数生成器
    /// </summary>
    public static class RandomHelper
    {
        private static System.Random _random;
        private static int _seed;
        private static readonly object _locker = new object();


        /// <summary>
        /// 创建一个随机数
        /// </summary>
        public static void Initialize()
        {
            _seed = Guid.NewGuid().GetHashCode();
            _random = new System.Random(_seed);
        }

        /// <summary>
        /// 通过外部种子初始化
        /// </summary>
        /// <param name="seed"></param>
        public static void Initialize(int seed)
        {
            _seed = seed;
            _random = new System.Random(seed);
        }

        /// <summary>
        /// 随机数种子
        /// </summary>
        public static int Seed
        {
            get { return _seed; }
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
        /// 获取随机数（包含min和max）
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static byte GetByte(byte min, byte max)
        {
            if (min < Byte.MinValue) min = Byte.MinValue;
            if (max + 1 > Byte.MaxValue) max = Byte.MaxValue;

            if (min == max) return min;
            if (min > max)
            {
                var tmp = max;
                max = min;
                min = tmp;
            }

            lock (_locker)
            {
                return (byte)_random.Next(min, max + 1);
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
            return GetInt32(0, 100);
        }

        /// <summary>
        /// 获取随机排列的数组
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="list">数组</param>
        /// <returns>随机排列后的数组</returns>
        public static List<T> GetRandomSortList<T>(List<T> list)
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
