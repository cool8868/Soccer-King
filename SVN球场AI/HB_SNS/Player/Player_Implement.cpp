/********************************************************************************
 * �ļ�����Player_Implement.cpp
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "Player.h"

#include "../AI/Rules/PlayerRule.h"
#include "../AI/States/DefenceState.h"

#include "../common/Enum/PlayerProperty.h"

/// ��Ա��ʼ����������
void Player::Init()
{
    InitStamina();

    PlayerRule::InitPassTarget(this);
    PlayerRule::InitCarePlayers(this);
}

/// ���µ�һ�غϵ���ʱִ�г�ʼ��
void Player::RoundInit()
{
    if (!m_Status->Enable())
    {
        return;
    }

    m_Status->GetPassStatus()->SetPassTarget(NULL);

    m_Status->RefreshDistance();

    /// ˢ����Ұ
    RefreshSight();

    /// ˢ���Ƿ���Ҫ����˼��
    RefreshEnableDecide();
}

/// ���µ�һ���ӵ���ʱִ�г�ʼ��
void Player::MinuteInit()
{
    RefreshStamina();
}

/// �°볡��ʼʱ����
void Player::SecondHalfStart()
{
    InitStamina();

    for (int i = PlayerProperty_Min; i < PlayerProperty_Max; ++i)
    {
        if (i == PlayerProperty_Stamina)
        {
            continue;
        }

        m_CurrProperty[i] = Defines_Match.STAMINA_SECOND_HALF_START / 100 * m_RawProperty[i];
    }

    m_Status->GetShootStatus()->SetShootRegion(PlayerRule::GetPlayerShootRegion(m_CurrProperty[PlayerProperty_Strength], m_Side));
}

/// ��ȡĿ�����ڵ�����
Zone Player::GetTargetZone(Coordinate target)
{
    if (target.X > (double)Defines_Pitch.MAX_WIDTH / 2)
    {
        return (m_Manager->GetSide() == Side_Home) ? Zone_OpposingHalf : Zone_OwnHalf;
    }
    else
    {
        return (m_Manager->GetSide() == Side_Home) ? Zone_OwnHalf : Zone_OpposingHalf;
    }
}

/// ������Ա��Ŀ��
void Player::SetTarget(Coordinate target)
{
    SetTarget(target.X, target.Y);
}

/// ������Ա��Ŀ��
void Player::SetTarget(double x, double y)
{
    //LogHelper.Insert("[SetTarget]SetTarget:" + x + ',' + y, Color.Green);

    m_Status->SetDestination(Coordinate(x, y));

    DecideEnd();
}

/// ����Ա��Ŀ������Ϊ��
void Player::SetTargetBall(bool isCurrent)
{
    Coordinate football = (isCurrent) ? m_Match->GetFootball()->Current() : m_Match->GetFootball()->Destination();

    if (isCurrent)
    {
        //LogHelper.Insert("[BallTarget]SetTarget to Ball's current, Ball position:" + football, Color.Green);
    }
    else
    {
        //LogHelper.Insert("[BallTarget]SetTarget to Ball's destination, Ball position:" + football, Color.Green);
    }

    SetTarget(football.X, football.Y);            
}

/// ����һ���������Ա��
/// �����ԱΪ���ӣ���ô���䣻
/// �����ԱΪ�Ͷӣ����Զ��������ĶԳ��ٸ�ֵ.
void Player::SetHomeSideCoordinate(Coordinate target)
{
    if (m_Manager->GetSide() == Side_Away)
    {
        target = target.Mirror();
    }

    m_Status->SetCurrent(target);
}

/// �ж�һ�������Ƿ�����Ա֮��
bool Player::IsCoordinateInPlayer(Coordinate coordinate)
{
    return (m_Status->Current().Distance(coordinate) <= m_Attribute->Width());
}

/// ��ʼ������
/// ���°볡��ʼ��ʱ����Ҫ��ʼ������
void Player::InitStamina()
{
    double start = 0.0f;
    double end = 0.0f;

    if (m_Match->Status()->IsFirstHalf())
    {
        start = Defines_Match.STAMINA_FIRST_HALF_START;
        end = 80 + m_CurrProperty[PlayerProperty_Stamina] * 0.1 / 100;
    }
    else
    {
        start = Defines_Match.STAMINA_SECOND_HALF_START;
        end = 70 + m_CurrProperty[PlayerProperty_Stamina] * 0.125 / 100;
    }

    if (end > start)
    {
        end = start;
    }

    if (m_Match->GetTotalRound() / 2 == 0)
    {
        m_Status->SetPerRoundDecrease(0);
    }
    else
    {
        m_Status->SetPerRoundDecrease((double)(start - end) / 4500);
    }
}

/// ÿ�غ�ˢ������
void Player::RefreshStamina()
{
    foreach (int& key, PlayerPropertyInitializer::PlayerPropertyIds())
    {
        if (key == PlayerProperty_Stamina)
        {
            continue;
        }

        double decrease = m_Status->PerRoundDecrease() + m_Status->PerRoundDecrease() * m_Manager->Status()->GetDecreaseStaminaRateStatus()->Rate() / 100;
        if (decrease < 0.0)
        {
            decrease = 0.0;
        }

        m_CurrProperty[key] -= m_RawProperty[key] * decrease;
    }
}

// ˢ���Ƿ���Ҫ����˼��
void Player::RefreshEnableDecide()
{
    if (m_Attribute->GetPosition() == Position_Goalkeeper) // GK decides each round for smooth moving.
    { 
        m_Status->Redecide();
    }

    if (m_Status->DestinationDistance() < 1) // each player has move to his destination will redecide.
    {  
        m_Status->Redecide();
        return;
    }

    if (m_Status->Hasball() && m_Status->State()->IsInState(DefenceState::Instance()))
    {
        m_Status->Redecide();
        return;
    }

    if (m_Status->Hasball() && !m_Status->Holdball())
    {
        m_Status->Redecide();
        return;
    }

    if (m_Status->IsAttackSide() == false) // defence side player will redecide in each round.
    { 
        m_Status->Redecide();
        return;
    }

    if (m_Status->Hasball())
    {
        Region region = (m_Side == Side_Home) ? m_Match->GetPitch()->AwayShootRegion() : m_Match->GetPitch()->HomeShootRegion();

        if (region.IsCoordinateInRegion(m_Status->Current()))
        {
            m_Status->Redecide();
        }
    }
}
