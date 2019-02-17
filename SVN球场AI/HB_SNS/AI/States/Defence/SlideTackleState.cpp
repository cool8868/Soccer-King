/********************************************************************************
 * 文件名：SlideTackleState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "SlideTackleState.h"

#include "../HoldBallState.h"
#include "../OffBallState.h"

SlideTackleState::SlideTackleState()
{
    m_Stopable = true;
}

SlideTackleState* SlideTackleState::Instance()
{
    static SlideTackleState instance;

    return &instance;
}

void SlideTackleState::Initialize()
{
    m_StateChain.push_back(DefenceState::Instance());

    m_StateCondition[DefenceState::Instance()]  = ValidateSlideTackleToDefence;
}

string SlideTackleState::ToString()
{
    return "SlideTackleState";
}

void SlideTackleState::Enter(IPlayer* player)
{
}

void SlideTackleState::Action(IPlayer* player)
{
    player->SlideTackleBall();
}

IState* SlideTackleState::QuickDecide(IPlayer* player, IState* preview)
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

bool SlideTackleState::ValidateSlideTackleToDefence(IPlayer* player, IState* preview)
{
    return true;
}

bool SlideTackleState::IsInState(IState* st)
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
