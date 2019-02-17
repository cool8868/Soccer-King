/********************************************************************************
 * 文件名：IdleState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "IdleState.h"
#include "ChaceState.h"
#include "HoldBallState.h"
#include "OffBallState.h"
#include "ActionState.h"

#include "../Actions/IdleAction.h"


IdleState::IdleState()
{
    m_Stopable = true;
}

IdleState* IdleState::Instance()
{
    static IdleState instance;

    return &instance;
}

string IdleState::ToString()
{
    return "IdleState";
}

void IdleState::Initialize()
{
    m_StateChain.push_back(ChaceState::Instance());
    m_StateChain.push_back(ActionState::Instance());
    m_StateChain.push_back(this);

    m_StateCondition[this]                      = ValidateIdleToIdle;
    m_StateCondition[ChaceState::Instance()]    = ValidateIdleToChace;
    m_StateCondition[ActionState::Instance()]   = ValidateIdleToAction;
}

void IdleState::Action(IPlayer* player)
{
    singleton_default<IdleAction>::instance().Action(player);
}

IState* IdleState::QuickDecide(IPlayer* player, IState* preview)
{
    if (player->Status()->NeedRedecide())
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
    else
    {
        if (player->Current() == player->Destination())
        {
            return IdleState::Instance();
        }
        else
        {
            return ChaceState::Instance();
        }
    }
}

bool IdleState::ValidateIdleToIdle(IPlayer* player, IState* preview)
{
    if (player->Status()->NeedRedecide())
    {
        return false;
    }

    return player->Current() == player->Destination();
}

bool IdleState::ValidateIdleToChace(IPlayer* player, IState* preview)
{
    if (player->Status()->NeedRedecide())
    {
        return false;
    }

    return player->Current() != player->Destination();
}

bool IdleState::ValidateIdleToAction(IPlayer* player, IState* preview)
{
    return player->Status()->NeedRedecide();
}

bool IdleState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    return false;
}
