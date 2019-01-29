using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase.Enum;

namespace SkillEngine.SkillBase
{
    public abstract class WaitBuffBase : IWaitBuff
    {
        #region Cache
        protected int _waitIndex;
        #endregion

        #region Facade
        public bool WaitingFlag
        {
            get { return this._waitIndex > 0; }
        }
        public bool GetWaitingFlag(EnumBuffLast lastType)
        {
            int lastVal = (int)lastType;
            if (lastVal >= 0)
                return false;
            return (_waitIndex >> (-lastVal) & 1) == 1;
        }
        protected bool SetWaitingFlag(EnumBuffLast lastType, bool waitingFlag)
        {
            int lastVal = (int)lastType;
            if (lastVal >= 0)
                return false;
            if (waitingFlag)
                this._waitIndex |= 1 << (-lastVal);
            else
                this._waitIndex &= ~(1 << (-lastVal));
            return true;
        }
        public bool SetWaitBuffEnd(EnumBuffLast lastType)
        {
            if (!GetWaitingFlag(lastType))
                return false;
            bool val=this.OnSetWaitBuffEnd(lastType);
            if (val)
                SetWaitingFlag(lastType, false);
            return val;
        }
        #endregion

        protected abstract bool OnSetWaitBuffEnd(EnumBuffLast lastType);
    }

    public abstract class BuffCoreBase : WaitBuffBase, IAddBuff
    {
        public bool AddBuff(IBuff inBuff)
        {
            bool val = this.OnAddBuff(inBuff);
            if (val && inBuff.TimeEnd <= (short)EnumBuffLast.TillWaitEnd)
                SetWaitingFlag((EnumBuffLast)inBuff.TimeEnd, true);
            return val;
        }
        protected abstract bool OnAddBuff(IBuff inBuff);
    }
}
