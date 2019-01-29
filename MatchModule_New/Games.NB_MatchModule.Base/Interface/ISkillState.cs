using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Base.Interface
{
    public interface ISkillState
    {
        bool ForceState(int forceState);
        bool SkillDecide();
        bool SkillAction();
    }
}
