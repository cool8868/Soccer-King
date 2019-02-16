using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Base.Enum
{
    /// <summary>
    /// 后台状态：State.ClientId
    /// </summary>
    public enum EnumAIState
    {
        None=-1,
        /// <summary>
        /// 0
        /// </summary>
        Action=0,
        /// <summary>
        /// 1
        /// </summary>
        Chace=1,
        /// <summary>
        /// 2
        /// </summary>
        Defence=2,
        /// <summary>
        /// 3
        /// </summary>
        DiveBall=3,
        /// <summary>
        /// 4
        /// </summary>
        Dribble=4,
        /// <summary>
        /// 5
        /// </summary>
        HoldBall=5,
        /// <summary>
        /// 6
        /// </summary>
        Idle=6,
        /// <summary>
        /// 7
        /// </summary>
        OffBall=7,
        /// <summary>
        /// 8
        /// </summary>
        Pass=8,
        /// <summary>
        /// 9
        /// </summary>
        Positional=9,
        /// <summary>
        /// 10
        /// </summary>
        Shoot=10,
        /// <summary>
        /// 11
        /// </summary>
        Stopball=11,
        /// <summary>
        /// 12
        /// </summary>
        Interruption=12,
        /// <summary>
        /// 13
        /// </summary>
        SlideTackle=13,
        /// <summary>
        /// 14
        /// </summary>
        DefaultDribble=14,
        /// <summary>
        /// 15
        /// </summary>
        LongPass=15,
        /// <summary>
        /// 16
        /// </summary>
        ShortPass=16,
        /// <summary>
        /// 17
        /// </summary>
        DefaultShoot=17,
        /// <summary>
        /// 18
        /// </summary>
        VolleyShoot=18,
        /// <summary>
        /// 19突破
        /// </summary>
        BreakThrough=19,
        /// <summary>
        /// 20任意球
        /// </summary>
        FreekickShoot=20,
        /// <summary>
        /// 21门将持球
        /// </summary>
        GKHoldBall=21,
        /// <summary>
        /// 22走路
        /// </summary>
        Walk=22,
        /// <summary>
        /// 23乌龙射门
        /// </summary>
        RebelShoot=23,
        /// <summary>
        /// 24头球对决
        /// </summary>
        HeadingDuel=24,

    }
    /// <summary>
    /// 球员动作
    /// </summary>
    public enum EnumSubState
    {
        None=0,
        /// <summary>
        /// 长传接球预备
        /// </summary>
        LongPassAccepting = -1,
        /// <summary>
        /// 长传接球
        /// </summary>
        LongPassAccepted = -2,
        /// <summary>
        /// 短传接球
        /// </summary>
        ShortPassAccepting=-3,
        /// <summary>
        /// 头球
        /// </summary>
        HeadBall=-4,
        /// <summary>
        /// 边线手抛球
        /// </summary>
        HandThrow = 13,
        /// <summary>
        /// 头球争顶
        /// </summary>
        HeadingDuel = 14,
        /// <summary>
        /// 倒挂金钩射门
        /// </summary>
        BicycleShot = 15,
        /// <summary>
        /// 扑救成功
        /// </summary>
        KeepBall=16,
        /// <summary>
        /// 乌龙射门
        /// </summary>
        RebelShoot = 17,
        /// <summary>
        /// 头球传球
        /// </summary>
        HeadPass = 18,
        /// <summary>
        /// 头球射门
        /// </summary>
        HeadShoot = 19,
        /// <summary>
        /// 头球接球
        /// </summary>
        HeadStop = 20,
      
    }
    /// <summary>
    /// 客户端显示状态
    /// </summary>
    public enum EnumClientState : byte
    {
        /// <summary>
        /// 0
        /// </summary>
        Chace = 0,
        /// <summary>
        /// 1
        /// </summary>
        DiveBall = 1,
        /// <summary>
        /// 2
        /// </summary>
        Idle = 2,
        /// <summary>
        /// 3
        /// </summary>
        Stopball = 3,
        /// <summary>
        /// 4
        /// </summary>
        Interruption = 4,
        /// <summary>
        /// 5
        /// </summary>
        SlideTackle = 5,
        /// <summary>
        /// 6
        /// </summary>
        DefaultDribble = 6,
        /// <summary>
        /// 7
        /// </summary>
        LongPass = 7,
        /// <summary>
        /// 8
        /// </summary>
        ShortPass = 8,
        /// <summary>
        /// 9
        /// </summary>
        Shoot = 9,
        /// <summary>
        /// 10
        /// </summary>
        VolleyShoot = 10,
        /// <summary>
        /// 11
        /// </summary>
        BreakThrough = 11,
        /// <summary>
        /// 12
        /// </summary>
        Walk = 12,
        /// <summary>
        /// 边线手抛球
        /// </summary>
        HandThrow = 13,
        /// <summary>
        /// 头球争顶
        /// </summary>
        HeadingDuel = 14,
        /// <summary>
        /// 倒挂金钩射门
        /// </summary>
        BicycleShot = 15,
        /// <summary>
        /// 扑救成功
        /// </summary>
        KeepBall=16,
        /// <summary>
        /// 乌龙射门
        /// </summary>
        RebelShoot = 17,
        /// <summary>
        /// 头球传球
        /// </summary>
        HeadPass = 18,
        /// <summary>
        /// 头球射门
        /// </summary>
        HeadShoot = 19,
        /// <summary>
        /// 头球接球
        /// </summary>
        HeadStop = 20,
    }
}
