/********************************************************************************
 * 文件名：Player_ISkill.cpp
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

void Player::RefreshSkill()
{
    vector<IActionSkill*> refreshSkillList(10);
    refreshSkillList.clear();

    foreach (MapIState_VectorIActionSkill::value_type& skills ,m_Skills)
    {
        foreach (IActionSkill* skill, skills)
        {
            if (find(refreshSkillList.begin(), refreshSkillList.end(), skill) == refreshSkillList.end())
            {
                refreshSkillList.push_back(skill);
            }
        }
    }

    foreach (IActionSkill* skill, refreshSkillList)
    {
        skill->TimeLapse();
    }
}

/// Refresh all the player's propertys by the incoming propertys.
void Player::RefreshProperty(AbsBuff* buff)
{
    if (buff->Type() == BuffType_Dot)
    {
        foreach (int& pro, buff->PropertyId())
        {
            m_CurrProperty[pro] += m_RawProperty[pro] * PointerCastSafe(AbsDotBuff, buff)->Gradient() / 100;
        }
    }
    else
    {
        foreach (int& pro, buff->PropertyId())
        {
            m_CurrProperty[pro] += m_RawProperty[pro] * buff->Rate() / 100;
        }
    }
}

/// Recover all the player's current propertys by the incoming propertys.
void Player::RecoverProperty(AbsBuff* buff)
{
    if (buff->Type() == BuffType_Dot)
    {
        foreach (int& pro, buff->PropertyId())
        {
            m_CurrProperty[pro] -= m_RawProperty[pro] * PointerCastSafe(AbsDotBuff, buff)->Gradient() / 100 * buff->Last();
        }
    }
    else
    {
        foreach (int& pro, buff->PropertyId())
        {
            m_CurrProperty[pro] -= m_RawProperty[pro] * buff->Rate() / 100;
        }
    }
}

