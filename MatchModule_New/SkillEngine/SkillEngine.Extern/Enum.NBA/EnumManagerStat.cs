using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern.Enum.NBA
{
    public enum EnumManagerStat
    {
        #region Goals
        /// <summary>
        /// 球队得分数
        /// </summary>
        GS = 1,
        /// <summary>
        /// 球队失分数
        /// </summary>
        GA = 2,
        /// <summary>
        /// 球队净得分
        /// </summary>
        GD = 3,
        /// <summary>
        /// 球队连续得分(天赋:士气高涨)
        /// </summary>
        CombScore = 4,
        /// <summary>
        /// 球队连续三分球(天赋:士气压制)
        /// </summary>
        CombThreePoint = 5,
        /// <summary>
        /// 单节扳平分差(天赋:无尽斗志)
        /// </summary>
        DrawLevelPerSection = 6
        #endregion

    }
}
