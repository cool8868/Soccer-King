/********************************************************************************
 * 文件名：IShootStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ISHOOTSTATUS_H__
#define __ISHOOTSTATUS_H__

#include "../../../common/Structs/ShootTarget.h"
#include "../../../common/Structs/Region.h"

/// 表示了球员射门时的状态
class IShootStatus 
{
public:

    virtual ~IShootStatus() {}

public:

    virtual ShootTarget GetShootTarget() = 0;
    virtual void        SetShootTarget(ShootTarget target) = 0;

    virtual int         ShootTargetIndex() = 0;
    virtual void        SetShootTargetIndex(int index) = 0;

    virtual int         ShootSpeed() = 0;
    virtual void        SetShootSpeed(int speed) = 0;

    virtual Region      ShootRegion() = 0;
    virtual void        SetShootRegion(Region region) = 0;
};

#endif //__ISHOOTSTATUS_H__
