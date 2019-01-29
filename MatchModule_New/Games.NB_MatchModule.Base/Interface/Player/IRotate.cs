/********************************************************************************
 * 文件名：IRotate
 * 创建人：
 * 创建时间：2009-12-9 13:38:30
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

namespace Games.NB.Match.Base.Interface.Player {

    /// <summary>
    /// Represents the object's rotate action.
    /// </summary>
    public interface IRotate {

        /// <summary>
        /// Rotate object to toward the target.
        /// </summary>
        /// <param name="target"><see cref="Coordinate"/></param>
        void Rotate(Coordinate target);
    }
}
