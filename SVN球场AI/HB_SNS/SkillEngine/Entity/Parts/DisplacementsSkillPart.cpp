/********************************************************************************
 * 文件名：DisplacementsSkillPart.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "DisplacementsSkillPart.h"

DisplacementsSkillPart::DisplacementsSkillPart()
{
    InitVariables();
}

DisplacementsSkillPart::DisplacementsSkillPart(DisplacementsSkillPart& rf)
{
    InitVariables();

    foreach (Displacement* d, rf.Displacements())
    {
        m_Displacements.push_back(d->Clone());
    }
}

DisplacementsSkillPart::~DisplacementsSkillPart()
{
    foreach (Displacement* d, m_Displacements)
    {
        delete d;
    }

    m_Displacements.clear();
}

void DisplacementsSkillPart::InitVariables()
{
    m_Displacements.reserve(4);
    m_Displacements.clear();
}

//////////////////////////////////////////////////////////////////////////
Displacement::Displacement()
{
    InitVariables();
}

Displacement::Displacement(Displacement& rf)
{
    InitVariables();

    m_Type              = rf.Type();
    m_Distance          = rf.Distance();
    m_Angle             = rf.Angle();
    m_Target            = rf.Target();
    m_TargetPosition    = rf.TargetPosition();
}

void Displacement::InitVariables()
{
    m_Type      = 0;
    m_Distance  = 0;
    m_Angle     = 0;
    m_Target    = 0;

    m_TargetPosition.clear();
}


