/********************************************************************************
 * 文件名：Player_IDefence.cpp
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

/// 抢断一个球
void Player::InterruptionBall()
{
    if (m_Status->IsAttackSide())
    {
        return;
    }

    InternalDefence();
    Rotate(m_Match->GetFootball()->Current());
    MoveTo(m_Match->GetFootball()->Current());
}

/// 铲球
void Player::SlideTackleBall()
{
    if (m_Status->IsAttackSide())
    {
        return;
    }

    InternalDefence();
    Rotate(m_Match->GetFootball()->Current());
    MoveTo(m_Match->GetFootball()->Current());
}

/// 防守逻辑
void Player::InternalDefence()
{
    // 如果本方是持球方，不允许断球或铲球
    // 决策时不能保证行动时自己是防守方，因为之前可能已经有队友将球断下，然后发生了攻防转换
    if (m_Match->Status()->BallHandler()->GetSide() == m_Side) 
    {
        return;
    }

    double percentage = 0.0;

    double interception = m_CurrProperty[PlayerProperty_Interception];
    double dribble = m_Match->Status()->BallHandler()->CurrProperty()[PlayerProperty_Dribble];

    if (m_Match->Status()->BallHandler()->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None)
    {
        percentage = 100;
    }
    else
    {
        percentage = (dribble <= 0) ? 100 : (pow(interception / dribble, 2) * 0.3) * 100;
    }

    if (RandomHelper::GetPercentage() <= percentage) // success
    {
        AddTargetInertia(m_Match->Status()->BallHandler());
        InterruptBall();
    }
    else
    {
        AddTargetInertia(this);
    }
}

/// 抢断球
void Player::InterruptBall()
{
    if (RandomHelper::GetPercentage() < (m_CurrProperty[PlayerProperty_Strength] - m_Match->Status()->BallHandler()->CurrProperty()[PlayerProperty_Strength]) * 2)
    {
        m_Match->Status()->BallHandler()->AddDownDebuff(m_Id, 4);
    }

    m_Status->SetHasball(true);
    m_Match->GetFootball()->Kick(m_Status->Destination(), 15, this); // 断球后，球滚动
    m_Match->GetFootball()->MoveTo(m_Match->GetFootball()->Current());
}

/// 添加惯性效果至目标
static void Player::AddTargetInertia(IPlayer* target)
{
    double probability = 100 - target->CurrProperty()[PlayerProperty_Balance] * 0.4;
    if (probability < 20.0)
    {
        probability = 20.0;
    }

    if (RandomHelper::GetPercentage() < probability)
    {                
        target->AddInertia();
    }
    else
    {
        target->AddInertia(1);
    }
}

