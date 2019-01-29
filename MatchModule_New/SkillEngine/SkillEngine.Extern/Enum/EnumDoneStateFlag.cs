using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern.Enum
{
    /// <summary>
    /// 状态结果指示
    /// </summary>
    public enum EnumDoneStateFlag : byte
    {
        /// <summary>
        /// 无结果
        /// </summary>
        None = 0,
        /// <summary>
        /// 动作失败
        /// </summary>
        Fail = 1,
        /// <summary>
        /// 动作成功
        /// </summary>
        Succ = 2,
        /// <summary>
        /// 动作结束
        /// </summary>
        Over = 3,
    }
}
