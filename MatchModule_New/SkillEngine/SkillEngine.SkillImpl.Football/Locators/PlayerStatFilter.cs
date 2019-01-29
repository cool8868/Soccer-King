using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum.Football;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;

namespace SkillEngine.SkillImpl.Football
{
    public class PlayerStatFilter : ValueRangeFilterBase<int>, IPlayerFilter, ITrigger
    {
        public PlayerStatFilter(int minValue, int maxValue, EnumPlayerStat statType)
            : base(minValue, maxValue)
        {
            this.StatType = statType;
        }

        #region Data
        public EnumPlayerStat StatType;
        #endregion

        #region IPlayerFilter
        public bool Check(ISkillManager srcManager, ISkillPlayer srcPlayer, ISkillPlayer dstPlayer)
        {
            return CheckValue(dstPlayer.GetStatInt((int)StatType));
        }
        protected override int InnerCompare(int x, int y)
        {
            return x - y;
        }
        #endregion

        #region ITrigger
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
        public bool Trigger(ISkill srcSkill, ISkillPlayer caster)
        {
            if (null == caster)
                return false;
            return CheckValue(caster.GetStatInt((int)StatType));
        }
        #endregion
    }
}
