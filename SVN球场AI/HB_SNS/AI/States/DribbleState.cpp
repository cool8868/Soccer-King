/********************************************************************************
 * 文件名：DribbleState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "DribbleState.h"
#include "HoldBallState.h"
#include "OffBallState.h"
#include "Dribble/DefaultDribbleState.h"
#include "../Decides/HoldBallPositionalDecide.h"

DribbleState::DribbleState()
{
    m_Stopable = false;
}

DribbleState* DribbleState::Instance()
{
    static DribbleState instance;

    return &instance;
}

string DribbleState::ToString()
{
    return "DribbleState";
}

void DribbleState::Initialize()
{
    m_StateChain.push_back(HoldBallState::Instance());
    m_StateChain.push_back(DefaultDribbleState::Instance());

    m_StateCondition[DefaultDribbleState::Instance()]       = ValidateDribbleToDefaultDribble;
    m_StateCondition[HoldBallState::Instance()]             = ValidateDribbleToHoldBall;
}

void DribbleState::Action(IPlayer* player)
{
    player->DribbleBall();
}

void DribbleState::Enter(IPlayer* player)
{
    player->Status()->DecideEnd();

    player->SetTarget(HoldBallPositionalDecide::GetTarget(player));
}

IState* DribbleState::QuickDecide(IPlayer* player, IState* preview)
{
    if (player->Status()->Hasball() == false)
    {
        return OffBallState::Instance();
    }
    else
    {
        if (player->Status()->Holdball() == false)
        {
            return HoldBallState::Instance();
        }
        else
        {
            return DefaultDribbleState::Instance();
        }
    }
}
bool DribbleState::ValidateDribbleToDefaultDribble(IPlayer* player, IState* preview)
{
    if (player->Status()->BallDistance() > 0)
    {
        return false;
    }

    if (preview == DefaultDribbleState::Instance())
    {
        return false;
    }

    return true;
}

bool DribbleState::ValidateDribbleToHoldBall(IPlayer* player, IState* preview)
{
    if (!player->Status()->Hasball()) return true;

    if (preview == DefaultDribbleState::Instance())
    {
        player->Redecide();

        return true;
    }

    return false;
}

bool DribbleState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    return false;
}
