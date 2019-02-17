/********************************************************************************
 * 文件名：ShortPassState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ShortPassState.h"

#include "../HoldBallState.h"
#include "../OffBallState.h"

ShortPassState::ShortPassState()
{
    m_Stopable = true;
}

ShortPassState* ShortPassState::Instance()
{
    static ShortPassState instance;

    return &instance;
}

void ShortPassState::Initialize()
{
    m_StateChain.push_back(PassState::Instance());

    m_StateCondition[PassState::Instance()]     = ValidateShortPassToPass;
}

void ShortPassState::Action(IPlayer* player)
{
    player->ShortPass();
}

string ShortPassState::ToString()
{
    return "ShortPassState";
}

void ShortPassState::Enter(IPlayer* player)
{
}

IState* ShortPassState::QuickDecide(IPlayer* player, IState* preview)
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

bool ShortPassState::ValidateShortPassToPass(IPlayer* player, IState* preview)
{
    return true;
}

bool ShortPassState::IsInState(IState* st)
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

