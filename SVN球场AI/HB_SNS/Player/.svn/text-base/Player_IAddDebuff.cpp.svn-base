/********************************************************************************
 * 文件名：Player_IAddDebuff.cpp
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

/// 添加惯性的Debuff
void Player::AddInertia()
{
    AddInertia((int)(1 / Defines_Match.ROUND_TIME));            
}

/// 添加惯性的Debuff
void Player::AddInertia(int last)
{
    if (m_Manager->Status()->GetImmunityDebuffsStatus()->DebuffProperty()[(int)DebuffType_InertiaDebuff] != 0)
    {
        if (RandomHelper::GetPercentage() < m_Manager->Status()->GetImmunityDebuffsStatus()->DebuffProperty()[(int)DebuffType_InertiaDebuff])
        {
            return;
        }
    }

    // +++tony: 如果被AddDebuff返回，则会存在内存泄露?
    InertiaDebuff* debuff = new InertiaDebuff(_id, last);
    AddDebuff(debuff);

    m_Status->GetDebuffStatus()->SetDebuffType(DebuffType_InertiaDebuff);
    m_Status->GetDebuffStatus()->SetDisableDebuff(debuff);
}

/// 添加倒地的Debuff
void Player::AddDownDebuff(int triggerId, int last)
{
    if (m_Manager->Status()->GetImmunityDebuffsStatus()->DebuffProperty()[(int)DebuffType_DownDebuff] != 0)
    {
        if (RandomHelper::GetPercentage() < m_Manager->Status()->GetImmunityDebuffsStatus()->DebuffProperty()[(int)DebuffType_DownDebuff])
        {
            return;
        }
    }

    DownDebuff* debuff = new DownDebuff(triggerId, last);
    AddDebuff(debuff);
    AddModel(debuff->MID, debuff->Last());

    m_Status->GetDebuffStatus()->SetDebuffType(DebuffType_DownDebuff);
    m_Status->GetDebuffStatus()->SetDisableDebuff(debuff);
}

/// 添加困惑的Debuff
void Player::AddPuzzleDebuff(int triggerId, int last)
{
    if (m_Manager->Status()->GetImmunityDebuffsStatus()->DebuffProperty()[(int)DebuffType_PuzzleDebuff] != 0)
    {
        if (RandomHelper::GetPercentage() < m_Manager->Status()->GetImmunityDebuffsStatus()->DebuffProperty()[(int)DebuffType_PuzzleDebuff])
        {
            return;
        }
    }

    // +++ tony:内存泄露?
    PuzzleDebuff* debuff = new PuzzleDebuff(triggerId, last);
    AddDebuff(debuff);
    AddModel(debuff->MID, debuff->Last());

    m_Status->GetDebuffStatus()->SetDebuffType(DebuffType_PuzzleDebuff);
    m_Status->GetDebuffStatus()->SetDisableDebuff(debuff);
}

/// 添加眩晕异常状态
void Player::AddStunDebuff(int triggerId, int level)
{
    if (m_Manager->Status()->GetImmunityDebuffsStatus()->DebuffProperty()[(int)DebuffType_StunDebuff] != 0)
    {
        if (RandomHelper::GetPercentage() < m_Manager->Status()->GetImmunityDebuffsStatus()->DebuffProperty()[(int)DebuffType_StunDebuff])
        {
            return;
        }
    }

    int time;

    if (level == 1)
    {
        time = static_cast<int>(0.5 + 2 * (200 / m_CurrProperty()[PlayerProperty_Balance]));
    }
    else
    {
        time = static_cast<int>(1 + 3 * (200 / m_CurrProperty()[PlayerProperty_Balance]));
    }

    StunDebuff* debuff = new StunDebuff(triggerId, time);
    AddDebuff(debuff);
    AddModel(debuff->MID, debuff->Last());

    m_Status->GetDebuffStatus()->SetDebuffType(DebuffType_StunDebuff);
    m_Status->GetDebuffStatus()->SetDisableDebuff(debuff);
}

/// 添加脱手异常状态
void Player::AddOutOfHandDebuff(int triggerId, int level)
{
    if (m_Manager->Status()->GetImmunityDebuffsStatus()->DebuffProperty()[(int)DebuffType_OutOfHandDebuff] != 0)
    {
        if (RandomHelper::GetPercentage() < m_Manager->Status()->GetImmunityDebuffsStatus()->DebuffProperty()[(int)DebuffType_OutOfHandDebuff])
        {
            return;
        }
    }

    OutOfHandDebuff* debuff = new OutOfHandDebuff(triggerId, 1, level);
    AddDebuff(debuff);
}

/// 添加静止异常状态
void Player::AddFreezeDebuff(int triggerId, int last)
{
    if (m_Manager->Status()->GetImmunityDebuffsStatus()->DebuffProperty()[(int)DebuffType_FreezeDebuff] != 0)
    {
        if (RandomHelper::GetPercentage() < m_Manager->Status()->GetImmunityDebuffsStatus()->DebuffProperty()[(int)DebuffType_FreezeDebuff])
        {
            return;
        }
    }

    FreezeDebuff* debuff = new FreezeDebuff(triggerId, last);
    AddDebuff(debuff);
    AddModel(debuff->MID, debuff->Last());

    m_Status->GetDebuffStatus()->SetDebuffType(DebuffType_FreezeDebuff);
    m_Status->GetDebuffStatus()->SetDisableDebuff(debuff);
}

