/********************************************************************************
 * 文件名：IFoul
 * 创建人：
 * 创建时间：2010-2-21 10:21:04
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the interface of the foul.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Base.Interface.Player
{

    /// <summary>
    /// Represents the interface of the foul.
    /// </summary>
    public interface IFoul
    {
        /// <summary>
        /// Foul
        /// </summary>
        /// <param name="level">
        /// foul's level.
        /// 0 - Normal
        /// 1 - Yellow Card
        /// 2 - Red Card
        /// </param>
        void Foul(byte level);
      
    }
}
