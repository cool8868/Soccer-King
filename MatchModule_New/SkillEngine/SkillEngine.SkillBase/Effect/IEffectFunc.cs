using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillBase
{
    public interface IEffectManager
    {
        bool EffectManager(ISkill srcSkill, ISkillPlayer caster, IList<ISkillManager> dstManagers);
    }
    public interface IEffectPlayers
    {
        bool EffectPlayers(ISkill srcSkill, ISkillPlayer caster, IList<ISkillPlayer> dstPlayers);
    }
    public interface IEffectSkills
    {
        bool EffectSkills(ISkill srcSkill, ISkillPlayer caster, IList<ISkill> dstSkills);
        bool UnEffectSkills(ISkill srcSkill, ISkillPlayer caster, ISkill dstSkill);
    }
    public interface IPlayerAction
    {
        bool PlayerAction(ISkill srcSkill, ISkillPlayer caster, ISkillPlayer dstPlayer);
    }
}
