using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern.Enum.Football
{
    public enum EnumManagerStat
    {
        #region Goals
        /// <summary>
        /// 进球数
        /// </summary>
        GS = 1,
        /// <summary>
        /// 失球数
        /// </summary>
        GA = 2,
        /// <summary>
        /// 净胜球
        /// </summary>
        GD = 3,
        /// <summary>
        /// 连续进球数
        /// </summary>
        CombScore = 4,
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
        /// 累计控球回合
        /// </summary>
        IncDribbleRound = 21,
        /// <summary>
        /// 累计控球分钟
        /// </summary>
        IncDribbleMinutes = 22,
        /// <summary>
        /// 连续控球回合
        /// </summary>
        CombDribbleRound = 23,
        /// <summary>
        /// 连续控球分钟
        /// </summary>
        CombDribbleMinutes = 24,
        #endregion
    }
}
