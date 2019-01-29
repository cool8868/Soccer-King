using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern.Enum.Football
{
    /// <summary>
    /// 动作状态
    /// </summary>
    public enum EnumMotionState
    {
        None = -1,
        /// <summary>
        /// 0
        /// </summary>
        Action = 0,
        /// <summary>
        /// 跑动
        /// </summary>
        Chace = 1,
        /// <summary>
        /// 2
        /// </summary>
        Defence = 2,
        /// <summary>
        /// 扑救
        /// </summary>
        DiveBall = 3,
        /// <summary>
        /// 4
        /// </summary>
        Dribble = 4,
        /// <summary>
        /// 5
        /// </summary>
        HoldBall = 5,
        /// <summary>
        /// 站立
        /// </summary>
        Idle = 6,
        /// <summary>
        /// 7
        /// </summary>
        OffBall = 7,
        /// <summary>
        /// 8
        /// </summary>
        Pass = 8,
        /// <summary>
        /// 9
        /// </summary>
        Positional = 9,
        /// <summary>
        /// 10
        /// </summary>
        Shoot = 10,
        /// <summary>
        /// 11
        /// </summary>
        Stopball = 11,
        /// <summary>
        /// 抢断
        /// </summary>
        Interruption = 12,
        /// <summary>
        /// 铲球
        /// </summary>
        SlideTackle = 13,
        /// <summary>
        /// 带球
        /// </summary>
        DefaultDribble = 14,
        /// <summary>
        /// 长传
        /// </summary>
        LongPass = 15,
        /// <summary>
        /// 短传
        /// </summary>
        ShortPass = 16,
        /// <summary>
        /// 普通射门
        /// </summary>
        DefaultShoot = 17,
        /// <summary>
        /// 大力射门
        /// </summary>
        VolleyShoot = 18,
        /// <summary>
        /// 过人
        /// </summary>
        BreakThrough = 19,
        /// <summary>
        /// 任意球射门
        /// </summary>
        FreekickShoot = 20,
        /// <summary>
        /// 守门员持球站立
        /// </summary>
        GKHoldBall = 21,
        /// <summary>
        /// 走动
        /// </summary>
        Walk = 22,
        /// <summary>
        /// 乌龙射门
        /// </summary>
        RebelShoot = 23,
    }
}
