/********************************************************************************
 * 文件名：IFoulStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IFOULSTATUS_H__
#define __IFOULSTATUS_H__

/// 表示了球员的犯规状态
class IFoulStatus
{
public:

    virtual ~IFoulStatus() {}

public:
    virtual int     FoulLevel() = 0;
    virtual void    SetFoulLevel(int level) = 0;

    virtual bool    IsYellowCard() = 0;

    virtual bool    IsRedCard() = 0;

    virtual bool    IsInjured() = 0;
};

#endif //__IFOULSTATUS_H__
