/********************************************************************************
 * 文件名：PassTargetDecideRule
 * 创建人：
 * 创建时间：4/23/2010 2:21:21 PM
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
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Common;
using Games.NB.Match.Common.Random;

namespace Games.NB.Match.BLL.Rules
{
    /// <summary>
    /// Represents a class that encapsulated the rules of the decids pass target.
    /// 表示了封装选择传球目标逻辑的类
    /// </summary>
    static class PassTargetDecideRule
    {
        /// <summary>
        /// Decides the passing target.
        /// 决定传球的目标
        /// </summary>
        /// <param name="player">Represents the player who needs to decide a pass target.</param>
        /// <returns>Returns the pass target.</returns>
        public static IPlayer Decide(IPlayer player)
        {
            // 如果该球员为最前的球员，不允许带球
            if (player.ClientId == player.Manager.Status.HeadMostPlayer.ClientId)
            {
                return null;
            }

            return InternalDecide(player);
        }

        /// <summary>
        /// Decides the passing target and force the ball handler must decides a target.
        /// 强制传球选择
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public unsafe static IPlayer ForcePassDecide(IPlayer player)
        {
            IPlayer target = InternalDecide(player);

            if (target != null)
            {
                return target;                
            }
            else
            {
                List<IPlayer> array = new List<IPlayer>(player.PassTargets.Count);
                double* targets = stackalloc double[player.PassTargets.Count];
                int length = 0;
                foreach (var p in player.PassTargets)
                {
                    if (!p.SkillEnable)
                        continue;
                    if (p.Input.AsPosition == Position.Goalkeeper)
                        continue;
                    array.Add(p);
                    targets[length++] = p.Current.SimpleDistance(player.Match.Football.Current);
                }
                if (length == 0)
                    return null;
                return array[Utility.GetDoubleMinIndexQuick(targets, length)];
            }
        }

        /// <summary>
        /// 传给最近的球员
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public unsafe static IPlayer PassClosest(IPlayer player)
        {
            double* distances = stackalloc double[Defines.Match.MAX_PLAYER_COUNT];
            List<IPlayer> array = new List<IPlayer>(Defines.Match.MAX_PLAYER_COUNT);
            int length = 0;
            foreach (var p in player.Manager.Players)
            {
                if (p == player)
                    continue;
                if (!p.SkillEnable)
                    continue;
                array.Add(p);
                distances[length++] = p.Current.SimpleDistance(player.Current);
            }
            if (array.Count == 0)
            {
                return player;
            }
            else
            {
                return array[Utility.GetDoubleMinIndexQuick(distances, array.Count)];
            }
        }

        /// <summary>
        /// 守门员开球门球
        /// </summary>
        /// <param name="player">发球的守门员</param>
        /// <returns>发球的目标</returns>
        public static IPlayer GoalKickDecide(IPlayer player)
        {
            var random = player.Match;
            List<IPlayer> array = new List<IPlayer>(7);
            foreach (var p in player.Manager.Players)
            {
                if (p == player || p.Current.X == player.Manager.Status.HeadMostPlayer.Current.X)
                    continue;
                if (!p.SkillEnable)
                    continue;

                if (p.Input.AsPosition == Position.Midfielder)
                {
                    if (random.RandomPercent() < 70)//if (random.RandomPercent() < 70)
                        array.Add(p);
                }
                else if (p.Input.AsPosition == Position.Forward)
                {
                    if (random.RandomPercent() < 30)
                        array.Add(p);
                }
                else
                {
                    array.Add(p);
                }
            }
            if (array.Count == 0)
            {
                return null;
            }

            return array[random.RandomInt32(0, array.Count - 1)];
        }

        /// <summary>
        /// Decides the open ball round's pass target.
        /// 决定开球回合的传球目标
        /// </summary>
        /// <param name="player">Represents the player who needs to decide a pass target.</param>
        /// <returns>Returns the pass target.</returns>
        public static IPlayer OpenballDecide(IPlayer player)
        {
            var random = player.Match;
            // Random the target list for avoid always pass to the same player.
            const int len=Defines.Match.MAX_PLAYER_COUNT;
            int pre = 0;
            int post = len - 1;
            IPlayer[] targets = new IPlayer[len];
            List<IPlayer> midArray = player.Manager.GetPlayersByPosition(Position.Midfielder);
            foreach (IPlayer p in midArray)
            {
                if (p.Disable) continue;
                if (p.Current.X == player.Current.X) continue;
                if (!p.Status.IsWinger)
                    targets[pre++] = p;
                else
                    targets[post--] = p;
            }

            if (pre > 0)
            {
                return targets[random.RandomInt32(0, pre - 1)];
            }
            if (post < len - 1)
            {
                return targets[random.RandomInt32(post + 1, len - 1)];
            }

            foreach (IPlayer p in player.Manager.Players)
            {
                if (p.Disable) continue;
                if (p.Input.AsPosition == Position.Goalkeeper) continue;
                if (p.Current.X == player.Current.X) continue;
                if (pre >= len)
                    break;
                targets[pre++] = p;
            }

            if (pre == 0)
            {
                foreach (IPlayer p in player.Manager.Opponent.Players)
                {
                    if (p.Disable) continue;
                    if (p.Input.AsPosition == Position.Goalkeeper) continue;
                    if (pre >= len)
                        break;
                    targets[pre++] = p;
                }
            }

            if (pre == 0)
            {
                return player.Manager[0];
            }

            return targets[random.RandomInt32(0, pre - 1)];
        }

        #region encapsulation

        /// <summary>
        /// The internal logic of the decide pass target.
        /// 选择传球人的内部实现
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private unsafe static IPlayer InternalDecide(IPlayer player)
        {
            var random = player.Match;
            List<IPlayer> passTargets = new List<IPlayer>(player.PassTargets.Count); // 可传球员的数组
            double* distanceArray = stackalloc double[player.PassTargets.Count];     // 可传球员离自己的防守人的距离            
            const double MINDefRangePow = 7 * 7;

            #region 将所有球员和他的防守距离加入至内存中
            for (int i = 0; i < player.PassTargets.Count; i++)
            {
                if (player.PassTargets[i] == player) continue;

                if (player.PassTargets[i].Disable)
                {
                    continue;
                }

                if (player.Match.Football.LastTouchMan != null)
                {
                    if (player.PassTargets[i].ClientId == player.Match.Football.LastTouchMan.ClientId)
                    {
                        continue;
                    }
                }

                // 插入距离数组中
                if (player.IsCoordinateEnablePass(player.PassTargets[i].Current))
                {
                    if (player.Current.SimpleDistance(player.PassTargets[i].Current) >= Defines.Player.PASS_MAX_RANGEPow)
                    {
                        continue;
                    }

                    //if (player.PassTargets[i].Status.DefenceStatus.DefenderDistancePow < MINDefRangePow)
                    //{
                    //    continue;
                    //}

                    passTargets.Add(player.PassTargets[i]);
                    distanceArray[passTargets.Count - 1] = player.PassTargets[i].Status.DefenceStatus.DefenderDistancePow;
                }
            }
            #endregion

            if (passTargets.Count == 0) // 无人可以传
            {
                return null;
            }

            //一定概率优先传给无人盯防的前锋或边路球员
            List<IPlayer> preWingsList = null;
            if (random.RandomPercent() < Defines.Player.PASS_GAP_PERCENTAGE)
                preWingsList = new List<IPlayer>(10);
            // 优先传给无球人
            List<IPlayer> nonDefenderArray = new List<IPlayer>(10);
            IPlayer target = null;
            for (int i = 0; i < passTargets.Count; i++)
            {
                target = passTargets[i];
                if (target.Status.DefenceStatus.DefenderDistancePow < MINDefRangePow)
                    continue;
                if (null != preWingsList)
                {
                    if (target.Input.AsPosition == Base.Enum.Position.Forward
                        || target.Input.AsPosition == Base.Enum.Position.Midfielder && target.Status.IsWinger)
                        preWingsList.Add(passTargets[i]);
                    continue;
                }
                nonDefenderArray.Add(passTargets[i]);
            }
            if (null != preWingsList && preWingsList.Count > 0)
                return preWingsList[random.RandomInt32(0, preWingsList.Count - 1)];
            if (nonDefenderArray.Count > 0)
                return nonDefenderArray[random.RandomInt32(0, nonDefenderArray.Count - 1)];
            // 没有无球人则传给离防守人最远的球员
            return passTargets[Utility.GetDoubleMaxIndexQuick(distanceArray, passTargets.Count)];
        }

        #endregion

        #region 边路传中
        /// <summary>
        /// 边路传中的禁区内目标
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static IPlayer WingCrossDecide(IPlayer player)
        {
            if (!player.Status.IsForceCross)
                return null;
            var crossRegion = (player.Side == Base.Enum.Side.Home) ? player.Match.Pitch.AwayWingCrossRegion : player.Match.Pitch.HomeWingCrossRegion;
            foreach (var p in player.Manager.Players)
            {
                if (p == player)
                    continue;
                if (p.Disable)
                    continue;
                if (p.Input.AsPosition == Position.Goalkeeper)
                    continue;
                if (crossRegion.IsCoordinateInRegion(p.Current))
                    return p;
            }
            return null;
        }
        #endregion
    }
}
