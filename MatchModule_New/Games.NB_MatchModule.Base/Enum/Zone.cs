/********************************************************************************
 * 文件名：Zone
 * 创建人：
 * 创建时间：2009-12-3 11:04:21
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the player's current region.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Base.Enum {

    /// <summary>
    /// Represents the player's current region.
    /// 表示了球员所在的区域
    /// </summary>
    public enum Zone {

        /// <summary>
        /// 对方半场
        /// </summary>
        OpposingHalf,        

        /// <summary>
        /// 本方半场
        /// </summary>
        OwnHalf
    }
}
