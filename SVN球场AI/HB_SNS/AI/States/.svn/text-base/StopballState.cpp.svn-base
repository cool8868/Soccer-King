/********************************************************************************
 * 文件名：StopballState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "StopballState.h"
#include "HoldBallState.h"
#include "OffBallState.h"

#include "../Actions/StopballAction.h"

StopballState::StopballState()
{
    m_Stopable = true;
}

StopballState* StopballState::Instance()
{
    static StopballState instance;

    return &instance;
}

void StopballState::Initialize()
{
    m_StateChain.push_back(HoldBallState::Instance());

    m_StateCondition[HoldBallState::Instance()]     = ValidateStopballToHoldBall;
}

string StopballState::ToString()
{
    return "StopballState";
}

/// Stopball Action
void StopballState::Action(IPlayer* player)
{
    singleton_default<StopballAction>::instance().Action(player);
}

IState* StopballState::QuickDecide(IPlayer* player, IState* preview)
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

bool StopballState::ValidateStopballToHoldBall(IPlayer* player, IState* preview)
{
    return true;
}

bool StopballState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    return false;
}

