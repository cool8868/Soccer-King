/********************************************************************************
 * 文件名：OpenerRawSkill.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "OpenerRawSkill.h"

OpenerRawSkill::OpenerRawSkill(string id, string name, int last, vector<int>& formation)
    : RawSkill(id, name, SkillType_Opener) 
{
    SetLast(last);
    SetFormation(formation);
}

void OpenerRawSkill::SetSkillPart(string name, ISkillPart* skillPart)
{
    string str_trim = "Builder";

    if (name == erase_last_copy(TypeId(ManagerChangesBuilder), str_trim))
    {
        m_ManagerChanges = skillPart;
    }
    else if (name == erase_last_copy(TypeId(SkillUpgradesBuilder), str_trim))
    {
        m_SkillUpgrades = skillPart;
    }
    else if (name == erase_last_copy(TypeId(ImmunityBuilder), str_trim))
    {
        m_Immunity = skillPart;
    }
    else if (name == erase_last_copy(TypeId(SpecialEffectsBuilder), str_trim))
    {
        m_SpecialEffects = skillPart;
    }
}
