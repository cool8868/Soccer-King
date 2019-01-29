/********************************************************************************
 * 文件名：FreeKickRuleFactory
 * 创建人：
 * 创建时间：4/20/2010 2:56:05 PM
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Text;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Common.Random;

namespace Games.NB.Match.BLL.Rules.FreeKickRules
{

    /// <summary>
    /// Represents the factory of the FreekickRule class.
    /// </summary>
    [Singleton]
    public class FreeKickRuleFactory
    {
        /// <summary>
        /// Represents the instance of the <see cref="FreeKickRuleFactory"/>.
        /// </summary>
        public static FreeKickRuleFactory Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Start a free kick.
        /// 进行一次任意球
        /// </summary>
        /// <param name="manager">
        /// Represents the Openball side manager.
        /// 表示了开球方的经理
        /// </param>
        /// <param name="point">
        /// Represents the current ball position.
        /// 表示了当前的开球点
        /// </param>
        public void Start(IManager manager, Coordinate point)
        {

            FreeKickRule rule = null;
            #region 角球
            if (manager.Match.Football.IsCorner)
            {
                double y = (point.Y < Defines.Pitch.MAX_HEIGHT / 2) ? 0 : Defines.Pitch.MAX_HEIGHT;
                rule = new CornerKickRules(manager, new Coordinate(point.X, y));
                rule.Start();
                return;
            }
            #endregion

            #region 边线球
            if (manager.Match.Football.IsOutBorder)
            {
                rule = new IndirectFreeKickRule(manager, point, true);
                rule.Start();
                return;
            }
            #endregion

            double sDistance = point.SimpleDistance((manager.Side == Side.Home) ? manager.Match.Pitch.AwayGoal : manager.Match.Pitch.HomeGoal);
            #region 如果在禁区为点球
            var region = (manager.Side == Side.Home) ? manager.Match.Pitch.AwayPenaltyRegion : manager.Match.Pitch.HomePenaltyRegion;
            if (region.IsCoordinateInRegion(point))
            {
                point = new Coordinate(25, 68);
                if (manager.Side == Side.Home)
                {
                    point = point.Mirror();
                }

                manager.Match.Football.MoveTo(point);
                rule = new PenaltyKickRule(manager, point);
                rule.Start();
                return;
            }
            #endregion

            #region 角度太小则为间接任意球
            if (point.Y < manager.Match.Pitch.HomePenaltyRegion.Start.Y - 10 || point.Y > manager.Match.Pitch.HomePenaltyRegion.End.Y + 10)
            {
                if ((manager.Side == Side.Home) && (point.X > manager.Match.Pitch.AwayPenaltyRegion.Start.X))
                {
                    rule = new IndirectFreeKickRule(manager, point);
                    rule.Start();
                    return;
                }

                if ((manager.Side == Side.Away) && (point.X < manager.Match.Pitch.HomePenaltyRegion.End.X))
                {
                    rule = new IndirectFreeKickRule(manager, point);
                    rule.Start();
                    return;
                }
            }
            #endregion

            if (sDistance < 3600)
            {
                rule = new DirectFreeKickRule(manager, point);
                rule.Start();
            }
            else
            {
                rule = new IndirectFreeKickRule(manager, point);
                rule.Start();
            }
        }

        private readonly static FreeKickRuleFactory _instance = new FreeKickRuleFactory();
    }
}
