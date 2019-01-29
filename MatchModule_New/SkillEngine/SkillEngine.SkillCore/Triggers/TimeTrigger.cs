using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public class TimeTrigger : ITrigger
    {
        #region Data
        public EnumSkillTimeType TimeType
        {
            get;
            set;
        }
        public int[] Values
        {
            get;
            set;
        }
        public bool Repeat
        {
            get;
            set;
        }
        public bool Recycle
        {
            get;
            set;
        }
        public bool Delay
        {
            get;
            set;
        }
        #endregion

        public bool Trigger(ISkill srcSkill, ISkillPlayer caster)
        {
            if (null == this.Values || this.Values.Length == 0)
                return true;
            int round = srcSkill.Context.MatchRound;
            int weight = BuffUtil.GetTimeWeight(srcSkill.Context, TimeType);
            int len = Values.Length;
            for (int i = 0; i < len; i += 2)
            {
                if (i + 1 >= len)
                    break;
                if (Values[i] * weight <= round && round <= Values[i + 1] * weight)
                    return true;
            }
            return false;
        }
    }

    public class PerTimeTrigger : ITrigger
    {
        #region Data
        public EnumSkillTimeType TimeType
        {
            get;
            set;
        }
        public int TimeStep
        {
            get;
            set;
        }
        public bool Repeat
        {
            get { return true; }
        }
        public bool Recycle
        {
            get;
            set;
        }
        public bool Delay
        {
            get;
            set;
        }
        #endregion

        public bool Trigger(ISkill srcSkill, ISkillPlayer caster)
        {
            if (TimeStep <= 0)
                return true;
            int round = srcSkill.Context.MatchRound;
            int weight = BuffUtil.GetTimeWeight(srcSkill.Context, TimeType);
            if (weight * TimeStep <= 1)
                return true;
            return round % (weight * TimeStep) == 1;
        }
    }
} 
