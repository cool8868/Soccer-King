/********************************************************************************
 * 文件名：IManagerStatus
 * 创建人：
 * 创建时间：2010/3/26 10:29:36
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System.Collections.Generic;

namespace Games.NB.Match.Base.Interface.Manager
{
    /// <summary>
    /// Represents the interface of the manager's status.
    /// 表示了经理的当前回合状态
    /// </summary>
    public interface IManagerStatus
    {
        /// <summary>
        /// Represents the rate of the help defense.
        /// 协防的概率
        /// </summary>
        int HelpDefenseRate { get; set; }

        /// <summary>
        /// Represents the first player.
        /// 表示了最前面的球员
        /// </summary>
        IPlayer HeadMostPlayer { get; set; }

        /// <summary>
        /// Represents the last player.(Except the goal keeper)
        /// 表示了最后一名球员(除守门员)
        /// </summary>
        IPlayer LastPlayer { get; set; }
        /// <summary>
        /// 经理数据统计
        /// </summary>
        IManagerStatStatus StatStatus { get; }

    }
}
