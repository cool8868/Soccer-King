/********************************************************************************
 * 文件名：OpenerSkillBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __OPENERSKILLBUILDER_H__
#define __OPENERSKILLBUILDER_H__

#include "SkillBuilder.h"

#include "../../Interface/Skill/IManagerSkill.h"

class OpenerSkillBuilder : public SkillBuilder 
{
public:

    /// Initialize the <see cref="OpenerSkillBuilder"/>.
    OpenerSkillBuilder();

public:

    /// Build a skill with the skill's code.
    IRawSkill*  BuildSkill(string skillId);

private:

    // 将经理技能添加到经理对象身上
    void        AttatchToManagerChangesSkillPart(xml_node& node, IManagerSkill* skill, ISkillPart* skillPart);
};

#endif //__OPENERSKILLBUILDER_H__

