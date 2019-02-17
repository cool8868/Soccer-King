/********************************************************************************
 * 文件名：ActionSkillBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ACTIONSKILLBUILDER_H__
#define __ACTIONSKILLBUILDER_H__

#include "SkillBuilder.h"

/// Represents the action skill's builder
class ActionSkillBuilder : public SkillBuilder 
{
public:

    ActionSkillBuilder();

public:

    /// Build a skill with the skill's code.
    IRawSkill* BuildSkill(string skillId);
};

#endif //__ACTIONSKILLBUILDER_H__
