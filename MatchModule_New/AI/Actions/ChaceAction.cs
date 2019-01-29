/********************************************************************************
 * 文件名：ChaceAction
 * 创建人：
 * 创建时间：2009-11-19 16:33:16
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the chace action.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Attributes;

namespace Games.NB.Match.AI.Actions {

    /// <summary>
    /// Represents the chace state's action.
    /// This class implemented the singleton pattern.
    /// </summary>
    [Singleton]
    class ChaceAction : IAction {

        /// <summary>
        /// Represents the instance of the <see cref="ChaceAction"/>.
        /// </summary>
        public static ChaceAction Instance {
            get { return _instance; }
        }

        /// <summary>
        /// Player to make a action.
        /// </summary>
        /// <param name="player"></param>
        public void Action(IPlayer player) {
            player.Move();
        }

        #region encapsulation

        private static readonly ChaceAction _instance = new ChaceAction();

        #endregion
    }
}
