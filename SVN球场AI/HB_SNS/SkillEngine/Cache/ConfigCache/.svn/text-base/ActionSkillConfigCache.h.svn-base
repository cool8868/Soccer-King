/********************************************************************************
 * 文件名：ActionSkillConfigCache.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ACTIONSKILLCONFIGCACHE_H__
#define __ACTIONSKILLCONFIGCACHE_H__

#include "../../../common/common.h"
#include "../../../Exception/MyException.h"
#include "../../../Log/LogHelper.h"

/// Represents the action skills xml file's cache.
class ActionSkillConfigCache
{
public:

    ActionSkillConfigCache();

    //获取所有的技能节点
    vector<xml_node> GetAllSkills();

    /// Get a skill by the skill's id.
    xml_node GetSkill(string id);

    /// Get a skill's part by the part name and the skill's id.
    /// This method will get the skill part from the config in the memory.
    xml_node GetSkillPart(string partName, string id);

    /// Get a skill's part by the part name and the <see cref="XElement"/> that contains the part.
    xml_node GetSkillPart(string partName, xml_node& skillElement);

private:

    xml_document m_Cache;
};

class ActionSkillConfigCacheException: public MyException
{
public:

    ActionSkillConfigCacheException(const string message)
    {
        m_err = "ActionSkillConfigCacheException:";
        m_err += message;
    }
};

#endif //__ACTIONSKILLCONFIGCACHE_H__
