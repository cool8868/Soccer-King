/********************************************************************************
 * 文件名：IPlayerStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IPLAYERSTATUS_H__
#define __IPLAYERSTATUS_H__

#include "IDefenceStatus.h"
#include "IDiveStatus.h"
#include "IFoulStatus.h"
#include "IShootStatus.h"
#include "IPassStatus.h"

#include "../../../common/Enum/Zone.h"
#include "../../IState.h"
#include "IActionStatus.h"
#include "IDefenceStatus.h"
#include "IDiveStatus.h"
#include "IFoulStatus.h"
#include "IPassStatus.h"
#include "IShootStatus.h"
#include "IDebuffStatus.h"
#include "IModelStatus.h"
#include "../INotifyRedecide.h"
#include "../IDecide.h"

/// 表示了球员的当前状态
class IPlayerStatus: public INotifyRedecide, public IDecideEnd
{
public:

    virtual ~IPlayerStatus() {}

public:

    /// 表示了球员是否拥有球，只有球员持球并离球小于2时为真
    virtual bool                Holdball() = 0;

    /// 表示了球员是否被标记为有球
    virtual bool                Hasball() = 0;
    virtual void                SetHasball(bool hasBall) = 0;

    /// 表示了球员的朝向角度
    /// 朝向正右方为0°，朝向正左方为180°
    virtual int                 Angle() = 0;
    virtual void                SetAngle(int angle) = 0;

    /// 表示了球员的当前速度
    virtual double              Speed() = 0;
    virtual void                SetSpeed(double speed) = 0;

    /// 表示了球员的最高速度
    virtual double              MaxSpeed() = 0;
    virtual void                SetMaxSpeed(double speed) = 0;

    /// 表示了球员的当前坐标
    virtual Coordinate          Current() = 0;
    virtual void                SetCurrent(Coordinate coord) = 0;

    /// 表示了球员的默认坐标
    virtual Coordinate          GetDefault() = 0;
    virtual void                SetDefault(Coordinate coord) = 0;

    /// 表示了球员的目标坐标
    virtual Coordinate          Destination() = 0;
    virtual void                SetDestination(Coordinate coord) = 0;

    /// 表示了球员的当前状态
    virtual IState*             State() = 0;
    virtual void                SetState(IState* state) = 0;

    /// 表示了球员的加速度
    virtual double              Acceleration() = 0;
    virtual void                SetAcceleration(double acc) = 0;

    /// 表示了球员是否是进攻方
    virtual bool                IsAttackSide() = 0;

    /// 表示了球员当前的区域
    virtual Zone                GetZone() = 0;

    /// 表示了球员是否向后移动
    virtual bool                IsBackward() = 0;
    virtual void                SetIsBackward(bool flag) = 0;

    /// 表示了守门员是否是站立状态
    virtual bool                IsStandUp() = 0 ;
    virtual void                SetIsStandUp(bool flag) = 0;

    /// 表示了球员的属性每回合下降数值
    virtual double              PerRoundDecrease() = 0;
    virtual void                SetPerRoundDecrease(double dec) = 0;

public:

    /// 表示了球员射门时的状态
    virtual IShootStatus*       GetShootStatus() = 0;

    /// 表示了球员的传球状态
    virtual IPassStatus*        GetPassStatus() = 0;

    /// 表示了球员的防守状态
    virtual IDefenceStatus*     GetDefenceStatus() = 0;

    /// 表示了球员的各种犯规状态
    virtual IFoulStatus*        GetFoulStatus() = 0;

    /// 表示了球员的当前异常状态
    virtual IDebuffStatus*      GetDebuffStatus() = 0;

    /// 表示了守门员扑救时的各种状态
    virtual IDiveStatus*        GetDiveStatus() = 0;

    /// 表示了行动进行中的相关状态
    virtual IActionStatus*      GetActionStatus() = 0;

    /// 表示了球员的模型状态
    virtual IModelStatus*       GetModelStatus() = 0;

public:

    /// 表示了球员离球的距离
    virtual int                 BallDistance() = 0;

    /// 表示了球员离目标点的距离
    virtual double              DestinationDistance() = 0;

    /// 表示了球员离传球目标的距离
    virtual int                 PassTargetDistance() = 0;

    /// 表示了球员离球门的距离
    virtual double              GoalDistance() = 0;

    /// 表示和防守目标的距离
    virtual double              DefenceTargetDistance() = 0;

    /// 表示了该球员和他最近的防守者的距离
    virtual double              DefenderDistance() = 0;

public:

    /// 表示了球员是否需要重新思考
    virtual bool                NeedRedecide() = 0;

    /// 表示了球员是否在场上
    virtual bool                Enable() = 0;

    /// 刷新所有的距离
    virtual void                RefreshDistance() = 0;
};

#endif //__IPLAYERSTATUS_H__
