/********************************************************************************
 * 文件名：PlayerNotifyRedecide
 * 创建人：
 * 创建时间：2009-12-18 22:31:36
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：This partial class is implemented the interface of the <see cref="IDecide"/>.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.BLL.Model.Creatures {

    /// <summary>
    /// Represents the player entity.
    /// This partial class is implemented the interface of the <see cref="IDecide"/>.
    /// </summary>
    public sealed partial class Player
    {

        public void Redecide()
        {
            _status.Redecide();
        }
        /// <summary>
        /// Decide is over.
        /// </summary>
        public void DecideEnd()
        {
            _status.DecideEnd();
        }

    }
}
