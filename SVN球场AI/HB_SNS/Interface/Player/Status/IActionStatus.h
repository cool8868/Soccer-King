/********************************************************************************
 * 文件名：IActionStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IACTIONSTATUS_H__
#define __IACTIONSTATUS_H__

/// 表示了行动进行中的相关参数
class IActionStatus 
{
public:

    virtual ~IActionStatus() {}

public:

    virtual int     ActionLast() = 0;
    virtual void    SetActionLast(int last) = 0;
    virtual void    IncActionLast() = 0;
};

#endif //__IACTIONSTATUS_H__
