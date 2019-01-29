/********************************************************************************
 * 文件名：IPositionalDecide
 * 创建人：
 * 创建时间：2009-12-21 13:24:09
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
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;

namespace Games.NB.Match.AI.Decides
{

    /// <summary>
    /// Decide the moving target.
    /// </summary>
    public interface IPositionalDecide
    {
        /// <summary>
        /// Decides the moving target.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        Coordinate DecideTarget(IPlayer player);
    }
}
