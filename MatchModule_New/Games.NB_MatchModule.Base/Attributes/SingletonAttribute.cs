/********************************************************************************
 * 文件名：SingletonAttribute
 * 创建人：
 * 创建时间：2009-12-7 10:05:48
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

namespace Games.NB.Match.Base.Attributes {

    /// <summary>
    /// Represents a singleton pattern class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class SingletonAttribute : Attribute {

        /// <summary>
        /// Initialize a new <see cref="SingletonAttribute"/>.
        /// </summary>
        public SingletonAttribute() {
        }
    }
}
