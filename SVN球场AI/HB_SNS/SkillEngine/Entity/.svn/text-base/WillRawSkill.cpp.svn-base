/********************************************************************************
 * 文件名：WillRawSkill.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "WillRawSkill.h"

WillRawSkill::WillRawSkill(string id, string name)
    : RawSkill(id, name, SkillType_Will)
{

}

void WillRawSkill::SetSkillPart(string name, ISkillPart* skillPart)
{
    string str_trim = "Builder";

    if (name == erase_last_copy(TypeId(TriggersBuilder), str_trim))
    {
        m_Triggers = skillPart;
    }
    else if (name == erase_last_copy(TypeId(PropertyChangesBuilder), str_trim))
    {
        m_PropertyChanges = skillPart;
    }
}

