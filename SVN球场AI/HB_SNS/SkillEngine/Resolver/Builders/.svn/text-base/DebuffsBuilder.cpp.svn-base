/********************************************************************************
 * 文件名：DebuffsBuilder.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "DebuffsBuilder.h"

/// Build a skill's part with the <see cref="XElement"/>.
ISkillPart* DebuffsBuilder::Build(xml_node& xelement) 
{
    if (!xelement)
    {
        return NULL;
    }

    DebuffsSkillPart* skillPart = new DebuffsSkillPart();

    for (xml_node_iterator it = xelement.begin(); it != xelement.end(); ++it)
    {
        if (it->name() == "Debuff")
        {
            skillPart->BallRelateds().push_back(CreateDebuff(*it));
        }
    }

    return skillPart;
}

Debuff* DebuffsBuilder::CreateDebuff(xml_node& xelement) 
{
    if (!xelement)
    {
        return NULL;
    }

    string type                     = it->attribute("type").value();
    string level                    = it->attribute("level") .value();
    string target                   = it->attribute("target").value();
    string targetPosition           = it->attribute("targetPosition") .value();
    string probability              = it->attribute("probability") .value();

    Debuff* entity                  = new Debuff();

    entity->SetType(lexical_cast<int>(type));
    entity->SetLevel(lexical_cast<int>(level));
    entity->SetTarget(lexical_cast<int>(target));
    entity->SetTargetPosition(targetPosition);
    entity->SetProbability(lexical_cast<double>(probability));

    LogHelper.Insert("Create the " + TypeId(Debuff) + "'s record.", LogType_Debug);

    return entity;
}

