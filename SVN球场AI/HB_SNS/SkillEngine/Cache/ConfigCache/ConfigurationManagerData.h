/********************************************************************************
 * 文件名：ConfigurationManagerData.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __CONFIGURATIONMANAGERDATA_H__
#define __CONFIGURATIONMANAGERDATA_H__

#include "../../../common/common.h"
#include "../../../Exception/MyException.h"
#include "../../../Log/LogHelper.h"

class ConfigurationManagerData
{
public:
    
    /// Initializes a new instance of the <see cref="WillConfigCache"/> class.
    ConfigurationManagerData();

    virtual ~ConfigurationManagerData() {}

public:

    /// Gets a will from the xml file with the will's id as parameter.
    xml_node    GetSection(string id);

private:
    
    xml_document m_Cache;
};

class ConfigurationManagerDataException: public MyException
{
public:

    ConfigurationManagerDataException(const string message)
    {
        m_err = "ConfigurationManagerDataException:";
        m_err += message;
    }
};

#endif //__CONFIGURATIONMANAGERDATA_H__
