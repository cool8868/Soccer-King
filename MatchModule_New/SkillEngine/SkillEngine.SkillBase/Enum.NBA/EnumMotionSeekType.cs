using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum.NBA
{
    public enum EnumMotionSeekType
    {
        /// <summary>
        /// 自己
        /// </summary>
        OwnPlayer = 1,
        /// <summary>
        /// 传球目标
        /// </summary>
        PairingPlayer = 2,
        /// <summary>
        /// 传球来源
        /// </summary>
        PairedPlayer = 3,
        /// <summary>
        /// 助攻球员
        /// </summary>
        AssistPlayer=4,
        /// <summary>
        /// 队友
        /// </summary>
        OwnMates = 8,
        /// <summary>
        /// 己方所有成员
        /// </summary>
        OwnTeam = 9,
        /// <summary>
        /// 对位球员
        /// </summary>
        OppPlayer = 11,
        /// <summary>
        /// 对方相同位置球员
        /// </summary>
        OppParaPlayer=12,
        /// <summary>
        /// 传球目标对位球员
        /// </summary>
        OppPairingPlayer = 13,
        /// <summary>
        /// 对方所有成员
        /// </summary>
        OppTeam = 19,
        /// <summary>
        /// 己方持球人
        /// </summary>
        OwnBallHandler = 101,
        /// <summary>
        /// 对方持球人
        /// </summary>
        OppBallHandler = 111,
     
    }
}
