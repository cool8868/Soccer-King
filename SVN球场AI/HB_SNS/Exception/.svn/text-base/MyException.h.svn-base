/********************************************************************************
 * 文件名：MyException.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MYEXCEPTION_H__
#define __MYEXCEPTION_H__

#include "../common/common.h"
#include "../common/String/String.h"
#include <exception>

class MyException: public std::exception
{
public:

    MyException()
    {

    }

    MyException(const string& str)
        : m_err(str)
    {

    }

    MyException(const string& str, MyException& ex)
    {
        m_err = ex.what();
        m_err += "\n";
        m_err += str;
    }

    virtual ~MyException() {}

    virtual const char* what() const throw()
    {
        string output = "MyException:";
        output += m_err;

        return output.c_str();
    }

protected:

    string  m_err;
};

#endif //__MYEXCEPTION_H__
