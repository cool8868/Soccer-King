/********************************************************************************
 * 文件名：DefenceState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "DefenceState.h"
#include "OffBallState.h"
#include "HoldBallState.h"

#include "Defence/SlideTackleState.h"
#include "Defence/InterruptionState.h"

DefenceState::DefenceState()
{
    m_Stopable = false;
}

DefenceState* DefenceState::Instance()
{
    static DefenceState instance;

    return &instance;
}

void DefenceState::Initialize()
{
    m_StateChain.push_back(SlideTackleState::Instance());
    m_StateChain.push_back(InterruptionState::Instance());
    m_StateChain.push_back(OffBallState::Instance());

    m_StateCondition[InterruptionState::Instance()]     = ValidateDefenceToInterruption;
    m_StateCondition[OffBallState::Instance()]          = ValidateDefenceToOffBall;
    m_StateCondition[SlideTackleState::Instance()]      = ValidateDefenceToSlideTackle;
}

void DefenceState::Enter(IPlayer* player)
{
    player->Status()->DecideEnd();
}

string DefenceState::ToString()
{
    return "DefenceState";
}

IState* DefenceState::QuickDecide(IPlayer* player, IState* preview)
{
    if (player->Status()->Hasball())
    {
        return HoldBallState::Instance();
    }
    else
    {
        if (preview->IsInState(DefenceState::Instance()))
        {
            return OffBallState::Instance();
        }
        else
        {
            if (RandomHelper::GetPercentage() < 50)
            {
                return SlideTackleState::Instance();
            }
            else
            {
                return InterruptionState::Instance();
            }
        }
    }
}

bool DefenceState::ValidateDefenceToInterruption(IPlayer* player, IState* preview)
{
    if (player->Status()->Hasball())
    {
        return false;
    }

    if (preview->IsInState(SlideTackleState::Instance()) || preview->IsInState(InterruptionState::Instance()))
    {
        return false;
    }

    return true;
}

bool DefenceState::ValidateDefenceToOffBall(IPlayer* player, IState* preview)
{
    return true;
}

bool DefenceState::ValidateDefenceToSlideTackle(IPlayer* player, IState* preview)
{
    if (player->Status()->Hasball())
    {
        return false;
    }

    if (preview->IsInState(SlideTackleState::Instance()) || preview->IsInState(InterruptionState::Instance()))
    {
        return false;
    }

    return RandomHelper::GetPercentage() < 50;
}

bool DefenceState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    return false;
}
