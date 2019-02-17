/********************************************************************************
 * 文件名：SecondHalfTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SECONDHALFTRIGGER_H__
#define __SECONDHALFTRIGGER_H__

#include "../../../Interface/Skill/ITrigger.h"

/// Represents a trigger that validate the skill needs to trigger in the second half.
class SecondHalfTrigger : public ITrigger 
{
public:

    /// Validate the skill is triggered or not.
    bool IsSkillTriggered(IPlayer* player) 
    { 
        return false; 
    }

    SecondHalfTrigger* Clone() 
    { 
        return &singleton_default<SecondHalfTrigger>::instance(); 
    }

    bool IsKindOf(string name) { return typeid(*this).name() == name; }

    void SetAttribute(xml_node& node) { }
};

#endif //__SECONDHALFTRIGGER_H__
