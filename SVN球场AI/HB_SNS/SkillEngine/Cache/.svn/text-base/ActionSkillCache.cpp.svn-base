/********************************************************************************
 * 文件名：ActionSkillCache.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ActionSkillCache.h"
#include "../../Exception/SkillErrorException.h"

ActionSkillCache::ActionSkillCache()
{
    Clear();
}

ActionSkillCache::~ActionSkillCache()
{
    foreach (MapString_IRawSkill::value_type& skill, m_Cache)
    {
        DeletePtrSafe(skill.second);
    }

    m_Cache.clear();
}

ActionSkillCache* ActionSkillCache::Instance()
{
    static ActionSkillCache instance;

    return &instance;
}

void ActionSkillCache::Insert(string skillId, IRawSkill* skill)
{
    m_Cache[skillId]    =   skill;
}

IRawSkill* ActionSkillCache::GetSkill(string skillId)
{
    if (m_Cache.find(skillId) == m_Cache.end())
    {
        throw SkillErrorException("Can't finds skill from the cache. id:" + skillId);
    }

    return m_Cache[skillId];
}
