/********************************************************************************
 * 文件名：TargetTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __TARGETTRIGGER_H__
#define __TARGETTRIGGER_H__

#include "../../../Interface/Skill/ITrigger.h"

/// Represents a trigger that triggered by target.
class TargetTrigger : public ITrigger 
{
public:

    TargetTrigger();

    TargetTrigger(TargetTrigger& rf);

public:

    int             Target() { return m_Target; }
    void            SetTarget(int vl) { m_Target = vl; }

    vector<int>&    TargetPosition() { return m_TargetPosition; }
    void            SetTargetPosition(vector<int>& vl) { m_TargetPosition = vl; }

    int             Distance() { return m_Distance; }
    void            SetDistance(int vl) { m_Distance = vl; }

    bool            IsSkillTriggered(IPlayer* player);

    TargetTrigger*  Clone() { return new TargetTrigger(*this); }

    bool            IsKindOf(string name) { return typeid(*this).name() == name; }

    void            SetAttribute(xml_node& node){ }

protected:

    /// Represents the target index.
    int             m_Target;

    /// Represents the target's position.
    vector<int>     m_TargetPosition;

    /// Represents the target's distance.
    int             m_Distance;

private:

    void    InitVariables();
};

#endif //__TARGETTRIGGER_H__
