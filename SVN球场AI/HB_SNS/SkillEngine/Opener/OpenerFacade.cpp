/********************************************************************************
 * 文件名：OpenerFacade.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "OpenerFacade.h"
#include "../Cache/OpenerSkillCache.h"
#include "../Resolver/OpenerSkillBuilder.h"

void OpenerFacade::Initialize()
{
    OpenerSkillCache::Instance()->Clear();
    singleton_default<OpenerSkillBuilder>::instance().Initialize();

    vector<xml_node>& skills = singleton_default<OpenerSkillConfigCache>::instance().GetAllSkills();
    int succ = 0;
    foreach (xml_node& skill, skills)
    {
        try
        {
            if (skill == NULL)
            {
                continue;
            }
            string skillId = skill.attribute("id").value();
            IRawSkill* openerSkill = singleton_default<OpenerSkillBuilder>::instance().BuildSkill(skillId);

            OpenerSkillCache::Instance()->Insert(skillId, openerSkill);
            succ++;
        }
        catch (SkillErrorException& ex)
        {
            LogHelper::Insert(&ex, "Initialize OpenerSkill throw exceptions.", LogType_Error);
        }
    }

    LogHelper::Insert(lexical_cast<string>(succ) + "/" + lexical_cast<string>(skills.size()) + " opener skills has loaded.", LogType_Info);
}

IRawSkill* OpenerFacade::GetSkill(string skillId)
{
    try
    {
        IRawSkill* opener = OpenerSkillCache::Instance().GetSkill(skillId);

        return opener.Clone();
    }
    catch (MyException& ex)
    {
        throw SkillErrorException("OpenerFacade class's GetSkill method causes exception.", ex);
    }
}

void OpenerFacade::Effect(IPlayer* player, IRawSkill* skill)
{
    try
    {
        IOpenerRawSkill* actionSkill = PointerCastSafe(IOpenerRawSkill, skill);
        if (actionSkill == NULL)
        {
            throw ArgumentException("Invokes the OpenerFacade class's Effect method with the error type argument. Expected:IOpenerRawSkill, actual:" + skill->Id());
        }

        singleton_default<SkillEffector>::instance().Effect(player, actionSkill);
    }
    catch (MyException& ex)
    {
        throw SkillErrorException("OpenerFacade class's Effect method causes exception.", ex);
    }
}
