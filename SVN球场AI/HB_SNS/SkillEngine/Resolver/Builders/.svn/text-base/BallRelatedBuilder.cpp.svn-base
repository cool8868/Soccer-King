/********************************************************************************
 * 文件名：BallRelatedBuilder.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "BallRelatedBuilder.h"

ISkillPart* BallRelatedBuilder::Build(xml_node& xelement) 
{
    if (!xelement)
    {
        return NULL;
    }

    BallRelatedSkillPart* skillPart = new BallRelatedSkillPart();

    for (xml_node_iterator it = xelement.begin(); it != xelement.end(); ++it)
    {
        if (it->name() == "Ball")
        {
            skillPart->BallRelateds().push_back(CreateBallRelated(*it));
        }
    }

    return skillPart;
}

BallRelated* BallRelatedBuilder::CreateBallRelated(xml_node& xelement) 
{
    if (!xelement)
    {
        return NULL;
    }

    string action       = it->attribute("action").value();
    string last         = it->attribute("last") .value();

    BallRelated* entity = new BallRelated();

    entity.SetAction(lexical_cast<int>(action));
    entity.SetLast(lexical_cast<int>(action));

    LogHelper.Insert("Create the " + TypeId(BallRelated) + "'s record.", LogType_Debug);

    return entity;
}

