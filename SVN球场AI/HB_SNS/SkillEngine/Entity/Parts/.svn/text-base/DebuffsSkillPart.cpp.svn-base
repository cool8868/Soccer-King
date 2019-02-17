/********************************************************************************
 * 文件名：DebuffsSkillPart.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "DebuffsSkillPart.h"

DebuffsSkillPart::DebuffsSkillPart()
{
    InitVariables();
}

DebuffsSkillPart::DebuffsSkillPart(DebuffsSkillPart& rf)
{
    InitVariables();

    foreach (Debuff* d, rf.Debuffs()) 
    {
        m_Debuffs.push_back(d->Clone());
    }
}

DebuffsSkillPart::~DebuffsSkillPart()
{
    foreach (Debuff* d, m_Debuffs)
    {
        delete d;
    }

    m_Debuffs.clear();
}

void DebuffsSkillPart::InitVariables()
{
    m_Debuffs.reserve(4);
    m_Debuffs.clear();
}

//////////////////////////////////////////////////////////////////////////
Debuff::Debuff()
{
    InitVariables();
}

Debuff::Debuff(Debuff& rf)
{
    InitVariables();

    m_Type              = rf.Type();
    m_Level             = rf.Level();
    m_Target            = rf.Target();
    m_TargetPosition    = rf.TargetPosition();
    m_Probability       = rf.Probability();
}

void Debuff::InitVariables()
{
    m_Type          = 0;
    m_Level         = 0;
    m_Target        = 0;
    m_Probability   = 0;

    m_TargetPosition.clear();
}


