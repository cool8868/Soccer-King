/********************************************************************************
 * 文件名：IFootball.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IFOOTBALL_H__
#define __IFOOTBALL_H__

#include "IMoveable.h"
#include "IVisibleObject.h"
#include "Player/IPlayer.h"

/// 代表了足球的接口
class IFootball : public IMoveable, public IVisibleObject
{
public:

    virtual ~IFootball() {}

public:

    virtual bool        IsInAir() = 0;
    virtual void        SetIsInAir(bool flag) = 0;

    virtual bool        IsPassDestination() = 0;
    virtual void        SetPassDestination(bool flag) = 0;

    virtual bool        Visible() = 0;
    virtual void        SetVisible(bool visible) = 0;

    virtual IPlayer*    LastTouchMan() = 0;
    virtual void        SetLastTouchMan(IPlayer* player) = 0;

    /// 把球踢往某一点
    virtual void        Kick(Coordinate coordinate, double speed, IPlayer* lastMan) = 0;

    /// 将球踢往某一点（球是在空中的）
    virtual void        Kick(Coordinate coordinate, double speed, int angle, IPlayer* lastMan) = 0;

    /// 将球隐藏一小段时间
    virtual void        Hide(int round) = 0;

    /// 强迫下一次传球为高球
    virtual void        ForceInAir() = 0;

    /// 在新一回合到来时初始化
    virtual void        RoundInit() = 0;
};

#endif //__IFOOTBALL_H__
