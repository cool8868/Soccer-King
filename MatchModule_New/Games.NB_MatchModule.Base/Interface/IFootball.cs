/********************************************************************************
 * 文件名：IFootball
 * 创建人：
 * 创建时间：2009-11-30 17:20:00
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

namespace Games.NB.Match.Base.Interface {

    /// <summary>
    /// Represents the interface of a football.
    /// 代表了足球的接口
    /// </summary>
    public interface IFootball : IMoveable {

        /// <summary>
        /// Represents the football's visible.
        /// 表示了球的可见性
        /// </summary>
        bool Visible { get; }
        /// <summary>
        /// Represents whether the ball is in the air.
        /// 表示了球是否在空中
        /// </summary>
        bool IsInAir { get; }

        /// <summary>
        /// Represents whether the ball has passed the destination.
        /// 是否通过了目标点（惯性移动）
        /// </summary>
        bool IsPassDestination { get; }

        /// <summary>
        /// 是否出界
        /// </summary>
        bool IsOutBorder { get; }
        /// <summary>
        /// 是否角球
        /// </summary>
        bool IsCorner { get; }

        int TurnCount { get; }
        bool TurnFlag { get; set; }
        /// <summary>
        /// Represents the last touch ball <see cref="IPlayer"/>.
        /// 表示了最后接触球的球员
        /// </summary>
        IPlayer LastTouchMan { get; }
        /// <summary>
        /// Force the next pass is in air.
        /// 强迫下一次传球为高球
        /// </summary>
        void ForceInAir();
        /// <summary>
        /// Kick the ball to a <see cref="Coordinate"/>.
        /// 把球踢往某一点
        /// </summary>
        /// <param name="coordinate">Represents the target <see cref="Coordinate"/>.</param>
        /// <param name="speed">Represents the ball's start speed.</param>
        /// <param name="lastMan">Represents the last touch ball <see cref="IPlayer"/>.</param>
        void Kick(Coordinate coordinate, double speed, IPlayer lastMan);

        /// <summary>
        /// Kick the ball to a <see cref="Coordinate"/>, and the ball will in the air all the time.
        /// 将球踢往某一点（球是在空中的）
        /// </summary>
        /// <param name="coordinate">Represents the target <see cref="Coordinate"/>.</param>
        /// <param name="speed">Represents the ball's start speed.</param>
        /// <param name="angle">Represents the ball's start angle.</param>
        /// <param name="lastMan">Represents the last touch ball <see cref="IPlayer"/>.</param>
        void Kick(Coordinate coordinate, double speed, byte angle, IPlayer lastMan);

      
    }
}
