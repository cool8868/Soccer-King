/********************************************************************************
 * 文件名：OpenerFacade.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __OPENERFACADE_H__
#define __OPENERFACADE_H__

#include "../../Interface/IRequestInitialize.h"
#include "../../Interface/Skill/IRawSkill.h"
#include "../../Interface/Player/IPlayer.h"

/// Represents the opener skill's facade.
class OpenerFacade : public IRequestInitialize
{
public:

    /// Initializes all the opener skills.
    void Initialize();

    /// Get a opener skill by skill id and the skill level.
    IRawSkill* GetSkill(string skillId);

    /// Effect a opener skill.
    void Effect(IPlayer* player, IRawSkill* skill);
};

#endif //__OPENERFACADE_H__

