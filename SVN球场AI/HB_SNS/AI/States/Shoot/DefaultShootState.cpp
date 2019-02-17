/********************************************************************************
 * 文件名：DefaultShootState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "DefaultShootState.h"

#include "../ShootState.h"
#include "../HoldBallState.h"
#include "../OffBallState.h"

DefaultShootState::DefaultShootState()
{
    m_Stopable = true;
}

DefaultShootState* DefaultShootState::Instance()
{
    static DefaultShootState instance;

    return &instance;
}

void DefaultShootState::Initialize()
{
    m_StateChain.push_back(ShootState::Instance());

    m_StateCondition[ShootState::Instance()]    = ValidateDefaultShootToShoot;
}

/// 普通射门行动
void DefaultShootState::Action(IPlayer* player)
{
    player->Shoot();
}

string DefaultShootState::ToString()
{
    return "DefaultShootState";
}

IState* DefaultShootState::QuickDecide(IPlayer* player, IState* preview)
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

bool DefaultShootState::ValidateDefaultShootToShoot(IPlayer* player, IState* preview)
{
    return true;
}

bool DefaultShootState::IsInState(IState* st)
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

