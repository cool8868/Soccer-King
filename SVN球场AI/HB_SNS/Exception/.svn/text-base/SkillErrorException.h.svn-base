/********************************************************************************
 * 文件名：SkillErrorException.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SKILLERROREXCEPTION_H__
#define __SKILLERROREXCEPTION_H__

#include <exception>
#include "MyException.h"

class SkillErrorException : public MyException
{
public:

    SkillErrorException()
    {

    }

    SkillErrorException(const string& message)
        : MyException(message)
    {

    }

    SkillErrorException(string message, MyException& inner) 
        : MyException(message, inner) 
    {

    }

public:

    const char* what() const throw()
    {
        string output = "SkillErrorException";
        output += m_err;

        return output.c_str();
    }
};

#endif //__SKILLERROREXCEPTION_H__
