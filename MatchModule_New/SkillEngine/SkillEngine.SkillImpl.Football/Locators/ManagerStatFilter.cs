using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum;
using SkillEngine.Extern.Enum.Football;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;

namespace SkillEngine.SkillImpl.Football
{
    public class ManagerStatFilter : ValueRangeFilterBase<int>, IManagerFilter, IPlayerFilter, ITrigger
    {
        public ManagerStatFilter(int minValue, int maxValue, EnumManagerStat statType)
            : base(minValue, maxValue)
        {
            this.StatType = statType;
            this.Side = EnumOwnSide.Own;
        }

        #region Data
        public EnumManagerStat StatType
        {
            get;
            set;
        }
        public EnumOwnSide Side
        {
            get;
            set;
        }
        #endregion

        #region IFilter
        public bool Check(ISkillManager srcManager, ISkillManager dstManager)
        {
            if (Side == EnumOwnSide.Opp)
                dstManager = dstManager.OppSkillManager;
            return CheckValue(dstManager.GetStatInt((int)StatType));
        }
        public bool Check(ISkillManager srcManager, ISkillPlayer srcPlayer, ISkillPlayer dstPlayer)
        {
            var dstManager = dstPlayer.SkillManager;
            if (Side == EnumOwnSide.Opp)
                dstManager = dstManager.OppSkillManager;
            return CheckValue(dstManager.GetStatInt((int)StatType));
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
            var trigger = null != caster ? caster.SkillManager : srcSkill.Owner as ISkillManager;
            if (null == trigger)
                return false;
            if (Side == EnumOwnSide.Opp)
                trigger = trigger.OppSkillManager;
            return CheckValue(trigger.GetStatInt((int)StatType));
        }
        #endregion
    }
}
