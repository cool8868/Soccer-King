/********************************************************************************
 * 文件名：Player.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PLAYER_H__
#define __PLAYER_H__

#include "../Interface/Player/IPlayer.h"
#include "../Interface/Player/Property/IPlayerAttribute.h"
#include "../Interface/Manager/IManager.h"
#include "../Interface/Player/Property/IRawPlayer.h"
#include "../Interface/IGoal.h"
#include "../AI/Rules/PlayerRule.h"

#include "../MatchProcess/Process.h"

#include "../Buff/Creature.h"

#include "Status/PlayerStatus.h"

class Player: public IPlayer, public Creature
{
public:

    typedef map<int, ISkillResult*> MapInt_ISkillResult;

public:

    Player(IRawPlayer* entity, int clientId, IManager* manager);

    Player(TransferPlayerEntity* entity, IManager* manager, int clientId, Coordinate currCoordinate);

    virtual ~Player();

//基类函数实现
//Player_IDecide
public: 

    void Action();
    
    void Decide();
    
    void QuickDecide();
    
    void DecideEnd();

    static void ValidateSkillTriggered(IPlayer* player);
private:

    static void SkillTriggered(IPlayer* player, IActionSkill* skill);

//Player_Idefence
public: 

    void InterruptionBall();

    void SlideTackleBall();

    void InternalDefence();

private:

    void InterruptBall();

    void AddTargetInertia(IPlayer* target);

//Player_IDiveBall
public:

    void DiveBall();

private:

    int GetDiveDirection(IPlayer* shooter);

    void OutOfHand(IPlayer* shooter);

//Player_IDribble
public:

    void DribbleBall();


//Player_IFoul
public:

    void Foul(int level);

    void Injured();


//Player_IMoveable
public:

    Coordinate Current();

    Coordinate Destination();

    vector<Process*>& Processes();

    double Speed();

    double Acceleration();

    void Save();

    void Save(int round);

    void Move();

    void MoveTo(Coordinate target);

    void MoveTo(double x, double y);

    void Reset();

    void DecreaseSpeed();

private:

    void ValidateOutofRegion();

    void InternalSave(int round);

//Player_IPass
public:

    void DecidePassTarget();

    void ShortPass();

    void LongPass();

    bool IsCoordinateEnablePass(Coordinate c);
    
private:

    void InternalPass();

    Coordinate GetTarget();

//Player_IRedecide
public:

    void Redecide();

//Player_IRotate
public:
    
    void Rotate(Coordinate target);


//Player_IShoot
public:

    void Shoot();

    void VolleyShoot();

    void FreeKickShoot();

    void InternalShoot(int property, int speed, double fix);

    void ShootToDoorFrame();

    void MoveBallToGoalkeeper();

//Player_ISight
public:

    vector<IPlayer*>& InSightPlayers() { return m_InSightPlayers; }

    void RefreshSight();

//Player_IStopball
public: 
    void Stopball();

//Player_IDisturb
public:
    void ValidateDisturbState();

//Player_IAddBuff
public:
    void AddFinishing();

//Player_IAddDebuff
public:
    void AddInertia();

    void AddInertia(int last);

    void AddDownDebuff(int triggerId, int last);

    void AddPuzzleDebuff(int triggerId, int last);

    void AddStunDebuff(int triggerId, int level);

    void AddOutOfHandDebuff(int triggerId, int level);

    void AddFreezeDebuff(int triggerId, int last);

//Player_IModel
    void AddModel(int model, int last, bool isHoldBall);

    void AddModel(int model, int last);
//////////////////////////////////////////////////////////////////////////
//IPlayer
public:

    /// 球员初始化各项数据
    void Init();

    /// 在新的一回合到来时执行初始化
    void RoundInit();

    void MinuteInit();

    /// 下半场开始时调用
    void SecondHalfStart();

    /// 获取目标所在的区域
    Zone GetTargetZone(Coordinate target);

    /// 设置球员的目标
    void SetTarget(Coordinate target);

    /// 设置球员的目标
    void SetTarget(double x, double y);

    /// 将球员的目标设置为球
    void SetTargetBall(bool isCurrent);

    /// 设置一个坐标给球员。
    /// 如果球员为主队，那么不变；
    /// 如果球员为客队，将自动进行中心对称再赋值.
    void SetHomeSideCoordinate(Coordinate target);

    /// 判断一个坐标是否在球员之内
    bool IsCoordinateInPlayer(Coordinate coordinate);

public:

    unsigned int        Id() { return m_Id; }
    int                 ClientId() { return m_ClientId; }

    IPlayerAttribute*   BuildinAttribute() { return m_Attribute; }

    MapInt_Double&      RawProperty() { return m_RawProperty; }
    MapInt_Double&      CurrProperty() { return m_CurrProperty; } 

    IManager*           GetManager() { return m_Manager; }
    IMatch*             GetMatch() { return m_Match; }

    Side                GetSide() { return m_Side; }

    IPlayerStatus*      Status() { return m_Status; }

    vector<IPlayer*>&   PassTargets() { return m_PassTargets; }
    vector<IPlayer*>&   CarePlayers() { return m_CarePlayers; }

protected:

    bool ValidateImmunityAddDebuff();

private:

    void OnMemberInit();

    void InitStamina();

    void RefreshEnableDecide();
    
    void RefreshStamina();

    void RefreshSkill();

    void RefreshProperty(AbsBuff* buff);

    void RecoverProperty(AbsBuff* buff);

    void RefreshDebuffStatus();

    void RefreshModel();

    // Buff对应的事件函数
    void AddBuff(AddBuffEventArgs& eventArgs);

    void RemoveBuff(RemoveBuffEventArgs& eventArgs);

    void AddDebuff(AddDebuffEventArgs& eventArgs);

    void RemoveDebuff(RemoveDebuffEventArgs& eventArgs);

    //初始化变量
    void InitVariables();

private:
    
    /// 表示了球员的ID
    unsigned int m_Id;

    /// 表示了客户端唯一标示符
    int m_ClientId;

    /// 表示了球员的天生特性
    IPlayerAttribute* m_Attribute;

    /// 表示了球员的原属性
    MapInt_Double  m_RawProperty;

    /// 表示了球员的当前属性
    MapInt_Double  m_CurrProperty;

    /// 表示了对经理对象的引用
    IManager*   m_Manager;

    /// 表示了对比赛对象的引用
    IMatch*     m_Match;

    /// 表示了球员的当前是主队还是客队
    Side m_Side;

    /// 表示了球员的当前状态
    IPlayerStatus* m_Status;

    /// 表示了球员的传球目标列表
    vector<IPlayer*> m_PassTargets;

    /// 表示了球员的关注列表
    vector<IPlayer*> m_CarePlayers;

    /// Represents the players in current player's sight.
    vector<IPlayer*> m_InSightPlayers;

    MapIState_VectorIActionSkill    m_Skills;
    vector<IActionSkill*>           m_SkillReferences;
    MapInt_ISkillResult             m_SkillResults;

    vector<Process*>                m_Processes;

    // 设置对应的事件
    Delegate<Player, AddBuffEventArgs>          m_AddBuff_Method;
    Delegate<Player, AddDebuffEventArgs>        m_AddDebuff_Method;
    Delegate<Player, RemoveBuffEventArgs>       m_RemoveBuff_Method;
    Delegate<Player, RemoveDebuffEventArgs>     m_RemoveDebuff_Method;
};

#endif //__PLAYER_H__
