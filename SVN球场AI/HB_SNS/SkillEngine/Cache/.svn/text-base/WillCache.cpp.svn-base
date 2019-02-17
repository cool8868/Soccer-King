/********************************************************************************
 * 文件名：WillCache.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "WillCache.h"
#include "../../Exception/SkillErrorException.h"

WillCache::WillCache()
{
    Clear();
}

WillCache::~WillCache()
{
    foreach (MapString_IRawSkill::value_type& skill, m_Cache)
    {
        DeletePtrSafe(skill.second);
    }

    m_Cache.clear();
}

WillCache* WillCache::Instance()
{
    static WillCache instance;

    return &instance;
}

void WillCache::Insert(string id, IRawSkill* will)
{
    m_Cache[id]     =   will;
}

IRawSkill* WillCache::GetWill(string id)
{
    if (m_Cache.find(id) == m_Cache.end())
    {
        throw SkillErrorException("Can't finds the will from the cache. id:" + id);
    }

    return m_Cache[id];
}



