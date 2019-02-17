/********************************************************************************
 * 文件名：Player_IBuff.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明： Buff对应的事件函数
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "Player.h"

void Player::AddBuff(AddBuffEventArgs& eventArgs)
{
    if (eventArgs.GetBuff()->Type() == BuffType_Dot)
    {
        return;
    }

    RefreshProperty(eventArgs.GetBuff());
}

void Player::RemoveBuff(RemoveBuffEventArgs& eventArgs)
{
    RecoverProperty(eventArgs.GetBuff());
}

void Player::AddDebuff(AddDebuffEventArgs& eventArgs)
{
    if (eventArgs.GetDebuff()->Type() == BuffType_Dot)
    {
        AbsDotBuff* buff = PointerCastSafe(eventArgs.GetDebuff());

        double gradient = buff->Gradient();
        buff->SetGradient(gradient + static_cast<short>(gradient * m_Manager->Status()->GetDecreaseDebuffRateStatus()->Rate()));

        return;
    }

    double rate = eventArgs.GetDebuff()->Rate();
    eventArgs.GetDebuff()->SetRate(rate + rate * m_Manager->Status()->GetDecreaseDebuffRateStatus()->Rate());

    RefreshProperty(eventArgs.GetDebuff());
}

/// Recover the debuff's propertys.
void Player::RemoveDebuff(RemoveDebuffEventArgs& eventArgs)
{
    RecoverProperty(eventArgs.GetDebuff());
}

/// 刷新异常状态效果
void Player::RefreshDebuffStatus()
{
    m_Status->GetDebuffStatus()->SetDebuffType(DebuffType_None);
    m_Status->GetDebuffStatus()->SetDisableDebuff(NULL);

    foreach (AbsBuff* debuff, m_DebuffBar)
    {
        if (debuff->Type() == BuffType_Disable)
        {
            m_Status->GetDebuffStatus()->SetDebuffType(PointerCastSafe(AbnormalDebuff, debuff)->GetDebuffType());

            DisableDebuff* d = PointerCastSafe(DisableDebuff, debuff);
            if (d != NULL)
            {
                m_Status->GetDebuffStatus()->SetDisableDebuff(d);
            }
            return;
        }
    }
}

/// 判断是否免疫Debuff效果（增加一个免疫的Debuff效果）
bool Player::ValidateImmunityAddDebuff()
{
    if (MATH::FloatEqual(m_Manager->Status()->GetImmunityDebuffSkillStatus()->Probability(), 0.0))
    {
        return false;
    }

    return RandomHelper::GetPercentage() < m_Manager->Status()->GetImmunityDebuffSkillStatus()->Probability();
}

