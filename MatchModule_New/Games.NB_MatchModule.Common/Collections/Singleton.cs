/********************************************************************************
 * 文件名：Singleton
 * 创建人：
 * 创建时间：2009-11-20 17:00:59
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

namespace Games.NB.Match.Common.Collections {
    public sealed class Singleton<T> where T : class, new() {
        public static T Instance {
            get { return _instance; }
        }

        #region encapsulation

        private static readonly T _instance = new T();

        private Singleton() { }

        #endregion
    }
}
