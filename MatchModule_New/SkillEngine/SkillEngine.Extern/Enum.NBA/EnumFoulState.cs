using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern.Enum.NBA
{
    public enum EnumFoulState : byte
    {
        /// <summary>
        /// 普通犯规
        /// </summary>
        FoulNormal = 1,
        /// <summary>
        /// 罚球
        /// </summary>
        FoulShot = 2,
        /// <summary>
        /// 技能罚球（天赋：上帝眷顾）
        /// </summary>
        SkillFoulShot = 3,

    }
}
