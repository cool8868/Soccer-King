/********************************************************************************
 * 文件名：FootballRule
 * 创建人：
 * 创建时间：2009-12-9 10:40:17
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Common.Random;

namespace Games.NB.Match.BLL.Rules
{
    /// <summary>
    /// Represents the rules for the football entity.
    /// </summary>
    static class FootballRule
    {
        /// <summary>
        /// Get the pass action's original ball speed.
        /// </summary>
        /// <param name="start">Represents the start coordinate.</param>
        /// <param name="target">Represents the target coordinate.</param>
        /// <returns>Represents the ball speed.</returns>
        public static double GetPassSpeed(Coordinate start, Coordinate target)
        {
            var vb = 2; // RandomHelper.GetInt32(5, 15); // 终点速度
            var d = start.Distance(target); // 距离
            var t = -(Math.Pow(Math.Pow(vb, 2) - 2 * Defines.Pitch.BALL_ACCELERATION * d, 0.5) + vb) / Defines.Pitch.BALL_ACCELERATION;
            return InternalGetPassSpeed(vb, t);
        }

        //public static double GetLongPassSpeed(Coordinate start, Coordinate target)
        //{
        //    var vb = 2; // RandomHelper.GetInt32(5, 15);
        //    var d = start.Distance(target);
        //    var t = -(Math.Pow(Math.Pow(vb, 2) - 2 * Defines.Pitch.BALL_ACCELERATION * d, 0.5) + vb) / Defines.Pitch.BALL_ACCELERATION / 2;
        //    return InternalGetPassSpeed(vb, t);
        //}

        #region encapsulation

        private static double InternalGetPassSpeed(double vb, double t)
        {
            return vb - Defines.Pitch.BALL_ACCELERATION * t;
        }

        #endregion
    }
}
