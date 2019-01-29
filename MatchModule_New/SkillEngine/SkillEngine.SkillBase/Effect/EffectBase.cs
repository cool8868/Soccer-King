using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillBase
{
    public abstract class EffectBase : IEffect, IEffectSkills
    {
        #region .ctor
        protected EffectBase(EnumBuffType buffType, int[] buffId, bool mainFlag, bool pureFlag, bool debuffFlag)
        {
            this.BuffType = buffType;
            this.BuffId = buffId;
            this.MainFlag = mainFlag;
            this.PureFlag = pureFlag;
            this.DebuffFlag = debuffFlag;
        }
        #endregion

        #region Data
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
        public EnumBuffType BuffType
        {
            get;
            protected set;
        }
        public int[] BuffId
        {
            get;
            protected set;
        }
        public bool MainFlag
        {
            get;
            protected set;
        }
        public bool PureFlag
        {
            get;
            protected set;
        }
        public bool DebuffFlag
        {
            get;
            protected set;
        }
        public int Last
        {
            get;
            set;
        }
        public short Repeat
        {
            get;
            set;
        }
        public bool Recycle
        {
            get;
            set;
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
        public SkillModelSetting SrcModelSetting
        {
            get;
            set;
        }
        public SkillModelSetting TgtModelSetting
        {
            get;
            set;
        }
        #endregion

        #region Facade
        public bool RedoState(ISkill srcSkill, ISkillPlayer caster)
        {
            if (this.Repeat <= 0)
                return false;
            int last = srcSkill.Context.GetBuffLast(srcSkill, caster, this.Last);
            if (last > 0)
                return srcSkill.Context.MatchRound < (srcSkill.TimeStart + last);
            if (this.Last == (int)EnumBuffLast.Undo)
                return true;
            if (this.Last <= (int)EnumBuffLast.TillWaitEnd)
                return srcSkill.WaitingFlag;
            return false;
        }
        public void CopyValue(IEffect srcEffect)
        {
            if (null == srcEffect)
                return;
            this.Rate = srcEffect.Rate;
            this.Point = srcEffect.Point;
            this.Percent = srcEffect.Percent;
            this.Last = srcEffect.Last;
            this.Repeat = srcEffect.Repeat;
            this.Recycle = srcEffect.Recycle;
            this.SrcModelSetting = srcEffect.SrcModelSetting;
            this.TgtModelSetting = srcEffect.TgtModelSetting;
        }
        #endregion

        #region SkillShow
        public void AddSrcShowModel(ISkill srcSkill, ISkillPlayer caster, int last)
        {
            if (null == caster || null == caster.SkillCore)
                return;
            var model = this.SrcModelSetting;
            if (null == model || model.ModelId <= 0)
                return;
            if (model.ModelLast > 0)
                last = srcSkill.Context.GetBuffLast(srcSkill, caster, model.ModelLast);
            caster.SkillCore.AddShowModel(srcSkill, model.ModelId, last);
        }
        public void AddTgtShowModel(ISkill srcSkill, ISkillOwner target, int last)
        {
            var player = target as ISkillPlayer;
            if (null == player || null == player.SkillCore)
                return;
            var model = this.TgtModelSetting;
            if (null == model || model.ModelId <= 0)
                return;
            if (model.ModelLast > 0)
                last = srcSkill.Context.GetBuffLast(srcSkill, null, model.ModelLast);
            player.SkillCore.AddShowModel(srcSkill, model.ModelId, last);
        }
        public void RemoveShowModel(ISkill srcSkill, ISkillOwner owner, bool tgtFlag)
        {
            var player = owner as ISkillPlayer;
            if (null == player || null == player.SkillCore)
                return;
            var model = tgtFlag ? this.TgtModelSetting : this.SrcModelSetting;
            if (null == model || model.ModelId <= 0)
                return;
            player.SkillCore.RemoveShowModel(srcSkill, model.ModelId);
        }
        #endregion

        #region EffectSkills
        public virtual bool EffectSkills(ISkill srcSkill, ISkillPlayer caster, IList<ISkill> dstSkills)
        {
            foreach (var target in dstSkills)
            {
                target.AddEffect(this);
            }
            return true;
        }
        public virtual bool UnEffectSkills(ISkill srcSkill, ISkillPlayer caster, ISkill dstSkill)
        {
            return dstSkill.RemoveEffect(this);
        }
        #endregion
    }
}
