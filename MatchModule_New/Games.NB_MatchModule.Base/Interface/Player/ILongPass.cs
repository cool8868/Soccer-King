/********************************************************************************
 * 文件名：ILongPass
 * 创建人：
 * 创建时间：2009-12-16 17:53:40
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

namespace Games.NB.Match.Base.Interface.Player {

    /// <summary>
    /// Defines the methods of a player to action a long pass.
    /// </summary>
    public interface ILongPass {        

        /// <summary>
        /// Player to action a long pass.
        /// </summary>
        void LongPass();
    }
}
