/********************************************************************************
 * 文件名：PositionalAction
 * 创建人：Jiawei Chegn
 * 创建时间：2009-11-19 15:09:04
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the positional state's action.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.Actions {

    /// <summary>
    /// Represents the positional state's action.
    /// This class implemented the singleton pattern.
    /// </summary>
    class PositionalAction : IAction {

        /// <summary>
        /// Represents the instance of the <see cref="PositionalAction"/>
        /// </summary>
        public static PositionalAction Instance {
            get { return _instance; }
        }

        /// <summary>
        /// Player to make a action.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        public void Action(IPlayer player) {
            player.Move();
        }

        #region encapsulation

        private static readonly PositionalAction _instance = new PositionalAction();

        #endregion
    }
}
