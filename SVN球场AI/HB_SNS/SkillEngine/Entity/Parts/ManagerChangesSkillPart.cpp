/********************************************************************************
 * 文件名：ManagerChangesSkillPart.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ManagerChangesSkillPart.h"

ManagerChangesSkillPart::ManagerChangesSkillPart()
{
    InitVariables();
}

ManagerChangesSkillPart::ManagerChangesSkillPart(ManagerChangesSkillPart& rf)
{
    InitVariables();

    foreach (ManagerPropertyChange* p, rf.ManagerPropertyChanges())
    {
        m_ManagerPropertyChanges.push_back(p->Clone());
    }
}

ManagerChangesSkillPart::~ManagerChangesSkillPart()
{
    m_SkillReference = NULL;

    foreach (ManagerPropertyChange* p, m_ManagerPropertyChanges)
    {
        delete p;
    }

    m_ManagerPropertyChanges.clear();
}

void ManagerChangesSkillPart::InitVariables()
{
    m_SkillReference        = NULL;

    m_ManagerPropertyChanges.reserve(4);
    m_ManagerPropertyChanges.clear();
}

//////////////////////////////////////////////////////////////////////////
ManagerPropertyChange::ManagerPropertyChange()
{
    InitVariables();
}

ManagerPropertyChange::ManagerPropertyChange(ManagerPropertyChange& rf)
{
    m_Property          = rf.Property();
    m_Rate              = rf.Rate();
    m_Gradient          = rf.Gradient();
    m_Target            = rf.Target();
    m_TargetPosition    = rf.TargetPosition();
    m_Coordinate        = rf.Coordinate();
}

void ManagerPropertyChange::InitVariables()
{
    m_Property      = String::Empty();
    m_Rate          = 0.0;
    m_Gradient      = 0.0;
    m_Target        = 0;

    m_TargetPosition.clear();
    m_Coordinate.clear();
}


