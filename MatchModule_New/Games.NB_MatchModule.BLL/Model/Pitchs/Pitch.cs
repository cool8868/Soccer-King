/*****************************************************************************
 * 文件名：Pitch
 * 
 * 创建人：
 * 
 * 创建时间：2009-11-5 13:44:59
 * 
 * 功能说明：Represents a pitch.
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
using System;
using System.Collections.Generic;
using Games.NB.Match.Base;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Common;
using Games.NB.Match.Common.Collections;

namespace Games.NB.Match.BLL.Model.Pitchs
{

    /// <summary>
    /// Represents a pitch.
    /// 表示了球场
    /// </summary>
    [Serializable]
    public class Pitch : BusinessBase, IPitch
    {
        /// <summary>
        /// Represents the home side's moving destinations.
        /// 表示了主队球员的移动目标
        /// </summary>
        public Dictionary<Direction, Line> HomeDestinations
        {
            get { return this._homeDestinations; }
        }

        /// <summary>
        /// Represents the away side's moving destinations.
        /// 表示了客队球员的移动目标
        /// </summary>
        public Dictionary<Direction, Line> AwayDestinations
        {
            get { return this._awayDestinations; }
        }

        /// <summary>
        /// Represents the home team's goal.
        /// 表示了主队球门的中心
        /// </summary>
        public Coordinate HomeGoal
        {
            get { return _homeGoal; }
        }

        /// <summary>
        /// Represents the away team's goal.
        /// 表示了客队球门的中心
        /// </summary>
        public Coordinate AwayGoal
        {
            get { return _awayGoal; }
        }

        /// <summary>
        /// Represents a common goal.
        /// 表示了一个通用的球门实例
        /// </summary>
        public IGoal Goal
        {
            get { return _goal; }
        }

        /// <summary>
        /// Represents a <see cref="Region"/> that away side player in there will force to shoot.
        /// 表示客队球员强制射门的区域
        /// </summary>
        public Region HomeShootRegion
        {
            get { return _homeShootRegion; }
        }

        /// <summary>
        /// Represents a <see cref="Region"/> that home side player in there will force to shoot.
        /// 表示了主队球员强制射门的区域
        /// </summary>
        public Region AwayShootRegion
        {
            get { return _awayShootRegion; }
        }

        /// <summary>
        /// Represents the home side's penalty region.
        /// 表示了主队的禁区
        /// </summary>
        public Region HomePenaltyRegion
        {
            get { return _homePenaltyRegion; }
        }

        /// <summary>
        /// Represents the away side's penalty region.
        /// 表示了客队的禁区
        /// </summary>
        public Region AwayPenaltyRegion
        {
            get { return _awayPenaltyRegion; }
        }

        /// <summary>
        /// Represents the array of the regions that away side player will force to pass.
        /// 表示了客队球员强制传球的区域
        /// (表示了左侧主队的区域)
        /// </summary>
        public Region HomeForcePassRegion
        {
            get { return _homeForcePassRegion; }
        }

        /// <summary>
        /// Represents the array of the regions that home side player will force to pass.
        /// 表示了主队球员强制传球的区域
        /// (表示了右侧客队的区域)
        /// </summary>
        public Region AwayForcePassRegion
        {
            get { return _awayForcePassRegion; }
        }

        /// <summary>
        /// 主队边路传中区域
        /// </summary>
        public Region HomeWingCrossRegion 
        {
            get { return _homeWingCroosRegion; }
        }
        /// <summary>
        /// 客队边路传中区域
        /// </summary>
        public Region AwayWingCrossRegion
        {
            get { return _awayWingCroosRegion; }
        }

        /// <summary>
        /// Create a new instance of the pitch.
        /// 创建一个新实例
        /// </summary>
        /// <returns></returns>
        public static Pitch New()
        {
            return _pitch;
        }

        #region encapsulation

        private static readonly Pitch _pitch = new Pitch();
        private static readonly IGoal _goal = Singleton<Goal>.Instance;
        private readonly Coordinate _homeGoal = Coordinate.Parse(Defines.Pitch.HOME_GOAL);
        private readonly Coordinate _awayGoal = Coordinate.Parse(Defines.Pitch.AWAY_GOAL);
        private readonly Dictionary<Direction, Line> _homeDestinations = new Dictionary<Direction, Line>(3);
        private readonly Dictionary<Direction, Line> _awayDestinations = new Dictionary<Direction, Line>(3);
        private readonly Region _homeShootRegion = Region.ParseByStr(Defines.Pitch.HOME_FORCE_SHOOT_REGION);
        private readonly Region _awayShootRegion = Region.ParseByStr(Defines.Pitch.HOME_FORCE_SHOOT_REGION).Mirror();
        private readonly Region _homePenaltyRegion = Region.ParseByStr(Defines.Pitch.HOME_PENALTY_AREA);
        private readonly Region _awayPenaltyRegion = Region.ParseByStr(Defines.Pitch.AWAY_PENALTY_AREA);
        private readonly Region _homeForcePassRegion = Region.ParseByStr(Defines.Pitch.HOME_FORCE_PASS_REGION);
        private readonly Region _awayForcePassRegion = Region.ParseByStr(Defines.Pitch.HOME_FORCE_PASS_REGION).Mirror();
        private readonly Region _homeWingCroosRegion = Region.ParseByStr(Defines.Pitch.HOME_WING_CROSS_REGION);
        private readonly Region _awayWingCroosRegion = Region.ParseByStr(Defines.Pitch.HOME_WING_CROSS_REGION).Mirror();

        private Pitch()
        {
            _homeDestinations.Add(Direction.Center, Line.ParseByStr(Defines.Pitch.HOME_DESTINATION_CENTER));
            _homeDestinations.Add(Direction.Left, Line.ParseByStr(Defines.Pitch.HOME_DESTINATION_LEFT));
            _homeDestinations.Add(Direction.Right, Line.ParseByStr(Defines.Pitch.HOME_DESTINATION_RIGHT));

            _awayDestinations.Add(Direction.Center, Line.ParseByStr(Defines.Pitch.AWAY_DESTINATION_CENTER));
            _awayDestinations.Add(Direction.Left, Line.ParseByStr(Defines.Pitch.AWAY_DESTINATION_LEFT));
            _awayDestinations.Add(Direction.Right, Line.ParseByStr(Defines.Pitch.AWAY_DESTINATION_RIGHT));
        }

        #endregion
    }
}