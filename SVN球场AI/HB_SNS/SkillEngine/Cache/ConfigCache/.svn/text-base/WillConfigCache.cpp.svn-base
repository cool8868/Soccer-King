/********************************************************************************
 * 文件名：WillConfigCache.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "WillConfigCache.h"

WillConfigCache::WillConfigCache()
{
    try
    {
        if (!m_Cache.load_file("MatchData/SkillConfig/Wills.xml"))
        {
            throw WillConfigCacheException("loadfile_failed");
        }

        LogHelper::Insert("Loads the wills.xml error.", LogType_Info);
    }
    catch (const std::exception& ex)
    {
        LogHelper::Insert(&ex);
    }
}

vector<xml_node> WillConfigCache::GetAllWills()
{
    // xml_document 不能为空
    if (!m_Cache)
    {
        return vector<xml_node>(0);
    }

    vector<xml_node> skills;

    xml_node tools = m_Cache.child("Wills");

    for (xml_node_iterator it = tools.begin(); it != tools.end(); ++it)
    {
        std::string formationid_str = it->attribute("id").value();

        if (!formationid_str.empty())
        {
            skills.push_back(*it);
        }
    }

    return skills;
}

xml_node WillConfigCache::GetWill(string id)
{
    // xml_document不能为空
    if (!m_Cache)
    {
        return xml_node();
    }

    xml_node tools = m_Cache.child("Wills");

    for (xml_node_iterator it = tools.begin(); it != tools.end(); ++it)
    {
        std::string formationid_str = it->attribute("id").value();

        if (formationid_str == id)
        {
            return *it;
        }
    }

    return xml_node();
}

xml_node WillConfigCache::GetWillPart(string partName, string id)
{
    return GetWill(id);
}

xml_node WillConfigCache::GetWillPart(string partName, xml_node& skillElement)
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

