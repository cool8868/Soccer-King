/********************************************************************************
 * 文件名：ActionSkillBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ActionSkillBuilder.h"
#include "../../Log/LogHelper.h"
#include "../../Exception/ObjectNotInitializedException.h"
#include "../../Exception/ConfigurationErrorsException.h"
#include "../../Exception/SkillErrorException.h"
#include "../Cache/ConfigCache/ActionSkillConfigCache.h"
#include "../Entity/ActionRawSkill.h"

ActionSkillBuilder::ActionSkillBuilder()
    : SkillBuilder("ActionSkillBuilderConfig") 
{

}

IRawSkill* ActionSkillBuilder::BuildSkill(string skillId) 
{
    try 
    {
        LogHelper::Insert(skillId + " begin initializing...");

        if (m_Builders.size() == 0) 
        {
            throw ObjectNotInitializedException("builder not initialized. builder name:" + TypeId(ActionSkillBuilder));
        }

        xml_node& skillElement = singleton_default<ActionSkillConfigCache>::instance().GetSkill(skillId);
        
        if (skillElement == NULL) 
        {
            format builder("Can't find the skill from the config file. Skill Id: %s");

            builder % skillId;

            throw ConfigurationErrorsException(builder.str());
        }

        xml_attribute& skillName = skillElement.attribute("skillName");
        xml_attribute& cd = skillElement.attribute("cd");
        xml_attribute& cls = skillElement.attribute("type");

        if (skillName == NULL)
        {
            format builder("Can't find the skill's name from the config file. Skill Id: %s");

            builder % skillId;

            throw ConfigurationErrorsException(builder.str());
        }

        if (cd == NULL) 
        {
            format builder("Can't find the skill's cd from the config file. Skill Id: %s");

            builder % skillId;
            
            throw ConfigurationErrorsException(builder.str());
        }

        if (cls == NULL) 
        {
            format builder("Can't find the skill's type(class) from the config file. Skill Id: %s");

            builder % skillId;

            throw ConfigurationErrorsException(builder.str());
        }

        ActionRawSkill* skill = new ActionRawSkill(skillId, skillName.value(), lexical_cast<int>(cd.value()));

        foreach (MapString_ISkillPartBuilder::value_type& builder, m_Builders) 
        {
            xml_node& partElement = singleton_default<ActionSkillConfigCache>::instance().GetSkillPart(builder.first, skillElement);

            if (partElement == NULL) 
            {
                continue;
            }

            ISkillPart* skillPart = builder.second->Build(partElement);

            skill->SetSkillPart(builder.first, skillPart);

            LogHelper::Insert(builder.first + "Skill part has been created.");
        }

        LogHelper::Insert(skillId + " has initialized.");

        // 防止出现二义性，明确指定是哪个基类的指针对象
        return PointerCastSafe(RawSkill, skill);
    }
    catch (MyException& ex) 
    {
        throw SkillErrorException("Invokes the ActionSkillBuilder class's Build method throw exceptions. Skill Id:" + skillId, ex);
    }
}