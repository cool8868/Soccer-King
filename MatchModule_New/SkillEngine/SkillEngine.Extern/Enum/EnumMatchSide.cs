using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern.Enum
{
    public enum EnumMatchSide:byte
    {
        /// <summary>
        /// 主场
        /// </summary>
        Home=0,
        /// <summary>
        /// 客场
        /// </summary>
        Away=1,
    }
    public enum EnumOwnSide : byte
    {
        /// <summary>
        /// 己方
        /// </summary>
        Own=0,
        /// <summary>
        /// 对方
        /// </summary>
        Opp=1,
    }
    public enum EnumGroundType : byte
    {
        /// <summary>
        /// 全场
        /// </summary>
        All = 0,
        /// <summary>
        /// 己方半场
        /// </summary>
        Own = 1,
        /// <summary>
        /// 对方半场
        /// </summary>
        Opp = 2,
    }
    public enum EnumBallSide : byte
    {
        /// <summary>
        /// 任意
        /// </summary>
        None=0,
        /// <summary>
        /// 进攻方
        /// </summary>
        Atk=1,
        /// <summary>
        /// 防守方
        /// </summary>
        Def=2,
    }
    public enum EnumBallState : byte
    {
        /// <summary>
        /// 任意
        /// </summary>
        None=0,
        /// <summary>
        /// 持球
        /// </summary>
        HoldBall=1,
        /// <summary>
        /// 无球
        /// </summary>
        OffBall=2,
    }


}
