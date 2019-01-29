using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public class BoostEffect : EffectBase, IBoostEffect
    {
        #region .ctor
        public BoostEffect(int[] buffId, EnumBoostType boostType,
            bool mainFlag, bool pureFlag, bool debuffFlag)
            : base(EnumBuffType.Boost, buffId, mainFlag, pureFlag, debuffFlag)
        {
            this.BoostType = boostType;
        }
        public BoostEffect(ISkill srcSkill, int[] buffId, EnumBoostType boostType,
            bool mainFlag, bool pureFlag, bool debuffFlag)
            : base(EnumBuffType.Boost, buffId, mainFlag, pureFlag, debuffFlag)
        {
            this.SrcSkill = srcSkill;
            this.BoostType = boostType;
        }
        #endregion

        #region Data
        public bool InvalidFlag
        {
            get
            {
                if (null == SrcSkill)
                    return false;
                return SrcSkill.InvalidFlag || this.TimeEnd == 0
                       || this.TimeEnd > 0 && this.TimeEnd <= SrcSkill.Context.MatchRound;
            }
        }
        public ISkill SrcSkill
        {
            get;
            protected set;
        }
        public EnumBoostType BoostType
        {
            get;
            private set;
        }
        public int[] AntiSkillType
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

        #region Facade
        public bool EffectManager(ISkill srcSkill, ISkillPlayer caster, IList<ISkillManager> dstManagers)
        {
            if (null == dstManagers || dstManagers.Count == 0)
                return false;
            this.StartEffect(srcSkill, caster);
            foreach (var item in dstManagers)
            {
                ((IBoostCore)item).AddBoost(this);
            }
            return true;
        }
        public bool EffectPlayers(ISkill srcSkill, ISkillPlayer caster, IList<ISkillPlayer> dstPlayers)
        {
            if (null == dstPlayers || dstPlayers.Count == 0)
                return false;
            this.StartEffect(srcSkill, caster);
            foreach (var item in dstPlayers)
            {
                ((IBoostCore)item).AddBoost(this);
            }
            return true;
        }
        public override bool EffectSkills(ISkill srcSkill, ISkillPlayer caster, IList<ISkill> dstSkills)
        {
            if (null == dstSkills || dstSkills.Count == 0)
                return false;
            this.StartEffect(srcSkill, caster);
            foreach (var item in dstSkills)
            {
                ((IBoostCore)item).AddBoost(this);
                if (this.Last <= (short)EnumBuffLast.TillWaitEnd && null != item.Owner)
                {
                    item.Owner.AddBoostToSkill(item);
                }
            }
            return true;
        }
        public override bool UnEffectSkills(ISkill srcSkill, ISkillPlayer caster, ISkill dstSkill)
        {
            return dstSkill.RemoveBoost(this);
        }

        public IBoostEffect Clone(ISkill srcSkill)
        {
            var effect = new BoostEffect(srcSkill, this.BuffId, this.BoostType, this.MainFlag, this.PureFlag, this.DebuffFlag);
            effect.CopyValue(this);
            effect.Times = this.Times;
            effect.TimeEnd = this.TimeEnd;
            return effect;
        }
        #endregion

        #region Tools
        void StartEffect(ISkill srcSkill, ISkillPlayer caster)
        {
            bool isStart = srcSkill.SkillState == EnumSkillState.Activate || srcSkill.SkillState == EnumSkillState.Triggering
                || srcSkill.TimeStart == srcSkill.Context.MatchRound;
            if (isStart)
            {
                short last = srcSkill.Context.GetBuffLast(srcSkill, caster, this.Last);
                if (last > 0)
                    last += srcSkill.Context.MatchRound;
                this.TimeEnd = last;
            }
            if (this.Repeat > 0)
            {
                if (isStart)
                    this.Times = 1;
                else
                {
                    if (this.TimeEnd < 0 || this.TimeEnd > srcSkill.Context.MatchRound)
                        this.Times++;
                }
            }
        }
        #endregion

    }
}
