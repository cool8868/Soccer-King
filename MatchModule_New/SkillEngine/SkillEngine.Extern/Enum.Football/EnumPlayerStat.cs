using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern.Enum.Football
{
    public enum EnumPlayerStat
    {
        #region Goals
        /// <summary>
        /// 进球数
        /// </summary>
        Goals = 1,
        #endregion

        #region Times
        /// <summary>
        /// 射门次数
        /// </summary>
        ShootTimes = 10,
        /// <summary>
        /// 传球次数
        /// </summary>
        PassTimes = 11,
        /// <summary>
        /// 传球成功次数
        /// </summary>
        SuccPassTimes = 12,
        /// <summary>
        /// 过人次数
        /// </summary>
        ThroughTimes = 13,
        /// <summary>
        /// 过人成功次数
        /// </summary>
        SuccThroughTimes = 14,
        /// <summary>
        /// 抢断次数
        /// </summary>
        StealTimes = 15,
        /// <summary>
        /// 抢断成功次数
        /// </summary>
        SuccStealTimes = 16,
        /// <summary>
        /// 扑救次数
        /// </summary>
        DiveTimes = 17,
        /// <summary>
        /// 扑救成功次数
        /// </summary>
        SuccDiveTimes = 18,
        #endregion

        #region Time
        /// <summary>
        /// 最长控球时间
        /// </summary>
        CombDribbleTime,
        #endregion
    }
}
