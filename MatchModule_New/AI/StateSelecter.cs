/********************************************************************************
 * 文件名：StateSelecter
 * 创建人：
 * 创建时间：2010-1-18 21:33:50
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System.Collections.Generic;
using Games.NB.Match.AI.States;
using Games.NB.Match.AI.States.Defence;
using Games.NB.Match.AI.States.Dribble;
using Games.NB.Match.AI.States.Idle;
using Games.NB.Match.AI.States.Pass;
using Games.NB.Match.AI.States.Shoot;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Log;

namespace Games.NB.Match.AI {

    /// <summary>
    /// Represents a selecter that helpers the client to get a state by state name.
    /// </summary>
    [Singleton]
    public sealed class StateSelecter {

        /// <summary>
        /// Get the instance of the <see cref="StateSelecter"/>.
        /// </summary>
        public static StateSelecter Instance {
            get { return _instance; }
        }        

        /// <summary>
        /// Initializes the <see cref="StateSelecter"/>.
        /// </summary>
        public void Initialize() {
            _states.Clear();
            _states.Add(ActionState.Instance.ToString(), ActionState.Instance);     // 0
            _states.Add(ChaceState.Instance.ToString(), ChaceState.Instance);       // 1
            _states.Add(DefenceState.Instance.ToString(), DefenceState.Instance);   // 2
            _states.Add(DiveBallState.Instance.ToString(), DiveBallState.Instance); // 3
            _states.Add(DribbleState.Instance.ToString(), DribbleState.Instance);   // 4
            _states.Add(HoldBallState.Instance.ToString(), HoldBallState.Instance); // 5
            _states.Add(IdleState.Instance.ToString(), IdleState.Instance);         // 6
            _states.Add(OffBallState.Instance.ToString(), OffBallState.Instance);   // 7
            _states.Add(PassState.Instance.ToString(), PassState.Instance);         // 8
            _states.Add(PositionalState.Instance.ToString(), PositionalState.Instance); // 9
            _states.Add(ShootState.Instance.ToString(), ShootState.Instance);       // 10
            _states.Add(StopballState.Instance.ToString(), StopballState.Instance); // 11
            _states.Add(InterruptionState.Instance.ToString(), InterruptionState.Instance); // 12
            _states.Add(SlideTackleState.Instance.ToString(), SlideTackleState.Instance);   // 13
            _states.Add(DefaultDribbleState.Instance.ToString(), DefaultDribbleState.Instance); // 14
            _states.Add(LongPassState.Instance.ToString(), LongPassState.Instance);         // 15
            _states.Add(ShortPassState.Instance.ToString(), ShortPassState.Instance);       // 16
            _states.Add(DefaultShootState.Instance.ToString(), DefaultShootState.Instance); // 17
            _states.Add(VolleyShootState.Instance.ToString(), VolleyShootState.Instance);   // 18
            _states.Add(BreakThroughState.Instance.ToString(), BreakThroughState.Instance); // 19
            _states.Add(FreekickShootState.Instance.ToString(), FreekickShootState.Instance); // 20
            _states.Add(GKHoldBallState.Instance.ToString(), GKHoldBallState.Instance);     // 21
            _states.Add(WalkState.Instance.ToString(), WalkState.Instance); //22
            _states.Add(RebelShootState.Instance.ToString(), RebelShootState.Instance); //22
            _states.Add(HeadingDuelState.Instance.ToString(), HeadingDuelState.Instance); //23
            byte index = 0;
            foreach (var state in _states.Values)
            {
                state.ClientId = index++;
            }

            LogHelper.Insert("StateSelecter has initialized.", LogType.Info);
        }

        /// <summary>
        /// Represents hash table of the <see cref="State"/> that with the state name for key and the <see cref="State"/> for value.
        /// </summary>
        public Dictionary<string, IState> States {
            get { return _states; }
        }

        /// <summary>
        /// Get a <see cref="IState"/> by string.
        /// </summary>
        /// <param name="state">State string.</param>
        /// <returns><see cref="IState"/>.</returns>
        public IState GetStateByString(string state) {
            try {
                return this._states[state];
            }
            catch(KeyNotFoundException) {
                LogHelper.Insert("Can't find the state by the name. State Name:" + state);
                return null;
            }
        }

        #region encapsulation

        private readonly static StateSelecter _instance = new StateSelecter();
        private readonly Dictionary<string, IState> _states = new Dictionary<string, IState>(20);

        private StateSelecter() {            
        }

        #endregion
    }
}
