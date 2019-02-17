/********************************************************************************
 * 文件名：IPassStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IPASSSTATUS_H__
#define __IPASSSTATUS_H__

class IPlayer;

/// 表示了球员传球的状态
class IPassStatus 
{
public:

    virtual ~IPassStatus() {}

public:

    virtual IPlayer*    PassTarget() = 0;
    virtual void        SetPassTarget(IPlayer* player) = 0;

    virtual bool        IsPassFail() = 0;
    virtual void        SetIsPassFail(bool flag) = 0;
};

#endif //__IPASSSTATUS_H__
