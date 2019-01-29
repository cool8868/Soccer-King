using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase.Enum;

namespace SkillEngine.SkillBase
{
    public abstract class BuffBase : IBuff
    {
        #region Cache
        protected readonly ISkill _skill;
        protected readonly EnumBuffType _buffType;
        protected readonly int[] _buffId;
        #endregion

        #region .ctor
        protected BuffBase(ISkill skill, EnumBuffType buffType, int[] buffId)
        {
            this._skill = skill;
            this._buffType = buffType;
            this._buffId = buffId;
        }
        #endregion

        #region Data
        public virtual bool DebuffFlag
        {
            get { return this.Rate < 0 || this.Point < 0 || this.Percent < 0; }
        }
        public virtual bool ValuedFlag
        {
            get { return this.Rate != 0 || this.Point != 0 || this.Percent != 0; }
        }
        public virtual bool InvalidFlag
        {
            get
            {
                return this._skill.InvalidFlag || this.TimeEnd == 0
                    || this.TimeEnd > 0 && this.TimeEnd <= this._skill.Context.MatchRound;
            }
        }
        public double AsPoint
        {
            get { return this.Point / SkillDefines.STEPStorePoint; }
        }
        public double AsPerPoint
        {
            get { return this.Point / SkillDefines.STEPStorePercent; }
        }
        public double AsPercent
        {
            get { return this.Percent / SkillDefines.STEPStorePercent; }
        }
        public double AsRate
        {
            get { return this.Rate / SkillDefines.STEPStorePercent; }
        }

        public ISkill SrcSkill
        {
            get { return this._skill; }
        }
        public EnumBuffType BuffType
        {
            get { return this._buffType; }
        }
        public int[] BuffId
        {
            get { return this._buffId; }
        }
        public int Rate
        {
            get;
            set;
        }
        public int Point
        {
            get;
            set;
        }
        public int Percent
        {
            get;
            set;
        }
        public short Times
        {
            get;
            set;
        }
        public short TimeEnd
        {
            get;
            set;
        }
        #endregion

        #region IBuff
        public bool PlusBuff(IBuff srcBuff)
        {
            return this.PlusCore(srcBuff);
        }
        public bool CoverBuff(IBuff srcBuff)
        {
            if (null == srcBuff)
                return false;
            if (this.Times > 0 && this.TimeEnd != 0)
                return this.PlusCore(srcBuff);
            else
                return this.CoverCore(srcBuff);
        }
        public void CopyValue(IBuff srcBuff)
        {
            this.CoverCore(srcBuff);
        }
        public virtual bool Clear()
        {
            this.Rate = 0;
            this.Point = 0;
            this.Percent = 0;
            return true;
        }
        public IBuff Clone()
        {
            return Clone(this.BuffId);
        }
        public abstract IBuff Clone(params int[] buffId);
        protected virtual bool PlusCore(IBuff srcBuff)
        {
            if (null == srcBuff)
                return false;
            this.Rate = srcBuff.Rate;
            this.Point += srcBuff.Point;
            this.Percent += srcBuff.Percent;
            return true;
        }
        protected virtual bool CoverCore(IBuff srcBuff)
        {
            if (null == srcBuff)
                return false;
            this.Rate = srcBuff.Rate;
            this.Point = srcBuff.Point;
            this.Percent = srcBuff.Percent;
            this.Times = srcBuff.Times;
            this.TimeEnd = srcBuff.TimeEnd;
            return true;
        }
        #endregion
    }
}
