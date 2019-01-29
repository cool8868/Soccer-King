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
    public class PlayerSkillSeeker : SkillSeekerBase, IPlayerSkillSeeker
    {
        #region .ctor
        public PlayerSkillSeeker()
        { }
        public PlayerSkillSeeker(string[] ids, EnumSkillSrcType[] srcTypes, EnumSkillActType[] actTypes)
            : base(ids, srcTypes, actTypes)
        { }
        #endregion

        public List<ISkill> Seek(List<ISkillPlayer> dstPlayers)
        {
            if (null == dstPlayers || dstPlayers.Count == 0)
                return null;
            var dstSkills = new List<ISkill>();
            foreach (var dstPlayer in dstPlayers)
            {
                if (null != dstPlayer.SkillCore.SkillList)
                    dstSkills.AddRange(dstPlayer.SkillCore.SkillList);
            }
            var rst = InnerSeek(dstSkills);
            dstSkills.Clear();
            return rst;
        }
    }
}
