/********************************************************************************
 * 文件名：PassiveSkillBuilder.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "PassiveSkillBuilder.h"

PassiveSkillBuilder::PassiveSkillBuilder()
    : SkillBuilder("PassiveSkillBuilderConfig")
{

}

IRawSkill* PassiveSkillBuilder::BuildSkill(string skillId)
{
    try
    {
        LogHelper::Insert(skillId + " begin initializing...");

        if (m_Builders.size() == 0)
        {
            throw ObjectNotInitializedException("builder not initialized. builder name:" + TypeId(PassiveSkillBuilder));
        }

        xml_node& skillElement = singleton_default<PassiveSkillConfigCache>::instance().GetSkill(skillId);

        if (skillElement == NULL)
        {
            format builder("Can't find the skill from the config file. Skill Id: %s");
            builder % skillId;
            throw ConfigurationErrorsException(builder.str());
        }

        xml_attribute& skillName    = skillElement.attribute("name");
        xml_attribute& last         = skillElement.attribute("last");
        xml_attribute& formation    = skillElement.attribute("formation");

        if (skillName == NULL)
        {
            format builder("Can't find the skill's name from the config file. Skill Id: %s");
  
            builder % skillId;

            throw ConfigurationErrorsException(builder.str());
        }

        if (last == NULL)
        {
            format builder("Can't find the skill's last from the config file. Skill Id: %s");
    
            builder % skillId;
            
            throw ConfigurationErrorsException(builder.str());
        }

        if (formation == NULL)
        {
            format builder("Can't find the skill's formation from the config file. Skill Id: %s");
   
            builder % skillId;

            throw ConfigurationErrorsException(builder.str());
        }

        vector<int>& formationIds = Common::TransToValueVector(formation.value(), ",");

        PassiveRawSkill* skill = new PassiveRawSkill(skillId, skillName.value(), lexical_cast<int>(last.value()), formationIds);

        foreach (MapString_ISkillPartBuilder::value_type& builder, m_Builders)
        {
            xml_node& partElement = singleton_default<PassiveSkillConfigCache>::instance().GetSkillPart(builder.first, skillElement);
            
            if (partElement == NULL)
            {
                continue;
            }

            ISkillPart* skillPart = builder.second->Build(partElement);

            // 设置PassiveRawSkill对象到ManagerChangesSkillPart中
            AttatchToManagerChangesSkillPart(partElement, skill);

            skill->SetSkillPart(builder.first, skillPart);

            LogHelper::Insert(builder.first + "Skill part has been created.");
        }

        LogHelper::Insert(skillId + " has initialized.");

        return skill;
    }
    catch (MyException& ex)
    {
        throw SkillErrorException("Invokes the PassiveSkillBuilder class throw exceptins. Skill Id:" + skillId, ex);
    }
}

void PassiveSkillBuilder::AttatchToManagerChangesSkillPart(xml_node& node, IManagerSkill* skill, ISkillPart* skillPart)
{
    string builderName = erase_last_copy(TypeId(ManagerChangesBuilder), "Builder");

    if (builderName == node.name())
    {
        PointerCastSafe(ManagerChangesSkillPart, skillPart)->SetSkillReference(skill);
    }
}

