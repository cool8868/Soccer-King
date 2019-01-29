using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern
{
    public class RandomHelper
    {
        #region .ctor
        readonly Random _random;
        readonly bool _syncFlag;
        readonly object _syncRoot;
        public RandomHelper()
            : this(false)
        { }
        public RandomHelper(bool syncFlag)
        {
            this._random = new Random(Guid.NewGuid().GetHashCode());
            this._syncFlag = syncFlag;
            if (syncFlag)
                this._syncRoot = new object();
        }
        #endregion

        #region Facade
        public bool RandomBool()
        {
            return RandomInt32(0, 1) == 0;
        }
        public byte RandomPercent()
        {
            return RandomByte(0, 100);
        }
        public byte RandomByte(byte min, byte max)
        {
            if (min == max)
                return min;
            if (min > max)
            {
                var tmp = max;
                max = min;
                min = tmp;
            }
            if (min < Byte.MinValue)
                min = Byte.MinValue;
            if (max + 1 > Byte.MaxValue)
                max = Byte.MaxValue;
            if (!_syncFlag)
                return (byte)_random.Next(min, max + 1);
            lock (_syncRoot)
            {
                return (byte)_random.Next(min, max + 1);
            }
        }
        public int RandomInt32(int min, int max)
        {
            if (min == max)
                return min;
            if (min > max)
            {
                var tmp = max;
                max = min;
                min = tmp;
            }
            if (max < Int32.MaxValue)
                max = max + 1;
            if (!_syncFlag)
                return _random.Next(min, max);
            lock (_syncRoot)
            {
                return (byte)_random.Next(min, max);
            }
        }
        #endregion

    }
}
