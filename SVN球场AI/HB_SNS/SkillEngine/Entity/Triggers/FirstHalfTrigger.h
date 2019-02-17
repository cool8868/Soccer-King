/********************************************************************************
 * 文件名：FirstHalfTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FIRSTHALFTRIGGER_H__
#define __FIRSTHALFTRIGGER_H__

#include "../../../Interface/Skill/ITrigger.h"
#include "../../../Interface/Player/IPlayer.h"
#include "../../../Interface/IMatch.h"

/// Represents a trigger that validate the skill needs to trigger in the first half.
class FirstHalfTrigger : public ITrigger 
{
public:

    /// Validate the skill is triggered or not.
    bool IsSkillTriggered(IPlayer* player) 
    {
        return player->GetMatch()->Status()->Round() == 1;
    }

    FirstHalfTrigger* Clone() 
    { 
        return &singleton_default<FirstHalfTrigger>::instance();
    }

    bool IsKindOf(string name) { return typeid(*this).name() == name; }

    void SetAttribute(xml_node& node) {}
};

#endif //__FIRSTHALFTRIGGER_H__
