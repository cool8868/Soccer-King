/********************************************************************************
 * 文件名：SkillTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SKILLTRIGGER_H__
#define __SKILLTRIGGER_H__

#include "../../Entity/Parts/TriggerSkillPart.h"
#include "../../../Exception/SkillErrorException.h"

/// Represents the trigger that to validate the skill has been triggered.
class SkillTrigger
{
public:

    virtual ~SkillTrigger() {}

public:

    /// Validate the action type skill is been trigger.
    bool IsSkillTrigger(IPlayer* player, ActionRawSkill* skill)
    {
        try
        {
            TriggerSkillPart* skillPart = PointerCastSafe(TriggerSkillPart, skill->TriggerConditions());

            foreach (ITrigger* trigger, skillPart->Triggers())
            {
                if (!trigger->IsSkillTriggered(player))
                {
                    return false;
                }
            }

            return true;
        }
        catch (MyException& ex)
        {
            throw SkillErrorException("ActionFacade class's ValidateSkillTrigger method causes exception.", ex);
        }
    }
};

#endif //__SKILLTRIGGER_H__
