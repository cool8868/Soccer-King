/********************************************************************************
 * 文件名：FoulRelatedBuilder.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "FoulRelatedBuilder.h"

/// Build a skill's part with the <see cref="XElement"/>.
ISkillPart* FoulRelatedBuilder::Build(xml_node& xelement) 
{
    FoulRelatedSkillPart* skillPart = new FoulRelatedSkillPart();

    for (xml_node_iterator it = xelement.begin(); it != xelement.end(); ++it)
    {
        if (it->name() == "Foul")
        {
            skillPart->Fouls().push_back(CreateFoul(*it));
        }
        else if(it->name() == "FoulChange")
        {
            skillPart->FoulChanges().push_back(CreateFoulChange(*it));
        }
    }

    return skillPart;
}

Foul* FoulRelatedBuilder::CreateFoul(xml_node& xelement) 
{
    if (!xelement)
    {
        return NULL;
    }

    string type             = it->attribute("type").value();
    string target           = it->attribute("target") .value();
    string probability      = it->attribute("probability") .value();

    Foul* entity            = new Foul();

    entity->SetType(lexical_cast<int>(type));
    entity->SetTarget(lexical_cast<int>(target));
    entity->SetProbability(lexical_cast<double>(probability));

    LogHelper.Insert("Create the " + TypeId(Foul) + "'s record.", LogType_Debug);

    return entity;
}

FoulChange* FoulRelatedBuilder::CreateFoulChange(xml_node& xelement) 
{
    if (!xelement)
    {
        return NULL;
    }

    string type             = it->attribute("type").value();
    string target           = it->attribute("target") .value();
    string probability      = it->attribute("probability") .value();

    FoulChange* entity      = new FoulChange();

    // 暂无

    LogHelper.Insert("Create the " + TypeId(FoulChange) + "'s record.", LogType_Debug);

    return entity;
}
