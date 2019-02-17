/********************************************************************************
 * 文件名：ActionSkill.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ActionSkill.h"

ActionSkill::ActionSkill(IRawSkill* rawSkill, int totalRound) 
{
    InitVariables();

    m_RawSkill = PointerCastSafe(IActionRawSkill, rawSkill);

    if (m_RawSkill == NULL)
    {
        LogHelper::Insert("Create a action skill with null referenced raw skill.");
        return;
    }

    TriggerSkillPart* triggerPart = PointerCastSafe(TriggerSkillPart, m_RawSkill->TriggerConditions());

    if (triggerPart == NULL)
    {
        LogHelper::Insert("Create a action skill cause error becaulse the triggerConditions is not TriggerSkillPart. SkillId:" + rawSkill->Id(), LogType_Error);
        return;
    }

    foreach (ITrigger* trigger, triggerPart->Triggers()) 
    {
        StateTrigger* stateTrigger = PointerCastSafe(StateTrigger, trigger);

        if (stateTrigger != NULL) 
        {
            foreach (string& state, stateTrigger->TriggerStates()) 
            {
                m_TriggerStates.push_back(StateSelecter::Instance()->GetStateByString(state));
            }
            break;
        }
    }

    m_MaxCoolDown   = Common::ConvertTimeToRound(m_RawSkill->CoolDown(), totalRound);
    m_Class         = m_RawSkill->Class();
}

void ActionSkill::TimeLapse() 
{
    if (m_CoolDown == 0) 
    {
        return;
    }

    m_CoolDown--;
}

vector<IPlayer*> ActionSkill::GetSkillTargets(IPlayer* triggerman) 
{
    return SkillFacade::Instance()->GetActionSkillTargets(triggerman, m_RawSkill);
}

string ActionSkill::GetSkillTargetsToString(IPlayer* triggerman) 
{
    vector<IPlayer*> targets = GetSkillTargets(triggerman);
    string builder;

    foreach (IPlayer* target, targets) 
    {
        builder += lexical_cast<string>(target->ClientId());
        builder += ",";
    }

    if (builder.empty() == false)
    {
        builder = builder.substr(0, builder.length() - 1);
    }

    return builder;
}

/// 更新技能的CD时间
void ActionSkill::UpdateMaxCoolDown(int cd) 
{
    m_MaxCoolDown = cd;

    // +++ tony:球球出现技能问题的地方
    //_coolDown = cd;
}

void ActionSkill::InitVariables()
{
    m_TriggerStates.reserv(4);
    m_TriggerStates.clear();
}
