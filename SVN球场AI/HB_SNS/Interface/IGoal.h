/********************************************************************************
 * 文件名：IGoal.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IGOAL_H__
#define __IGOAL_H__

#include "../common/Structs/ShootTarget.h"

/// 表示了一个球门
class IGoal 
{
public:

    virtual ~IGoal() {}

public:

    /// 通过index获取一个射门目标
    virtual ShootTarget GetShootTargetByIndex(int index) = 0;

    /// 获取一个随机门框
    virtual ShootTarget GetRandomDoorFrame() = 0;
};

#endif //__IGOAL_H__
