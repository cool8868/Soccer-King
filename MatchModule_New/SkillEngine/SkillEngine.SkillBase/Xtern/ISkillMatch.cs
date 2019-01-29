using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Xtern
{
    public interface ISkillMatch : ISkillContext
    {
        ISkillManager SkillHomeManager
        {
            get;
        }
        ISkillManager SkillAwayManager
        {
            get;
        }
        ISkillPlayer SkillBallHandler
        {
            get;
        }
    }
}
