/********************************************************************************
 * 文件名：ModelsBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ModelsBuilder.h"

ISkillPart* ModelsBuilder::Build(xml_node& xelement) 
{
    if (!xelement)
    {
        return NULL;
    }

    ModelsSkillPart* skillPart = new ModelsSkillPart();
    
    for (xml_node_iterator it = xelement.begin(); it != xelement.end(); ++it)
    {
        if (it->name() == "Model")
        {
            skillPart->Models().push_back(CreateModelRecord(*it));
        }
    }

    return skillPart;
}

ModelRecord* ModelsBuilder::CreateModelRecord(xml_node& xelement)
{
    if (!xelement)
    {
        return NULL;
    }

    string mid              = it->attribute("mid").value();
    string last             = it->attribute("last") .value();
    string target           = it->attribute("target") .value();
    string targetPosition   = it->attribute("targetPosition") .value();

    ModelRecord* entity = new ModelRecord();

    entity->SetMid(lexical_cast<int>(mid));
    entity->SetLast(lexical_cast<short>(last));
    entity->SetTarget(lexical_cast<int>(target));
    entity->SetTargetPosition(targetPosition);

    LogHelper.Insert("Create the " + TypeId(ModelRecord) + "'s record.", LogType_Debug);

    return entity;
}
