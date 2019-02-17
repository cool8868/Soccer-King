/********************************************************************************
 * 文件名：BreakThroughState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "BreakThroughState.h"

#include "../ActionState.h"
#include "../HoldBallState.h"
#include "../OffBallState.h"

BreakThroughState::BreakThroughState()
{
    m_Stopable = true;
}

BreakThroughState* BreakThroughState::Instance()
{
    static BreakThroughState instance;

    return &instance;
}

void BreakThroughState::Initialize()
{
    m_StateChain.push_back(ActionState::Instance());

    m_StateCondition[ActionState::Instance()]   = ValidateBreakThroughToAction;
}

void BreakThroughState::Enter(IPlayer* player)
{
}

string BreakThroughState::ToString()
{
    return "BreakThroughState";
}

IState* BreakThroughState::QuickDecide(IPlayer* player, IState* preview)
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

bool BreakThroughState::ValidateBreakThroughToAction(IPlayer* player, IState* preview)
{
    return true;
}

bool BreakThroughState::IsInState(IState* st)
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

