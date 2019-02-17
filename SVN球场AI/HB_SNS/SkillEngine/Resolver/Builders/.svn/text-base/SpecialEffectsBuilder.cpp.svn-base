/********************************************************************************
 * 文件名：SpecialEffectsBuilder.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SPECIALEFFECTSBUILDER_H__
#define __SPECIALEFFECTSBUILDER_H__

ISkillPart* SpecialEffectsBuilder::Build(xml_node& xelement) 
{
    if (!xelement)
    {
        return NULL;
    }

    SpecialEffectsSkillPart* skillPart = new SpecialEffectsSkillPart();

    for (xml_node_iterator it = xelement.begin(); it != xelement.end(); ++it)
    {
        if (it->name() == "SpecialEffect")
        {
            skillPart->Specials().push_back(CreateSpecial(*it));
        }
    }

    return skillPart;
}

Special* SpecialEffectsBuilder::CreateSpecial(xml_node& xelement) 
{
    <SpecialEffect name="PassOutboundEffector" value=""></SpecialEffect>

    string name             = it->attribute("name").value();
    string value            = it->attribute("value") .value();

    Special* entity         = new Special();

    entity->SetName(name);
    entity->SetValue(value);

    LogHelper.Insert("Create the " + TypeId(Special) + "'s record.", LogType_Debug);

    return entity;

}
