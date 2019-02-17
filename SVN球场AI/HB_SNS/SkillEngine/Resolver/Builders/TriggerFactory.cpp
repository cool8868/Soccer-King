/********************************************************************************
 * 文件名：TriggerFactory.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "TriggerFactory.h"
#include "../../Cache/ConfigCache/ConfigurationManager.h"
#include "../../../Exception/ConfigurationErrorsException.h"
#include "../../../common/Utility.h"
#include "../../Entity/Triggers/FirstHalfTrigger.h"
#include "../../Entity/Triggers/SecondHalfTrigger.h"
#include "../../Entity/Triggers/StateTrigger.h"
#include "../../Entity/Triggers/PercentageTrigger.h"
#include "../../Entity/Triggers/TargetTrigger.h"
#include "../../Entity/Triggers/HoldBallTrigger.h"

ISkillPart* TriggerFactory::Build(xml_node& element)
{
    const string sectionName = "TriggerMapping";
    xml_node& section = singleton_default<ConfigurationManager>::instance().GetSection(sectionName);

    if (section == NULL)
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
        throw TriggerFactoryException(string("Can't find the trigger. trigger id:") + element.attribute("type").value());
    }

    // 设置对应的属性
    trigger->SetAttribute(element);

    return trigger;
}

ITrigger* TriggerFactory::CreateInstance(string name)
{
    if (name.empty())
    {
        return NULL;
    }

    ITrigger* trigger = NULL;

    //TriggerMapping对应的集中类型结构
    if (name == TypeId(FirstHalfTrigger))
    {
        // 0
        trigger = new FirstHalfTrigger();
    }
    else if (name == TypeId(SecondHalfTrigger))
    {
        // 1
        trigger = new SecondHalfTrigger();
    }
    else if (name == TypeId(StateTrigger))
    {
        // 2
        trigger = new StateTrigger();
    }
    else if (name == TypeId(TargetTrigger))
    {
        // 3
        trigger = new TargetTrigger();
    }
    else if (name == TypeId(PercentageTrigger))
    {
        // 4
        trigger = new PercentageTrigger();
    }
    else if (name == TypeId(HoldBallTrigger))
    {
        // 5
        trigger = new HoldBallTrigger();
    }

    return trigger;
}
