using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern
{
    public interface ISkillFoul
    {
        byte SkillFoulState
        {
            get;
        }
        void SkillFoul(int foulType);
    }
}
