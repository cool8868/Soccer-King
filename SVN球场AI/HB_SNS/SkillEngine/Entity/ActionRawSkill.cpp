/********************************************************************************
 * 文件名：ActionRawSkill.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ActionRawSkill.h"

ActionRawSkill::ActionRawSkill(string id, string name, int cd)
    : RawSkill(id, name, SkillType_Action) 
{
    m_CoolDown = cd;
}

void ActionRawSkill::SetSkillPart(string name, ISkillPart* skillPart)
{
    string str_trim = "Builder";

    if (name == erase_last_copy(TypeId(TriggerConditionsBuilder), str_trim))
    {
        m_TriggerConditions = skillPart;
    }
    else if (name == erase_last_copy(TypeId(PropertyChangesBuilder), str_trim))
    {
        m_PropertyChanges = skillPart;
    }
    else if (name == erase_last_copy(TypeId(DisplacementsBuilder), str_trim))
    {
        m_Displacements = skillPart;
    }
    else if (name == erase_last_copy(TypeId(FoulRelatedBuilder), str_trim))
    {
        m_FoulRelated = skillPart;
    }
    else if (name == erase_last_copy(TypeId(BallRelatedBuilder), str_trim))
    {
        m_BallRelated = skillPart;
    }
    else if (name == erase_last_copy(TypeId(DebuffsBuilder), str_trim))
    {
        m_Debuffs = skillPart;
    }
    else if (name == erase_last_copy(TypeId(SpecialEffectsBuilder), str_trim))
    {
        m_SpecialEffects = skillPart;
    }
    else if (name == erase_last_copy(TypeId(ModelsBuilder), str_trim))
    {
        m_Models = skillPart;
    }
}

