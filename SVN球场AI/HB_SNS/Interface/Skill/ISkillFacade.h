/********************************************************************************
 * 文件名：ISkillFacade.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ISKILLFACADE_H__
#define __ISKILLFACADE_H__

#include "IRawSkill.h"

#include "../Player/IPlayer.h"

class ISkillFacade 
{
public:
    
    virtual ~ISkillFacade() {}

public:

    /// Intialize the skill engine.
    virtual void            Initialize() = 0;

    /// Get a skill by skill's id and skill's level.
    virtual IRawSkill*      GetActionSkill(string skillId) = 0;

    /// Validate a action type skill has chance to triggered.
    /// <param name="player"><see cref="IPlayer"/></param>
    /// <param name="skill"><see cref="IActionSkill"/></param>
    virtual bool            IsActionSkillTriggered(IPlayer* player, IActionSkill* skill) = 0;
};

#endif //__ISKILLFACADE_H__
