/********************************************************************************
 * 文件名：ShootState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ShootState.h"
#include "HoldBallState.h"
#include "OffBallState.h"
#include "Shoot/DefaultShootState.h"
#include "Shoot/VolleyShootState.h"

#include "../../common/Enum/PlayerProperty.h"

#include "../../MatchProcess/ShootProcess.h"

ShootState::ShootState()
{
    m_Stopable = false;
}

ShootState* ShootState::Instance()
{
    static ShootState instance;

    return &instance;
}

void ShootState::Initialize()
{
    m_StateChain.push_back(VolleyShootState::Instance());
    m_StateChain.push_back(DefaultShootState::Instance());
    m_StateChain.push_back(HoldBallState::Instance());

    m_StateCondition[VolleyShootState::Instance()]          = ValidateShootToVolleyShoot;
    m_StateCondition[DefaultShootState::Instance()]         = ValidateShootToDefaultShoot;
    m_StateCondition[HoldBallState::Instance()]             = ValidateShootToHoldBall;
}

void ShootState::Enter(IPlayer* player)
{
    player->Status()->DecideEnd();
}

Process* ShootState::Save(IPlayer* player)
{
    ShootProcess* process = new ShootProcess();

    //调用基类的函数保存，注意隐藏规则
    State::Save(process, player);

    //写入ShootProcess独有的数据
    process->SetShootSpeed(player->Status()->GetShootStatus()->ShootSpeed());
    process->SetShootTarget(player->Status()->GetShootStatus()->GetShootTarget().Output());
    process->SetShootTargetIndex(player->Status()->GetShootStatus()->ShootTargetIndex());

    return process;
}

string ShootState::ToString()
{
    return "ShootState";
}

IState* ShootState::QuickDecide(IPlayer* player, IState* preview)
{
    if (player->Status()->Hasball() == false)
    {
        return OffBallState::Instance();
    }

    if (player->Status()->BallDistance() > 0)
    {
        return HoldBallState::Instance();
    }

    if (player->CurrProperty()[PlayerProperty_Strength] > 100)
    {
        if (RandomHelper::GetPercentage() < 0.2 * player->CurrProperty()[PlayerProperty_Strength])
        {
            return VolleyShootState::Instance();
        }
    }

    return DefaultShootState::Instance();
}

bool ShootState::ValidateShootToVolleyShoot(IPlayer* player, IState* state)
{
    if (player->CurrProperty()[PlayerProperty_Strength] < 100)
    {
        return false;
    }

    return RandomHelper::GetPercentage() < 0.2 * player->CurrProperty()[PlayerProperty_Strength];
}

bool ShootState::ValidateShootToDefaultShoot(IPlayer* player, IState* state)
{
    if (!player->Status()->Hasball())
    {
        return false;
    }

    if (player->Status()->BallDistance() > 0)
    {
        return false;
    }

    return true;
}

bool ShootState::ValidateShootToHoldBall(IPlayer* player, IState* state)
{
    if (!player->Status()->Hasball())
    {
        return true;
    }

    if (player->Status()->BallDistance() > 0)
    {
        return true;
    }

    return !player->Status()->NeedRedecide();
}

bool ShootState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    return false;
}
