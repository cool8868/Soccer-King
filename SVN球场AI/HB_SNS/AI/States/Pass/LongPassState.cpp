/********************************************************************************
 * 文件名：LongPassState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "LongPassState.h"

#include "../HoldBallState.h"
#include "../OffBallState.h"

LongPassState::LongPassState()
{
    m_Stopable = true;
}

LongPassState* LongPassState::Instance()
{
    static LongPassState instance;

    return &instance;
}

void LongPassState::Initialize()
{
    m_StateChain.push_back(PassState::Instance());

    m_StateCondition[PassState::Instance()]     = ValidateLongPassToPass;
}

void LongPassState::Action(IPlayer* player)
{
    player->LongPass();
}

string LongPassState::ToString()
{
    return "LongPassState";
}

void LongPassState::Enter(IPlayer* player)
{
}

IState* LongPassState::QuickDecide(IPlayer* player, IState* preview)
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

bool LongPassState::ValidateLongPassToPass(IPlayer* player, IState* preview)
{
    return true;
}

bool LongPassState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    if (typeid(PassState) == typeid(*st))
    {
        return true;
    }

    return false;
}

