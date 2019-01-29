using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NB.Match.Base.Enum
{
    public enum EnumMatchBreakState
    {
        /// <summary>
        /// 正常进行中
        /// </summary>
        None = 0,
        /// <summary>
        /// 上下半场开球
        /// </summary>
        SectionOpen = 1,
        /// <summary>
        /// 中场开球
        /// </summary>
        MFOpen = 2,
        /// <summary>
        /// 开球门球
        /// </summary>
        GKOpen = 3,
        /// <summary>
        /// 任意球
        /// </summary>
        FreeKick = 4,
        /// <summary>
        /// 射门
        /// </summary>
        Shooted = 5,
        /// <summary>
        /// 犯规
        /// </summary>
        Fouled = 6,
        /// <summary>
        /// 出界
        /// </summary>
        OutBorder = 7,
        /// <summary>
        /// 边线手抛球
        /// </summary>
        HandThrow = 11,
        /// <summary>
        /// 角球
        /// </summary>
        CornerKick = 12,
        /// <summary>
        /// 球门球
        /// </summary>
        GoalKick = 13,
        /// <summary>
        /// 射飞
        /// </summary>
        ShootFly = 14,
        /// <summary>
        /// 射进
        /// </summary>
        ShootInto = 15,
        /// <summary>
        /// 间接任意球
        /// </summary>
        IndirectKick = 16,
        /// <summary>
        /// 直接任意球
        /// </summary>
        DirectKick = 17,
        /// <summary>
        /// 点球
        /// </summary>
        PenaltyKick = 18,

    }
    public enum EnumMatchBreakStateV2
    {
        /// <summary>
        /// 正常进行中
        /// </summary>
        None = 0,
        /// <summary>
        /// 上下半场开球
        /// </summary>
        SectionOpen = 1,
        /// <summary>
        /// 中场开球
        /// </summary>
        MFOpen = 2,
        /// <summary>
        /// 开球门球
        /// </summary>
        GKOpen = 3,
        /// <summary>
        /// 角球
        /// </summary>
        CornerKick = 4,
        /// <summary>
        /// 边线手抛球
        /// </summary>
        HandThrow = 5,
        /// <summary>
        /// 任意球
        /// </summary>
        FreeKick = 6,
        /// <summary>
        /// 点球
        /// </summary>
        PenaltyKick=7,
        /// <summary>
        /// 射门
        /// </summary>
        Shooted = 11,
        /// <summary>
        /// 犯规
        /// </summary>
        Fouled = 12,
        /// <summary>
        /// 出界
        /// </summary>
        OutBorder = 13,
        /// <summary>
        /// 射飞
        /// </summary>
        ShootFly = 14,
        /// <summary>
        /// 射进
        /// </summary>
        ShootInto = 15,

    }

    public enum EnumMatchFoulState
    {
        /// <summary>
        /// 无
        /// </summary>
        None=0,
        /// <summary>
        /// 犯规
        /// </summary>
        Foul=1,
    }
}
