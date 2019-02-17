/********************************************************************************
 * 文件名：TargetTrigger.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "TargetTrigger.h"
#include "../Effects/TargetSelector.h"

TargetTrigger::TargetTrigger()
{
    InitVariables();
}

TargetTrigger::TargetTrigger(TargetTrigger& rf)
{
    InitVariables();

    m_Target                = rf.Target();
    m_TargetPosition        = rf.TargetPosition();
    m_Distance              = rf.Distance();
}

/// Validate the skill is triggered or not.
bool TargetTrigger::IsSkillTriggered(IPlayer* player) 
{
    vector<IPlayer*> targets = singleton_default<TargetSelector>::instance().GetTarget(m_Target, m_TargetPosition, player);

    if (targets.size() == 0) 
    {
        return false;
    }

    foreach (IPlayer* target, targets) 
    {
        if (player->Current().Distance(target->Current()) > m_Distance) 
        {
            return false;
        }
    }

    return true;
}

void TargetTrigger::InitVariables()
{
    m_Target        = 0;
    m_Distance      = 0;

    m_TargetPosition.clear();
}

