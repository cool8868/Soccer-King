using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern
{
    public sealed class Singleton<T> where T : class,new()
    {
        static T s_instnce = null;
        public static T Instance
        {
            get
            {
                if (null == s_instnce)
                {
                    s_instnce = new T();
                }
                return s_instnce;
            }
        }
    }
}
