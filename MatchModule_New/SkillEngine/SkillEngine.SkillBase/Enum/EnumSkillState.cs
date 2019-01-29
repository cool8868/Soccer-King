using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum
{

    public enum EnumSkillState
    {
        /// <summary>
        /// 无效
        /// </summary>
        Invalid=-1,
        /// <summary>
        /// 活动
        /// </summary>
        Activate = 0,
        /// <summary>
        /// 触发中
        /// </summary>
        Triggering = 1,
        /// <summary>
        /// 生效
        /// </summary>
        Effecting = 2,
        /// <summary>
        /// 冷却
        /// </summary>
        CoolDown = 3,
    }

    public enum EnumSkillFlag
    {
        None = 0,
        /// <summary>
        /// 增益技能
        /// </summary>
        Buff = 1,
        /// <summary>
        /// 减益技能
        /// </summary>
        Debuff = 2,
        /// <summary>
        /// 混合
        /// </summary>
        Mix=3,
    }
}
