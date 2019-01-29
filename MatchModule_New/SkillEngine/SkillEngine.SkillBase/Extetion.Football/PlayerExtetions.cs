using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum.Football;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillBase.Extetion.Football
{
    public static class PlayerExtetions
    {
        public static int GetStatInt(this ISkillManager manager, EnumManagerStat statType)
        {
            return manager.GetStatInt((int)statType);
        }
        public static int GetStatInt(this ISkillPlayer player, EnumManagerStat statType)
        {
            return player.GetStatInt((int)statType);
        }
    }
}
