/********************************************************************************
 * 文件名：PositionalDecide
 * 创建人：
 * 创建时间：2009-12-21 13:25:17
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
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Common.Random;

namespace Games.NB.Match.AI.Decides
{

    /// <summary>
    /// Represents the positional state's decide.
    /// </summary>    
    public class PositionalDecide : IPositionalDecide
    {

        /// <summary>
        /// Decide the player's destination.
        /// </summary>
        /// <param name="player">Represents the current player.</param>
        /// <returns>The player's destination.</returns>        
        public Coordinate DecideTarget(IPlayer player)
        {
            player.DecideEnd();
            return (player.Status.IsAttackSide) ? OffenseSideDecide(player) : DefenceSideDecide(player);
        }

        /// <summary>
        /// 防守人决策自己的防守目标
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public virtual Coordinate DefenceSideDecide(IPlayer player)
        {
            if (player.Match.Status.IsNoBallHandler)
            {
                return player.Match.Football.Current;
            }

            var ballHandler = player.Match.Status.BallHandler;
            if (!ballHandler.SkillEnable)
                return player.Match.Football.Current;
           
            #region 没有防守人时，跟随整体移动
            if (player.Status.DefenceStatus.DefenceTarget == null)
            {
                #region 限制区域
                bool isHome = player.Manager.Side == Side.Home;
                double curX = player.Current.X;
                double opX = player.Manager.Opponent.Status.LastPlayer.Status.Current.X;
                if (isHome && curX >= opX + 10
                    || !isHome && curX <= opX - 10)
                {
                    double difY = 0;
                    var defer = player.Status.DefenceStatus.Defender;
                    if (null != defer)
                        difY = (player.Current.Y - defer.Current.Y) > 0 ? -5 : 5;
                    curX = isHome ? curX - 8 : curX + 8;
                    difY += player.Current.Y;
                    return new Coordinate(curX, difY).Regulate();
                }
                #endregion

                #region 反盯防，找空挡
                if (isHome && player.Current.X >= Defines.Pitch.MID_WIDTH
                    || !isHome && player.Current.X <= Defines.Pitch.MID_WIDTH)
                {
                    Coordinate antiTarget;
                    if (Utility.GetAntiDefencePosition(out antiTarget, player, player.Manager.Opponent.Players, player.Manager.Players))
                        return antiTarget;
                }
                #endregion

                return InternalDefenceDecide(player);
            }

            if (!player.Status.DefenceStatus.DefenceTarget.Status.Hasball)
            {
                return InternalDefenceDecide(player);
            }
            #endregion

            #region 球在传递过程中主动去断球，比较容易出现平衡性失控
            //if (player.Status.BallDistance < 10)
            //{
            //    return player.Match.Football.Current;
            //}
            //if (player.Status.DefenceStatus.DefenceTarget == player.Match.Status.BallHandler && player.Match.Status.BallHandler.Status.Holdball == false)
            //{
            //    if (player.Match.Football.Current.Distance(player.Match.Football.Destination) < 10)
            //    {
            //        return player.Match.Football.Current;
            //    }
            //}
            #endregion

            return Utility.GetDefencePosition(player, player.Status.DefenceStatus.DefenceTarget);
        }

        /// <summary>
        /// Decide the moving target while player's in the offense side.
        /// </summary>
        /// <param name="player">Represents the deciding player.</param>
        /// <returns>Represents the moving target.</returns>
        public virtual Coordinate OffenseSideDecide(IPlayer player)
        {
            var random = player.Match;
            if (player.Match.Status.IsNoBallHandler)
            {
                return player.Match.Football.Current;
            }

            #region 持球人行动
            if (player.Status.Hasball)
            {
                if (player.Status.DefenceStatus.DefenderDistancePow <= 1600)
                {
                    var goal = (player.Side == Side.Home) ? player.Match.Pitch.AwayGoal : player.Match.Pitch.HomeGoal;
                    return Utility.GetPointWithRange(player, goal, Defines.Player.SHORT_MOVE_RANGE);
                }
                int index = player.Match.RandomInt32(0, 2);
                IPitch pitch = player.Match.Pitch;

                Dictionary<Direction, Line> lines = (player.Side == Side.Home) ? pitch.AwayDestinations : pitch.HomeDestinations;

                Coordinate target = new Coordinate();
                if (index == 0)
                {
                    target = lines[Direction.Left].GetRandomPoint(random);
                }

                if (index == 1)
                {
                    target = lines[Direction.Center].GetRandomPoint(random);
                }

                if (index == 2)
                {
                    target = lines[Direction.Right].GetRandomPoint(random);
                }

                player.Rotate(target);
                var x = player.Current.X + Defines.Player.SHORT_MOVE_RANGE * Math.Cos(player.Status.Angle * Math.PI / 180);
                var y = player.Current.Y + Defines.Player.SHORT_MOVE_RANGE * Math.Sin(player.Status.Angle * Math.PI / 180);

                return new Coordinate(x, y);
            }
            #endregion
            const double lx_min = 0;
            const double rx_min = 8d / 8d * Defines.Pitch.MAX_WIDTH;
            const double lx_max = 3d / 8d * Defines.Pitch.MAX_WIDTH;
            const double rx_max = 12d / 8d * Defines.Pitch.MAX_WIDTH;

            const double uy_min = (-1d / 8d) * Defines.Pitch.MAX_HEIGHT;
            const double dy_min = 7d / 8d * Defines.Pitch.MAX_HEIGHT;
            const double uy_max = 1d / 8d * Defines.Pitch.MAX_HEIGHT;
            const double dy_max = (9d / 8d) * Defines.Pitch.MAX_HEIGHT;

            bool isHome = player.Side == Side.Home;
            double lxmin = isHome ? lx_min : Defines.Pitch.MAX_WIDTH - rx_max;
            double lxmax = isHome ? lx_max : Defines.Pitch.MAX_WIDTH - rx_min;
            double rxmin = isHome ? rx_min : Defines.Pitch.MAX_WIDTH - lx_max;
            double rxmax = isHome ? rx_max : Defines.Pitch.MAX_WIDTH - lx_min;

            double lux = lxmin + player.Match.Football.Current.X / Defines.Pitch.MAX_WIDTH * (lxmax - lxmin);
            double luy = uy_min + player.Match.Football.Current.Y / Defines.Pitch.MAX_HEIGHT * (uy_max - uy_min);
            double rdx = rxmin + player.Match.Football.Current.X / Defines.Pitch.MAX_WIDTH * (rxmax - rxmin);
            double rdy = dy_min + player.Match.Football.Current.Y / Defines.Pitch.MAX_HEIGHT * (dy_max - dy_min);

            double rx = lux + player.Status.Default.X / Defines.Pitch.MAX_WIDTH * (rdx - lux);
            double ry = luy + player.Status.Default.Y / Defines.Pitch.MAX_HEIGHT * (rdy - luy);

            const double midY = Defines.Pitch.MID_HEIGHT;
            int offsetX = 0;
            int offsetY = 0;
            if (player.Input.AsPosition == Position.Fullback)
            {
                offsetX = isHome ? 20 : -20;
                offsetY = player.Match.Football.Current.Y > midY ? 10 : -10;
            }
            else if (player.Input.AsPosition == Position.Midfielder)
            {
                offsetX = isHome ? 8 : -8;
                offsetY = player.Match.Football.Current.Y > midY ? 10 : -10;
            }
            double fact = 0;
            if (offsetX != 0)
            {
                fact = Math.Abs(player.Status.Default.Y - midY) / midY;
                if (offsetX != 0)
                    rx += offsetX * fact;
            }
            if (offsetY != 0)
            {
                fact = Math.Abs(player.Status.Default.Y - player.Match.Football.Current.Y) / dy_max;
                if (0.4 <= fact)
                    ry += offsetY * fact;
            }
            return new Coordinate(rx, ry);
        }

        private Coordinate InternalDefenceDecide(IPlayer player)
        {
            const double lx_min = 0;
            const double rx_min = 4d / 8d * Defines.Pitch.MAX_WIDTH;
            const double lx_max = 3d / 8d * Defines.Pitch.MAX_WIDTH;
            const double rx_max = 1d / 1d * Defines.Pitch.MAX_WIDTH;

            const double uy_min = 0;
            const double dy_min = 3d / 4d * Defines.Pitch.MAX_HEIGHT;
            const double uy_max = 1d / 4d * Defines.Pitch.MAX_HEIGHT;
            const double dy_max = Defines.Pitch.MAX_HEIGHT;

            bool isHome = player.Side == Side.Home;
            double lxmin = isHome ? lx_min : Defines.Pitch.MAX_WIDTH - rx_max;
            double lxmax = isHome ? lx_max : Defines.Pitch.MAX_WIDTH - rx_min;
            double rxmin = isHome ? rx_min : Defines.Pitch.MAX_WIDTH - lx_max;
            double rxmax = isHome ? rx_max : Defines.Pitch.MAX_WIDTH - lx_min;

            double lux = lxmin + player.Match.Football.Current.X / Defines.Pitch.MAX_WIDTH * (lxmax - lxmin);
            double luy = uy_min + player.Match.Football.Current.Y / Defines.Pitch.MAX_HEIGHT * (uy_max - uy_min);
            double rdx = rxmin + player.Match.Football.Current.X / Defines.Pitch.MAX_WIDTH * (rxmax - rxmin);
            double rdy = dy_min + player.Match.Football.Current.Y / Defines.Pitch.MAX_HEIGHT * (dy_max - dy_min);

            double x = lux + player.Status.HalfDefault.X / Defines.Pitch.MAX_WIDTH * (rdx - lux);
            double y = luy + player.Status.HalfDefault.Y / Defines.Pitch.MAX_HEIGHT * (rdy - luy);
            int offsetX = 0;
            if (player.Input.AsPosition == Position.Fullback
                || player.Input.AsPosition == Position.Midfielder)
            {
                offsetX = isHome ? -20 : 20;
            }
            if (offsetX != 0)
            {
                double fact = Math.Abs(player.Status.HalfDefault.Y - player.Match.Football.Current.Y) / dy_max;
                if (fact >= 0.16)
                    x += offsetX * fact;
            }
            return new Coordinate(x, y);
        }
    }
}
