/*****************************************************************************
 * 文件名：KeyValueTalbe
 * 
 * 创建人：
 * 
 * 创建时间：2009-11-10 16:24:44
 * 
 * 功能说明：
 * 封装了系统的System.Collection.Generic.Dictionary
 *  
 * 历史修改记录：
 *   2009-11-30 11:14:32 修改了类的名称
 * 
 * ***************************************************************************/
using System;
using System.Collections.Generic;

namespace Games.NB.Match.Common.Collections
{
    /// <summary>
    /// Key Value Table
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class KeyValueTable<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _dictionary;
        private readonly object _syncRoot = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueTable"/>
        /// </summary>
        public KeyValueTable()
        {
            _dictionary = new System.Collections.Generic.Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueTable"/>
        /// </summary>
        /// <param name="capacity"></param>
        public KeyValueTable(int capacity)
        {
            _dictionary = new System.Collections.Generic.Dictionary<TKey, TValue>(capacity);
        }

        /// <summary>
        /// Count
        /// </summary>
        public virtual int Count
        {
            get { return _dictionary.Count; }
        }

        /// <summary>
        /// Keys
        /// </summary>
        public virtual Dictionary<TKey, TValue>.KeyCollection Keys
        {
            get { return _dictionary.Keys; }
        }

        /// <summary>
        /// Values
        /// </summary>
        public virtual Dictionary<TKey, TValue>.ValueCollection Values
        {
            get { return _dictionary.Values; }
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
        }

        /// <summary>
        /// Clear
        /// </summary>
        public virtual void Clear()
        {
            _dictionary.Clear();
        }

        /// <summary>
        /// ContainsKey
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Contains Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool ContainsValue(TValue value)
        {
            return _dictionary.ContainsValue(value);
        }

        public virtual bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }

        public virtual TValue this[TKey key]
        {
            get { return _dictionary[key]; }
            set { _dictionary[key] = value; }
        }

        #region encapsulation

        protected Object SyncRoot
        {
            get { return _syncRoot; }
        }

        #endregion
    }
}
