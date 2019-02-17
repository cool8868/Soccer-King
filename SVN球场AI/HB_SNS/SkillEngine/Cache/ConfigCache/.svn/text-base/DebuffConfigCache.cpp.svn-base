/********************************************************************************
 * 文件名：DebuffConfigCache.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "DebuffConfigCache.h"

DebuffConfigCache::DebuffConfigCache() 
{
    try
    {
        if (!m_Doc.load_file("MatchData/SkillConfig/DebuffConfig.xml"))
        {
            throw DebuffConfigCacheException("loadfile_failed");
        }

        LogHelper::Insert("Debuff configs has loaded.", LogType_Info);
    }
    catch (const std::exception& ex)
    {
        LogHelper::Insert(&ex);
    }
}

int DebuffConfigCache::GetDebuffLastTime(int level, int type) 
{
    // xml_document 不能为空
    if (!m_Doc)
    {
        return 0;
    }

    xml_node tools = m_Doc.child("debuffs");

    for (xml_node_iterator it = tools.begin(); it != tools.end(); ++it)
    {
        string level_str    = it->attribute("level").value();
        string type_str     = it->attribute("type").value();

        if (level == lexical_cast<int>(level_str) && type == lexical_cast<int>(type_str))
        {
            string last_str = it->attribute("last").value();

            return lexical_cast<int>(last_str);
        }
    }

    //找到不到返回0
    return 0;
}