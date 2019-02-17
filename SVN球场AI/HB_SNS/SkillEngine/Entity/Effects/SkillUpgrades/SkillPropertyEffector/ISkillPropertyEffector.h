/********************************************************************************
 * 文件名：ISkillPropertyEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ISKILLPROPERTYEFFECTOR_H__
#define __ISKILLPROPERTYEFFECTOR_H__

/// Represents the interface of a effector that will update the skill's properties.
class ISkillPropertyEffector 
{
public:

    virtual ~ISkillPropertyEffector() {}

public:

    /// Effects all the imcoming <see cref="IActionSkill"/>s and uses the <see cref="SkillUpgrade"/> as arguments.
    /// <param name="targetSkills">Represents a list of <see cref="IActionSkill"/> that will be update.</param>
    /// <param name="upgrade">Represents the skill upgrade arguments.</param>
    virtual void Effect(vector<IActionSkill*>& targetSkills, SkillUpgrade* upgrade) = 0;
};

#endif //__ISKILLPROPERTYEFFECTOR_H__
