/********************************************************************************
 * 文件名：FreeKickRule
 * 创建人：
 * 创建时间：4/20/2010 2:32:33 PM
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
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;

namespace Games.NB.Match.BLL.Rules.FreeKickRules
{
    /// <summary>
    /// Represents the Free Kick rule.
    /// </summary>
    abstract class FreeKickRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FreeKickRule"/> class.
        /// </summary>
        /// <param name="manager">Represents a player who will take the kick.</param>
        /// <param name="point">Represents a <see cref="Coordinate"/> that is the open ball point.</param>
        protected FreeKickRule(IManager manager, Coordinate point)
        {
#if DEBUG
            if (manager == null) {
                throw new NullReferenceException("Initializes the FreeKickRule class with null argument: manager");
            }
#endif

            this.manager = manager;
            this.point = point;
        }

        /// <summary>
        /// Starts a free kick.
        /// </summary>
        public abstract void Start();

        #region encapsulation

        protected readonly IManager manager;
        protected readonly Coordinate point;

        protected Coordinate CloseMove(double x, double y)
        {
            double distance = point.SimpleDistance(new Coordinate(x, y));
            if (distance < 100 * 100)
            {
                distance = Math.Sqrt(distance);
                if (distance > 0)
                {
                    var move = (distance + 50) / 10;
                    if ((distance - move) < 19)
                    {
                        move = distance - 19;
                    }

                    x = x + (point.X - x) * move / distance;
                    y = y + (point.Y - y) * move / distance;
                }
            }
            x = Math.Max(0, Math.Min(Base.Defines.Pitch.MAX_WIDTH, x));
            y = Math.Max(0, Math.Min(Base.Defines.Pitch.MAX_HEIGHT, y));
            return new Coordinate(x, y);
        }

        #endregion

        #region TowardMove
        protected Coordinate TowardMove(double x, double y)
        {
            double distance = point.Distance(new Coordinate(x, y));
            if (distance == 0)
                return new Coordinate(x, y);
            double move = 0;
            if (distance < 100)
            {
                move = (distance + 50) / 10;
                if ((distance - move) < 19)
                {
                    move = distance - 19;
                }
            }
            else if (distance < 150)
            {
                move = 15;
            }
            if (move == 0)
                return new Coordinate(x, y);
            x = x + (point.X - x) * move / distance;
            y = y + (point.Y - y) * move / distance;
            return new Coordinate(x, y);
        } 
        #endregion
    }
}
