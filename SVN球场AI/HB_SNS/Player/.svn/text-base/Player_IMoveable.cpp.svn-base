/********************************************************************************
 * 文件名：Player_IMoveable.cpp
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

#include "../AI/States/IdleState.h"

Coordinate Player::Current()
{
    return m_Status->Current();
}

Coordinate Player::Destination()
{
    return m_Status->Destination();
}

vector<Process*>& Player::Processes()
{
    return m_Processes;
}

double Player::Speed()
{
    return m_Status->Speed();
}

double Player::Acceleration()
{
    return m_Status->Acceleration();
}

void Player::Save()
{
    if (m_Processes.size() == 0)
    {
        InternalSave(m_Match->Status()->Round());
    }
    else
    {
        if (m_Processes[m_Processes.size() - 1]->Round() != m_Match->Status()->Round())
        {
            InternalSave(m_Match->Status()->Round());
        }
    }
}

/// Saves the current process with the round.
void Player::Save(int round)
{
    if (m_Processes.size() == 0)
    {
        InternalSave(round);
    }
    else
    {
        if (m_Processes[m_Processes.size() - 1]->Round() != m_Match->Status()->Round())
        {
            InternalSave(round);
        }
    }
}

/// 球员移动
void Player::Move()
{
    // 判定当前球员是否移动出了可移动区域
    ValidateOutofRegion();

    // 球员旋转
    int angle = m_Status->Angle();
    Rotate(m_Status->Destination());

    if (abs(m_Status->Angle() - angle) > 90)
    {
        DecreaseSpeed();    // decrease the speed while the player rotate large than 90 degree

        return;
    }

    // 门将的后退
    if (m_Attribute->GetPosition() == Position_Goalkeeper)
    {
        if (m_Side == Side_Home)
        {
            m_Status->SetIsBackward((m_Status->Destination().X < m_Status->Current().X));
        }
        else
        {
            m_Status->SetIsBackward((m_Status->Destination().X > m_Status->Current().X));
        }

        if (m_Status->Hasball()) // 如果门将往球移动，则为站起状态
        {
            m_Status->SetIsStandUp(false);
        }
    }
    
    // 球员移动
    // 计算总移动的距离
    double totalDistance = Current().Distance(Destination());
    if ((int)totalDistance == 0)
    {
        Redecide();

        return;
    }

    // 当前回合可移动的距离
    double topSpeed = PlayerRule::GetSpeed(this, Speed() + Acceleration() * Defines_Match.ROUND_TIME);
    double passedDistance = (Speed() + topSpeed) / 2 * Defines_Match.ROUND_TIME;
    
    if (passedDistance > totalDistance)
    {
        m_Status->SetCurrent(m_Status->Destination());

        return;
    }

    // 将移动距离映射至坐标系上
    double changeX = fabs(m_Status->Destination().X - m_Status->Current().X) * passedDistance / totalDistance;
    double changeY = fabs(m_Status->Destination().Y - m_Status->Current().Y) * passedDistance / totalDistance;

    // 计算移动后坐标
    double x = m_Status->Current().X;
    double y = m_Status->Current().Y;
    x += (m_Status->Destination().X > m_Status->Current().X) ? changeX : -changeX;
    y += (m_Status->Destination().Y > m_Status->Current().Y) ? changeY : -changeY;

    // 防止守门员离开禁区 同时守门员持球时可以离开禁区 （解决门将无法接传球失误的球 2010-6-21 成嘉伟）
    if (m_Attribute->GetPosition() == Position_Goalkeeper && m_Status->Hasball() == false)
    { 
        Region region = (m_Side == Side_Home) ? m_Match->GetPitch()->HomePenaltyRegion() : m_Match->GetPitch()->AwayPenaltyRegion();
        
        if (x < region.Start.X) x = region.Start.X;
        if (x > region.End.X)   x = region.End.X;
        if (y < region.Start.Y) y = region.Start.Y;
        if (y > region.End.Y)   y = region.End.Y;
    }

    // 防止球员移动出场地
    if (x < 0) x = 0;
    if (x > Defines_Pitch.MAX_WIDTH) x = Defines_Pitch.MAX_WIDTH;
    if (y < 0) y = 0;
    if (y > Defines_Pitch.MAX_HEIGHT) y = Defines_Pitch.MAX_HEIGHT;

    // 修正最后坐标&速度
    m_Status->SetCurrent(Coordinate(x, y));
    m_Status->SetSpeed(topSpeed);

    LogHelper::Insert("[Player]After Moving. current:" + Current + ", ball destination:" + lexical_cast<string>(m_Status->BallDistance()), Color_Brown);
}

void Player::MoveTo(Coordinate target)
{
    m_Status->SetCurrent(Coordinate(target.X, target.Y));
}

void Player::MoveTo(double x, double y)
{
    m_Status->SetCurrent(Coordinate(x, y));
}

/// 球员重置位置
void Player::Reset()
{
    m_Status->SetCurrent(m_Status->GetDefault());
    m_Status->SetDestination(m_Status->GetDefault());
    m_Status->SetState(IdleState::Instance());
    m_Status->SetSpeed(0);
    m_Status->SetAngle((m_Side == Side_Home) ? 0 : 180);
}

/// 将球员的速度降低为当前速度的一半
void Player::DecreaseSpeed()
{
    m_Status->SetSpeed(m_Status->Speed() / 2);
}

/// 判断球员是否已经移动超出了可移动区域
void Player::ValidateOutofRegion()
{
    // 持球人不考虑可移动区域
    if (m_Status->Hasball())
    {
        return;
    }

    // 当有防守人时可以移动出区域
    //if (_status.DefenceStatus.DefenceTarget != null)
    //{
    //    return;
    //}

    Region region = m_Attribute->MoveRegion();

    if (m_Status->IsAttackSide()) // 进攻方 
    {
        if (m_Side == Side_Home)
        {
            region.End.X += 30;
        }
        else
        {
            region.Start.X -= 30;
        }
    }
    else // 防守方
    {
        if (m_Side == Side_Home)
        {
            region.Start.X -= 30;
        }
        else
        {
            region.End.X += 30;
        }
    }

    if (m_Status->Current().X < region.Start.X ||
        m_Status->Current().X > region.End.X ||
        m_Status->Current().Y < region.Start.Y ||
        m_Status->Current().Y > region.End.Y)
    { // out of the move region

        double x = m_Status->Current().X;
        double y = m_Status->Current().Y;

        if (m_Status->Current().X < region.Start.X)
        {
            x = region.Start.X + 5;
        }

        if (m_Status->Current().X > region.End.X)
        {
            x = region.End.X - 5;
        }

        if (m_Status->Current().Y < region.Start.Y)
        {
            y = region.Start.Y + 5;
        }

        if (m_Status->Current().Y > region.End.Y)
        {
            y = region.End.Y - 5;
        }

        // 更改球员的移动目标为可移动范围内
        SetTarget(x, y);
    }
}

/// 内部保存函数
void Player::InternalSave(int round)
{
    m_Processes.push_back(m_Status->State()->Save(this));
}
