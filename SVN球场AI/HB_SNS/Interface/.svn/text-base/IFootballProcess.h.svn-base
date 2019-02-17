/********************************************************************************
 * 文件名：IFootballProcess.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IFOOTBALLPROCESS_H__
#define __IFOOTBALLPROCESS_H__

#include "IProcess.h"

/// 表示了足球的单回合数据
class IFootballProcess : public IMoveableProcess 
{
public:

    virtual ~IFootballProcess() {}

public:

    virtual int     Angle() = 0;
    virtual void    SetAngle(int angle) = 0;

    virtual string  Destination() = 0;
    virtual void    SetDestination(string destination) = 0;

    virtual bool    IsInAir() = 0;
    virtual void    SetIsInAir(bool inAir) = 0;
};


#endif //__IFOOTBALLPROCESS_H__
