/********************************************************************************
 * 文件名：DebuffConfigCache.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DEBUFFCONFIGCACHE_H__
#define __DEBUFFCONFIGCACHE_H__

#include "../../../common/common.h"
#include "../../../Exception/MyException.h"
#include "../../../Log/LogHelper.h"

class DebuffConfigCache 
{
public:

    /// Initializes a new instance of the <see cref="DebuffConfigCache"/>.
    DebuffConfigCache();

    virtual ~DebuffConfigCache() {}

public:

    int GetDebuffLastTime(int level, int type);

private:

    xml_document m_Doc;
};

class DebuffConfigCacheException: public MyException
{
public:

    DebuffConfigCacheException(const string message)
    {
        m_err = "DebuffConfigCacheException:";
        m_err += message;
    }
};

#endif //__DEBUFFCONFIGCACHE_H__
