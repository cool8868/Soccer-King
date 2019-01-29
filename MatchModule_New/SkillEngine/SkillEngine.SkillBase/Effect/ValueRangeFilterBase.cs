using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase
{
    public abstract class ValueRangeFilterBase<T>
    {
        #region .ctor
        protected ValueRangeFilterBase(T minValue, T maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }
        #endregion

        #region Data
        public T MinValue
        {
            get;
            protected set;
        }
        public T MaxValue
        {
            get;
            protected set;
        }
        #endregion

        protected bool CheckValue(T inValue)
        {
            return InnerCompare(MinValue, inValue) <= 0 && InnerCompare(inValue, MaxValue) <= 0;
        }
        protected abstract int InnerCompare(T x, T y);
    }
}
