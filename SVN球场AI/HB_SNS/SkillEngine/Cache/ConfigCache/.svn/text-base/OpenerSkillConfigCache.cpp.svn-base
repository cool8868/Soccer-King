/********************************************************************************
 * 文件名：OpenerSkillConfigCache.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "OpenerSkillConfigCache.h"

OpenerSkillConfigCache::OpenerSkillConfigCache() 
{
    try
    {
        if (!m_Cache.load_file("MatchData/SkillConfig/Openers.xml"))
        {
            throw OpenerSkillConfigCacheException("loadfile_failed");
        }

        LogHelper::Insert("Opener skills xml file has loaded.", LogType_Info);
    }
    catch (const std::exception& ex)
    {
        LogHelper::Insert(&ex);
    }
}

vector<xml_node> OpenerSkillConfigCache::GetAllSkills()
{
    if (!m_Cache) 
    {
        return vector<xml_node>(0);
    }

    vector<xml_node> skills;

    xml_node openers = m_Cache.child("Openers");

    for (xml_node_iterator it = openers.begin(); it != openers.end(); ++it)
    {
        std::string id_str = it->attribute("id").value();

        if (!id_str.empty())
        {
            skills.push_back(*it);
        }
    }

    return skills;
}

xml_node OpenerSkillConfigCache::GetSkill(string id) 
{
    if (!m_Cache)
    {
        return xml_node();
    }

    xml_node openers = m_Cache.child("Openers");

    for (xml_node_iterator it = openers.begin(); it != openers.end(); ++it)
    {
        std::string id_str = it->attribute("id").value();

        if (id_str == id)
        {
            return *it;
        }
    }

    return xml_node();
}

xml_node OpenerSkillConfigCache::GetSkillPart(string partName, string id) 
{
    return GetSkill(id);
}

xml_node OpenerSkillConfigCache::GetSkillPart(string partName, xml_node& skillElement) 
{
    if (skillElement)
    {
        for (xml_node_iterator it = skillElement.begin(); it != skillElement.end(); ++it)
        {
            std::string skill = it->name();

            if (skill == partName)
            {
                return *it;
            }
        }                
    }

    return xml_node();
}
