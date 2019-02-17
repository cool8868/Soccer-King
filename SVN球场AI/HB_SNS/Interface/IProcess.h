/********************************************************************************
 * 文件名：IProcess.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IPROCESS_H__
#define __IPROCESS_H__

#include "../common/common.h"
#include "../common/Structs/Coordinate.h"

/// 表示了一回合的数据
class IProcess
{
public:

    virtual ~IProcess() {}

public:

    virtual int         Round() = 0;
    virtual void        SetRound(int round) = 0;
    virtual void        IncRound() = 0;
};

/// 表示了比赛的一回合数据
class IMatchProcess: public IProcess
{
public:

    virtual ~IMatchProcess() {}

public:

    virtual string      Score() = 0;
    virtual void        SetScore(string score) = 0;

    virtual bool        IsGoal() = 0;
    virtual void        SetIsGoal(bool isGoal) = 0;

    virtual bool        GetInterruption() = 0;
    virtual void        SetInterruption(bool interruption) = 0;
};

/// 表示了可移动物体的单回合数据
class IMoveableProcess
{
public:

    virtual ~IMoveableProcess() {}

public:

    virtual string      GetCoordinate() = 0;
    virtual void        SetCoordinate(string coord) = 0;

    virtual Coordinate  GetCoordinate2() = 0;
    virtual void        SetCoordinate2(Coordinate coord) = 0;

    virtual int         Acceleration() = 0;
    virtual void        SetAcceleration(int acc) = 0;
};

/// 表示了球员的单回合数据
class IPlayerProcess: public IMoveableProcess
{
public:

    virtual ~IPlayerProcess() {}

public:

    virtual int         Angle() = 0;
    virtual void        SetAngle(int angle) = 0;

    virtual int         GetState() = 0;
    virtual void        SetState(int state) = 0;

    virtual bool        NameVisible() = 0;
    virtual void        SetNameVisible(bool visible) = 0;

    virtual bool        HasBall() = 0;
    virtual void        SetHasBall(bool flag) = 0;

    virtual int         FoulLevel() = 0;
    virtual void        SetFoulLevel(int level) = 0;
};

/// 表示了球员射门回合的数据
class IShootProcess: public IPlayerProcess
{
public:

    virtual ~IShootProcess() {}

public:

    virtual string      GetShootTarget() = 0;
    virtual void        SetShootTarget(string target) = 0;

    virtual int         ShootTargetIndex() = 0;
    virtual void        SetShootTargetIndex(int index) = 0;

    virtual int         GetShootSpeed() = 0;
    virtual void        SetShootSpeed(int speed) = 0;
};

/// 表示了守门员的单回合数据
class IGoalkeeperProcess: public IPlayerProcess
{
public:

    virtual ~IGoalkeeperProcess() {}

public:

    virtual bool        IsBackward() = 0;
    virtual void        SetIsBackward(bool flag) = 0;

    virtual bool        IsStandUp() = 0;
    virtual void        SetIsStandUp(bool flag) = 0;

    virtual int         DiveDirection() = 0;
    virtual void        SetDiveDirection(int dir) = 0;
};

/// 表示了传球时的单回合数据
class IPassProcess: public IPlayerProcess
{
public:

    virtual ~IPassProcess() {}

public:

    virtual bool        IsPassFail() = 0;
    virtual void        SetIsPassFail(bool flag) = 0;
};

#endif //__IPROCESS_H__
