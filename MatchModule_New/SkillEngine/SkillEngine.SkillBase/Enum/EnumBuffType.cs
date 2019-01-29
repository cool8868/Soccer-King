using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum
{
    public enum EnumBuffType : byte
    {
        /// <summary>
        /// 属性加成
        /// </summary>
        PropPlus = 1,
        /// <summary>
        /// 属性置换
        /// </summary>
        PropAlter = 2,
        /// <summary>
        /// 动作概率
        /// </summary>
        ActionRate = 3,
        /// <summary>
        /// 动作参数
        /// </summary>
        ActionParm = 4,
        /// <summary>
        /// 异常状态
        /// </summary>
        BlurState = 5,
        /// <summary>
        /// 犯规
        /// </summary>
        Foul = 6,
        /// <summary>
        /// 效果加强
        /// </summary>
        Boost = 9,
        /// <summary>
        /// 特殊效果
        /// </summary>
        Spec = 10
    }
    public enum EnumBuffLast
    {
        /// <summary>
        /// 直至死球
        /// </summary>
        TillDeadBall = -12,
        /// <summary>
        /// 直至事件点
        /// </summary>
        TillWaitEnd = -11,
        /// <summary>
        /// 直至回收
        /// </summary>
        Undo = -10,
        /// <summary>
        /// 直至节末
        /// </summary>
        TillSectionEnd = -1,
        /// <summary>
        /// 一次比对
        /// </summary>
        Action = 0,
        /// <summary>
        /// 回合
        /// </summary>
        Round = 1,
        /// <summary>
        /// 分钟
        /// </summary>
        Minutes = 2,
    }
    public enum EnumBuffRepeat
    {
        WaitOnce = -2,
        /// <summary>
        /// 一次性非叠加
        /// </summary>
        PickOnce = -1,
        /// <summary>
        /// 一次性可叠加
        /// </summary>
        Once = 0,
        /// <summary>
        /// 持续性
        /// </summary>
        OverTime = 1,
    }
    public enum EnumBuffExec
    {
        /// <summary>
        /// 无需执行
        /// </summary>
        None = 0,
        /// <summary>
        /// 执行无需命中
        /// </summary>
        IgnoreHit = 1,
        /// <summary>
        /// 执行且命中
        /// </summary>
        MustHit = 2
    }
    public enum EnumBuffFact
    {
        None = 0,
        /// <summary>
        /// 颜色数量
        /// </summary>
        ColourQty = 1,
        /// <summary>
        /// 累计控球分钟
        /// </summary>
        IncDribbleMinutes = 2,
        /// <summary>
        /// 连续控球分钟
        /// </summary>
        CombDribbleMinutes = 3,
    }

}
