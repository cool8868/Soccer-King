/********************************************************************************
 * 文件名：IDefenceStatus
 * 创建人：
 * 创建时间：2009-12-19 14:05:18
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the contracts of the defence status.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Base.Interface.Player.Status
{

    /// <summary>
    /// Represents the contracts of the defence status.
    /// 表示了球员防守时的状态
    /// </summary>
    public interface IDefenceStatus
    {
        /// <summary>
        /// Represents the defence target.
        /// 表示了该球员的防守目标
        /// </summary>
        IPlayer DefenceTarget { get; set; }
        /// <summary>
        /// Represents the player's defenders.
        /// 表示该球员的防守人
        /// </summary>
        IPlayer[] Defenders { get; }

        /// <summary>
        /// Represents the closest defender.
        /// 表示最近的防守人
        /// </summary>
        IPlayer Defender { get;}
        /// <summary>
        /// 表示最近的防守人的距离平方
        /// </summary>
        double DefenderDistancePow { get; }
        /// <summary>
        /// 表示协防的防守人
        /// </summary>
        IPlayer HelpDefender { get;  }
        /// <summary>
        /// 表示协防的防守人的距离平方
        /// </summary>
        double HelpDefenderDistancePow { get; }
        /// <summary>
        /// 刷新防守人
        /// </summary>
        void RefreshDefenders(bool forceFlag = false);

        /// <summary>
        /// 成功标记
        /// </summary>
        int SuccFlag { get; set; }
        /// <summary>
        /// 初始成功率
        /// </summary>
        int RawSuccRate { get; set; }
        /// <summary>
        /// 最终成功率
        /// </summary>
        int NewSuccRate { get; set; }

    }
}
