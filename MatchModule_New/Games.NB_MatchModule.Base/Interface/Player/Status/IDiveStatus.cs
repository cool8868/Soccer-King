/********************************************************************************
 * 文件名：IDiveStatus
 * 创建人：
 * 创建时间：2010-3-1 13:44:17
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the goal keeper in dive ball state's status.
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
    /// Represents the goal keeper in dive ball state's status.
    /// 表示了守门员扑救时的状态
    /// </summary>
    public interface IDiveStatus
    {

        /// <summary>
        /// Represents the dive direction
        /// 表示了扑球的方向
        /// <example>
        /// 0 - 左
        /// 1 - 中
        /// 2 - 右
        /// </example>
        /// </summary>
        byte DiveDirection { get; set; }

        ///// <summary>
        ///// 成功扑球次数
        ///// </summary>
        //byte DiveSuccessNum { get; set; }
    }
}
