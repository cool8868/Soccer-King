/********************************************************************************
 * 文件名：VolleyShootState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "VolleyShootState.h"

#include "../HoldBallState.h"
#include "../OffBallState.h"

VolleyShootState::VolleyShootState()
{
    m_Stopable = true;
}

VolleyShootState* VolleyShootState::Instance()
{
    static VolleyShootState instance;

    return &instance;
}

void VolleyShootState::Initialize()
{
    m_StateChain.push_back(ShootState::Instance());

    m_StateCondition[ShootState::Instance()]    = ValidateVolleyShootToShoot;
}

void VolleyShootState::Action(IPlayer* player)
{
    player->VolleyShoot();
}

string VolleyShootState::ToString()
{
    return "VolleyShootState";
}

IState* VolleyShootState::QuickDecide(IPlayer* player, IState* preview)
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

bool VolleyShootState::ValidateVolleyShootToShoot(IPlayer* player, IState* preview)
{
    return true;
}

bool VolleyShootState::IsInState(IState* st)
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
