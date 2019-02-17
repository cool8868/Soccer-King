/********************************************************************************
 * 文件名：DefaultDribbleState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "DefaultDribbleState.h"

#include "../OffBallState.h"
#include "../HoldBallState.h"

DefaultDribbleState::DefaultDribbleState()
{
    m_Stopable = true;
}

DefaultDribbleState* DefaultDribbleState::Instance()
{
    static DefaultDribbleState instance;

    return &instance;
}

void DefaultDribbleState::Initialize()
{
    m_StateChain.push_back(this);
    m_StateChain.push_back(DribbleState::Instance());

    m_StateCondition[DribbleState::Instance()]              = ValidateDefaultDribbleToDribble;
    m_StateCondition[this]                                  = ValidateDefaultDribbleToDefaultDribble;
}

string DefaultDribbleState::ToString()
{
    return "DefaultDribbleState";
}

void DefaultDribbleState::Enter(IPlayer* player)
{
}

IState* DefaultDribbleState::QuickDecide(IPlayer* player, IState* preview)
{
    if (player->Status()->Hasball() == false)
    {
        return OffBallState::Instance();
    }
    else
    {
        return HoldBallState::Instance();
    }
}

bool DefaultDribbleState::ValidateDefaultDribbleToDribble(IPlayer* player, IState* preview)
{
    return true;
}

bool DefaultDribbleState::ValidateDefaultDribbleToDefaultDribble(IPlayer* player, IState* preview)
{
    if (!player->Status()->Hasball())
    {
        return false;
    }

    return (player->Status()->NeedRedecide() == false);
}

bool DefaultDribbleState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    if (typeid(DribbleState) == typeid(*st))
    {
        return true;
    }

    return false;
}

