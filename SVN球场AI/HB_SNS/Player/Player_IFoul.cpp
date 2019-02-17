/********************************************************************************
 * 文件名：Player_IFoul.cpp
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

/// 犯规
/// foul's level.
/// 0 - Normal
/// 1 - Yellow Card
/// 2 - Red Card
void Player::Foul(int level)
{
    if (m_Status->GetFoulStatus().IsInjured() || m_Status->GetFoulStatus()->IsRedCard())
    {
        throw ApplicationException("Can't foul to a injured player or foul out player.");
    }

    // manager skills's effects have chances to avoid foul.
    // 经理技能有一定的概率避免抵抗犯规
    if (MATH::FloatEqual(m_Status->Status()->GetNoFoulLevelStatus()->Probability(), 0.0) == false)
    {
        if (RandomHelper::GetPercentage() < m_Manager->Status()->GetNoFoulLevelStatus()->Probability())
        {
            return;
        }
    }

    // manager skills's effects have chances to decrease the foul level.
    // 经理技能有一定概率减少犯规等级
    if (MATH::FloatEqual(m_Manager->Status()->GetDecreaseFoulLevelStatus()->Probability(), 0.0) == false)
    {
        if (RandomHelper::GetPercentage() < m_Manager->Status()->GetDecreaseFoulLevelStatus()->Probability())
        {
            if (level > Defines_FoulLevel.NORMAL)
            {
                level += (int)m_Manager->Status()->GetDecreaseFoulLevelStatus()->Rate();

                if (level < Defines_FoulLevel.NORMAL)
                {
                    level = Defines_FoulLevel.NORMAL;
                }

                if (level > Defines_FoulLevel.RED_CARD)
                {
                    level = Defines_FoulLevel.RED_CARD;
                }
            }                    
        }
    }

    // 两张黄牌累计为一张红牌
    if (level == Defines_FoulLevel.YELLOW_CARD && m_Status->GetFoulStatus()->FoulLevel() == Defines_FoulLevel.YELLOW_CARD)
    {
        level = Defines_FoulLevel.RED_CARD;
    }

    if (level < Defines_FoulLevel.NORMAL || level > Defines_FoulLevel.RED_CARD)
    {
        throw ArgumentOutOfRangeException("level", "Invokes Player class's Foul method with error argument - level:" + lexical_cast<string>(level));
    }

    if (level == Defines_FoulLevel.NORMAL) // normal foul
    { 
        m_Manager->Foul();
    }

    if (level == Defines_FoulLevel.YELLOW_CARD) // yellow card
    { 
        if (m_Status->Enable())
        {
            m_Status->GetFoulStatus()->SetFoulLevel(level);
        }

        m_Manager->Foul();

        return;
    }

    if (level == Defines_FoulLevel.RED_CARD) // red card
    { 
        m_Status->GetFoulStatus()->SetFoulLevel(level);

        Save(m_Match->Status()->Round()); // 红牌必须保存记录                
        
        m_Manager->Foul();                
    }
}

/// Injured
void Player::Injured()
{
    // 经理技能有一定概率避免受伤
    // manager skill's effects have chances to avoid injured.
    if (MATH::FloatEqual(m_Manager->Status()->GetNoInjuredStatus()->Probability(), 0.0) == false)
    {
        if (RandomHelper::GetPercentage() < m_Manager->Status()->GetNoInjuredStatus()->Probability())
        {
            return;
        }
    }

    m_Status->GetFoulStatus()->SetFoulLevel(Defines_FoulLevel.INJURED);
    
    Save(m_Match->Status()->Round()); // 受伤必须保存
}
