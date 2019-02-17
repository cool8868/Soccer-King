/********************************************************************************
 * 文件名：ISkillBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ISKILLBUILDER_H__
#define __ISKILLBUILDER_H__

#include "../IRawSkill.h"

class ISkillBuilder 
{
public:

    virtual ~ISkillBuilder() {};

public:

    /// Initialize.
    virtual void        Initialize() = 0;

    /// Build a skill.
    virtual IRawSkill*  BuildSkill(string skillCode) = 0;
};

#endif //__ISKILLBUILDER_H__
