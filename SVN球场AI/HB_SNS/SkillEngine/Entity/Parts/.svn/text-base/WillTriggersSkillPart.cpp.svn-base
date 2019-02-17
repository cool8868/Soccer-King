/********************************************************************************
 * 文件名：WillTriggersSkillPart.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "WillTriggersSkillPart.h"

WillTriggersSkillPart::WillTriggersSkillPart()
{
    InitVariables();
}

WillTriggersSkillPart::WillTriggersSkillPart(WillTriggersSkillPart& rf)
{
    InitVariables();

    foreach (ITrigger* t, rf.Triggers())
    {
        m_Triggers.push_back(t->Clone());
    }
}

WillTriggersSkillPart::~WillTriggersSkillPart()
{
    foreach (ITrigger* t, m_Triggers)
    {
        delete t;
    }

    m_Triggers.clear();
}

void WillTriggersSkillPart::InitVariables()
{
    m_Triggers.reserve(5);
    m_Triggers.clear();
}


