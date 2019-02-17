/********************************************************************************
 * 文件名：StateTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __STATETRIGGER_H__
#define __STATETRIGGER_H__

#include "../../../Interface/Skill/ITrigger.h"

/// Represents the skill that trigged by enter the <see cref="IState"/>.
class StateTrigger : public ITrigger
{
public:
    
    StateTrigger();

    StateTrigger(StateTrigger& rf);

public:

    string              States() { return m_States; }
    void                SetStates(string vl);

    /// Represents the list of the trigger states.
    vector<string>&     TriggerStates() { return m_TriggerStates; }

    /// Validate the skill is triggered or not.
    bool                IsSkillTriggered(IPlayer* player) { return true; }

    StateTrigger*       Clone() { return new StateTrigger(*this); }

    bool                IsKindOf(string name) { return typeid(*this).name() == name; }

    void SetAttribute(xml_node& node)
    {
        if (!node)
        {
            return;
        }

        string states           = "states";
        string value;

        if (Common::GetAttribute(node, states, value))
        {
            SetStates(value);
        }
    }

protected:

    /// Represents the raw state config.
    /// <example>HoldBallState, DiveBallState</example>
    string              m_States;

    vector<string>      m_TriggerStates;

private:

    void    InitVariables();
};

#endif //__STATETRIGGER_H__
