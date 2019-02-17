/********************************************************************************
 * 文件名：StateSelecter.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "StateSelecter.h"
#include "States/ActionState.h"
#include "States/ChaceState.h"
#include "States/DefenceState.h"
#include "States/DiveBallState.h"
#include "States/DribbleState.h"
#include "States/HoldBallState.h"
#include "States/IdleState.h"
#include "States/OffBallState.h"
#include "States/PassState.h"
#include "States/PositionalState.h"
#include "States/ShootState.h"
#include "States/StopballState.h"
#include "States/Defence/InterruptionState.h"
#include "States/Defence/SlideTackleState.h"
#include "States/Dribble/DefaultDribbleState.h"
#include "States/Dribble/BreakThroughState.h"
#include "States/Idle/GKHoldBallState.h"
#include "States/Shoot/DefaultShootState.h"
#include "States/Shoot/FreekickShootState.h"
#include "States/Shoot/VolleyShootState.h"
#include "States/Pass/LongPassState.h"
#include "States/Pass/ShortPassState.h"

StateSelecter* StateSelecter::Instance()
{
    static StateSelecter instance;

    return &instance;
}        

void StateSelecter::Initialize()
{
    m_States.clear();
    m_States[ActionState::Instance()->ToString()]           = ActionState::Instance();              // 0
    m_States[ChaceState::Instance()->ToString()]            = ChaceState::Instance();               // 1
    m_States[DefenceState::Instance()->ToString()]          = DefenceState::Instance();             // 2
    m_States[DiveBallState::Instance()->ToString()]         = DiveBallState::Instance();            // 3
    m_States[DribbleState::Instance()->ToString()]          = DribbleState::Instance();             // 4
    m_States[HoldBallState::Instance()->ToString()]         = HoldBallState::Instance();            // 5
    m_States[IdleState::Instance()->ToString()]             = IdleState::Instance();                // 6
    m_States[OffBallState::Instance()->ToString()]          = OffBallState::Instance();             // 7
    m_States[PassState::Instance()->ToString()]             = PassState::Instance();                // 8
    m_States[PositionalState::Instance()->ToString()]       = PositionalState::Instance();          // 9
    m_States[ShootState::Instance()->ToString()]            = ShootState::Instance();               // 10
    m_States[StopballState::Instance()->ToString()]         = StopballState::Instance();            // 11
    m_States[InterruptionState::Instance()->ToString()]     = InterruptionState::Instance();        // 12
    m_States[SlideTackleState::Instance()->ToString()]      = SlideTackleState::Instance();         // 13
    m_States[DefaultDribbleState::Instance()->ToString()]   = DefaultDribbleState::Instance();      // 14
    m_States[LongPassState::Instance()->ToString()]         = LongPassState::Instance();            // 15
    m_States[ShortPassState::Instance()->ToString()]        = ShortPassState::Instance();           // 16
    m_States[DefaultShootState::Instance()->ToString()]     = DefaultShootState::Instance();        // 17
    m_States[VolleyShootState::Instance()->ToString()]      = VolleyShootState::Instance();         // 18
    m_States[BreakThroughState::Instance()->ToString()]     = BreakThroughState::Instance();        // 19
    m_States[FreekickShootState::Instance()->ToString()]    = FreekickShootState::Instance();       // 20
    m_States[GKHoldBallState::Instance()->ToString()]       = GKHoldBallState::Instance();          // 21

    foreach (MapString_IState::value_type& state, m_States) 
    {
        State* pState = PointerCastSafe(State, state.second);
   
        pState->SetClientId(index++);
    }

    LogHelper::Insert("StateSelecter has initialized.", LogType_Info);
}

IState* StateSelecter::GetStateByString(string state) 
{
    if (m_States.find(state) != m_States.end())
    {
        return m_States[state];
    }

    LogHelper::Insert("Can't find the state by the name. State Name:" + state);
    return (IState*)NULL;
}