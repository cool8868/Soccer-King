using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public abstract class SpecEffect : EffectBase, ISpecEffect
    {
        #region .ctor
        protected SpecEffect(int buffId, int specTiming,
           bool mainFlag, bool pureFlag, bool debuffFlag)
            : base(EnumBuffType.Spec, new int[] { buffId }, mainFlag, pureFlag, debuffFlag)
        {
            this.SpecTiming = specTiming;
        }
        protected SpecEffect(ISkill srcSkill, int buffId, int specTiming,
            bool mainFlag, bool pureFlag, bool debuffFlag)
            : base(EnumBuffType.Spec, new int[] { buffId }, mainFlag, pureFlag, debuffFlag)
        {
            this.SrcSkill = srcSkill;
            this.SpecTiming = specTiming;
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
        public int SpecTiming
        {
            get;
            private set;
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
            this.StartEffect(srcSkill);
            foreach (var item in dstManagers)
            {
                ((ISpecBuffCore)item).AddSpecBuff(this);
            }
            return true;
        }
        public bool EffectPlayers(ISkill srcSkill, ISkillPlayer caster, IList<ISkillPlayer> dstPlayers)
        {
            if (null == dstPlayers || dstPlayers.Count == 0)
                return false;
            this.StartEffect(srcSkill);
            foreach (var item in dstPlayers)
            {
                ((ISpecBuffCore)item).AddSpecBuff(this);
            }
            return true;
        }
        public virtual bool PlayerAction(ISkill srcSkill, ISkillPlayer caster, ISkillPlayer dstPlayer)
        {
            if (this.OnPlayerAction(srcSkill, caster, dstPlayer))
            {
                this.TimeEnd = 0;
                return true;
            }
            return false;
        }
        public bool IsInvalid(ISkill srcSkill)
        {
            return srcSkill.InvalidFlag || this.TimeEnd == 0
                || this.TimeEnd > 0 && this.TimeEnd <= srcSkill.Context.MatchRound;
        }
        //TODO CoolDown in Action
        protected abstract bool OnPlayerAction(ISkill srcSkill, ISkillPlayer caster, ISkillPlayer dstPlayer);
        public abstract ISpecEffect Clone(ISkill srcSkill);
        #endregion

        #region Tools
        void StartEffect(ISkill srcSkill)
        {
            bool isStart = srcSkill.SkillState == EnumSkillState.Activate || srcSkill.SkillState == EnumSkillState.Triggering;
            if (isStart)
            {
                this.TimeEnd = Convert.ToInt16(srcSkill.Context.MatchRound + 2);
            }
        }
        #endregion

    }
}
