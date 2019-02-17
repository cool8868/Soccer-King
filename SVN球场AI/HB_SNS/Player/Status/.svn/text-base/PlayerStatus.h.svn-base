/********************************************************************************
 * 文件名：PlayerStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PLAYERSTATUS_H__
#define __PLAYERSTATUS_H__

#include "../../Interface/Player/Status/IPlayerStatus.h"
#include "../../Interface/Player/Status/IDiveStatus.h"
#include "../../Interface/IMatch.h"
#include "../../Interface/Player/Property/IPlayerAttribute.h"
#include "../../common/Utility.h"

class PlayerStatus: public IPlayerStatus
{
public:

    PlayerStatus(IPlayer* player, Coordinate coordinate);

    ~PlayerStatus();

//基类的重载函数
public:

    //实例化抽象类
    //INotifyRedecide
    void Redecide();

    //IDecideEnd
    void DecideEnd();

public:

    bool        Holdball();

    bool        Hasball() { return m_Hasball; }
    void        SetHasball(bool hasBall);

    int         Angle() { return m_Angle; }
    void        SetAngle(int angle) { m_Angle = angle; }

    double      Speed() { return m_Speed; }
    void        SetSpeed(double speed) { m_Speed = speed; }

    double      MaxSpeed() { return m_MaxSpeed; }
    void        SetMaxSpeed(double speed) { m_MaxSpeed = speed; }

    Coordinate  Current() { return m_Current; }
    void        SetCurrent(Coordinate coord) { m_Current = coord; }

    Coordinate  GetDefault() { return m_Default; }
    void        SetDefault(Coordinate coord) { m_Default = coord; }

    Coordinate  Destination() { return m_Destination; }
    void        SetDestination(Coordinate coord) { m_Destination = coord; }

    IState*     State() { return m_State; }
    void        SetState(IState* state) { m_State = state; }

    double      Acceleration() { return m_Acceleration; }
    void        SetAcceleration(double acc) { m_Acceleration = acc; }

    bool        IsAttackSide();

    Zone        GetZone();

    bool        IsBackward() { return m_IsBackward; }
    void        SetIsBackward(bool flag) { m_IsBackward = flag; }

    bool        IsStandUp() { return m_IsStandUp; }
    void        SetIsStandUp(bool flag) { m_IsStandUp = flag; }

    double      PerRoundDecrease() { return m_PerRoundDecrease; }
    void        SetPerRoundDecrease(double dec) { m_PerRoundDecrease = dec; }

public:

    IShootStatus*   GetShootStatus() { return m_ShootStatus; }
    IPassStatus*    GetPassStatus() { return m_PassStatus; }
    IDefenceStatus* GetDefenceStatus() { return m_DefenceStatus; }
    IFoulStatus*    GetFoulStatus() { return m_FoulStatus; }
    IDiveStatus*    GetDiveStatus() { return m_DiveStatus; }
    IDebuffStatus*  GetDebuffStatus() { return m_DebuffStatus; }
    IActionStatus*  GetActionStatus() { return m_ActionStatus; }
    IModelStatus*   GetModelStatus() { return m_ModelStatus; }

public:

    int     BallDistance() { return m_BallDistance; }
    double  DestinationDistance() { return m_DefenderDistance; }
    int     PassTargetDistance() { return m_PassTargetDistance; }
    double  GoalDistance() { return m_GoalDistance; }
    double  DefenceTargetDistance() { return m_DefenceTargetDistance; }
    double  DefenderDistance() { return m_DefenderDistance; }

public:

    bool    NeedRedecide() { return m_NeedRedecide; }
    bool    Enable();

private:

    /// 刷新了该球员与球的距离
    void RefreshBallDistance();

    /// 刷新了球员和他的目标坐标的距离
    void RefreshDestinationDistance();

    /// 刷新球员和他的传球目标的距离
    void RefreshPassTargetDistance();

    /// 刷新该球员和他的防守人的距离
    void RefreshDefenceTargetDistance();

    /// 刷新了该球员和他的最近的防守者之间的距离
    void RefreshDefenderDistance();

    /// 刷新该球员与球门的距离
    void RefreshGoalDistance();

    void RefreshDistance();

    //初始化变量
    void InitVariables();

private:

    /// 表示了球员是否拥有球，只有球员持球并离球小于2时为真
    bool m_Holdball;

    /// 表示了球员是否被标记为有球
    bool m_Hasball;

    /// 表示了球员的朝向角度
    /// 朝向正右方为0°，朝向正左方为180°
    int m_Angle;

    /// 表示了球员的当前速度
    double m_Speed;

    /// 表示了球员的最高速度
    double m_MaxSpeed;

    /// 表示了球员的当前坐标
    Coordinate m_Current;

    /// 表示了球员的默认坐标
    Coordinate m_Default;

    /// 表示了球员的目标坐标
    Coordinate m_Destination;

    /// 表示了球员的当前状态
    IState* m_State;

    /// 表示了球员的加速度
    double m_Acceleration;

    /// 表示了球员是否是进攻方
    bool m_IsAttackSide;

    /// 表示了球员当前的区域
    Zone m_Zone;

    /// 表示了球员是否向后移动
    bool m_IsBackward;

    /// 表示了守门员是否是站立状态
    bool m_IsStandUp;

    /// 表示了球员的属性每回合下降数值
    double m_PerRoundDecrease;
 
private:    //球员的各种状态

    /// 表示了球员射门时的状态
    IShootStatus*   m_ShootStatus;

    /// 表示了球员的传球状态
    IPassStatus*    m_PassStatus;

    /// 表示了球员的防守状态
    IDefenceStatus* m_DefenceStatus;

    /// 表示了球员的各种犯规状态
    IFoulStatus*    m_FoulStatus;

    /// 表示了守门员扑救时的各种状态
    IDiveStatus*    m_DiveStatus;

    /// 表示了球员的当前异常状态
    IDebuffStatus*  m_DebuffStatus;

    /// 表示了行动进行中的相关状态
    IActionStatus*  m_ActionStatus;

    /// 表示了球员的模型状态
    IModelStatus*   m_ModelStatus;

private:    //距离相关

    /// 表示了球员离球的距离
    int m_BallDistance;

    /// 表示了球员离目标点的距离
    double m_DestinationDistance;

    /// 表示了球员离传球目标的距离
    int m_PassTargetDistance;

    /// 表示了球员离球门的距离
    double m_GoalDistance;

    /// 表示和防守目标的距离
    double m_DefenceTargetDistance;

    /// 表示了该球员和他最近的防守者的距离
    double m_DefenderDistance;

protected:  //比赛相关变量

    /// 表示了球员是否需要重新思考
    bool m_NeedRedecide;

    /// 表示了球员是否在场上
    bool m_Enable;

private:    //PlayerStatus数据成员

    IPlayer*    m_Player;
};

// 其他的状态
class PassStatus : public IPassStatus
{
public:

    PassStatus()
        : m_PassTarget(NULL)
        , m_IsPassFail(false)
    {

    }

    IPlayer*    PassTarget() { return m_PassTarget; }
    void        SetPassTarget(IPlayer* player) { m_PassTarget = player; }

    bool        IsPassFail() { return m_IsPassFail; }
    void        SetIsPassFail(bool flag) { m_IsPassFail = flag; }

private:

    /// 表示了球员的传球目标
    IPlayer* m_PassTarget;

    /// 表示了是否传球失败
    bool m_IsPassFail;
};

class DiveStatus : public IDiveStatus
{
public:

    DiveStatus() : m_DiveDirection(0) {}

    int     DiveDirection() { return m_DiveDirection; }
    void    SetDiveDirection(int dir) { m_DiveDirection = dir; }

private:

    /// 表示了扑球的方向
    /// 0 - 左
    /// 1 - 中
    /// 2 - 右
    int m_DiveDirection;
};

class DebuffStatus : public IDebuffStatus
{
public:

    DebuffStatus() : m_DisableDebuff(NULL) {}

    DebuffType          GetDebuffType() { return m_DebuffType; }
    void                SetDebuffType(DebuffType vl) { m_DebuffType = vl; }

    DisableDebuff*      GetDisableDebuff() { return m_DisableDebuff; }
    void                SetDisableDebuff(DisableDebuff* vl) { m_DisableDebuff = vl; }

private:

    /// 表示了球员受到的异常状态类型
    DebuffType      m_DebuffType;

    /// 表示了球员的当前异常行动状态
    DisableDebuff*  m_DisableDebuff;
};

class ShootStatus : public IShootStatus
{
public:

    ShootStatus()
        : m_ShootTargetIndex(0)
        , m_ShootSpeed(0)
    {

    }

    ShootTarget GetShootTarget() { return m_ShootTarget; }
    void        SetShootTarget(ShootTarget target) { m_ShootTarget = target; }

    int         ShootTargetIndex() { return m_ShootTargetIndex; }
    void        SetShootTargetIndex(int index) { m_ShootTargetIndex = index; }

    int         ShootSpeed() { return m_ShootSpeed; }
    void        SetShootSpeed(int speed) { m_ShootSpeed = speed; }

    Region      ShootRegion() { return m_ShootRegion; }
    void        SetShootRegion(Region region) { m_ShootRegion = region; }

private:

    /// 表示了射门的目标
    ShootTarget m_ShootTarget;

    /// 表示了射门目标的编号
    int m_ShootTargetIndex;

    /// 表示了射门的球速
    int m_ShootSpeed;

    /// 表示了球员的射门区域
    Region m_ShootRegion;
};

class DefenceStatus : public IDefenceStatus
{
public:

    DefenceStatus(IPlayer* player);

    IPlayer*            DefenceTarget() { return m_DefenceTarget; }
    void                SetDefenceTarget(IPlayer* player);

    IPlayer*            ClosestDefender() { return m_ClosestDefender; }
    void                SetClosestDefender(IPlayer* player) { m_ClosestDefender = player; }

    vector<IPlayer*>&   Defenders() { return m_Defenders; }

    /// 找出最近的防守人
    IPlayer*            GetClosestDefender();

    // 刷新最近的防守人
    void                RefreshClosestDefender();

private:

    IPlayer* m_Player;

    /// 表示该球员的防守人
    vector<IPlayer*> m_Defenders;

    /// 表示了该球员的防守目标
    IPlayer* m_DefenceTarget;

    /// 表示了最近的防守队员
    IPlayer* m_ClosestDefender;
};

class FoulStatus: public IFoulStatus
{
public:

    FoulStatus()
        : m_FoulLevel(0)
        , m_IsYellowCard(0)
        , m_IsRedCard(0)
        , m_IsInjured(0)
    {

    }

    int     FoulLevel() { return m_FoulLevel; }
    void    SetFoulLevel(int level) { m_FoulLevel = level; }

    bool    IsYellowCard();

    bool    IsRedCard();

    bool    IsInjured();

private:

    /// 表示了球员的犯规等级
    ///  0 - None       
    ///  1 - Yellow Card
    ///  2 - Red Card
    ///  3 - Injured
    int m_FoulLevel;

    /// 是否黄牌
    bool m_IsYellowCard;

    /// 是否红牌
    bool m_IsRedCard;

    /// 是否受伤
    bool m_IsInjured;
};

class ActionStatus: public IActionStatus
{
public:

    ActionStatus() : m_ActionLast(0) {}

    int     ActionLast() { return m_ActionLast; }
    void    SetActionLast(int last) { m_ActionLast = last; }
    void    IncActionLast() { m_ActionLast++; }

private:

    int m_ActionLast;
};

class ModelStatus : public IModelStatus
{
public:

    ModelStatus()
        : m_Mid(0)
        , m_RemainTime(0)
        , m_IsHoldBall(false)
    {

    }

    int         Mid() { return m_Mid; }
    void        SetMid(int vl) { m_Mid = vl; } 

    int         RemainTime() { return m_RemainTime; }
    void        SetRemainTime(int vl) { m_RemainTime = vl; }
    void        DecRemainTime() { m_RemainTime--; }

    bool        IsHoldBall() { return m_IsHoldBall; }
    void        SetIsHoldBall(bool vl) { m_IsHoldBall = vl; }

private:

    /// 表示了模型id
    int         m_Mid;
    
    /// 表示了模型的剩余时间(Round)
    int         m_RemainTime;

    /// 表示了该模型是否是持球时就保持
    bool        m_IsHoldBall;
};

#endif //__PLAYERSTATUS_H__
