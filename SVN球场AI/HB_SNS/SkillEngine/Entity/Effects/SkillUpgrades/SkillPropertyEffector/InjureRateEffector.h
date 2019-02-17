/********************************************************************************
 * 文件名：InjureRateEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __INJURERATEEFFECTOR_H__
#define __INJURERATEEFFECTOR_H__

/// Represents a effector that will effects the skill's foul rates.
class InjureRateEffector : public ISkillPropertyEffector 
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

            if (actionRawSkill->FoulRelated() == NULL) 
            {
                continue;
            }                

            FoulRelatedSkillPart* foulRelatedSkillPart = PointerCastSafe(FoulRelatedSkillPart, actionRawSkill->FoulRelated());

            foreach (Foul* foul, foulRelatedSkillPart->Fouls()) 
            {
                if (foul->Type() == Defines_FoulLevel.INJURED) 
                {
                    double rate;

                    if (upgrade->FixType() == 0) 
                    {
                        rate = foul->Probability() + upgrade->Rate();
                    }
                    else 
                    {
                        rate = foul->Probability() + foul->Probability() * upgrade->Rate() / 100;
                    }

                    foul->SetProbability(rate);
                }
            }
        }
    }
};

#endif //__INJURERATEEFFECTOR_H__
