/********************************************************************************
 * 文件名：OpenerSkillCache.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __OPENERSKILLCACHE_H__
#define __OPENERSKILLCACHE_H__

#include "../../common/common.h"
#include "../../Interface/Skill/IRawSkill.h"

class OpenerSkillCache 
{
public:

    OpenerSkillCache();

    virtual ~OpenerSkillCache();

public:

    /// Get the instance of the <see cref="OpenerSkillCache"/> class.
    static OpenerSkillCache* Instance();

    /// Insert the skill to the cache.
    void Insert(string skillId, IRawSkill* skill);

    /// Get a skill from the cache.
    IRawSkill* GetSkill(string skillId);

    /// Clear all cached skill.
    void Clear() { m_Cache.clear(); }

private:

    MapString_IRawSkill     m_Cache;
};

#endif //__OPENERSKILLCACHE_H__
