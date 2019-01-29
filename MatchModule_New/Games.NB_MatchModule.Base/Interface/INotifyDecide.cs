/********************************************************************************
 * 文件名：INotifyRedecide
 * 创建人：
 * 创建时间：2009-11-18 14:28:53
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the interface of to notify creatures to redecide.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/

namespace Games.NB.Match.Base.Interface {

    /// <summary>
    /// Represents the methods that how to notify creatures to redecide.
    /// </summary>
    public interface INotifyDecide {

        /// <summary>
        /// Redecide.
        /// </summary>
        void Redecide();
        /// <summary>
        /// Decide is over.
        /// </summary>
        void DecideEnd();
    }
}
