/********************************************************************************
 * 文件名：PassiveSkillConfigCache.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PASSIVESKILLCONFIGCACHE_H__
#define __PASSIVESKILLCONFIGCACHE_H__

#include "../../../common/common.h"
#include "../../../Exception/MyException.h"
#include "../../../Log/LogHelper.h"

class PassiveSkillConfigCache
{
public:

    /// Initializes a new instance of the <see cref="PassiveSkillConfigCache"/> class.
    PassiveSkillConfigCache();

    virtual ~PassiveSkillConfigCache() {}

public:

    /// Get all the skill's <see cref="XElement"/> from the config file.
    vector<xml_node> GetAllSkills();

    /// Get a skill by the skill's id.
    xml_node GetSkill(string id);

    /// Get a skill's part by the part name and the skill's id.
    /// This method will get the skill part from the config in the memory.
    xml_node GetSkillPart(string partName, string id);

    /// Get a skill's part by the part name and the <see cref="XElement"/>
    /// that contains the part.
    xml_node GetSkillPart(string partName, xml_node& skillElement);

private:
    
    xml_document m_Cache;
};

class PassiveSkillConfigCacheException: public MyException
{
public:

    PassiveSkillConfigCacheException(const string message)
    {
        m_err = "PassiveSkillConfigCacheException:";
        m_err += message;
    }
};

#endif //__PASSIVESKILLCONFIGCACHE_H__
