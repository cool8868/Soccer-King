/********************************************************************************
 * 文件名：WillTriggerFactory.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "WillTriggerFactory.h"

ISkillPart* WillTriggerFactory::Build(xml_node& element)
{
    const string sectionName = "WillTriggerMapping";
    xml_node& section = singleton_default<ConfigurationManager>::instance().GetSection(sectionName);

    if (!section)
    {
        throw ConfigurationErrorsException("Can't find the config section. section name:" + sectionName);
    }

    //是否存在这个属性
    if (element.attribute("type") == false)
    {
        throw ConfigurationErrorsException("Trigger conditions missing the type attribute.");
    }

    string triggerName = Common::GetAttribute(section, "key", element.attribute("type").value(), "value");

    //创建实例对象
    ITrigger* trigger = CreateInstance(trim_copy(triggerName));

    if (trigger == NULL)
    {
        // eg:不能找到 key:1 对应的类"FirstHalfTrigger"
        throw TriggerFactoryException("Can't find the trigger. trigger id:" + element.attribute("type").value());
    }

    //设置对应的属性
    trigger->SetAttribute(element);

    return trigger;
}

ITrigger* WillTriggerFactory::CreateInstance(string name)
{
    if (name.empty())
    {
        return NULL;
    }

    ITrigger* trigger = NULL;

    //TriggerMapping对应的集中类型结构
    if (name == TypeId(WillActionTrigger))  
    {
        // 0
        trigger = new WillActionTrigger();
    }
    else if (name == TypeId(WillOpenballTrigger))
    {
        // 1
        trigger = new WillOpenballTrigger();
    }
    else if (name == TypeId(WillAreaTrigger))
    {
        // 2
        trigger = new WillAreaTrigger();
    }
    else if (name == TypeId(WillAreaNumTrigger))
    {
        // 3
        trigger = new WillAreaNumTrigger();
    }
    else if (name == TypeId(WillDisableNumTrigger))
    {
        // 4
        trigger = new WillDisableNumTrigger();
    }

    return trigger;
}
