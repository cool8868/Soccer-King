/********************************************************************************
 * 文件名：PassiveSkillBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PASSIVESKILLBUILDER_H__
#define __PASSIVESKILLBUILDER_H__

/// Represents the passive skill's builder.
class PassiveSkillBuilder : SkillBuilder
{
public:

    PassiveSkillBuilder();

public:

    /// Build a skill with the skill's code.
    IRawSkill* BuildSkill(string skillId);;

private:

    // 将经理技能添加到经理对象身上
    void        AttatchToManagerChangesSkillPart(xml_node& node, IManagerSkill* skill, ISkillPart* skillPart);
};

#endif //__PASSIVESKILLBUILDER_H__

