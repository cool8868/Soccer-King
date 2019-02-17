/********************************************************************************
 * 文件名：CdEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __CDEFFECTOR_H__
#define __CDEFFECTOR_H__

/// Represents a effector that will effect cool down.
class CdEffector : public ISkillPropertyEffector 
{
public:

    /// Effects all the imcoming <see cref="IActionSkill"/>s and uses the <see cref="SkillUpgrade"/> as arguments.
    /// <param name="targetSkills">Represents a list of <see cref="IActionSkill"/> that will be update.</param>
    /// <param name="upgrade">Represents the skill upgrade arguments.</param>
    void Effect(vector<IActionSkill*> targetSkills, SkillUpgrade* upgrade) 
    {
        foreach (IActionSkill* skill, targetSkills) 
        {
            int rate;

            if (upgrade->FixType() == 0) 
            {
                rate = skill->MaxCoolDown() + (int)upgrade->Rate();
            }
            else 
            {
                rate = static_cast<int>(skill->MaxCoolDown + skill->MaxCoolDown() * upgrade->Rate() / 100);
            }

            skill->UpdateMaxCoolDown(rate);
        }
    }        
};

#endif //__CDEFFECTOR_H__

