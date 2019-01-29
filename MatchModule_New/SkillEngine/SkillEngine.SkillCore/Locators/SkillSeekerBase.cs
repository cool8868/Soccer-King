using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillBase.Enum.Football;

namespace SkillEngine.SkillCore
{
    public class SkillSeekerBase
    {
        #region .ctor
        protected SkillSeekerBase()
        { }
        protected SkillSeekerBase(string[] ids, EnumSkillSrcType[] srcTypes, EnumSkillActType[] actTypes)
        {
            this.Ids = ids;
            this.SrcTypes = srcTypes;
            this.ActTypes = actTypes;
        }
        #endregion

        #region Data
        public string[] Ids
        {
            get;
            set;
        }
        public EnumSkillFlag SkillFlag
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

        protected List<ISkill> InnerSeek(List<ISkill> dstSkills)
        {
            if (null == dstSkills)
                return null;
            var rst = new List<ISkill>();
            bool hitFlag = true;
            foreach (var item in dstSkills)
            {
                hitFlag = true;
                if (SkillFlag != EnumSkillFlag.None)
                {
                    if ((SkillFlag & item.SkillFlag) == 0)
                        hitFlag = false;
                    if (!hitFlag)
                        continue;
                }
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


    }
}
