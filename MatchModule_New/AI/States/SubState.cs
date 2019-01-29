using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.States
{
    public class SubStateObj : ISubState
    {
        #region Cache
        EnumSubState _subState = EnumSubState.None;
        EnumAIState _hostState = EnumAIState.None;
        int _roundEnd = 0;
        #endregion

        #region ISubState
        public void SetSubState(EnumSubState subState, int roundEnd, EnumAIState hostState = EnumAIState.None)
        {
            this._subState = subState;
            this._roundEnd = roundEnd;
            this._hostState = hostState;
        }
        public EnumSubState GetSubState(int round)
        {
            if (this._roundEnd <= 0 || round <= 0 || round <= this._roundEnd)
                return this._subState;
            return EnumSubState.None;
        }
        public EnumAIState GetHostState()
        {
            return _hostState;
        }
        #endregion

    }
}
