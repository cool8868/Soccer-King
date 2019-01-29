using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillBase.Enum.Football;


namespace SkillEngine.SkillCore
{
    public class ClearCDEffect : EffectBase, INormalEffect
    {
        #region .ctor
        public ClearCDEffect(bool mainFlag, bool pureFlag, bool debuffFlag)
            : base(EnumBuffType.Spec, new int[] { 0 }, mainFlag, pureFlag, debuffFlag)
        { }
        #endregion


        #region Data
        public string[] Ids
        {
            get;
            set;
        }
        public EnumSkillSrcType[] SrcTypes
        {
            get;
            set;
        }
        public EnumSkillActType[] ActTypes
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
            int rate = this.Rate;
            int maxRate = SkillDefines.MAXStorePercent;
            if (rate <= 0 || rate < maxRate && srcSkill.Context.RandomPercent(maxRate) > rate)
                return false;
            bool rtnVal = false;
            List<ISkill> dstSkills;
            foreach (var target in dstManagers)
            {
                dstSkills = InnerSeek(target.SkillCore.SkillList);
                if (null == dstSkills)
                    continue;
                foreach (var skill in dstSkills)
                {
                    skill.SkillState = EnumSkillState.Activate;
                }
                rtnVal = true;
            }
            if (rtnVal && null != this.SrcModelSetting && this.SrcModelSetting.ModelId > 0)
                this.AddSrcShowModel(srcSkill, caster, srcSkill.Context.GetBuffLast(srcSkill, caster, this.Last));
            return rtnVal;
        }
        public bool EffectPlayers(ISkill srcSkill, ISkillPlayer caster, IList<ISkillPlayer> dstPlayers)
        {
            if (null == dstPlayers || dstPlayers.Count == 0)
                return false;
            int rate = this.Rate;
            int maxRate = SkillDefines.MAXStorePercent;
            if (rate <= 0 || rate < maxRate && srcSkill.Context.RandomPercent(maxRate) > rate)
                return false;
            bool rtnVal = false;
            List<ISkill> dstSkills;
            foreach (var target in dstPlayers)
            {
                dstSkills = InnerSeek(target.SkillCore.SkillList);
                if (null == dstSkills)
                    continue;
                foreach (var skill in dstSkills)
                {
                    skill.SkillState = EnumSkillState.Activate;
                }
                rtnVal = true;
            }
            if (rtnVal && null != this.SrcModelSetting && this.SrcModelSetting.ModelId > 0)
                this.AddSrcShowModel(srcSkill, caster, srcSkill.Context.GetBuffLast(srcSkill, caster, this.Last));
            return rtnVal;
        }
        public override bool EffectSkills(ISkill srcSkill, ISkillPlayer caster, IList<ISkill> dstSkills)
        {
            if (null == dstSkills || dstSkills.Count == 0)
                return false;
            int rate = this.Rate;
            int maxRate = SkillDefines.MAXStorePercent;
            if (rate <= 0 || rate < maxRate && srcSkill.Context.RandomPercent(maxRate) > rate)
                return false;
            foreach (var skill in dstSkills)
            {
                skill.SkillState = EnumSkillState.Activate;
            }
            return true;
        }
        public override bool UnEffectSkills(ISkill srcSkill, ISkillPlayer caster, ISkill dstSkill)
        {
            return true;
        }
        #endregion

        #region Tools
        List<ISkill> InnerSeek(List<ISkill> dstSkills)
        {
            if (null == dstSkills)
                return null;
            if (null == Ids && null == SrcTypes && null == ActTypes)
                return dstSkills;
            var rst = new List<ISkill>();
            bool hitFlag = true;
            foreach (var item in dstSkills)
            {
                hitFlag = true;
                if (null != Ids && Ids.Length > 0)
                {
                    for (int i = 0; i < Ids.Length; i++)
                    {
                        if (Ids[i] == item.RawSkill.SkillCode)
                            break;
                        else if (i == Ids.Length - 1)
                            hitFlag = false;
                    }
                    if (!hitFlag)
                        continue;
                }
                if (null != SrcTypes && SrcTypes.Length > 0)
                {
                    for (int i = 0; i < SrcTypes.Length; i++)
                    {
                        if (SrcTypes[i] == item.RawSkill.SrcType)
                            break;
                        else if (i == SrcTypes.Length - 1)
                            hitFlag = false;
                    }
                    if (!hitFlag)
                        continue;
                }
                if (null != ActTypes && ActTypes.Length > 0)
                {
                    for (int i = 0; i < ActTypes.Length; i++)
                    {
                        if (ActTypes[i] == item.RawSkill.ActType)
                            break;
                        else if (i == ActTypes.Length - 1)
                            hitFlag = false;
                    }
                    if (!hitFlag)
                        continue;
                }
                if (hitFlag)
                    rst.Add(item);
            }
            return rst;
        }
        #endregion

        public INormalEffect Clone()
        {
            var effect = new ClearCDEffect(this.MainFlag, this.PureFlag, this.DebuffFlag);
            effect.CopyValue(this);
            effect.Ids = this.Ids;
            effect.SrcTypes = this.SrcTypes;
            effect.ActTypes = this.ActTypes;
            return effect;
        }
    }
}
