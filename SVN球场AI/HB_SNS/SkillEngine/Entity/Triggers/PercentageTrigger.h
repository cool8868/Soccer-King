/********************************************************************************
 * 文件名：PercentageTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PERCENTAGETRIGGER_H__
#define __PERCENTAGETRIGGER_H__

#include "../../../Interface/Skill/ITrigger.h"

/// Represents a trigger that validate the trigger's trigger percentage.
class PercentageTrigger : public ITrigger 
{
public:

    PercentageTrigger() 
    { 
        InitVariables(); 
    }

    PercentageTrigger(PercentageTrigger& rf)
    {
        InitVariables();

        m_Probability   = rf.Probability();
    }

public:

    /// Represents the skill trigger's probability.
    int                 Probability() { return m_Probability; }
    void                SetProbability(int vl) { m_Probability = vl; }

    /// Validate the skill is triggered or not.
    bool                IsSkillTriggered(IPlayer* player) { return RandomHelper::GetPercentage() < m_Probability; }

    PercentageTrigger*  Clone() { return new PercentageTrigger(*this); }

    bool                IsKindOf(string name) { return typeid(*this).name() == name; }

    void SetAttribute(xml_node& node)
    {
        if (!node)
        {
            return;
        }

        string probability  = "probability";
        string value;

        if (Common::GetAttribute(node, probability, value))
        {
            SetProbability(lexical_cast<int>(value));
        }        
    }

protected:

    /// Represents the skill trigger's probability.
    int     m_Probability;

private:

    void InitVariables()
    {
        m_Probability = 0;
    }
};

#endif //__PERCENTAGETRIGGER_H__
