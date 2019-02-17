/********************************************************************************
 * 文件名：ImmunityBuilder.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ImmunityBuilder.h"

ISkillPart* ImmunityBuilder::Build(xml_node& xelement) 
{
    if (!xelement)
    {
        return NULL;
    }

    ImmunitySkillPart* skillPart = new ImmunitySkillPart();

    for (xml_node_iterator it = xelement.begin(); it != xelement.end(); ++it)
    {
        if (it->name() == "Immunity")
        {
            skillPart->Immunities().push_back(CreateImmunity(*it));
        }
    }

    return skillPart;
}  

Immunity* ImmunityBuilder::CreateImmunity(xml_node& xelement)
{
    if (!xelement)
    {
        return NULL;
    }

    string type             = it->attribute("type").value();
    string rate             = it->attribute("rate") .value();
    string probability      = it->attribute("probability") .value();

    Immunity* entity        = new Immunity();

    entity->SetType(lexical_cast<int>(type));
    entity->SetRate(lexical_cast<double>(rate));
    entity->SetProbability(lexical_cast<int>(probability));

    LogHelper.Insert("Create the " + TypeId(Immunity) + "'s record.", LogType_Debug);

    return entity;
}

