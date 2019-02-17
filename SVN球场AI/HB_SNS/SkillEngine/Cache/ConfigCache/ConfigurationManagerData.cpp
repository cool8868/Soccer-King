/********************************************************************************
 * 文件名：ConfigurationManagerData.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ConfigurationManagerData.h"

ConfigurationManagerData::ConfigurationManagerData()
{
    try
    {
        if(!m_Cache.load_file("MatchData/SkillConfig/ManagerDataAppconfig.xml"))
        {
            throw ConfigurationManagerDataException("loadfile_failed");
        }
    }
    catch (const std::exception& ex)
    {
        LogHelper::Insert(&ex);
    }
}

xml_node ConfigurationManagerData::GetSection(string id)
{
    // xml_document不能为空
    if (!m_Cache)
    {
        return xml_node();
    }

    xml_node tools = m_Cache.child("configuration");

    for (xml_node_iterator it = tools.begin(); it != tools.end(); ++it)
    {
        string name = it->name();

        if (name == id)
        {
            return *it;
        }
    }

    return xml_node();
}





