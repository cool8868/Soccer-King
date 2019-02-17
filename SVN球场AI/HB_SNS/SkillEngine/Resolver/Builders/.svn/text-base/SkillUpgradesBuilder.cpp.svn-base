/********************************************************************************
 * 文件名：SkillUpgradesBuilder.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "SkillUpgradesBuilder.h"

ISkillPart* SkillUpgradesBuilder::Build(xml_node& xelement) 
{
    if (!xelement)
    {
        return NULL;
    }

    SkillUpgradesSkillPart* skillPart = new SkillUpgradesSkillPart();

    for (xml_node_iterator it = xelement.begin(); it != xelement.end(); ++it)
    {
        if (it->name() == "SkillUpgrade")
        {
            skillPart->SkillUpgrades().push_back(CreateSkillUpgrade(*it));
        }
    }

    return skillPart;
}  

SkillUpgrade* SkillUpgradesBuilder::CreateSkillUpgrade(xml_node& xelement)
{
    if (!xelement)
    {
        return NULL;
    }

    //<SkillUpgrade type="3" skillType="*" target="3" targetPosition="0" targetSkillProperty="6" rate="15" fixType="1" />
    string type                 = it->attribute("type").value();
    string skillType            = it->attribute("skillType") .value();
    string target               = it->attribute("target").value();
    string targetPosition       = it->attribute("targetPosition") .value();
    string targetSkillProperty  = it->attribute("targetSkillProperty").value();
    string rate                 = it->attribute("rate") .value();
    string fixType              = it->attribute("fixType").value();

    SkillUpgrade* entity    = new SkillUpgrade();

    entity->SetType(lexical_cast<int>(type));
    entity->SetSkillType(skillType);
    entity->SetTarget(lexical_cast<int>(target));
    entity->SetTargetPosition(targetPosition);
    entity->SetRate(lexical_cast<double>(rate));
    entity->SetFixType(lexical_cast<int>(fixType));

    LogHelper.Insert("Create the " + TypeId(SkillUpgrade) + "'s record.", LogType_Debug);

    return entity;
}
