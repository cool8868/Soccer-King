/********************************************************************************
 * 文件名：Player_IDisturb.cpp
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

/// Validate current player has into any defender's defence range.
/// Only the ball handler will decrease attribute-points, 
/// off-ball player has no disturb effect.
void Player::ValidateDisturbState()
{
    // while the player is not the ball handler.
    if (m_Status->Hasball() == false)
    {
        return;
    }

    // insert new disturb debuff
    double decrease = 0.0;
    foreach (IPlayer* d, m_Manager->Opponent()->Players())
    {
        if (!d->Status()->Enable())
        {
            continue;
        }

        if (d->Status()->GetDebuffStatus()->GetDebuffType() == DebuffType_InertiaDebuff)
        {
            continue;
        }

        if (Current().Distance(d->Current()) <= m_Attribute->DefenceRange())
        {
            decrease += -d->CurrProperty()[PlayerProperty_Disturb] * 0.25;
        }
    }

    // all the disturb debuff will compose one debuff, so the trigger is self.
    if (decrease < 0.0)
    {
        decrease = decrease + m_CurrProperty[PlayerProperty_Balance] * 0.125;

        if (decrease < 0.0) // 不允许干扰提高数值
        {
            DeBuff* debuff = new DeBuff(m_Id, 1, BuffType_Disturb);

            vector<int>& vecPropertyId = debuff->PropertyId();
            vecPropertyId += PlayerProperty_Passing, PlayerProperty_Shooting, PlayerProperty_Mentality;

            debuff->SetRate(decrease);
            AddDebuff(debuff);
        }
    }
}
