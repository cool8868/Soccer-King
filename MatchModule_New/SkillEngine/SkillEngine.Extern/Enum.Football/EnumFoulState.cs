using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern.Enum.Football
{
    public enum EnumFoulState : byte
    {
        None=9,
        /// <summary>
        /// 普通犯规
        /// </summary>
        FoulNormal = 0,
        /// <summary>
        /// 黄牌
        /// </summary>
        FoulYellow = 1,
        /// <summary>
        /// 红牌
        /// </summary>
        FoulRed = 2,
    }
}
