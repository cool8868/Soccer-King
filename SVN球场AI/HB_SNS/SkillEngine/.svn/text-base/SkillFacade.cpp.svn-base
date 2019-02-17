/********************************************************************************
 * 文件名：SkillFacade.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "SkillFacade.h"
#include "Action/ActionFacade.h"
#include "Opener/OpenerFacade.h"
#include "Passive/PassiveFacade.h"
#include "Will/WillFacade.h"

#include "../Exception/SkillErrorException.h"
#include "../Log/LogHelper.h"
#include "../Interface/Skill/IActionSkill.h"

SkillFacade* SkillFacade::Instance()
{
    static SkillFacade instance;

    return &instance;
}

void SkillFacade::Initialize()
{
    singleton_default<ActionFacade>::instance().Initialize();
    singleton_default<OpenerFacade>::instance().Initialize();
    singleton_default<PassiveFacade>::instance().Initialize();
    singleton_default<WillFacade>::instance().Initialize();
}

//Action Skills
IRawSkill* SkillFacade::GetActionSkill(string skillId)
{
    try
    {
        return singleton_default<ActionFacade>::instance().GetSkill(skillId);
    }
    catch (SkillErrorException& ex)
    {
        LogHelper::Insert(&ex, "Get action type skill cause exception. SkillId:" + skillId, LogType_Error);
        return NULL;
    }
}

bool SkillFacade::IsActionSkillTriggered(IPlayer* player, IActionSkill* skill)
{
    ActionRawSkill* rawSkill = PointerCastSafe(ActionRawSkill, skill->GetRawSkill());
   
    return singleton_default<ActionFacade>::instance().IsSkillTrigger(player, rawSkill);
}

void SkillFacade::ActionSkillEffect(IPlayer* player, IActionSkill* skill)
{
    singleton_default<ActionFacade>::instance().Effect(player, skill->GetRawSkill());

    try
    {
        singleton_default<ActionFacade>::instance().Effect(player, skill->GetRawSkill());
    }
    catch (SkillErrorException& ex)
    {
        LogHelper::Insert(&ex, "Invokes SkillFacade class's ActionSkillEffect method causes exceptions. SkillId:" + skill->SkillId());
    }
}

vector<IPlayer*> SkillFacade::GetActionSkillTargets(IPlayer* player, IActionRawSkill* skill)
{
    return singleton_default<ActionFacade>::instance().GetSkillTargets(player, skill);
}

//Opener Skills
IRawSkill* SkillFacade::GetOpenerSkill(string skillId)
{
    try
    {
        return singleton_default<OpenerFacade>::instance().GetSkill(skillId);
    }
    catch (SkillErrorException& ex)
    {
        LogHelper::Insert(&ex, "Get opener type skill cause exception. SkillId:" + skillId, LogType_Error);
        return NULL;
    }
}

void SkillFacade::OpenerSkillEffect(IPlayer* player, IOpenerSkill* skill)
{
    try
    {
        singleton_default<OpenerFacade>::instance().Effect(player, skill->GetRawSkill());
    }
    catch (SkillErrorException& ex)
    {
        LogHelper::Insert(&ex, "Invokes SkillFacade class's OpenerSkillEffect method causes exceptions. SkillId:" + skill->SkillId());
    }
}

//Passive Skills
void SkillFacade::PassiveSkillEffect(IPlayer* player, string skillId)
{
    try
    {
        IRawSkill* rawSkill = singleton_default<PassiveFacade>::instance().GetSkill(skillId);
        if (rawSkill != NULL)
        {
            singleton_default<PassiveFacade>::instance().Effect(player, rawSkill);
        }
    }
    catch (SkillErrorException& ex)
    {
        LogHelper::Insert(&ex, "Invokes SkillFacade class's PassiveSkillEffect method causes exceptions. SkillId:" + skillId);
    }
}

//Will Skills
IWillRawSkill* SkillFacade::GetWillSkill(string id)
{
    try
    {
        return PointerCastSafe(IWillRawSkill, singleton_default<WillFacade>::instance().GetWill(id));
    }
    catch (SkillErrorException& ex)
    {
        LogHelper::Insert(&ex, "Get will cause exception. SkillId:" + id, LogType_Error);
        return NULL;
    }
}

bool SkillFacade::IsWillTriggered(IPlayer* player, IWillRawSkill* will)
{
    try
    {
        return singleton_default<WillFacade>::instance().IsTriggered(player, will);
    }
    catch (SkillErrorException& ex)
    {
        LogHelper::Insert(&ex, "Validates will triggered cause exception. SkillId:" + will->Id(), LogType_Error);
        return false;
    }
}

void SkillFacade::WillEffect(IPlayer* player, IWillRawSkill* will)
{
    try
    {
        singleton_default<WillFacade>::instance().IsTriggered(player, will);
    }
    catch (SkillErrorException& ex)
    {
        LogHelper::Insert(&ex, "Will effects cause exception. SkillId:" + will->Id(), LogType_Error);
    }
}
