using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase.Enum.Football;

namespace SkillEngine.SkillBase.Extetion.Football
{
    public static class SpecBuffCoreExtetions
    {
        public static bool TryPickSpecBuff(this ISpecBuffCore core, EnumSpecTiming inTiming, out ISpecEffect outSpec)
        {
            return core.TryPickSpecBuff((int)inTiming, out outSpec);
        }
    }
}
