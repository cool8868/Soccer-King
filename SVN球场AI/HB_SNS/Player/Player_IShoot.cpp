/********************************************************************************
 * 文件名：Player_IShoot.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "Player.h"

#include "../common/Enum/PlayerProperty.h"

/// 射门
void Player::Shoot()
{
    MadmanEffect();

    double fix = 0.0;
    
    if (m_Side == Side_Home)
    {
        fix = (210.0 - Current().X) / 3;
    }
    else
    {
        fix = Current().X / 3;
    }

    InternalShoot(PlayerProperty_Shooting, RandomHelper::GetInt32(30, 45), fix);
}

/// 球员发动一次大力抽射
void Player::VolleyShoot()
{
    MadmanEffect();

    double fix = 0.0;
 
    if (m_Side == Side_Home)
    {
        fix = (210.0 - Current().X) / 3;
    }
    else
    {
        fix = Current().X / 3;
    }

    // rnd(10)+15+x/10
    int speed = static_cast<int>(RandomHelper::GetInt32(0, 10) + 35 + m_CurrProperty[PlayerProperty_Strength] / 10);
    
    InternalShoot(PlayerProperty_Shooting, speed, fix);
}

/// 球员发动一次直接任意球射门
/// 修正任意球属性无效
void Player::FreeKickShoot()
{
    MadmanEffect();

    double tmpFreeKick = m_CurrProperty[PlayerProperty_FreeKick];
    double tmpShooting = m_CurrProperty[PlayerProperty_Shooting];

    m_CurrProperty[PlayerProperty_FreeKick] = m_RawProperty[PlayerProperty_FreeKick];
    m_CurrProperty[PlayerProperty_Shooting] = m_RawProperty[PlayerProperty_Shooting];
    
    double distance = (m_Side == Side_Home) ? 210 - Current().X : Current().X;
    double fk = m_CurrProperty[PlayerProperty_FreeKick];
    double fix = (distance / 30) * 2 * ((fk / 4) * pow(fk / 100, 3) + 25 * pow(fk / 100, 2) + 25 * (fk / 100));

    // rnd(10)+15+x/10
    int speed = static_cast<int>(RandomHelper::GetInt32(0, 10) + 35 + m_CurrProperty[PlayerProperty_Strength] / 10);
    InternalShoot(PlayerProperty_FreeKick, speed, fix);

    m_CurrProperty[PlayerProperty_FreeKick] = tmpFreeKick;
    m_CurrProperty[PlayerProperty_Shooting] = tmpShooting;
}

/// Internal logic of the shoot.
/// 射门的内部逻辑
void Player::InternalShoot(int property, int speed, double fix)
{
    // 球员朝向
    IPlayer* gk = m_Manager->Opponent()->GetPlayersByPosition(Position_Goalkeeper)[0];
    Rotate(gk->Current());

    // 射中门框   
    if (RandomHelper::GetPercentage() < Defines_Shoot.ShootToFramePercentage)
    {
        ShootToDoorFrame();
        LogHelper.Insert("[射门]射中门框", Color_Black);

        m_Manager->Opponent()->GkOpenball();
        
        m_Manager->ClearDisable();
        m_Manager->Opponent()->ClearDisable();
        m_Manager->Opponent()->GkOpenball();

        return;
    }
    else
    {
        int index = PlayerRule::GetShootTargetIndex(m_CurrProperty[property], this, fix);
        m_Status->GetShootStatus()->SetShootTargetIndex(index);
        m_Status->GetShootStatus()->SetShootTarget(m_Match->GetPitch()->GetGoal()->GetShootTargetByIndex(index));
        m_Status->GetShootStatus()->SetShootSpeed(speed);

        format fmt("[射门]球员射门, 目标编号:%d, 目标点:%s, 球速:%d");
        fmt % m_Status->GetShootStatus()->SetShootTargetIndex() % m_Status->GetShootStatus()->GetShootTarget().Output(), % m_Status->GetShootStatus()->ShootSpeed();
        
        LogHelper.Insert(fmt.str(), Color_Black);

        MoveBallToGoalkeeper();

        if (!gk->Status()->Enable() ||                                                      // 守门员离场
            gk->Status()->GetDebuffStatus()->GetDebuffType() == DebuffType_DownDebuff ||    // 守门员倒地
            gk->Status()->GetDebuffStatus()->GetDebuffType() == DebuffType_FreezeDebuff ||  // 守门员被冻结
            gk->Status()->GetDebuffStatus()->GetDebuffType() == DebuffType_PuzzleDebuff ||  // 守门员被困惑
            gk->Status()->GetDebuffStatus()->GetDebuffType() == DebuffType_StunDebuff)      // 守门员被晕眩
        {
            if (index != 0 && index != 4)
            {
                m_Manager->Goal();
                return;
            }
            else
            {
                m_Manager->ClearDisable();
                m_Manager->Opponent()->ClearDisable();
                m_Manager->Opponent()->GkOpenball();
            }
        }
    }
}

/// 射中门框
void Player::ShootToDoorFrame()
{
    m_Status->GetShootStatus()->SetShootTargetIndex(0);
    m_Status->GetShootStatus()->SetShootTarget(m_Match->GetPitch()->GetGoal()->GetRandomDoorFrame());
    MoveBallToGoalkeeper();
}

/// 把球移动至对方的门将
void Player::MoveBallToGoalkeeper()
{
    IPlayer* gk = m_Manager->Opponent()->GetPlayersByPosition(Position_Goalkeeper)[0];
    
    m_Match->GetFootball()->MoveTo(gk->Current());
}

void Player::MadmanEffect()
{
    IManager* opp = m_Manager->Opponent(); // 本方

    // 狂人效果
    foreach (IPlayer* p, opp->Players())
    {
        foreach (int& pro, PlayerPropertyInitializer::PlayerPropertyIds())
        {
            // +++ tony:
            p->CurrProperty()[pro] += p->CurrProperty[pro] * opp->Status()->GetMadmanStatus()->SelfUpgradeRate() / 100;
        }
    }

    foreach (IPlayer* p, opp->Opponent()->Players())
    {
        foreach (int& pro, PlayerPropertyInitializer::PlayerPropertyIds())
        {
            p->CurrProperty()[pro] -= p->CurrProperty()[pro] * opp->Status()->GetMadmanStatus()->OppDecreaseRate() / 100;
        }
    }
}

