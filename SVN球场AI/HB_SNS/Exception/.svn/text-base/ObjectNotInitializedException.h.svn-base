/********************************************************************************
 * 文件名：ObjectNotInitializedException.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __OBJECTNOTINITIALIZEDEXCEPTION_H__
#define __OBJECTNOTINITIALIZEDEXCEPTION_H__

#include "MyException.h"

/// Represents the object not initialized error that occur during application execution.
class ObjectNotInitializedException : public MyException 
{
public:

    ObjectNotInitializedException() 
    {

    }

    ObjectNotInitializedException(string message) 
        : MyException(message) 
    {

    }

public:

    const char* what() const throw()
    {
        string output = "ObjectNotInitializedException:";
        output += m_err;

        return output.c_str();
    }
};

#endif //__OBJECTNOTINITIALIZEDEXCEPTION_H__

