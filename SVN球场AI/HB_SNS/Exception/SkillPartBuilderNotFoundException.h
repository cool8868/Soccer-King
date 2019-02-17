/********************************************************************************
 * 文件名：SkillPartBuilderNotFoundException.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SKILLPARTBUILDERNOTFOUNDEXCEPTION_H__
#define __SKILLPARTBUILDERNOTFOUNDEXCEPTION_H__

#include <exception>
#include "MyException.h"

class SkillPartBuilderNotFoundException : public MyException
{
public:

    SkillPartBuilderNotFoundException()
    {

    }

    SkillPartBuilderNotFoundException(const string& message)
        : MyException(message)
    {

    }

public:

    const char* what() const throw()
    {
        string output = "SkillPartBuilderNotFoundException:";
        output += m_err;

        return output.c_str();
    }
};

#endif //__SKILLPARTBUILDERNOTFOUNDEXCEPTION_H__

