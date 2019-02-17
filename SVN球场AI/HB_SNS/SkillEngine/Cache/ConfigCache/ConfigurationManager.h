/********************************************************************************
 * 文件名：ConfigurationManager.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __CONFIGURATIONMANAGER_H__
#define __CONFIGURATIONMANAGER_H__

#include "../../../common/common.h"
#include "../../../Exception/MyException.h"
#include "../../../Log/LogHelper.h"

class ConfigurationManager
{
public:
    
    /// Initializes a new instance of the <see cref="WillConfigCache"/> class.
    ConfigurationManager();

    virtual ~ConfigurationManager() {}

public:

    /// Gets a will from the xml file with the will's id as parameter.
    xml_node    GetSection(string id);

private:
    
    xml_document m_Cache;
};

class ConfigurationManagerException: public MyException
{
public:

    ConfigurationManagerException(const string message)
    {
        m_err = "ConfigurationManagerException:";
        m_err += message;
    }
};

#endif //__CONFIGURATIONMANAGER_H__
