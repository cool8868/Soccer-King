using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum.NBA
{
    public enum EnumGraphSeekType
    {
        /// <summary>
        /// 己方篮筐
        /// </summary>
        OwnBasket = 1,
        /// <summary>
        /// 己方底线
        /// </summary>
        OwnBackLine = 2,
        /// <summary>
        /// 己方3分线
        /// </summary>
        Own3PointLine = 3,
        /// <summary>
        /// 己方3秒区
        /// </summary>
        Own3SecArea = 4,
        /// <summary>
        /// 对方篮筐
        /// </summary>
        OppBasket = 11,
        /// <summary>
        /// 对方底线
        /// </summary>
        OppBackLine = 12,
        /// <summary>
        /// 对方3分线
        /// </summary>
        Opp3PointLine = 13,
        /// <summary>
        /// 对方3秒区
        /// </summary>
        Opp3SecArea = 14,
        /// <summary>
        /// 球员坐标
        /// </summary>
        PlayerPoint = 100,
    }
}
