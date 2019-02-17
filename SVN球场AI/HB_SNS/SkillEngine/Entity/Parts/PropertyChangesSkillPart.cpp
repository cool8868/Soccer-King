/********************************************************************************
 * 文件名：PropertyChangesSkillPart.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "PropertyChangesSkillPart.h"

PropertyChangesSkillPart::PropertyChangesSkillPart()
{
    InitVariables();
}

PropertyChangesSkillPart::PropertyChangesSkillPart(PropertyChangesSkillPart& rf)
{
    InitVariables();

    foreach (PropertyChange* p, rf.PropertyChanges())
    {
        m_PropertyChanges.push_back(p->Clone());
    }
}

PropertyChangesSkillPart::~PropertyChangesSkillPart()
{
    foreach (PropertyChange* p, m_PropertyChanges)
    {
        DeletePtrSafe(p);
    }

    m_PropertyChanges.clear();
}

void PropertyChangesSkillPart::InitVariables()
{
    m_PropertyChanges.reserve(5);
    m_PropertyChanges.clear();
}

//////////////////////////////////////////////////////////////////////////
PropertyChange::PropertyChange()
{
    InitVariables();
}

PropertyChange::PropertyChange(PropertyChange& rf)
{
    InitVariables();

    m_Property          = rf.Property();
    m_Rate              = rf.Rate();
    m_Last              = rf.Last();
    m_Gradient          = rf.Gradient();
    m_Target            = rf.Target();
    m_TargetPosition    = rf.TargetPosition();
}

void PropertyChange::InitVariables()
{
    m_Property      = String::Empty();
    m_Rate          = 0.0;
    m_Last          = 0;
    m_Gradient      = 0.0;
    m_Target        = 0;

    m_TargetPosition.clear();
}

