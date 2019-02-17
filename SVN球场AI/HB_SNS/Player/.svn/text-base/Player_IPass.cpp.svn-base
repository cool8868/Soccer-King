/********************************************************************************
 * 文件名：Player_IPass.cpp
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

#include "../AI/Rules/PassTargetDecideRule.h"
#include "../AI/Rules/FootballRule.h"

#include "../AI/Decides/Utility.h"

#include "../common/Enum/PlayerProperty.h"

/// 选择传递目标
void Player::DecidePassTarget()
{
    if (m_Attribute->GetPosition() == Position_Goalkeeper)
    {
        m_Status->GetPassStatus()->SetPassTarget(PassTargetDecideRule::GoalKickDecide(this));
    }
    else
    {
        Region region = (m_Side == Side_Home) ? m_Match->GetPitch()->AwayForcePassRegion() : m_Match->GetPitch()->HomeForcePassRegion();
        
        if (region.IsCoordinateInRegion(Current()))
        {
            m_Status->GetPassStatus()->SetPassTarget(PassTargetDecideRule::ForcePassDecide(this));
        }
        else
        {
            m_Status->GetPassStatus()->SetPassTarget(PassTargetDecideRule::Decide(this));
        }
    }
}

/// Player to pass the ball to passtarget.
void Player::ShortPass()
{
    m_Status->GetPassStatus()->SetIsPassFail(false);

    if (m_Status->Hasball() == false || m_Status->Holdball() == false)
    {
        return;
        // throw new NotSupportedException("[传球]球不在这个球员身上：" + this);
    }

    Coordinate target = GetTarget();
    double speed = FootballRule::GetPassSpeed(m_Status->Current(), target);

    format fmt("[短传]%d 把球传递至(%.2f, %.2f), 球速: %.2f");
    fmt % m_Id % target.X % target.Y % speed;

    LogHelper::Insert(fmt.str(), Color_Black);

    Rotate(target);
    m_Match->GetFootball()->Kick(target, speed, this);

    InternalPass();
}

/// Player to action a long pass.
void Player::LongPass()
{
    m_Status->GetPassStatus()->SetIsPassFail(false);

    if (m_Status->Hasball() == false || m_Status->BallDistance() > 0)
    {
        return;
        // throw new NotSupportedException("[传球]球在不在这个球员身上：" + this);
    }

    Coordinate target = GetTarget();
    double speed = FootballRule::GetPassSpeed(m_Status->Current(), target);
    int angle = FootballRule::GetLongPassAngle();

    format fmt("[长传]%d 把球踢去%d, Speed:%.2f");
    fmt % m_Id % m_Status->GetPassStatus()->PassTarget()->Id() % speed;

    LogHelper.Insert(fmt.str(), Color_Black);

    Rotate(m_Status->GetPassStatus()->PassTarget()->Current());
    m_Match->GetFootball()->Kick(m_Status->GetPassStatus()->PassTarget()->Current(), speed, angle, this);

    InternalPass();
}

/// 判定坐标是否可以传(回传线)
/// <param name="c"><see cref="Coordinate"/></param>
/// <returns>Enable to pass.</returns>
bool Player::IsCoordinateEnablePass(Coordinate c)
{
    if (m_Side == Side_Home)
    {
        return c.X > (m_Status->Current().X - Defines_Player.PASS_BACK_RANGE);
    }
    else
    {
        return c.X < (m_Status->Current().X + Defines_Player.PASS_BACK_RANGE);
    }
}

/// 内部传球逻辑
void Player::InternalPass()
{
    IPlayer* receiver = m_Status->GetPassStatus()->PassTarget();
    receiver->Status()->SetHasball(true);

    m_Status->Redecide();
    receiver->Status()->Redecide();
}

/// 获取传球的目标点
Coordinate Player::GetTarget()
{
    Coordinate target = m_Status->GetPassStatus()->PassTarget()->Current();

    if (target.Y < Defines_Pitch.PASS_PROTECTED_LINE || target.Y > Defines_Pitch.MAX_HEIGHT - Defines_Pitch.PASS_PROTECTED_LINE)
    {
        if (m_Side == Side_Home)
        {
            target = Coordinate(target.X + 10, target.Y);
        }
        else
        {
            target = Coordinate(target.X - 10, target.Y);
        }
    }
    else
    {
        target = Utility::GetPointWithRange(m_Status->GetPassStatus()->PassTarget(), m_Status->GetPassStatus()->PassTarget()->Destination(), 
            Defines_Player.PASS_AHEAD_RANGE);
    }

    // The probility of the success pass
    double pro = 50 + 0.2 * m_CurrProperty[PlayerProperty_Passing];
    if (pro > 90.0)
    {
        pro = 90.0;
    }

    // 守门员不会传球失误
    if (m_Attribute->GetPosition() != Position_Goalkeeper)
    {
        // 传球失误
        if (RandomHelper::GetPercentage() >= pro)
        {
            target = PlayerRule::GetPassOffset(m_CurrProperty[PlayerProperty_Passing], target);
            m_Status->GetPassStatus()->SetIsPassFail(true);
        }
    }

    return target;
}
