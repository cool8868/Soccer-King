/********************************************************************************
 * 文件名：IPitch.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IPITCH_H__
#define __IPITCH_H__

#include "../common/Enum/Direction.h"
#include "../common/Structs/Coordinate.h"
#include "../common/Structs/Region.h"
#include "../common/Structs/Line.h"

class IGoal;

typedef map<Direction, Line> MapDirection_Line;

/// 表示了球场的接口
class IPitch
{
public:

    virtual ~IPitch() {}

public:

    virtual IGoal*              GetGoal() = 0;
    virtual void                SetGoal(IGoal* goal) = 0;

    virtual Coordinate          HomeGoal() = 0;

    virtual Coordinate          AwayGoal() = 0;

    virtual Region              HomeShootRegion() = 0;
    
    virtual Region              AwayShootRegion() = 0;

    virtual Region              HomePenaltyRegion() = 0;

    virtual Region              AwayPenaltyRegion() = 0;

    virtual Region              HomeForcePassRegion() = 0;

    virtual Region              AwayForcePassRegion() = 0;

    virtual MapDirection_Line&  HomeDestinations() = 0;
    virtual MapDirection_Line&  AwayDestinations() = 0;
};

#endif //__IPITCH_H__
