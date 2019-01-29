using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Interface;


namespace Games.NB.Match.Base.Interface
{
    public interface ISubState
    {
        void SetSubState(EnumSubState subState, int roundEnd, EnumAIState hostState = EnumAIState.None);
        EnumSubState GetSubState(int round);
        EnumAIState GetHostState();
    }
}
