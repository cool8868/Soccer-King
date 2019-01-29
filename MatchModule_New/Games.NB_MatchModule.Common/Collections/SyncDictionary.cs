/*****************************************************************************
 * 文件名：SyncDictionary
 * 
 * 创建人：
 * 
 * 创建时间：2009-11-10 16:37:06
 * 
 * 功能说明：线程安全的Dictionary
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
using System;
using System.Collections.Generic;

namespace Games.NB.Match.Common.Collections {
    public class SyncDictionary<TKey, TValue> : KeyValueTable<TKey, TValue> {
        public SyncDictionary()
            : base() {
        }


        public SyncDictionary(int capacity)
            : base(capacity) {
        }


        public override int Count {
            get {
                lock (base.SyncRoot) {
                    return base.Count;
                }
            }
        }

        public override Dictionary<TKey, TValue>.KeyCollection Keys {
            get {
                lock (base.SyncRoot) {
                    return base.Keys;
                }
            }
        }

        public override Dictionary<TKey, TValue>.ValueCollection Values {
            get {
                lock (base.SyncRoot) {
                    return base.Values;
                }
            }
        }

        public override void Add(TKey key, TValue value) {
            lock (base.SyncRoot) {
                base.Add(key, value);
            }
        }

        public override void Clear() {
            lock (base.SyncRoot) {
                base.Clear();
            }
        }

        public override bool ContainsKey(TKey key) {
            lock (base.SyncRoot) {
                return base.ContainsKey(key);
            }
        }

        public override bool ContainsValue(TValue value) {
            lock (base.SyncRoot) {
                return base.ContainsValue(value);
            }
        }

        public override bool Remove(TKey key) {
            lock (base.SyncRoot) {
                return base.Remove(key);
            }
        }

        public override TValue this[TKey key] {
            get {
                lock (base.SyncRoot) {
                    return base[key];
                }
            }
            set {
                lock (base.SyncRoot) {
                    base[key] = value;
                }
            }
        }
    }
}
