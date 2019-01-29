using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern.Enum.NBA
{
    /// <summary>
    /// 动作状态
    /// </summary>
    public enum EnumMotionState
    {
        /* *前台状态-后台状态
         *0 跑动:2,19,40,43
         *1 进攻待机:1
         *2 防守待机:25
         *3 运球:16,18
         *4 单手传球:14,15,42
         *5 双手传球:34,35
         *6 接球:33
         *7 投篮:10
         *8 单手扣篮:12
         *9 罚球:13
         *10 抢篮板:45,46
         *11 拦截:22
         *12 抢断:23
         *13 封盖:24
         *14 防守干扰:26
         *15 持球观察:36
         *16 边线开球:37
         *17 跳球:38
         *18 持续动作:39
         *19 运球观察:17
         *20 三分投篮:11
         *21 界外球:41
         * */
       
        /// <summary>
        /// 行动(Last:)
        /// </summary>
        ActionState=0,
        /// <summary>
        /// 站立
        /// </summary>
        IdleState=1,
        /// <summary>
        /// 跑动
        /// </summary>
        ChaceState=2,
        /// <summary>
        /// 持球
        /// </summary>
        HoldBallState=3,
        /// <summary>
        /// 无球
        /// </summary>
        OffBallState=4,
        /// <summary>
        /// 寻位
        /// </summary>
        PositionalState=5,
        /// <summary>
        /// 投篮
        /// </summary>
        ShootState=6,
        /// <summary>
        /// 传球
        /// </summary>
        PassState=7,
        /// <summary>
        /// 带球
        /// </summary>
        DribbleState=8,
        /// <summary>
        /// 防守
        /// </summary>
        DefenseState=9,
        /// <summary>
        /// 普通投篮(4)
        /// </summary>
        DefaultShootState=10,
        /// <summary>
        /// 三分(4)
        /// </summary>
        ThreepointShootState=11,
        /// <summary>
        /// 扣篮(4)
        /// </summary>
        SlamdunkState=12,
        /// <summary>
        /// 罚球(3)
        /// </summary>
        FreeThrowState=13,
        /// <summary>
        /// 短传(2)
        /// </summary>
        ShortPassState=14,
        /// <summary>
        /// 长传(2)
        /// </summary>
        LongPassState=15,
        /// <summary>
        /// 运球推进
        /// </summary>
        DefaultDribbleState=16,
        /// <summary>
        /// 运球观察
        /// </summary>
        DribbleObservationState=17,
        /// <summary>
        /// 运球突破
        /// </summary>
        BreakThroughState=18,
        /// <summary>
        /// 战术跑位
        /// </summary>
        StrategyChaceState=19,
        /// <summary>
        /// 挡拆
        /// </summary>
        PickRollState=20,
        /// <summary>
        /// 篮板(4)
        /// </summary>
        ReboundState=21,
        /// <summary>
        /// 传球拦截
        /// </summary>
        InterceptionPassState=22,
        /// <summary>
        /// 突破抢断
        /// </summary>
        StealBreakthroughState=23,
        /// <summary>
        /// 盖帽(4)
        /// </summary>
        RejectionShotState=24,
        /// <summary>
        /// 防守站位
        /// </summary>
        DefensePositionState=25,
        /// <summary>
        /// 防守干扰
        /// </summary>
        DefenseDisturbState=26,
        /// <summary>
        /// 防守盯人
        /// </summary>
        DefenseStareState=27,
        /// <summary>
        /// 盯人转换
        /// </summary>
        DefenseStareChangeState=28,
        /// <summary>
        /// 盯防持球人
        /// </summary>
        DefenseBallhandlerState=29,
        /// <summary>
        /// 协防持球人
        /// </summary>
        HelpDefendBallhandlerState=30,
        /// <summary>
        /// 盯防非持球人
        /// </summary>
        DefenseNonballmanState=31,
        /// <summary>
        /// 协防非持球人
        /// </summary>
        HelpDefendNonballmanState=32,
        /// <summary>
        /// 接球
        /// </summary>
        StopballState=33,
        /// <summary>
        /// 双手短传(2)
        /// </summary>
        HandsShortPassState=34,
        /// <summary>
        /// 双手长传(2)
        /// </summary>
        HandsLongPassState=35,
        /// <summary>
        /// 持球观察
        /// </summary>
        HoldBallObservationState=36,
        /// <summary>
        /// 边线开球(3)
        /// </summary>
        SidelineKickoffState=37,
        /// <summary>
        /// 跳球
        /// </summary>
        JumpballState=38,
        /// <summary>
        /// 持续回合
        /// </summary>
        ContinueState=39,
        /// <summary>
        /// 篮板争抢
        /// </summary>
        ReboundChaceState=40,
        /// <summary>
        /// 界外球(3)
        /// </summary>
        InboundsState=41,
        /// <summary>
        /// 跳球后传球
        /// </summary>
        StrategyShortPassState=42,
        /// <summary>
        /// 防守持球人跑动
        /// </summary>
        DefenseBallHandlerChaceState=43,
        /// <summary>
        /// 进攻篮板
        /// </summary>
        ReboundOffenseState=45,
        /// <summary>
        /// 防守篮板
        /// </summary>
        ReboundDefenseState=46 
    }
}
