/********************************************************************************
 * 文件名：Utility
 * 创建人：
 * 创建时间：2010-1-28 13:33:56
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
using Games.NB.Match.Base.Structs;

namespace Games.NB.Match.AI.Decides
{

    /// <summary>
    /// Represents the utility methods.
    /// </summary>
    public static class Utility
    {

        /// <summary>
        /// Get a point that is in the line between the player's current <see cref="Coordinate"/> and the target's <see cref="Coordinate"/>, 
        /// which is in the range of the <see cref="IPlayer"/>'s width.
        /// </summary>
        /// <param name="player">Represents the current player.</param>
        /// <param name="target">Represents the target point.</param>
        /// <returns>Represents the <see cref="Coordinate"/> on player's width.</returns>
        public static Coordinate GetWidthPoint(IPlayer player, Coordinate target)
        {
            player.Rotate(target);
            var x = player.Current.X + player.Status.Width * Math.Cos(player.Status.Angle * Math.PI / 180);
            var y = player.Current.Y + player.Status.Width * Math.Sin(player.Status.Angle * Math.PI / 180);
            return new Coordinate(x, y);
        }

        /// <summary>
        /// Get a point that is in the line between the player's current <see cref="Coordinate"/> and the target's <see cref="Coordinate"/>, 
        /// which is in the range.
        /// </summary>
        /// <param name="player">Represents the current <see cref="IPlayer"/>.</param>
        /// <param name="target">Represents the target <see cref="Coordinate"/>.</param>
        /// <param name="range">Represents the range of the player.</param>
        /// <returns>Represents the <see cref="Coordinate"/> on the range.</returns>
        public static Coordinate GetPointWithRange(IPlayer player, Coordinate target, int range)
        {
            player.Rotate(target);
            var x = player.Current.X + range * Math.Cos(player.Status.Angle * Math.PI / 180);
            var y = player.Current.Y + range * Math.Sin(player.Status.Angle * Math.PI / 180);
            return new Coordinate(x, y);
        }

        /// <summary>
        /// Get a point that is the player's defence target.
        /// </summary>
        /// <param name="player">Represents the defender.</param>
        /// <param name="target">Represents the defence target.</param>
        /// <returns></returns>
        public static Coordinate GetDefencePosition(IPlayer player, IPlayer target)
        {
            if (target == null)
            {
                return player.Current;
            }
            Coordinate dest;
            if (player.Match.Football.IsInAir)
            {
                dest = new Region(new Coordinate(target.Current.X - 3, target.Current.Y - 2), new Coordinate(target.Current.X + 2, target.Current.Y + 3)).GetRandomPoint(player.Match);
            }
            else
            {
                if (player.Side == Side.Home)
                {
                    dest = new Region(new Coordinate(target.Current.X - 12, target.Current.Y - 5), new Coordinate(target.Current.X, target.Current.Y + 5)).GetRandomPoint(player.Match);
                }
                else
                {
                    dest = new Region(new Coordinate(target.Current.X, target.Current.Y - 5), new Coordinate(target.Current.X + 12, target.Current.Y + 5)).GetRandomPoint(player.Match);
                }
            }
            return dest.Regulate();
        }

        #region 反盯防，找空档
        /// <summary>
        /// 反盯防，找空挡
        /// </summary>
        /// <param name="player"></param>
        /// <param name="defenders"></param>
        /// <param name="partners"></param>
        /// <returns></returns>
        public unsafe static bool GetAntiDefencePosition(out Coordinate target, IPlayer player, List<IPlayer> defenders, List<IPlayer> partners)
        {
            var random = player.Match;
            target = player.Destination;
            if (null == defenders || defenders.Count == 0)
                return false;
            Coordinate pCur = player.Current;
            const double minXY = 2;
            const double maxX = Base.Defines.Pitch.MAX_WIDTH - 2;
            const double maxY = Base.Defines.Pitch.MAX_HEIGHT - 2;
            double lux, luy, rdx, rdy;
            lux = luy = rdx = rdy = 0;
            double max = 1000d;
            bool isHome = player.Manager.Side == Side.Home;
            var defer = player.Status.DefenceStatus.Defender;
            if (player == player.Manager.Status.HeadMostPlayer)
            {
                if (null == defer)
                    return false;
                Coordinate pDef = defer.Current;
                double difx = pCur.X - pDef.X;
                double dify = pCur.Y - pDef.Y;
                if (difx < 0 && dify < 0)
                {
                    lux = Math.Max(minXY, pCur.X - 10);
                    rdx = Math.Max(minXY, pCur.X - 2);
                    luy = Math.Max(minXY, pCur.Y - 15);
                    rdy = Math.Max(minXY, pCur.Y - 5);
                }
                else if (difx < 0 && dify > 0)
                {
                    lux = Math.Max(minXY, pCur.X - 10);
                    rdx = Math.Max(minXY, pCur.X - 2);
                    luy = Math.Min(maxY, pCur.Y + 5);
                    rdy = Math.Min(maxY, pCur.Y + 15);
                }
                else if (difx > 0 && dify > 0)
                {
                    lux = Math.Min(maxX, pCur.X + 2);
                    rdx = Math.Min(maxX, pCur.X + 10);
                    luy = Math.Min(maxY, pCur.Y + 5);
                    rdy = Math.Min(maxY, pCur.Y + 15);
                }
                else
                {
                    lux = Math.Min(maxX, pCur.X + 2);
                    rdx = Math.Min(maxX, pCur.X + 10);
                    luy = Math.Max(minXY, pCur.Y - 15);
                    rdy = Math.Max(minXY, pCur.Y - 5);
                }
                target = new Region(new Coordinate(lux, luy), new Coordinate(rdx, rdy)).GetRandomPoint(random);
                return true;
            }
            double* arrayAss = stackalloc double[partners.Count];
            double* arrayAssX = stackalloc double[partners.Count];
            double curX = player.Current.X;
            for (int i = 0; i < partners.Count; i++)
            {
                arrayAss[i] = max;
                arrayAssX[i] = 0;
                if (player == partners[i])
                    continue;
                if (partners[i].Disable)
                    continue;
                if (partners[i].Input.AsPosition == Position.Goalkeeper)
                    continue;
                arrayAss[i] = player.Current.SimpleDistance(partners[i].Current);
                arrayAssX[i] = partners[i].Current.X - curX;
            }
            int idx2 = Common.Utility.GetDoubleMinIndexQuick(arrayAss, partners.Count);
            var asser = partners[idx2];
            Coordinate pAss = asser.Current;
            double lnxt = -1000d;
            double rnxt = 1000d;
            double lnxt2 = 0;
            double rnxt2 = 0;
            for (int i = 0; i < partners.Count; i++)
            {
                if (arrayAssX[i] == 0)
                    continue;
                if (arrayAssX[i] < 0)
                {
                    if (arrayAssX[i] > lnxt)
                    {
                        if (lnxt2 == 0)
                            lnxt2 = arrayAssX[i];
                        else
                            lnxt2 = lnxt;
                        lnxt = arrayAssX[i];
                    }
                }
                else if (arrayAssX[i] > 0)
                {
                    if (arrayAssX[i] < rnxt)
                    {
                        if (rnxt2 == 0)
                            rnxt2 = arrayAssX[i];
                        else
                            rnxt2 = rnxt;
                        rnxt = arrayAssX[i];
                    }
                }
            }
            if (isHome)
            {
                if (rnxt2 == 0 || lnxt >= -20 && (lnxt + lnxt2) >= -35)
                    return false;
                lux = Math.Max(minXY, pCur.X - 15);
                rdx = Math.Max(minXY, pCur.X - 10);
            }
            else
            {
                if (lnxt2 == 0 || rnxt <= 20 && (rnxt + rnxt2) <= 35)
                    return false;
                lux = Math.Min(maxX, pCur.X + 10);
                rdx = Math.Min(maxX, pCur.X + 15);
            }
            double dify1 = 0;
            if(null!=defer)
                dify1 = pCur.Y - defer.Current.Y;
            double dify2 = pCur.Y - pAss.Y;
            if (dify2 > 0)
            {
                rdy = Math.Min(maxY, pCur.Y + 15);
                if (dify1 > 0)
                    luy = Math.Min(maxY, pCur.Y + 5);
                else if (dify1 < 0)
                    luy = Math.Min(maxY, pCur.Y - 5);
            }
            else
            {
                luy = Math.Max(minXY, pCur.Y - 15);
                if (dify1 > 0)
                    rdy = Math.Min(maxY, pCur.Y + 5);
                else if (dify1 < 0)
                    rdy = Math.Min(maxY, pCur.Y - 5);
            }
            target = new Region(new Coordinate(lux, luy), new Coordinate(rdx, rdy)).GetRandomPoint(random);
            return true;
        }
        #endregion

        #region 单刀
        public static bool IfSolo(IPlayer player)
        {
            if (null == player)
                return false;
            bool isHome = player.Manager.Side == Side.Home;
            double pX = player.Current.X;
            double opX = player.Manager.Opponent.Status.LastPlayer.Current.X;
            if (isHome && pX >= opX
                || !isHome && pX <= opX)
                return true;
            double tpx = player.Manager.Status.HeadMostPlayer.Current.X;
            if (Math.Abs(pX - tpx) > 10)
                return false;
            var defender = player.Status.DefenceStatus.Defender;
            var defender2 = player.Status.DefenceStatus.HelpDefender;
            if (null == defender && null == defender2)
                return false;
            double defenderDif = player.Status.DefenceStatus.DefenderDistancePow;
            double defenderDif2 = player.Status.DefenceStatus.HelpDefenderDistancePow;
            if (defenderDif >= Defines.Player.DEFENCE_RANGESoloPow)
                return true;
            if (isHome)
            {
                if (defender.Current.X < player.Current.X)
                {
                    if (defender2.Current.X < player.Current.X || defenderDif2 >= Defines.Player.DEFENCE_RANGEPow)
                        return true;
                }
            }
            else
            {
                if (defender.Current.X > player.Current.X)
                {
                    if (defender2.Current.X > player.Current.X || defenderDif2 >= Defines.Player.DEFENCE_RANGEPow)
                        return true;
                }
            }
            return false;
        }
        public static Coordinate GetSoloPosition(IPlayer player)
        {
            bool isHome = player.Manager.Side == Side.Home;
            double minXY = 1;
            double maxX = Base.Defines.Pitch.MAX_WIDTH - 1;
            double maxY = Base.Defines.Pitch.MAX_HEIGHT - 1;
            double lux, luy, rdx, rdy;
            Coordinate pCur = player.Current;
            if (isHome)
            {
                lux = Math.Min(maxX, pCur.X + 3);
                rdx = Math.Min(maxX, lux + 2);
            }
            else
            {
                rdx = Math.Max(minXY, pCur.X - 3);
                lux = Math.Max(minXY, rdx - 2);
            }
            if (pCur.Y <= 28)
            {
                luy = Math.Max(minXY, pCur.Y - 1);
                rdy = Math.Min(maxY, pCur.Y + 6);
            }
            else if (pCur.Y >= 108)
            {
                luy = Math.Max(minXY, pCur.Y - 6);
                rdy = Math.Min(maxY, pCur.Y + 1);
            }
            else
            {
                luy = Math.Max(minXY, pCur.Y - 5);
                rdy = Math.Min(maxY, pCur.Y + 5);
            }
            var defer = player.Status.DefenceStatus.Defender;
            if (null != defer && !player.Status.IsWinger
                && player.Status.DefenceStatus.DefenderDistancePow <= Defines.Player.DEFENCE_RANGESoloPow)
            {
                if (luy <= defer.Current.Y && defer.Current.Y <= rdy)
                {
                    if (defer.Current.Y <= pCur.Y)
                    {
                        luy = Math.Min(maxY, defer.Current.Y + 2);
                        rdy = Math.Min(maxY, luy + 5);
                    }
                    else
                    {
                        rdy = Math.Max(minXY, defer.Current.Y - 2);
                        luy = Math.Max(minXY, rdy - 5);
                    }
                }
            }
            return new Region(new Coordinate(lux, luy), new Coordinate(rdx, rdy)).GetRandomPoint(player.Match);
        }
        #endregion

        #region 突破
        public static bool IfRush(IPlayer player)
        {
            if (null == player.Status.DefenceStatus.Defender
               || player.Status.DefenceStatus.DefenderDistancePow > Defines.Player.DEFENCE_RANGERushPow)
                return true;
            if (!player.Status.IsWinger)
                return false;
            if (player.Input.AsPosition != Position.Fullback)
                return false;
            bool homeFlag = player.Manager.Side == Side.Home;
            if (homeFlag && player.Current.X >= 105)
                return true;
            if (!homeFlag && player.Current.X <= 105)
                return true;
            return false;
        }
        public static Coordinate GetRushPosition(IPlayer player)
        {
            bool isHome = player.Manager.Side == Side.Home;
            double minXY = 1;
            double maxX = Base.Defines.Pitch.MAX_WIDTH - 1;
            double maxY = Base.Defines.Pitch.MAX_HEIGHT - 1;
            double lux, luy, rdx, rdy;
            Coordinate pCur = player.Current;
            if (isHome)
            {
                lux = Math.Min(maxX, pCur.X + 3);
                rdx = Math.Min(maxX, lux + 3);
            }
            else
            {
                rdx = Math.Max(minXY, pCur.X - 3);
                lux = Math.Max(minXY, rdx - 3);
            }
            luy = Math.Max(minXY, pCur.Y - 3);
            rdy = Math.Min(maxY, pCur.Y + 3);
            return new Region(new Coordinate(lux, luy), new Coordinate(rdx, rdy)).GetRandomPoint(player.Match);
        }
        #endregion
    }
}
