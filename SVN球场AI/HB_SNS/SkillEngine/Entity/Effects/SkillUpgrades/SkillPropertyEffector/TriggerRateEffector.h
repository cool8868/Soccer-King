/********************************************************************************
 * 文件名：TriggerRateEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __TRIGGERRATEEFFECTOR_H__
#define __TRIGGERRATEEFFECTOR_H__

/// Represents a effector that will upgrade the skill's trigger rate.
class TriggerRateEffector : public ISkillPropertyEffector
{
public:

    /// Effects the upgrade.
    /// <param name="targetSkills">Represents the target skills.</param>
    /// <param name="upgrade">Represents the skill upgrade parameters.</param>
    void Effect(vector<IActionSkill*>& targetSkills, SkillUpgrade* upgrade)
    {
        foreach (IActionSkill* skill, targetSkills)
        {
            IActionRawSkill* action     = PointerCastSafe(IActionRawSkill, skill->GetRawSkill());
            TriggerSkillPart* part      = PointerCastSafe(TriggerSkillPart, action->TriggerConditions());
            PercentageTrigger* trigger  = NULL;

            foreach (ITrigger* t, part->Triggers())
            {
                if (Is(t, PercentageTrigger))
                {
                    PercentageTrigger* trigger = PointerCastSafe(PercentageTrigger, t);
                    break;
                }
            }

            if (trigger != NULL)
            {
                double rate = 0.0;

                if (upgrade->FixType() == 0)
                {
                    rate = trigger->Probability() + upgrade->Rate();
                }
                else
                {
                    rate = trigger->Probability() + trigger->Probability() * upgrade->Rate() / 100;
                }

                trigger->SetProbability((int)rate);
            }
        }
    }
};

#endif //__TRIGGERRATEEFFECTOR_H__

