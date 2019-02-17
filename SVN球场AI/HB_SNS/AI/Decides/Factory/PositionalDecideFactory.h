/********************************************************************************
 * 文件名：PositionalDecideFactory.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __POSITIONALDECIDEFACTORY_H__
#define __POSITIONALDECIDEFACTORY_H__

#include "../../../common/common.h"
#include "../PositionalDecide.h"
#include "../Forward/Forward_PositionalDecide.h"
#include "../Midfielder/Midfielder_PositionalDecide.h"
#include "../Fullback/Fullback_PositionalDecide.h"
#include "../Goalkeeper/Goalkeeper_PositionalDecide.h"

class _PositionalDecideFactory
{
public:

    _PositionalDecideFactory()
    {
        m_cache.clear();

        m_cache[Position_Forward]           = &singleton_default<Forward_PositionalDecide>::instance();
        m_cache[Position_Midfielder]        = &singleton_default<Midfielder_PositionalDecide>::instance();
        m_cache[Position_Fullback]          = &singleton_default<Fullback_PositionalDecide>::instance();
        m_cache[Position_Goalkeeper]        = &singleton_default<Goalkeeper_PositionalDecide>::instance();
    }

    PositionalDecide* Create(Position position)
    {
        return m_cache[position];
    }

private:

    map<Position, PositionalDecide*> m_cache;
};

/// 寻位工厂
class PositionalDecideFactory
{
public:

    /// 创建寻位对象
    static PositionalDecide* Create(Position position)
    {
        return singleton_default< _PositionalDecideFactory >::instance().Create(position);
    }
};

#endif //__POSITIONALDECIDEFACTORY_H__
