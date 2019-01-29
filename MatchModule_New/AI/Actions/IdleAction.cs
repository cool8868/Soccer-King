/********************************************************************************
 * 文件名：IdleAction
 * 创建人：
 * 创建时间：2009-11-23 16:25:52
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the idle state's action.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.Actions {

    /// <summary>
    /// Represents the idle state's action.
    /// This class implemented the singleton pattern.
    /// </summary>
    class IdleAction : IAction {

        /// <summary>
        /// Player to make a action.
        /// </summary>
        /// <param name="player"></param>
        public void Action(IPlayer player) {
            // throw new NotImplementedException();            
            (player as INotifyDecide).Redecide();
        }
    }
}
