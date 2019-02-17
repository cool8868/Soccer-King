/********************************************************************************
 * 文件名：DebuffStatusEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DEBUFFSTATUSEFFECTOR_H__
#define __DEBUFFSTATUSEFFECTOR_H__

/// Represents a effector that will effects the skill's debuff part.
class DebuffStatusEffector : public ISkillPropertyEffector 
{
public:

    /// Effects all the imcoming <see cref="IActionSkill"/>s and uses the <see cref="SkillUpgrade"/> as arguments.
    /// <param name="targetSkills">Represents a list of <see cref="IActionSkill"/> that will be update.</param>
    /// <param name="upgrade">Represents the skill upgrade arguments.</param>
    void Effect(vector<IActionSkill*> targetSkills, SkillUpgrade* upgrade) 
    {
        foreach (IActionSkill* skill, targetSkills) 
        {
            IActionRawSkill* actionRawSkill = PointerCastSafe(IActionRawSkill, skill->GetRawSkill());

            if (actionRawSkill->Debuffs() == NULL) 
            {
                continue;
            }

            DebuffsSkillPart* debuffSkillPart = PointerCastSafe(DebuffsSkillPart, actionRawSkill->Debuffs());

            foreach (int debuffId, upgrade->TargetDebuff()) 
            {
                foreach (Debuff* d, debuffSkillPart->Debuffs()) 
                {
                    if (d->Type() == debuffId) 
                    {
                        double rate = (upgrade->FixType() == 0) ? d->Probability() + upgrade->Rate() : d->Probability() + d->Probability() * upgrade->Rate() / 100;
                        d->SetProbability(rate);
                    }
                }
            }
        }
    }
};

#endif //__DEBUFFSTATUSEFFECTOR_H__
