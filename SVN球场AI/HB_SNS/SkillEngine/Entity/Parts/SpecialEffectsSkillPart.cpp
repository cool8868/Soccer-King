/********************************************************************************
 * 文件名：SpecialEffectsSkillPart.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "SpecialEffectsSkillPart.h"

SpecialEffectsSkillPart::SpecialEffectsSkillPart()
{
    InitVariables();
}

SpecialEffectsSkillPart::SpecialEffectsSkillPart(SpecialEffectsSkillPart& rf)
{
    InitVariables();

    foreach (Special* s, rf.Specials())
    {
        m_Specials.push_back(s->Clone());
    }
}

SpecialEffectsSkillPart::~SpecialEffectsSkillPart()
{
    foreach (Special* s, m_Specials)
    {
        delete s;
    }

    m_Specials.clear();
}

void SpecialEffectsSkillPart::InitVariables()
{
    m_Specials.reserve(4);
    m_Specials.clear();
}
//////////////////////////////////////////////////////////////////////////
Special::Special()
{
    InitVariables();
}

Special::Special(Special& rf)
{
    m_Name      = rf.Name();
    m_Value     = rf.Value();
}

void Special::InitVariables()
{

}


