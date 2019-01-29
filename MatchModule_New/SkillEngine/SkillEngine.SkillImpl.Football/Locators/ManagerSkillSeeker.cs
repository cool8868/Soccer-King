using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;
using SkillEngine.SkillBase.Enum.Football;

namespace SkillEngine.SkillImpl.Football
{
    public class ManagerSkillSeeker : SkillSeekerBase, IManagerSkillSeeker
    {
        #region .ctor
        public ManagerSkillSeeker()
        { }
        public ManagerSkillSeeker(string[] ids, EnumSkillSrcType[] srcTypes, EnumSkillActType[] actTypes)
            : base(ids, srcTypes, actTypes)
        { }
        #endregion


        public List<ISkill> Seek(ISkillManager dstManager)
        {
            return InnerSeek(dstManager.SkillCore.SkillList);
        }
    }
}
