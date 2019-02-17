/********************************************************************************
 * 文件名：String.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "String.h"

typedef char*   VarList;
#define VarStart(ap,v)		(ap = (VarList)&v + FourByteAlign(v))
#define FourByteAlign(n)	((sizeof(n)+sizeof(int) - 1) & ~(sizeof(int) - 1)) // 将n四位对齐

std::string String::Format(char* format, ...)
{
    char str[1024] = {};
    VarList argPtr;
    VarStart(argPtr, format);
    vsnprintf_s(str, sizeof(str)-1, format, argPtr);	// 将参数的值转化到字符串中

    return str;
}

std::string String::Empty()
{
    return "Empty";
}

bool String::IsNullOrEmpty(std::string str)
{
    if (str == "Empty" || str.empty())
    {
        return true;
    }

    return false;
}
