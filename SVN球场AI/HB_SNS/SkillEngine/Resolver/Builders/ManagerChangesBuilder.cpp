/********************************************************************************
 * 文件名：ManagerChangesBuilder.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ManagerChangesBuilder.h"

ISkillPart* ManagerChangesBuilder::Build(xml_node& xelement) 
{
    if (!xelement)
    {
        return NULL;
    }

    ManagerChangesSkillPart* skillPart  = new ManagerChangesSkillPart();

    for (xml_node_iterator it = xelement.begin(); it != xelement.end(); ++it)
    {
        if (it->name() == "PropertyChange")
        {
            skillPart->ManagerPropertyChanges().push_back(CreateManagerPropertyChange(*it));
        }
    }

    return skillPart;
}

ManagerPropertyChange* ManagerChangesBuilder::CreateManagerPropertyChange(xml_node& xelement)
{
    if (!xelement)
    {
        return NULL;
    }

    string property         = it->attribute("property").value();
    string rate             = it->attribute("rate") .value();
    string gradient         = it->attribute("gradient").value();
    string target           = it->attribute("target") .value();
    string targetPosition   = it->attribute("targetPosition").value();

    ManagerPropertyChange* entity = new ManagerPropertyChange();

    entity->SetProperty(property);
    entity->SetRate(lexical_cast<double>(rate));
    entity->SetGradient(lexical_cast<double>(gradient));
    entity->SetTarget(lexical_cast<int>(target));
    entity->SetTargetPosition(targetPosition);

    LogHelper.Insert("Create the " + TypeId(ManagerPropertyChange) + "'s record.", LogType_Debug);

    return entity;
}

