/********************************************************************************
 * 文件名：IPitch
 * 创建人：
 * 创建时间：2009-12-16 10:39:52
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
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Structs;

namespace Games.NB.Match.Base.Interface
{
    /// <summary>
    /// Represents a pitch.
    /// 表示了球场的接口
    /// </summary>
    public interface IPitch
    {

        /// <summary>
        /// Represents a common goal.
        /// 表示了一个通用的球门
        /// </summary>
        IGoal Goal { get; }

        /// <summary>
        /// Represents the home team's goal.
        /// 表示了主队球门的中心
        /// </summary>
        Coordinate HomeGoal { get; }

        /// <summary>
        /// Represents the away team's goal.
        /// 表示了客队球门的中心
        /// </summary>
        Coordinate AwayGoal { get; }

        /// <summary>
        /// Represents a <see cref="Region"/> that away side player in there will force to shoot.
        /// 表示了客队球员直接射门区域
        /// </summary>
        Region HomeShootRegion { get; }

        /// <summary>
        /// Represents a <see cref="Region"/> that home side player in there will force to shoot.
        /// 表示了主队球员直接射门区域
        /// </summary>
        Region AwayShootRegion { get; }

        /// <summary>
        /// Represents the home side's penalty region.
        /// 表示了主队的禁区
        /// </summary>
        Region HomePenaltyRegion { get; }

        /// <summary>
        /// Represents the away side's penalty region.
        /// 表示了客队的禁区
        /// </summary>
        Region AwayPenaltyRegion { get; }

        /// <summary>
        /// Represents the home side's moving destinations.
        /// 表示了主队的行动目标点
        /// </summary>
        Dictionary<Direction, Line> HomeDestinations { get; }

        /// <summary>
        /// Represents the away side's moving destinations.
        /// 表示了客队的行动目标点
        /// </summary>
        Dictionary<Direction, Line> AwayDestinations { get; }

        /// <summary>
        /// Represents the array of the regions that away side player will force to pass.
        /// 表示了客队球员强制传球的区域
        /// (表示了左侧主队的区域)
        /// </summary>
        Region HomeForcePassRegion { get; }

        /// <summary>
        /// Represents the array of the regions that home side player will force to pass.
        /// 表示了主队球员强制传球的区域
        /// (表示了右侧客队的区域)
        /// </summary>
        Region AwayForcePassRegion { get; }

        /// <summary>
        /// 主队边路传中区域
        /// </summary>
        Region HomeWingCrossRegion { get; }
        /// <summary>
        /// 客队边路传中区域
        /// </summary>
        Region AwayWingCrossRegion { get; }
    }
}
