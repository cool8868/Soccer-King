/********************************************************************************
 * 文件名：StateInitializer
 * 创建人：
 * 创建时间：2009-11-20 17:55:17
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the state's initializer.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using Games.NB.Match.Log;

namespace Games.NB.Match.AI {

    /// <summary>
    /// Represents the state's initializer.
    /// </summary>
    public static class StateInitializer {

        /// <summary>
        /// Initialize all the states.
        /// </summary>
        public static void Initialize() {
            lock (_syncRoot) {
                if (_hasInitialized) {
                    return;
                }

                try {
                    StateSelecter.Instance.Initialize();
                    foreach (var state in StateSelecter.Instance.States.Values) {
                        state.Initialize();
                    }

                    LogHelper.Insert("State initialized.", LogType.Info);
                    _hasInitialized = true;
                }
                catch (Exception ex) {
                    LogHelper.Insert(ex, "State Initializer cause exceptions.");
                }
            }

        }

        #region encapsulation

        private static bool _hasInitialized;
        private static readonly object _syncRoot = new object();

        #endregion
    }
}
