/********************************************************************************
 * 文件名：IMoveable.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IMOVEABLE_H__
#define __IMOVEABLE_H__

#include "../common/Structs/Coordinate.h"
#include "IOutputer.h"

///可移动的基类
class IMoveable: public IOutputer
{
public:

    virtual ~IMoveable() {}

public:

    /// 物体根据自身的趋势移动
    virtual void Move() = 0;

    /// 将物体强行移动至某处
    virtual void MoveTo(Coordinate target) = 0;

    /// 将物体强行移动至某处
    virtual void MoveTo(double x, double y) = 0;

    /// 保存物体的当前状态
    virtual void Save() = 0;

    /// 保存物体的当前状态
    virtual void Save(int round) = 0;

    /// 重置物体的当前状态
    virtual void Reset() = 0;

public:

    virtual Coordinate          Current() = 0;

    virtual Coordinate          Destination() = 0;

    virtual double              Speed() = 0;

    virtual double              Acceleration() = 0;
};

#endif //__IMOVEABLE_H__
