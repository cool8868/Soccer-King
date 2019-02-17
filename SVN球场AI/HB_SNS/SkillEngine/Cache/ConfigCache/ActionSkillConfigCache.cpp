/********************************************************************************
 * 文件名：ActionSkillConfigCache.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ActionSkillConfigCache.h"

ActionSkillConfigCache::ActionSkillConfigCache()
{
    try
    {
        if (!m_Cache.load_file("MatchData/SkillConfig/Actions.xml"))
        {
            throw ActionSkillConfigCacheException("loadfile_failed");
        }

        LogHelper::Insert("Action skills xml file has loaded.", LogType_Info);
    }
    catch (const std::exception& ex)
    {
        LogHelper::Insert(&ex);
    }
}

vector<xml_node> ActionSkillConfigCache::GetAllSkills()
{
    // xml_document 不能为空
    if (!m_Cache)
    {
        return vector<xml_node>(0);
    }

    vector<xml_node> skills;

    xml_node tools = m_Cache.child("CommonActions");

    for (xml_node_iterator it = tools.begin(); it != tools.end(); ++it)
    {
        std::string skill = it->attribute("id").value();

        if (!skill.empty())
        {
            skills.push_back(*it);
        }
    }

    return skills;
}

xml_node ActionSkillConfigCache::GetSkill(string id)
{
    // xml_document不能为空
    if (!m_Cache)
    {
        return xml_node();
    }

    xml_node tools = m_Cache.child("CommonActions");

    for (xml_node_iterator it = tools.begin(); it != tools.end(); ++it)
    {
        string skill = it->attribute("id").value();

        if (skill == id)
        {
            return *it;
        }
    }

    return xml_node();
}

xml_node ActionSkillConfigCache::GetSkillPart(string partName, string id)
{
    // 这里的partName仅仅作为异常抛出时的信息之用
    return GetSkill(id);
}

xml_node ActionSkillConfigCache::GetSkillPart(string partName, xml_node& skillElement)
{
    //节点不为空
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
