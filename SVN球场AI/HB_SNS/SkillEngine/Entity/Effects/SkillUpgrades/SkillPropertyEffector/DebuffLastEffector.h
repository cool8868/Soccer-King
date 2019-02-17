/********************************************************************************
 * 文件名：DebuffLastEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DEBUFFLASTEFFECTOR_H__
#define __DEBUFFLASTEFFECTOR_H__

/// Represents a effector that will effects the debuff's lasting time.
class DebuffLastEffector : public ISkillPropertyEffector 
{
public:

    /// Effects all the imcoming <see cref="IActionSkill"/>s and uses the <see cref="SkillUpgrade"/> as arguments.
    /// <param name="targetSkills">Represents a list of <see cref="IActionSkill"/> that will be update.</param>
    /// <param name="upgrade">Represents the skill upgrade arguments.</param>
    void Effect(vector<IActionSkill*>& targetSkills, SkillUpgrade* upgrade) 
    {
        foreach (IActionSkill* skill, targetSkills) 
        {
            IActionRawSkill* actionRawSkill = PointerCastSafe(IActionRawSkill, skill->GetRawSkill());

            if (actionRawSkill->PropertyChanges() != NULL) 
            {
                PropertyChangesSkillPart* propertyChangesSkillPart = PointerCastSafe(PropertyChangesSkillPart, actionRawSkill->PropertyChanges());

                foreach (PropertyChange* propertyChange, propertyChangesSkillPart->PropertyChanges()) 
                {
                    if (MATH::FloatEqual(propertyChange->Gradient(), 0.0) == false) 
                    {
                        // dot type
                        if (propertyChange->Gradient() > 0.0) 
                        {
                            continue;
                        }
                    }
                    else 
                    {  
                        // buff type
                        if (propertyChange->Rate() > 0.0) 
                        {
                            continue;
                        }
                    }

                    short rate = (upgrade->FixType() == 0) ? static_cast<short>(propertyChange->Last() + upgrade->Rate()) : static_cast<short>(propertyChange->Last() + propertyChange->Last() * upgrade->Rate() / 100);
                    propertyChange->SetLast(rate);
                }
            }
        }
    }
};

#endif //__DEBUFFLASTEFFECTOR_H__

