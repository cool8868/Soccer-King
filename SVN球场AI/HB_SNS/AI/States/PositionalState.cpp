/********************************************************************************
 * 文件名：PositionalState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "PositionalState.h"
#include "OffBallState.h"
#include "ChaceState.h"
#include "../Decides/Factory/PositionalDecideFactory.h"

PositionalState::PositionalState()
{
    m_Stopable = false;
}

PositionalState* PositionalState::Instance()
{
    static PositionalState instance;

    return &instance;
}

string PositionalState::ToString()
{
    return "PositionalState";
}

void PositionalState::Initialize()
{
    m_StateChain.push_back(OffBallState::Instance());

    m_StateCondition[OffBallState::Instance()]      = ValidatePositionalToOffBall;
}

void PositionalState::Enter(IPlayer* player)
{
    player->SetTarget(PositionalDecideFactory::Create(player->BuildinAttribute()->GetPosition())->DecideTarget(player));
}

void PositionalState::Exit(IPlayer* player)
{
    player->Status()->DecideEnd();
}

IState* PositionalState::QuickDecide(IPlayer* player, IState* preview)
{
    return ChaceState::Instance();
}

bool PositionalState::ValidatePositionalToOffBall(IPlayer* player, IState* preview)
{
    return true;
}

bool PositionalState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    return false;
}
