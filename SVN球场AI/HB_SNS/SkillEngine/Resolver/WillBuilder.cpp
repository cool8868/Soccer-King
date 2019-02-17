/********************************************************************************
 * 文件名：WillBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "WillBuilder.h"
#include "../../Exception/ObjectNotInitializedException.h"
#include "../../Exception/ConfigurationErrorsException.h"

#include "../Cache/ConfigCache/WillConfigCache.h"

WillBuilder::WillBuilder()
    : SkillBuilder("WillBuilderConfig")
{

}

IRawSkill* WillBuilder::BuildSkill(string skillId)
{
    try
    {
        LogHelper::Insert(skillId + " begin initializing...");

        // 没有任何构建的对象
        if (m_Builders.size() == 0)
        {
            throw ObjectNotInitializedException("builder not initialized. builder name:" + TypeId(WillBuilder));
        }

        xml_node& willElement = singleton_default<WillConfigCache>::instance().GetWill(skillId);
   
        if (!willElement)
        {
            throw ConfigurationErrorsException("Can't finds the will from the config file. id:" + skillId);
        }

        xml_attribute& nameAttribute = willElement.attribute("name");
  
        if (!nameAttribute)
        {
            throw ConfigurationErrorsException("Can't finds the will's name from the config file. id:" + skillId);
        }

        WillRawSkill will = new WillRawSkill(skillId, nameAttribute.value());
        
        foreach (MapString_ISkillPartBuilder::value_type& builder, m_Builders)
        {
            xml_node& partElement = singleton_default<WillConfigCache>::instance().GetWillPart(builder.first, willElement);
  
            if (!partElement)
            {
                continue;
            }

            ISkillPart* part = builder.second->Build(partElement);

            // 将创建的对象设置到对应的指针变量中
            will->SetSkillPart(builder.first, part);

            LogHelper::Insert(builder.first + "Will part has been created.");
        }

        LogHelper::Insert(skillId + " has initialized.");

        return will;
    }
    catch (MyException& ex)
    {
        throw SkillErrorException("Builds will causes exception.", ex);
    }
}

