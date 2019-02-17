/********************************************************************************
 * 文件名：FreekickShootState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "FreekickShootState.h"

#include "../HoldBallState.h"
#include "../OffBallState.h"

FreekickShootState::FreekickShootState()
{
    m_Stopable = true;
}

FreekickShootState* FreekickShootState::Instance()
{
    static FreekickShootState instance;

    return &instance;
}

void FreekickShootState::Initialize()
{
    m_StateChain.push_back(ShootState::Instance());
    
    m_StateCondition[ShootState::Instance()]    = ValidateFreekickShootToShoot;
}

/// 触发一次任意球直接射门
void FreekickShootState::Action(IPlayer* player)
{
    player->FreeKickShoot();
}

string FreekickShootState::ToString()
{
    return "FreekickShootState";
}

IState* FreekickShootState::QuickDecide(IPlayer* player, IState* preview)
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

bool FreekickShootState::ValidateFreekickShootToShoot(IPlayer* player, IState* preview)
{
    return true;
}

bool FreekickShootState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    if (typeid(ShootState) == typeid(*st))
    {
        return true;
    }

    return false;
}
