/*****************************************************************************
 * 文件名：BusinessBase
 * 
 * 创建人：
 * 
 * 创建时间：2009-11-5 13:19:47
 * 
 * 功能说明：Represents all the business objects in this system's base.
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Games.NB.Match.Base.BaseClass
{
    /// <summary>
    /// Represents all the business objects in this system's base.
    /// </summary>
    [Serializable]
    public abstract class BusinessBase : ICloneable
    {
        /// <summary>
        /// Clone current object.
        /// This method will invoke the GetClone method have the real result.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return GetClone();
        }

        /// <summary>
        /// Represents a common function to clone a object.
        /// While you want to improve the performance, you should override 
        /// this method in sub class.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        protected virtual BusinessBase GetClone()
        {
            using(var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(stream, this);
                stream.Flush();
                return formatter.Deserialize(stream) as BusinessBase;
            }
        }
    }
}
