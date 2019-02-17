/********************************************************************************
 * �ļ�����Player.h
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
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

//���ຯ��ʵ��
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

    /// ��Ա��ʼ����������
    void Init();

    /// ���µ�һ�غϵ���ʱִ�г�ʼ��
    void RoundInit();

    void MinuteInit();

    /// �°볡��ʼʱ����
    void SecondHalfStart();

    /// ��ȡĿ�����ڵ�����
    Zone GetTargetZone(Coordinate target);

    /// ������Ա��Ŀ��
    void SetTarget(Coordinate target);

    /// ������Ա��Ŀ��
    void SetTarget(double x, double y);

    /// ����Ա��Ŀ������Ϊ��
    void SetTargetBall(bool isCurrent);

    /// ����һ���������Ա��
    /// �����ԱΪ���ӣ���ô���䣻
    /// �����ԱΪ�Ͷӣ����Զ��������ĶԳ��ٸ�ֵ.
    void SetHomeSideCoordinate(Coordinate target);

    /// �ж�һ�������Ƿ�����Ա֮��
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

    // Buff��Ӧ���¼�����
    void AddBuff(AddBuffEventArgs& eventArgs);

    void RemoveBuff(RemoveBuffEventArgs& eventArgs);

    void AddDebuff(AddDebuffEventArgs& eventArgs);

    void RemoveDebuff(RemoveDebuffEventArgs& eventArgs);

    //��ʼ������
    void InitVariables();

private:
    
    /// ��ʾ����Ա��ID
    unsigned int m_Id;

    /// ��ʾ�˿ͻ���Ψһ��ʾ��
    int m_ClientId;

    /// ��ʾ����Ա����������
    IPlayerAttribute* m_Attribute;

    /// ��ʾ����Ա��ԭ����
    MapInt_Double  m_RawProperty;

    /// ��ʾ����Ա�ĵ�ǰ����
    MapInt_Double  m_CurrProperty;

    /// ��ʾ�˶Ծ�����������
    IManager*   m_Manager;

    /// ��ʾ�˶Ա������������
    IMatch*     m_Match;

    /// ��ʾ����Ա�ĵ�ǰ�����ӻ��ǿͶ�
    Side m_Side;

    /// ��ʾ����Ա�ĵ�ǰ״̬
    IPlayerStatus* m_Status;

    /// ��ʾ����Ա�Ĵ���Ŀ���б�
    vector<IPlayer*> m_PassTargets;

    /// ��ʾ����Ա�Ĺ�ע�б�
    vector<IPlayer*> m_CarePlayers;

    /// Represents the players in current player's sight.
    vector<IPlayer*> m_InSightPlayers;

    MapIState_VectorIActionSkill    m_Skills;
    vector<IActionSkill*>           m_SkillReferences;
    MapInt_ISkillResult             m_SkillResults;

    vector<Process*>                m_Processes;

    // ���ö�Ӧ���¼�
    Delegate<Player, AddBuffEventArgs>          m_AddBuff_Method;
    Delegate<Player, AddDebuffEventArgs>        m_AddDebuff_Method;
    Delegate<Player, RemoveBuffEventArgs>       m_RemoveBuff_Method;
    Delegate<Player, RemoveDebuffEventArgs>     m_RemoveDebuff_Method;
};

#endif //__PLAYER_H__
