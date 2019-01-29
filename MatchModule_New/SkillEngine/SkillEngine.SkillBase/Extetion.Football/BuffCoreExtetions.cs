using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase.Enum.Football;

namespace SkillEngine.SkillBase.Extetion.Football
{
    public static class BuffCoreExtetions
    {
        public static bool TryGetBuff(this IBuffCore core, EnumBuffCode buffCode, ref IBuff buff)
        {
            return core.TryGetBuff((int)buffCode, ref buff);
        }
        public static bool RemoveBuff(this IBuffCore core, EnumBuffCode buffCode, int skillId)
        {
            return core.RemoveBuff((int)buffCode, skillId);
        }
        public static bool ForceSyncBuff(this IBuffCore core, EnumBuffCode buffCode, bool forceFlag)
        {
            return core.ForceSyncBuff((int)buffCode, forceFlag);
        }
    }
}
