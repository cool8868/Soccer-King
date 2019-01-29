/********************************************************************************
 * 文件名：IShootStatus
 * 创建人：
 * 创建时间：2009-12-19 13:44:02
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Structs;

namespace Games.NB.Match.Base.Interface.Player.Status {

    /// <summary>
    /// Represents the player shooting's status.
    /// 表示了球员射门时的状态
    /// </summary>
    public interface IShootStatus {

        /// <summary>
        /// Represents the player's shooting target.
        /// 表示了射门的目标
        /// </summary>
        ShootTarget ShootTarget { get; set; }

        /// <summary>
        /// Represents the player's shooting target index
        /// 表示了射门目标的编号
        /// </summary>
        int ShootTargetIndex { get; set; }

        /// <summary>
        /// Represents the player's shoot speed.
        /// 表示了射门的球速
        /// </summary>
        int ShootSpeed { get; set; }

        /// <summary>
        /// Represents a region that player in there will have a chance to shoot.
        /// 表示了球员的射门区域
        /// </summary>
        Region ShootRegion { get; set; }
        /// <summary>
        /// 成功标记 0-射正失败;1-射正成功;
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
