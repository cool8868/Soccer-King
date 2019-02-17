/********************************************************************************
 * 文件名：ActionFacade.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ACTIONFACADE_H__
#define __ACTIONFACADE_H__

#include "../../common/common.h"
#include "../../Interface/IRequestInitialize.h"
#include "../../Interface/Player/IPlayer.h"
#include "../Entity/ActionRawSkill.h"

class ActionFacade : public IRequestInitialize
{
public:

    /// Initializes all the action skills.
    void                Initialize();

    /// Get a action skill by skill id and the skill level.
    IRawSkill*          GetSkill(string skillId);

    /// Validate the action type skill is been trigger.
    bool                IsSkillTrigger(IPlayer* player, ActionRawSkill* skill);

    /// Effect a skill.
    void                Effect(IPlayer* player, IRawSkill* skill);

    /// Get the skill's targets.
    vector<IPlayer*>    GetSkillTargets(IPlayer* player, IActionRawSkill* skill);
};

#endif //__ACTIONFACADE_H__
