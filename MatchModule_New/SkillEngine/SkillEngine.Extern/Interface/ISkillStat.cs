using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern
{
    public interface ISkillStat
    {
        object GetStatObj(int statType);
        int GetStatInt(int statType);
    }
}
