using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum.NBA
{
    public enum EnumSpecTiming
    {
        None=0,
        /// <summary>
        /// 回合开始
        /// </summary>
        RoundStart=1,
        /// <summary>
        /// 思考开始
        /// </summary>
        DecideStart=2,
        /// <summary>
        /// 思考结束
        /// </summary>
        DecideEnd=3,
        /// <summary>
        /// 行动开始
        /// </summary>
        ActionStart=4,
        /// <summary>
        /// 带球时
        /// </summary>
        DribbleStart=11,
        /// <summary>
        /// 传球时
        /// </summary>
        PassStart = 12,
        /// <summary>
        /// 射门时
        /// </summary>
        ShootStart=13,
        /// <summary>
        /// 扑救时
        /// </summary>
        DiveStart=14,
        /// <summary>
        /// 防守时
        /// </summary>
        DefenceStart=15,
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
    public enum EnumSpecEffect
    {
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
    }

}
