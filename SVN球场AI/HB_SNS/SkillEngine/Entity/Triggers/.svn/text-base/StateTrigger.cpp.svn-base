/********************************************************************************
 * 文件名：StateTrigger.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "StateTrigger.h"

StateTrigger::StateTrigger()
{
    InitVariables();
}

StateTrigger::StateTrigger(StateTrigger& rf)
{
    InitVariables();

    foreach (string& state, rf.TriggerStates())
    {
        m_TriggerStates.push_back(state);
    }
}


void StateTrigger::SetStates(string vl)
{
    if (vl[0] == '*')
    {
        m_States = String::Empty();

        foreach (MapString_IState::value_type& state, StateSelecter::Instance()->States())
        {
            m_States    = state.first;
            m_States    += ",";
        }
    }
    else
    {
        m_States = vl;
    }

    m_TriggerStates = Common::TransToStringVector(m_States, ",");
}

bool StateTrigger::IsSkillTriggered(IPlayer* player)
{
    //if (player.Status.State is DefenceState) {
    //    if (player.Status.DefenceStatus.DefenceTarget == null) {
    //        return false;
    //    }

    //    if (player.Status.DefenceTargetDistance > Defines.Player.DEFENCE_RANGE) {
    //        return false;
    //    }

    //    if (!player.Status.DefenceStatus.DefenceTarget.Status.Holdball) {
    //        return false;
    //    }

    //}
    return true;
}

void StateTrigger::InitVariables()
{
    m_States        = String::Empty();

    m_TriggerStates.reserve(4);
    m_TriggerStates.clear();
}
