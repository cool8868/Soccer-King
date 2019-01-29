using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;

namespace SkillEngine.SkillCore
{
    public class BoostBuff : IBoostBuff
    {
        #region Data
        public bool ValuedFlag
        {
            get { return this.Point != 0 || this.Percent != 0; }
        }
        public double AsPoint
        {
            get { return this.Point / SkillDefines.STEPStorePoint; }
        }
        public double AsPercent
        {
            get { return this.Percent / SkillDefines.STEPStorePercent; }
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
        #endregion

        #region .ctor
        public BoostBuff()
        { }
        public BoostBuff(int point, int percent)
        {
            this.Point = point;
            this.Percent = percent;
        }
        #endregion

        public bool PlusBuff(IBoostBuff srcBuff)
        {
            this.Point += srcBuff.Point;
            this.Percent += srcBuff.Percent;
            return true;
        }
        public bool Clear()
        {
            this.Point = 0;
            this.Percent = 0;
            return true;
        }
        public IBoostBuff Clone()
        {
            return new BoostBuff(this.Point, this.Percent);
        }
    }

    public class BoostAntiBuff : BoostBuff, IBoostAntiBuff
    {
        #region Data
        public int[] AntiSkillType
        {
            get;
            set;
        }
        #endregion

        #region .ctor
        public BoostAntiBuff()
        { }
        public BoostAntiBuff(int point, int percent, int[] antiSkillType)
        {
            this.Point = point;
            this.Percent = percent;
            this.AntiSkillType = antiSkillType;
        }
        #endregion

        public bool PlusBuff(IBoostBuff srcBuff)
        {
            var antiBuff = srcBuff as BoostAntiBuff;
            if (null == antiBuff)
                return false;
            this.Point += antiBuff.Point;
            this.Percent += antiBuff.Percent;
            this.AntiSkillType = antiBuff.AntiSkillType;
            return true;
        }
        public bool Clear()
        {
            this.Point = 0;
            this.Percent = 0;
            return true;
        }
        public IBoostBuff Clone()
        {
            return new BoostAntiBuff(this.Point, this.Percent, this.AntiSkillType);
        }
    }
}
