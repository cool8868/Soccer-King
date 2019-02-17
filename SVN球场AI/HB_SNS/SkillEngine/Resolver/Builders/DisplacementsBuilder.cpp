/********************************************************************************
 * 文件名：DisplacementsBuilder.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "DisplacementsBuilder.h"

/// Build a skill's part with the <see cref="XElement"/>.
ISkillPart* DisplacementsBuilder::Build(xml_node& xelement) 
{
    if (!xelement)
    {
        return NULL;
    }

    DisplacementsSkillPart* skillPart = new DisplacementsSkillPart();

    for (xml_node_iterator it = xelement.begin(); it != xelement.end(); ++it)
    {
        if (it->name() == "Displacement")
        {
            skillPart->Displacements().push_back(CreateDisplacement(*it));
        }
    }

    return skillPart;
}

Displacement* DisplacementsBuilder::CreateDisplacement(xml_node& xelement) 
{
    if (!xelement)
    {
        return NULL;
    }

    BallRelated* entity = new BallRelated();

    // 暂无对应的xml配置

    LogHelper.Insert("Create the " + TypeId(Displacement) + "'s record.", LogType_Debug);

    return entity;
}


