using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum.Football
{
    public enum EnumSpecTiming
    {
        None = 0,
        /// <summary>
        /// 回合开始
        /// </summary>
        RoundStart = 1,
        /// <summary>
        /// 思考开始
        /// </summary>
        DecideStart = 2,
        /// <summary>
        /// 思考结束
        /// </summary>
        DecideEnd = 3,
        /// <summary>
        /// 行动开始
        /// </summary>
        ActionStart = 4,
        /// <summary>
        /// 带球时
        /// </summary>
        DribbleStart = 11,
        /// <summary>
        /// 传球时
        /// </summary>
        PassStart = 12,
        /// <summary>
        /// 射门时
        /// </summary>
        ShootStart = 13,
        /// <summary>
        /// 扑救时
        /// </summary>
        DiveStart = 14,
        /// <summary>
        /// 防守时
        /// </summary>
        DefenceStart = 15,
    }
    public enum EnumBuffEventType
    {
        None = 0,
        /// <summary>
        /// 异常
        /// </summary>
        Blur = 1,
        /// <summary>
        /// 犯规
        /// </summary>
        Foul = 2,
    }
    public enum EnumEventTargetSide
    {
        /// <summary>
        /// 自己
        /// </summary>
        OwnPlayer = 0,
        /// <summary>
        /// 对位球员
        /// </summary>
        OppPlayer = 1,
        /// <summary>
        /// 己方
        /// </summary>
        OwnManager=2,
        /// <summary>
        /// 对方
        /// </summary>
        OppManager=3,
    }
    public enum EnumSpecEffect
    {
        None = 0,
        /// <summary>
        /// 抛传
        /// </summary>
        HighPass = 8001,
        /// <summary>
        /// 恢复体力
        /// </summary>
        RecoverStamina = 8002,
        /// <summary>
        /// 清除技能CD
        /// </summary>
        ClearCD = 8003,
        /// <summary>
        /// 破坏
        /// </summary>
        PassOutside = 8004,
        /// <summary>
        /// 伤残立即复活
        /// </summary>
        Reborn = 8005,
        /// <summary>
        /// 倒地后致伤
        /// </summary>
        FalldownThenInjure = 8006,
        /// <summary>
        /// 强制状态
        /// </summary>
        ForceState = 8010,
    }
    public enum EnumForceState
    {
        None = 0,
        /// <summary>
        /// 射门
        /// </summary>
        ShootState = 8011,
        /// <summary>
        /// 无视地形射门
        /// </summary>
        UltraShootState = 8012,
        /// <summary>
        /// 传球
        /// </summary>
        PassState = 8013,
        /// <summary>
        /// 带球
        /// </summary>
        DribbleState = 8014,
        /// <summary>
        /// 防守
        /// </summary>
        DefenceState = 8015,
        /// <summary>
        /// 瞬移抢断
        /// </summary>
        UltraDefenceState = 8016,

    }
}
