/********************************************************************************
 * 文件名：IAction
 * 创建人：
 * 创建时间：2009-11-18 16:06:51
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.Base.Interface {

    /// <summary>
    /// Represents the interface of player's action.
    /// </summary>
    public interface IAction {

        /// <summary>
        /// Player to make a action.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        void Action(IPlayer player);
    }
}
