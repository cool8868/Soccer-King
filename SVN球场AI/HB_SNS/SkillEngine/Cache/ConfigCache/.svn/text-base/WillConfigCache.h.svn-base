/********************************************************************************
 * 文件名：WillConfigCache.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WILLCONFIGCACHE_H__
#define __WILLCONFIGCACHE_H__

#include "../../../common/common.h"
#include "../../../Exception/MyException.h"
#include "../../../Log/LogHelper.h"

class WillConfigCache
{
public:
    
    /// Initializes a new instance of the <see cref="WillConfigCache"/> class.
    WillConfigCache();

    virtual ~WillConfigCache() {}

public:

    /// Gets all the wills from the xml file.
    vector<xml_node> GetAllWills();

    /// Gets a will from the xml file with the will's id as parameter.
    xml_node GetWill(string id);

    /// Gets a will's part by the part name and the skill's id.
    xml_node GetWillPart(string part, string id);

    /// Gets a will's part by the part name and the <see cref="XElement"/>.
    xml_node GetWillPart(string part, xml_node& element);

private:
    
    xml_document m_Cache;
};

class WillConfigCacheException: public MyException
{
public:

    WillConfigCacheException(const string message)
    {
        m_err = "WillConfigCacheException:";
        m_err += message;
    }
};

#endif //__WILLCONFIGCACHE_H__
