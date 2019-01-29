/********************************************************************************
 * 文件名：IPassStatus
 * 创建人：
 * 创建时间：2009-12-19 13:59:35
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the player passing's status.
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
    /// Represents the player passing's status.
    /// 表示了球员传球的状态
    /// </summary>
    public interface IPassStatus
    {
        /// <summary>
        /// Represents the passing's target.
        /// 表示了球员的传球目标
        /// </summary>
        IPlayer PassTarget { get; set; }
        /// <summary>
        /// 传球的来源
        /// </summary>
        IPlayer PassFrom { get; set; }
        /// <summary>
        /// Represnts whether is the pass fail.
        /// 表示了是否传球失败
        /// </summary>
        bool IsPassFail { get; set; }

    }
}
