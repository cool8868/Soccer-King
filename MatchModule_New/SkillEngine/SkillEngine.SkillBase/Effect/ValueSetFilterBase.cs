using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase
{
    public abstract class ValueSetFilterBase<T>
    {
        #region .ctor
        protected ValueSetFilterBase(T[] values)
        {
            this.Values = values;
        }
        #endregion

        #region Data
        public T[] Values
        {
            get;
            protected set;
        }
        #endregion

        protected bool CheckValue(T inValue)
        {
            if (null == Values || Values.Length == 0)
                return true;
            foreach (var val in Values)
            {
                if (InnerEquals(val, inValue))
                    return true;
            }
            return false;
        }
        protected abstract bool InnerEquals(T x, T y);
    }
}
