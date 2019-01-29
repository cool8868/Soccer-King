using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum.Football
{
    public enum EnumGraphSeekType
    {
        /// <summary>
        /// 己方球门
        /// </summary>
        OwnGoal = 1,
        /// <summary>
        /// 己方底线
        /// </summary>
        OwnBackLine = 2,
        /// <summary>
        /// 己方禁区
        /// </summary>
        OwnGoalArea = 3,
        /// <summary>
        /// 己方区域
        /// </summary>
        OwnRegion = 9,
        /// <summary>
        /// 对方球门
        /// </summary>
        OppGoal = 11,
        /// <summary>
        /// 对方底线
        /// </summary>
        OppBackLine = 12,
        /// <summary>
        /// 对方禁区
        /// </summary>
        OppGoalArea = 13,
        /// <summary>
        /// 对方区域
        /// </summary>
        OppRegion = 19,
        /// <summary>
        /// 球员坐标
        /// </summary>
        PlayerPoint = 100,
    }
}
