/********************************************************************************
 * 文件名：Player_IDiveBall.cpp
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

/// 扑救
void Player::DiveBall()
{
    // 当守门员离场时直接返回
    if (!m_Status->Enable())
    {
        return;
    }

#if Debug_Mode
    if (m_Attribute->GetPosition() != Position_Goalkeeper) 
    {
        throw ApplicationException("Not a goal keeper has decide to make dive ball action. The player:" + lexical_cast<int>(m_Id));
    }
#endif

    IPlayer* shooter = m_Match->Status()->BallHandler();
    int shootIndex = shooter->Status()->GetShootStatus()->ShootTargetIndex();
    int speed = shooter->Status()->GetShootStatus()->ShootSpeed();

    m_Status->GetDiveStatus()->SetDiveDirection(GetDiveDirection(shooter));

    // 球打飞
    if (shootIndex == 4 || shootIndex == 0)
    {
        LogHelper::Insert("球打飞了", Color_Black);
        m_Status->SetHasball(true);
        m_Match->GetFootball()->MoveTo(m_Status->GetDefault());
        m_Match->MissShoot();

        return;
    }

    double n = 0.0;

    if (shootIndex == 1)
    {
        n = 0.9;
    }

    if (shootIndex == 2)
    {
        n = 0.6;
    }

    if (shootIndex == 3)
    {
        n = 0.4;
    }

    n = n * (1 - m_CurrProperty[PlayerProperty_Positioning] * 0.00167);

    // y=(v-30)/35*0.2+n-x*0.125%
    double property = ((double)(speed - 30) / 35 * 0.2 + n - m_CurrProperty[PlayerProperty_Reflexes] * 0.00125) * 100;

    format fmt("射门成功率:%.2f", Color_Black);
    fmt % property;

    LogHelper::Insert(fmt.str());

    if (RandomHelper::GetPercentage() < property) // Goal
    {
        m_Manager->Opponent()->Goal();
    }
    else
    {
        LogHelper::Insert("守门员扑到球", Color_Red);
        m_Status->SetHasball(true);

        OutOfHand(shooter);
    }
}

/// 获取扑救方向
/// <param name="shooter">射门的球员</param>
/// <returns>扑救方向</returns>
int Player::GetDiveDirection(IPlayer* shooter)
{
    if (shooter->Status()->GetShootStatus()->GetShootTarget().IsFrame) // shoot to the door frame
    {
        switch (shooter->Status()->GetShootStatus()->GetShootTarget().X)
        {
        case 1:
        case 2:
            return Direction_Left;
        case 3:
        case 4:
            return Direction_Right;
        }
    }

    if (shooter->Status()->GetShootStatus()->GetShootTarget().X < 6)
    {
        return Direction_Left;
    }

    if (shooter->Status()->GetShootStatus()->GetShootTarget().X > 13)
    {
        return Direction_Right;
    }

    return Direction_Center;
}

/// 守门员脱手
/// <param name="shooter">射门的球员</param>
void Player::OutOfHand(IPlayer* shooter)
{
    bool isOutHand = false;

    foreach (AbsBuff* debuff, m_DebuffBar)
    {
        AbnormalDebuff* abnormalDebuff = PointerCastSafe(AbnormalDebuff, debuff);
        if (abnormalDebuff != NULL)
        {
            if (abnormalDebuff->GetDebuffType() == DebuffType_OutOfHandDebuff)
            {
                isOutHand = true;
                break;
            }
        }
    }

    double speed = shooter->Status()->GetShootStatus()->ShootSpeed();
    double percentage = speed * 2 - m_CurrProperty[PlayerProperty_Handling] * 0.25;         

    if (RandomHelper::GetPercentage() < percentage || isOutHand) // 没有脱手            
    {
        double y1 = Defines_Pitch.MAX_HEIGHT - shooter->Current().Y;
        const double y2 = Defines_Pitch.MAX_HEIGHT / 2;
        const double x  = Defines_Pitch.MAX_WIDTH / 2;

        int y = RandomHelper::GetInt32((int)y1, (int)y2);

        m_Match->GetFootball()->Kick(Coordinate(x, y), speed * 0.6, this);
        m_Match->Status()->SetIsNoBallHandler(true);                
    }
}