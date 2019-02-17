/********************************************************************************
 * 文件名：GKHoldBallState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "GKHoldBallState.h"

#include "../ActionState.h"

GKHoldBallState::GKHoldBallState()
{
    m_TimeLast = 2;
}

GKHoldBallState* GKHoldBallState::Instance()
{
    static GKHoldBallState instance;

    return &instance;
}

void GKHoldBallState::Initialize()
{
    m_StateChain.push_back(ActionState::Instance());

    m_StateCondition[ActionState::Instance()]   = ValidateGKHoldToAction;
}

string GKHoldBallState::ToString()
{
    return "GKHoldBallState";
}

IState* GKHoldBallState::QuickDecide(IPlayer* player, IState* preview)
{
    return ActionState::Instance();
}

bool GKHoldBallState::ValidateGKHoldToAction(IPlayer* player, IState* preview)
{
    return true;
}

bool GKHoldBallState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    if (typeid(IdleState) == typeid(*st))
    {
        return true;
    }

    return false;
}

