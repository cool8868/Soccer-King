/********************************************************************************
 * 文件名：ActionState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ActionState.h"
#include "IdleState.h"
#include "ChaceState.h"
#include "HoldBallState.h"
#include "OffBallState.h"

#include "../../common/Enum/Position.h"

ActionState::ActionState()
{
    m_Stopable  = false;
}

ActionState::~ActionState()
{

}

ActionState* ActionState::Instance()
{
    static ActionState instance;

    return &instance;
}

void ActionState::Initialize()
{
    m_StateChain.push_back(IdleState::Instance());
    m_StateChain.push_back(ChaceState::Instance());
    m_StateChain.push_back(HoldBallState::Instance());
    m_StateChain.push_back(OffBallState::Instance());

    m_StateCondition[IdleState::Instance()]     = ValidateActionToIdle;
    m_StateCondition[ChaceState::Instance()]    = ValidateActionToChace;
    m_StateCondition[HoldBallState::Instance()] = ValidateActionToHoldBall;
    m_StateCondition[OffBallState::Instance()]  = ValidateActionToOffBall;
}

void ActionState::Enter(IPlayer* player)
{
    if (player->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
    {
        double distance = player->Current().Distance(player->GetMatch()->GetFootball()->Current());

        if (distance <= 2)
        {
            player->Status()->SetHasball(true);
        }
    }
}


string ActionState::ToString()
{
    return "ActionState";
}

IState* ActionState::QuickDecide(IPlayer* player, IState* preview)
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
        if (player->Current() != player->Destination())
        {
            return ChaceState::Instance();
        }
        else
        {
            return IdleState::Instance();
        }
    }
}

bool ActionState::ValidateActionToIdle(IPlayer* player, IState* preview)
{
    if (player->Status()->NeedRedecide())
    {
        return false;
    }

    return player->Current() == player->Destination();
}

bool ActionState::ValidateActionToChace(IPlayer* player, IState* preview)
{
    if (player->Status()->NeedRedecide())
    {
        return false;
    }

    return player->Current() != player->Destination();
}

bool ActionState::ValidateActionToHoldBall(IPlayer* player, IState* preview)
{
    if (!player->Status()->NeedRedecide())
    {
        return false;
    }

    return player->Status()->Hasball();
}

bool ActionState::ValidateActionToOffBall(IPlayer* player, IState* preview)
{
    if (!player->Status()->NeedRedecide())
    {
        return false;
    }

    return !player->Status()->Hasball();
}

bool ActionState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    return false;
}