/********************************************************************************
 * 文件名：MatchRule
 * 创建人：
 * 创建时间：2009-11-17 13:54:14
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using Games.NB.Match.BLL.Model;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Log;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Common;
using SkillEngine.Extern;

namespace Games.NB.Match.BLL.Rules
{
    public class MatchRule
    {
        #region 时间回合转换
        /// <summary>
        /// 分钟转换为回合
        /// </summary>
        /// <param name="time"></param>
        /// <param name="totalRound"></param>
        /// <returns></returns>
        public static int ConvertMinute2Round(int minute, int totalRound)
        {
            return totalRound * minute / 90;
        }
        public static int ConvertRound2Minute(int round, int totalRound)
        {
            return (int)(0.99 + (double)round / totalRound * 90);
        } 
        #endregion

        #region 获取离球最近的本方球员
        /// <summary>
        /// 获取离球最近的本方球员
        /// </summary>
        /// <returns>离球最近的本方球员</returns>
        public unsafe static IPlayer GetClosestPlayerFromBallInMySide(IManager manager)
        {
            Coordinate point = manager.Match.Football.Current;

            double* array = stackalloc double[Defines.Match.MAX_PLAYER_COUNT - 1]; // 距离数组
            List<IPlayer> targetArray = new List<IPlayer>(Defines.Match.MAX_PLAYER_COUNT - 1); // 目标的数组

            int count = 0;
            foreach (var p in manager.Players)
            {
                if (p.Input.AsPosition == Position.Goalkeeper)
                    continue;
                if (!p.SkillEnable)
                    continue;
                targetArray.Add(p);
                array[count++] = p.Current.SimpleDistance(point);
            }
            if (count == 0)
            {
                return null;
            }

            return targetArray[Utility.GetDoubleMinIndexQuick(array, count)];
        }
        #endregion

        #region 获取本方某个属性最高的球员
        /// <summary>
        /// 获取本方某个属性最高的球员
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public unsafe static IPlayer GetHighestPropertyPlayer(IManager manager, byte property)
        {
            double* array = stackalloc double[Defines.Match.MAX_PLAYER_COUNT - 1];              // 属性值数组
            List<IPlayer> targetArray = new List<IPlayer>(Defines.Match.MAX_PLAYER_COUNT - 1);  // 球员数组

            int count = 0;
            foreach (var p in manager.Players)
            {
                if (p.Input.AsPosition == Position.Goalkeeper)
                    continue;
                if (!p.SkillEnable)
                    continue;
                targetArray.Add(p);
                array[count++] = p.PropCore[property];    
            }
           
            if (targetArray.Count == 0) // 如果没有满足条件的球员则返回空
            {
                return null;
            }

            return targetArray[Utility.GetDoubleMaxIndexQuick(array, count)];
        }
        #endregion

        #region 获取矩形内随机坐标
        const int MINxy = 10;
        static readonly int MAXx = Defines.Pitch.MAX_WIDTH - 10;
        static readonly int MAXy = Defines.Pitch.MAX_HEIGHT - 10;
        public static Coordinate RandPointInRect(IRandom random,Region region)
        {
            int x = random.RandomInt32((int)region.Start.X, (int)region.End.X);
            int y = random.RandomInt32((int)region.Start.Y, (int)region.End.Y);
            return new Coordinate(x, y);
        }
        public static Coordinate RandPointInRect(IRandom random, Coordinate orig, double minRange, double maxRange)
        {
            int range = (int)(maxRange - minRange);
            if (range <= 0)
                return orig;
            int x = random.RandomInt32(-range, range);
            int y = random.RandomInt32(-range, range);
            if (x <= 0)
                x = Convert.ToInt32(orig.X + x - minRange);
            else
                x = Convert.ToInt32(orig.X + x + minRange);
            if (y <= 0)
                y = Convert.ToInt32(orig.Y + y - minRange);
            else
                y = Convert.ToInt32(orig.Y + y + minRange);
            x = Math.Max(MINxy, Math.Min(MAXx, x));
            y = Math.Max(MINxy, Math.Min(MAXy, y));
            return new Coordinate(x, y);
        }
        #endregion

        #region 获取距离内最近球员
        public unsafe static IPlayer GetNearPlayerInRound(Coordinate orig, IList<IPlayer> targets, double range)
        {
            var list = GetPlayersInRect(orig, targets, range);
            if (null == list)
                return null;
            if (list.Count == 1)
                return list[0];
            double* dists = stackalloc double[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                dists[i] = list[i].Current.SimpleDistance(orig);
            }
            return list[Common.Utility.GetDoubleMinIndexQuick(dists, list.Count)];
        }
        #endregion

        #region 获取距离内所有球员
        public static List<IPlayer> GetPlayersInRect(Coordinate orig, IList<IPlayer> dests, double range)
        {
            range = Math.Abs(range);
            List<IPlayer> list = null;
            Region region = new Region(orig.X - range, orig.Y - range, orig.X + range, orig.Y + range);
            foreach (var p in dests)
            {
                if (!region.IsCoordinateInRegion(p.Current))
                    continue;
                if (null == list)
                    list = new List<IPlayer>(dests.Count);
                list.Add(p);
            }
            return list;
        }
        #endregion

        #region 获取直线距离内坐标
        public static Coordinate GetNearPointInLine(Coordinate orig, Coordinate dest, double range)
        {
            double dist = dest.Distance(orig);
            if (dist <= range)
                return dest;
            double rate = (dist - range) / dist;
            double x = orig.X + (dest.X - orig.X) * rate;
            double y = orig.Y + (dest.Y - orig.Y) * rate;
            x = Math.Max(MINxy, Math.Min(MAXx, x));
            y = Math.Max(MINxy, Math.Min(MAXy, y));
            return new Coordinate(x, y);
        }
        #endregion

    }
}
