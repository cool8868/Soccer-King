using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum
{
    #region SeekJoin
    public enum EnumSeekJoinType
    {
        /// <summary>
        /// 嵌套
        /// </summary>
        Nest = 1,
        /// <summary>
        /// 交集
        /// </summary>
        And = 2,
        /// <summary>
        /// 并集
        /// </summary>
        Or = 3,
    }
    #endregion
}
