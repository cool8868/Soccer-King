using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillEngine.Editor.Football.Util
{
    public class CollectionsUtil
    {
        #region Dictionary
        public static bool TryGetDicKeyByValue<K, V>(Dictionary<K, V> dic, V val, out K key)
        {
            key = default(K);
            foreach (KeyValuePair<K, V> item in dic)
            {
                if (item.Value.Equals(val))
                {
                    key = item.Key;
                    return true;
                }
            }
            return false;
        }
        public static K GetDicKeyByValue<K, V>(Dictionary<K, V> dic, V val)
        {
            K rtnK = default(K);
            foreach (KeyValuePair<K, V> item in dic)
            {
                if (item.Value.Equals(val))
                    return item.Key;
            }
            return rtnK;
        }
        public static K GetDicKeyByIndex<K, V>(Dictionary<K, V> dic, int index)
        {
            K rtnK = default(K);
            int count = 0;
            foreach (KeyValuePair<K, V> item in dic)
            {
                if (index == count) 
                    return item.Key;
                count++;
            }
            return rtnK;
        }
        public static V GetDicValueByIndex<K, V>(Dictionary<K, V> dic, int index)
        {
            V rtnV = default(V);
            int count = 0;
            foreach (KeyValuePair<K, V> item in dic)
            {
                if (index == count) 
                    return item.Value;
                count++;
            }
            return rtnV;
        }
        #endregion

        #region List
        public static string JoinStringList(List<string> list, string splitStr)
        {
            string str = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                    str += list[i];
                else
                    str += splitStr + list[i];
            }
            return str;
        }
        #endregion
    }
}
