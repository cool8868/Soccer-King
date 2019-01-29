/********************************************************************************
 * 文件名：PlayerRule
 * 创建人：
 * 创建时间：2009-11-24 15:23:06
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the rules for the <see cref="IPlayer"/>.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Model;
using Games.NB.Match.AI.States;
using SkillEngine.Extern;

namespace Games.NB.Match.BLL.Rules
{
    /// <summary>
    /// Represents the rules for the <see cref="IPlayer"/>.
    /// 封装了球员的业务规则
    /// </summary>
    public static class PlayerRule
    {
        /// <summary>
        /// Get the goal keeper's move region.
        /// 守门员的移动区域
        /// </summary>
        /// <param name="position"><see cref="Position"/></param>
        /// <param name="side"><see cref="Side"/></param>
        /// <returns><see cref="Region"/></returns>
        public static Region GetGoalKeeperMoveRegion(Position position, Side side)
        {
            return (side == Side.Home) ? Region.ParseByStr(Defines.Pitch.HOME_PENALTY_AREA) : Region.ParseByStr(Defines.Pitch.AWAY_PENALTY_AREA);
        }
        /// <summary>
        /// 获取球员活跃区域
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static Region GetActiveRegion(bool xPlusFlag, Region moveRegion)
        {
            var region = moveRegion;
            if (xPlusFlag)
                region.End.X += 30;
            else
                region.Start.X -= 30;
            return region;
        }
        /// <summary>
        /// Get the player's shoot region.
        /// 球员的射门区域，将赋值给球员的Status中
        /// </summary>
        /// <returns></returns>
        public static Region GetPlayerShootRegion(double strength, Side side)
        {
            // （60+力量属性/5）
            //var region = new Region(0, 20, (int)(20 + strength / 5), Defines.Pitch.MAX_HEIGHT - 20); // away side region
            // 30+力量属性/5
            var region = new Region(0, 20, (int)(25 + Math.Pow(strength,0.5)), Defines.Pitch.MAX_HEIGHT - 20); 
            return (side == Side.Home) ? region.Mirror() : region;
        }

        /// <summary>
        /// Initializes the player's pass targets.
        /// 初始化球员的可传球列表
        /// </summary>
        /// <param name="player">Current <see cref="IPlayer"/>.</param>
        public static void InitPassTarget(IPlayer player)
        {
            player.PassTargets.AddRange(player.Manager.Players);
            player.PassTargets.Remove(player);            
        }

        /// <summary>
        /// Get player's real speed.
        /// 获取球员的速度
        /// <remarks>While player in different status will have different speed.</remarks>
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        /// <param name="speed">Current speed.</param>
        /// <returns>Real speed.</returns>
        public static double GetSpeed(IPlayer player, double speed)
        {
            var max = player.Status.MaxSpeed;
            var maxSpeed = max;

            if (player.Status.Hasball == false)
            {
                if (player.Status.DefenceStatus.DefenceTarget != null) // 存在防守人
                {
                    maxSpeed = max;
                }
                else // 没有防守人
                {
                    maxSpeed = max * 0.9;
                }
                if (player.Status.State != null
                    && player.Status.State.ClientId == WalkState.Instance.ClientId)
                {
                    maxSpeed = maxSpeed * 0.5;
                }
            }
            else if (player.Status.Holdball) // 持球只能以90%的最高速度
            {
                //带球0.8
                maxSpeed = max * 0.9;
            }

            if (speed > maxSpeed)
            {
                return maxSpeed;
            }

            return speed;
        }

        /// <summary>
        /// Get player's max speed.
        /// 获取球员的最大速度
        /// </summary>
        /// <param name="speed">Represents the player's speed property.</param>
        /// <returns>Player's max speed.</returns>
        public static double GetMaxSpeed(double speed)
        {
            // (10 + (x-100) / 50) * 2
            //return (10 + (speed - 100) / 50) * 2;
            return 12 + speed * 2 / 100;
        }

        /// <summary>
        /// Get a player's acceleration by speed property point and the acceleration property point.
        /// 获取球员的加速度
        /// </summary>
        /// <param name="speed">Speed attribute</param>
        /// <param name="acceleration">Acceleration attribute</param>
        /// <returns>The player's acceleration.</returns>
        public static double GetAcceleration(double speed, double acceleration)
        {
            return GetMaxSpeed(speed) / Defines.Player.PLAYER_ACCELERATION[GetAccelerationIndex(acceleration)];
        }
        /// <summary>
        /// Get the pass action's offset coordinate.
        /// 获取球员的传球偏差
        /// </summary>
        /// <param name="passProperty">The player's pass property.</param>
        /// <param name="target">Represents the pass target's <see cref="Coordinate"/>.</param>
        /// <returns>The offset <see cref="Coordinate"/>.</returns>
        public static Coordinate GetPassOffset(IRandom random,IPlayer passFrom, Coordinate target)
        {
            int min = 10;
            int max = 30;
            max = (int)(max - 0.1 *  passFrom.PropCore[PlayerProperty.Passing]);

            if (max <= min) // 没有偏差
            {
                return target;
            }
            else // 有偏差
            {
                double offset = random.RandomInt32(min, max);
                if (offset < min)
                {
                    offset = min;
                }
                double x = 0;
                if ( target.X - passFrom.Current.X > 0)
                    x = random.RandomInt32(Convert.ToInt32(target.X + min), Convert.ToInt32(target.X + offset));
                else
                    x = random.RandomInt32(Convert.ToInt32(target.X - offset), Convert.ToInt32(target.X - min));
                var y = random.RandomInt32(Convert.ToInt32(target.Y - offset), Convert.ToInt32(target.Y + offset));
                return new Coordinate(x, y);
            }
        }

        public static int GetShootTargetIndex(IRandom random, double property, IPlayer player, double dist, double coord)
        {
            double a = 20;
            double b = 25 * 25 * (property / 100);
            double c = 25 * Math.Pow(property / 100, 2);
            double d = 25 * Math.Pow(property / 100, 3);
            double z = a + b + c + d;

            double adj = 0;
            if (coord >= 45)
            {
                if (coord < 70)
                    adj += coord * 0.5;
                else if (coord > 70)
                    adj += 35 + (coord - 70) * 1;
            }
            adj = Math.Max(20, 100 - adj)/100;
            b = b * adj;
            c = c * adj;
            d = d * adj;
            a = z - b - c - d;
            var rnd = random.RandomInt32(0, Convert.ToInt32(z));
            if (rnd <= a)
                return 4;
            else if (rnd <= a + b)
                return 3;
            else if (rnd <= a + b + c)
                return 2;
            else if (rnd <= z)
                return 1;
            return 4;
        }

        #region Old
        ///// <summary>
        ///// Gets the shoot action target's index.
        ///// 获取射门的目标区域编号
        ///// </summary>
        ///// <param name="property">属性值</param>
        ///// <param name="player">射门的球员</param>
        ///// <param name="fix">修正值</param>
        ///// <returns>返回目标区域编号</returns>
        //public static int GetShootTargetIndex(IRandom random, double property, IPlayer player, double fix)
        //{
        //    //Coordinate target = (player.Side == Side.Home) ? player.Match.Pitch.AwayGoal : player.Match.Pitch.HomeGoal;
        //    //if (player.Current.Distance(target) > 70)
        //    //{
        //    //    if (RandomHelper.GetPercentage() < 30)
        //    //    {
        //    //        return 4;
        //    //    }
        //    //}            

        //    // r=rnd(25 * （x / 100 ）^3 + 25 * (x / 100)^2+ 25 * (x / 100) + fix)
        //    var r = random.RandomInt32(0, Convert.ToInt32((property / 4) * Math.Pow(property / 100, 3) + 25 * Math.Pow(property / 100, 2) + 25 * (property / 100) + fix));
            
        //    if (r >= 0 && r <= fix)
        //    {
        //        return 4;
        //    }

        //    if (r > fix && r <= 25 * (property / 100) + fix)
        //    {
        //        return 3;
        //    }

        //    // 25(x/100)+10<r≤15(x/100)2+25(x/100)+10
        //    if (r > 25 * (property / 100) + fix && r <= 25 * Math.Pow(property / 100, 2) + 25 * (property / 100) + fix)
        //    {
        //        return 2;
        //    }

        //    // 25(x/100)2+25(x/100)+10<r≤25（x/100）3+25(x/100)2+25(x/100)+10
        //    if (r > 25 * Math.Pow(property / 100, 2) + 25 * (property / 100) + fix &&
        //        r <= Convert.ToInt32((property / 4) * Math.Pow(property / 100, 3) + 25 * Math.Pow(property / 100, 2) + 25 * (property / 100) + fix))
        //    {
        //        return 1;
        //    }
        //    return 4;

            
        //}
        #endregion

        #region encapsulation

        private static int GetAccelerationIndex(double acceleration)
        {
            if (acceleration >= 0 && acceleration < 40)
            {
                return 0;
            }

            if (acceleration >= 40 && acceleration < 80)
            {
                return 1;
            }

            if (acceleration >= 80 && acceleration < 120)
            {
                return 2;
            }

            if (acceleration >= 120 && acceleration < 160)
            {
                return 3;
            }

            return 4;
        }

        #endregion
    }
}
