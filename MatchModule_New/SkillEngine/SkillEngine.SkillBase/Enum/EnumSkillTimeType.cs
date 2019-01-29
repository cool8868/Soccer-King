using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum
{
    public enum EnumSkillTimeType : byte
    {
        /// <summary>
        /// 回合
        /// </summary>
        Round = 0,
        /// <summary>
        /// 分钟
        /// </summary>
        Minute = 1,
        /// <summary>
        /// 节
        /// </summary>
        Section = 2,
        /// <summary>
        /// 场次
        /// </summary>
        Session = 3,
        /// <summary>
        /// 思考前
        /// </summary>
        PreDecide=4,

    }
   
}
