/********************************************************************************
 * 文件名：PassiveFacade.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
void PassiveFacade::Initialize()
{
    PassiveSkillCache::instance().Clear();
    singleton_default<PassiveSkillBuilder>::instance().Initialize();

    vector<xml_node>& skills = singleton_default<PassiveSkillConfigCache>::instance().GetAllSkills();
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
            IRawSkill* actionSkill  = singleton_default<PassiveSkillBuilder>::instance().BuildSkill(skillId);

            PassiveSkillCache::Instance()->Insert(skillId, actionSkill);
            succ++;
        }
        catch (SkillErrorException& ex)
        {
            LogHelper::Insert(&ex, "Initialize PassiveSkill throw exceptions.", LogType_Error);
        }
    }

    LogHelper::Insert(lexical_cast<string>(succ) + "/" + lexical_cast<string>(skills.size()) + " opener skills has loaded.", LogType_Info);
}

IRawSkill* PassiveFacade::GetSkill(string skillId)
{
    try
    {
        IRawSkill* skill = PassiveSkillCache::Instance()->GetSkill(skillId);

        return skill.Clone() as IRawSkill;
    }
    catch (MyException& ex)
    {
        throw SkillErrorException("PassiveFacade class's GetSkill method causes exception.", ex);
    }
}

void PassiveFacade::Effect(IPlayer* player, IRawSkill* skill)
{
    try
    {
        IPassiveRawSkill* passiveSkill = PointerCastSafe(IPassiveRawSkill, skill);
        if (passiveSkill == NULL)
        {
            throw ArgumentException("Invokes the PassiveFacade class's Effect method with the error type argument. Expected:IPassiveRawSkill, actual:" + skill->Type());
        }

        singleton_default<SkillEffector>::instance().Effect(player, passiveSkill);
    }
    catch (MyException& ex)
    {
        throw SkillErrorException("PassiveFacade class's Effect method causes exception.", ex);
    }
}

