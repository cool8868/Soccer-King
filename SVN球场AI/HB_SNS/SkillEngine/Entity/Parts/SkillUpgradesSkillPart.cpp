/********************************************************************************
 * 文件名：SkillUpgradesSkillPart.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "SkillUpgradesSkillPart.h"

SkillUpgradesSkillPart::SkillUpgradesSkillPart()
{
    InitVariables();
}

SkillUpgradesSkillPart::SkillUpgradesSkillPart(SkillUpgradesSkillPart& rf)
{
    foreach (SkillUpgrade* s, rf.SkillUpgrades())
    {
        m_Upgrades.push_back(s->Clone());
    }
}

SkillUpgradesSkillPart::~SkillUpgradesSkillPart()
{
    m_SkillReference    = NULL;

    foreach (SkillUpgrade* s, m_Upgrades)
    {
        delete s;
    }

    m_Upgrades.clear();
}

void SkillUpgradesSkillPart::InitVariables()
{
    m_SkillReference    = NULL;

    m_Upgrades.reserve(4);
    m_Upgrades.clear();
}

//////////////////////////////////////////////////////////////////////////
SkillUpgrade::SkillUpgrade()
{
    InitVariables();
}

SkillUpgrade::SkillUpgrade(SkillUpgrade& rf)
{
    m_Type                  = rf.Type();
    m_SkillType             = rf.SkillType();
    m_SkillTarget           = rf.SkillTarget();
    m_Target                = rf.Target();
    m_TargetPosition        = rf.TargetPosition();
    m_TargetDebuff          = rf.TargetDebuff();
    m_TargetSkillProperty   = rf.TargetSkillProperty();
    m_Rate                  = rf.Rate();
    m_FixType               = rf.FixType();
}

void SkillUpgrade::InitVariables()
{
    m_Type              = 0;
    m_SkillTarget       = String::Empty();
    m_Target            = 0;
    m_Rate              = 0.0;
    m_FixType           = 0;

    m_SkillType.clear();
    m_TargetPosition.clear();
    m_TargetDebuff.clear();
    m_TargetSkillProperty.clear();
}

