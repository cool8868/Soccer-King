using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public class SkillLocator : ILocator<ISkill>
    {
        public PlayerLocator PlayerLocator
        {
            get;
            set;
        }
        public IManagerSkillSeeker ManagerSeeker
        {
            get;
            set;
        }
        public IPlayerSkillSeeker PlayerSeeker
        {
            get;
            set;
        }
        public List<ISkill> Locate(ISkill srcSkill, ISkillPlayer caster)
        {
            var targets = new List<ISkill>();
            if (!this.Locate(targets, srcSkill, caster))
                return null;
            return targets;
        }
        public bool Locate(List<ISkill> targets, ISkill srcSkill, ISkillPlayer caster)
        {
            targets.Clear();
            var srcManager = null != caster ? caster.SkillManager : srcSkill.Owner as ISkillManager;
            List<ISkill> skills = null;
            if (null != this.ManagerSeeker)
            {
                skills = this.ManagerSeeker.Seek(srcManager);
                if (null != skills && skills.Count > 0)
                    targets.AddRange(skills);
            }
            List<ISkillPlayer> dstPlayers = null;
            if (null != this.PlayerLocator)
                dstPlayers = this.PlayerLocator.Locate(srcSkill, caster);
            if (null == dstPlayers && null == caster)
                return true;
            if (null == dstPlayers)
            {
                dstPlayers = new List<ISkillPlayer>(1);
                dstPlayers.Add(caster);
            }
            if (null == this.PlayerSeeker)
            {
                foreach (var dstPlayer in dstPlayers)
                {
                    if (null != dstPlayer.SkillCore.SkillList)
                        targets.AddRange(dstPlayer.SkillCore.SkillList);
                }
            }
            else
            {
                skills = this.PlayerSeeker.Seek(dstPlayers);
                if (null != skills && skills.Count > 0)
                    targets.AddRange(skills);
            }
            return true;
        }

    }
}
