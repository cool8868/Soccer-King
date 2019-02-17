/********************************************************************************
 * 文件名：WillCache.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WILLCACHE_H__
#define __WILLCACHE_H__

#include "../../common/common.h"
#include "../../Interface/Skill/IRawSkill.h"

/// 意志的缓存
class WillCache
{
public:

    WillCache();

    virtual ~WillCache();

public:

    /// 实例
    static WillCache* Instance();

    /// 插入一个意志
    /// <param name="id">意志编号</param>
    /// <param name="will">意志对象</param>
    void Insert(string id, IRawSkill* will);

    /// 获取一个意志
    /// <param name="id">意志编号</param>
    /// <returns></returns>
    IRawSkill* GetWill(string id);

    /// 清空所有的意志
    void Clear() { m_Cache.clear(); }

private:

    MapString_IRawSkill     m_Cache;
};

#endif //__WILLCACHE_H__
