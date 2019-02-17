/********************************************************************************
 * 文件名：InterruptionState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "InterruptionState.h"

#include "../HoldBallState.h"
#include "../OffBallState.h"

InterruptionState::InterruptionState()
{
    m_Stopable = true;
}

InterruptionState* InterruptionState::Instance()
{
    static InterruptionState instance;

    return &instance;
}

void InterruptionState::Initialize()
{
    m_StateChain.push_back(DefenceState::Instance());

    m_StateCondition[DefenceState::Instance()]      = ValidateInterruptionToDefence;
}

string InterruptionState::ToString()
{
    return "InterruptionState";
}

void InterruptionState::Enter(IPlayer* player)
{
}

void InterruptionState::Action(IPlayer* player)
{
    player->InterruptionBall();
}

IState* InterruptionState::QuickDecide(IPlayer* player, IState* preview)
{
    if (player->Status()->Hasball())
    {
        return HoldBallState::Instance();
    }
    else
    {
        return OffBallState::Instance();
    }
}

bool InterruptionState::ValidateInterruptionToDefence(IPlayer* player, IState* preview)
{
    return true;
}

bool InterruptionState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    if (typeid(DefenceState) == typeid(*st))
    {
        return true;
    }

    return false;
}
