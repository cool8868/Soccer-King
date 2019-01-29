/********************************************************************************
 * 文件名：Utility
 * 创建人：
 * 创建时间：2010-2-22 14:03:22
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Common
{

    /// <summary>
    /// Represents the System's utilities.
    /// </summary>
    public static class Utility
    {

        /// <summary>
        /// ToUpper the string's first letter.
        /// 将首字母转化为大写
        /// </summary>
        /// <param name="str"><see cref="string"/></param>
        /// <returns>The <see cref="string"/> that first letter has to upper.</returns>
        public static string UpperFirstLetter(string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1, str.Length - 1);
        }

       
        /// <summary>
        /// Converts a string into a byte array.
        /// 字符串转化为byte的数组
        /// </summary>
        /// <param name="value">Represents the converted string that split by ,</param>
        /// <returns>Byte Array</returns>
        public static byte[] ConvertStringToByteArray(string value)
        {
            var strArray = value.Split(',');
            byte[] array = new byte[strArray.Length];

            for(var i = 0; i < strArray.Length; i++)
            {
                array[i] = byte.Parse(strArray[i]);
            }

            return array;
        }

        /// <summary>
        /// Get the max number's index from the array.
        /// 从列表中获取最大的数
        /// </summary>
        /// <typeparam name="T">ValueType</typeparam>
        /// <param name="array">Represents the array.</param>
        /// <returns>Returns the max number's index.</returns>
        public static int GetMaxIndex<T>(T[] array) where T : IComparable
        {
            T max = array[0];
            int index = 0;

            for(int i = 0; i < array.Length; i++)
            {
                if(array[i].CompareTo(max) > 0)
                {
                    max = array[i];
                    index = i;
                }
            }

            return index;
        }

        /// <summary>
        /// Get the max number from the array.
        /// 从列表中获取最大的数
        /// </summary>
        /// <typeparam name="T">ValueType</typeparam>
        /// <param name="array">Represents the array.</param>
        /// <returns>Returns the max number.</returns>
        public static T GetMax<T>(List<T> array) where T : IComparable
        {
            T max = array[0];

            for(int i = 0; i < array.Count; i++)
            {
                if(array[i].CompareTo(max) > 0)
                {
                    max = array[i];
                }
            }

            return max;
        }

        /// <summary>
        /// A quick function to find out the maximum number's index from a array.
        /// 快速从一个数组中获取最大的数字所在的索引位数
        /// </summary>
        /// <param name="array">Represents the point of the array.</param>
        /// <param name="length">Represents the array's length.</param>
        /// <returns>Returns the maximum value.</returns>
        public unsafe static int GetInt32MaxIndexQuick(int* array, int length)
        {
            int max = array[0];
            int index = 0;

            for(int i = 0; i < length; i++)
            {
                if(array[i] > max)
                {
                    max = array[i];
                    index = i;
                }
            }

            return index;

        }

        /// <summary>
        /// A quick function to find out the max number's index from a array.
        /// 快速从一个数组中获取最大的数字所在的索引位数
        /// </summary>
        /// <param name="array">Represents the point of the array.</param>
        /// <param name="length">Represents the array's length.</param>
        /// <returns>Returns the maximum value.</returns>
        public unsafe static int GetDoubleMaxIndexQuick(double* array, int length)
        {
            double max = array[0];
            int index = 0;

            for(int i = 0; i < length; i++)
            {
                if(array[i] > max)
                {
                    max = array[i];
                    index = i;
                }
            }

            return index;
        }

        /// <summary>
        /// A quick function to find out the min number's index from a array.
        /// 快速从一个数组中获取最小的数字所在的索引位数
        /// </summary>
        /// <param name="array">Represents the point of the array.</param>
        /// <param name="length">Represents the array's length.</param>
        /// <returns>Returns the maximum value.</returns>
        public unsafe static int GetDoubleMinIndexQuick(double* array, int length)
        {
            double min = array[0];
            int index = 0;

            for(int i = 0; i < length; i++)
            {
                if(array[i] < min)
                {
                    min = array[i];
                    index = i;
                }
            }

            return index;
        }

        /// <summary>
        /// A quick function to find out the max number from a array.
        /// 快速从一个数组中获取最大的数字
        /// </summary>
        /// <param name="array">Represents the point of the array.</param>
        /// <param name="length">Represents the array's length.</param>
        /// <returns>Returns the maximum value.</returns>
        public unsafe static double GetDoubleMaxQuick(double* array, int length)
        {
            double max = array[0];

            for(int i = 0; i < length; i++)
            {
                if(array[i] > max)
                {
                    max = array[i];
                }
            }

            return max;
        }

        /// <summary>
        /// A quick function to find out the min number from a array.
        /// 快速从一个数组中获取最小的数字
        /// </summary>
        /// <param name="array">Represents the point of the array.</param>
        /// <param name="length">Represents the array's length.</param>
        /// <returns>Returns the minimum value.</returns>
        public unsafe static double GetDoubleMinQuick(double* array, int length)
        {
            double min = array[0];

            for(int i = 0; i < length; i++)
            {
                if(array[i] < min)
                {
                    min = array[i];
                }
            }

            return min;
        }

        /// <summary>
        /// A quick function to sort a array that will sort from max to min.
        /// 快速将一个数组从大到小进行排序
        /// </summary>
        /// <param name="array">Represents the array's pointer.</param>
        /// <param name="length">Represents the array's length.</param>
        public unsafe static void SortMaxDoubleArrayQuick(double* array, int length)
        {
            for(int i = 0; i < length; i++)
            {
                if(i == 0) continue;

                for(int m = i; m > 0; m--)
                {
                    if(array[m] > array[m - 1])
                    {
                        double tmp = array[m];
                        array[m] = array[m - 1];
                        array[m - 1] = tmp;
                    }
                }
            }
        }

        /// <summary>
        /// A quick function to sort a array and returns the index of raw array.
        /// 快速将一个数组从大到小进行排序，但返回的是排序后的原数据的索引号
        /// </summary>
        /// <param name="array">Represents the array's pointer.</param>
        /// <param name="length">Represents the array's length.</param>
        /// <returns>Returns the sorted array index.</returns>
        public unsafe static int[] SortMaxDoubleArrayIndexQuick(double* array, int length)
        {
            int[] indexes = new int[length];
            for(int i = 0; i < length; i++)
            {
                indexes[i] = i;
            }

            for(int i = 0; i < length; i++)
            {
                if(i == 0) continue;

                for(int m = i; m > 0; m--)
                {
                    if(array[m] > array[m - 1])
                    {
                        double dTmp = array[m];
                        array[m] = array[m - 1];
                        array[m - 1] = dTmp;

                        int intTmp = indexes[m];
                        indexes[m] = indexes[m - 1];
                        indexes[m - 1] = intTmp;
                    }
                }
            }

            return indexes;
        }

        /// <summary>
        /// A quick function to sort a array that will sort from min to max.
        /// 快速将一个数组从小到大进行排序
        /// </summary>
        /// <param name="array">Represents the array's pointer.</param>
        /// <param name="length">Represents the array's length.</param>
        public unsafe static void SortMinDoubleArrayQuick(double* array, int length)
        {
            for(int i = 0; i < length; i++)
            {
                if(i == 0) continue;

                for(int m = i; m > 0; m--)
                {
                    if(array[m] < array[m - 1])
                    {
                        double tmp = array[m];
                        array[m] = array[m - 1];
                        array[m - 1] = tmp;
                    }
                }
            }
        }

        /// <summary>
        /// A quick function to sort a array and returns the index of raw array.
        /// 快速将一个数组从小到大进行排序，但返回的是排序后的原数据的索引号
        /// </summary>
        /// <param name="array">Represents the array's pointer.</param>
        /// <param name="length">Represents the array's length.</param>
        /// <returns>Returns the sorted array index.</returns>
        public unsafe static int[] SortMinDoubleArrayIndexQuick(double* array, int length)
        {
            int[] indexes = new int[length];
            for(int i = 0; i < length; i++)
            {
                indexes[i] = i;
            }

            for(int i = 0; i < length; i++)
            {
                if(i == 0) continue;

                for(int m = i; m > 0; m--)
                {
                    if(array[m] < array[m - 1])
                    {
                        double dTmp = array[m];
                        array[m] = array[m - 1];
                        array[m - 1] = dTmp;

                        int intTmp = indexes[m];
                        indexes[m] = indexes[m - 1];
                        indexes[m - 1] = intTmp;
                    }
                }
            }

            return indexes;
        }

        #region Safe Code
        public static int GetDoubleMinIndexSafe(double[] array, int length)
        {
            double min = array[0];
            int index = 0;

            for (int i = 0; i < length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                    index = i;
                }
            }
            return index;
        }
        public static int GetDoubleMaxIndexSafe(double[] array, int length)
        {
            double max = array[0];
            int index = 0;

            for (int i = 0; i < length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                    index = i;
                }
            }
            return index;
        }
        #endregion
    }
}
