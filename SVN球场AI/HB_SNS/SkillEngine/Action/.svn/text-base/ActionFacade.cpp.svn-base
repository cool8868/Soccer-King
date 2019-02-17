/********************************************************************************
 * 文件名：ActionFacade.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ActionFacade.h"
#include "Trigger/SkillTrigger.h"
#include "Effect/SkillEffector.h"
#include "../Cache/ActionSkillCache.h"
#include "../Cache/ConfigCache/ActionSkillConfigCache.h"
#include "../Resolver/ActionSkillBuilder.h"

#include "../../Exception/SkillErrorException.h"
#include "../../Exception/ArgumentException.h"


void ActionFacade::Initialize()
{
    ActionSkillCache::Instance()->Clear();
    singleton_default<ActionSkillBuilder>::instance().Initialize();

    vector<xml_node>& skills = singleton_default<ActionSkillConfigCache>::instance().GetAllSkills();

    int succ = 0;
    foreach (xml_node& skill, skills)
    {
        try
        {
            if (skill == NULL)
            {
                continue;
            }

            string skillId          = skill.attribute("id").value();
            IRawSkill* actionSkill  = singleton_default<ActionSkillBuilder>::instance().BuildSkill(skillId);
            ActionSkillCache::Instance()->Insert(skillId, actionSkill);

            succ++;
        }
        catch (SkillErrorException& ex)
        {
            LogHelper::Insert(&ex, "Initialize ActionSkill throw exceptions.", LogType_Error);
        }
    }

    LogHelper::Insert(lexical_cast<string>(succ) + "/" + lexical_cast<string>(skills.size()) + " action skills has loaded.", LogType_Info);
}

IRawSkill* ActionFacade::GetSkill(string skillId)
{
    try
    {
        return ActionSkillCache::Instance()->GetSkill(skillId)->Clone();
    }
    catch (SkillErrorException& ex)
    {
        throw SkillErrorException("ActionFacade class's GetSkill method causes exception.", ex);
    }
}

bool ActionFacade::IsSkillTrigger(IPlayer* player, ActionRawSkill* skill)
{
    return singleton_default<SkillTrigger>::instance().IsSkillTrigger(player, skill);
}

void ActionFacade::Effect(IPlayer* player, IRawSkill* skill)
{
    try
    {
        IActionRawSkill* actionSkill = PointerCastSafe(IActionRawSkill, skill);
        if (actionSkill == NULL)
        {
            throw ArgumentException("Invokes the ActionFacade class's Effect method with the error type argument. Expected:IActionRawSkill, actual:" + skill->Id());
        }

        singleton_default<SkillEffector>::instance().Effect(player, actionSkill);
    }
    catch (MyException& ex)
    {
        throw SkillErrorException("ActionFacade class's Effect method causes exception.", ex);
    }
}

vector<IPlayer*> ActionFacade::GetSkillTargets(IPlayer* player, IActionRawSkill* skill)
{
    vector<IPlayer*> targets(10);
    targets.clear();

    Common::AddRange<IPlayer>(targets, singleton_default<PropertyChangesEffector>::instance().GetSkillTargets(player, skill->PropertyChanges()))
    Common::AddRange<IPlayer>(targets, singleton_default<PropertyChangesEffector>::instance().GetSkillTargets(player, skill->PropertyChanges()));
    Common::AddRange<IPlayer>(targets, singleton_default<DisplacementsEffector>::instance().GetSkillTargets(player, skill->Displacements()));
    Common::AddRange<IPlayer>(targets, singleton_default<FoulRelatedEffector>::instance.GetSkillTargets(player, skill->FoulRelated()));
    Common::AddRange<IPlayer>(targets, singleton_default<DebuffsEffector>::instance.GetSkillTargets(player, skill->Debuffs()));

    Common::Remove<IPlayer>(targets, player);

    vector<IPlayer*> list(targets.size() / 2);
    list.clear();

    foreach (IPlayer* target, targets)
    {
        if (find(list.begin(), list.end(), target) == list.end())
        {
            list.push_back(target);
        }
    }

    return list;
}
