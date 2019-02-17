/********************************************************************************
 * 文件名：ImmunitySkillPart.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ImmunitySkillPart.h"

ImmunitySkillPart::ImmunitySkillPart()
{
    InitVariables();
}

ImmunitySkillPart::ImmunitySkillPart(ImmunitySkillPart& rf)
{
    InitVariables();

    foreach (Immunity* i, rf.Immunities())
    {
        m_Immunities.push_back(i->Clone());
    }
}

ImmunitySkillPart::~ImmunitySkillPart()
{
    m_SkillReference    = NULL;

    foreach (Immunity* i, m_Immunities)
    {
        delete i;
    }

    m_Immunities.clear();
}

void ImmunitySkillPart::InitVariables()
{
    m_SkillReference    = NULL;

    m_Immunities.reserve(4);
    m_Immunities.clear();
}

//////////////////////////////////////////////////////////////////////////
Immunity::Immunity()
{
    InitVariables();
}

Immunity::Immunity(Immunity& rf)
{
    InitVariables();

    m_Type          = rf.Type();
    m_Rate          = rf.Rate();
    m_TargetDebuff  = rf.TargetDebuff();
    m_Probability   = rf.Probability();
}

void Immunity::InitVariables()
{
    m_Type              = 0;
    m_Rate              = 0.0;
    m_TargetDebuff      = 0;
    m_Probability       = 0;
}


