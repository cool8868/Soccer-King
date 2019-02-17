/********************************************************************************
 * 文件名：TransferEntity.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __TRANSFERENTITY_H__
#define __TRANSFERENTITY_H__

#include "../common/common.h"
#include "../common/String/String.h"
#include "../common/Enum/Position.h"
#include "../common/Package/packetbase.h"

class TransferManagerEntity;
class TransferPlayerEntity;

/// 表示了一个用来传递的比赛对象
class TransferMatchEntity
{
public:

    TransferMatchEntity();

    virtual ~TransferMatchEntity();

public:

    int                         MatchType() { return m_MatchType; }
    void                        SetMatchType(int type) { m_MatchType = type; }

    double                      Time() { return m_Time; }
    void                        SetTime(double time) { m_Time = time; }

    bool                        IsNotDraw() { return m_IsNotDraw; }
    void                        SetIsNotDraw(bool flag) { m_IsNotDraw = flag; }

    int                         MapId() { return m_MapId; }
    void                        SetMapId(int id) { m_MapId = id; }

    TransferManagerEntity*      HomeManager() { return m_HomeManager; }
    void                        SetHomeManager(TransferManagerEntity* entity) { m_HomeManager = entity; }

    TransferManagerEntity*      AwayManager() { return m_AwayManager; }
    void                        SetAwayManager(TransferManagerEntity* entity) { m_AwayManager = entity; }

    //解析数据
    void                        Deserialize(CUtlBuffer& buffer);
    void                        DeserializeFromFile(ifstream& reader);

protected:

    /// 表示了比赛的类型
    int m_MatchType;

    /// 表示了比赛的时长（分钟）
    double m_Time;

    /// 表示了比赛是否不能为平局
    bool m_IsNotDraw;

    /// 表示了地图编号
    int m_MapId;

    /// 表示了主队的经理对象
    TransferManagerEntity* m_HomeManager;

    /// 表示了客队的经理对象
    TransferManagerEntity* m_AwayManager;
};

class TransferPlayerEntity;

/// 表示了一个用来传递的经理对象
class TransferManagerEntity
{
public:

    TransferManagerEntity();

    virtual ~TransferManagerEntity();

public:

    unsigned int                    Id() { return m_Id; }
    void                            SetId(unsigned int id) { m_Id = id; } 

    string                          Name(){ return m_Name; }
    void                            SetName(string name) { m_Name = name; } 

    string                          SpellName() { return m_SpellName; }
    void                            SetSpellName(string name) { m_SpellName = name; }

    string                          Logo() { return m_Logo; }
    void                            SetLogo(string logo) { m_Logo = logo; }

    int                             FormationId() { return m_FormationId; }
    void                            SetFormationId(int formationId) { m_FormationId = formationId; }

    bool                            IsNoSp() { return m_IsNoSp; }
    void                            SetIsNoSp(bool flag) { m_IsNoSp = flag; }

    vector<TransferPlayerEntity*>&  Players() { return m_Players; }

    vector<string>&                 OpenerSkills() { return m_OpenerSkills; }
    void                            SetOpenerSkills(vector<string>& vl) { m_OpenerSkills = vl; }

    vector<string>&                 PassiveSkills() { return m_PassiveSkills; }
    void                            SetPassiveSkills(vector<string>& vl) { m_PassiveSkills = vl; }

    vector<string>&                 Wills() { return m_Wills; }
    void                            SetWills(vector<string>& vl) { m_Wills = vl; }

    vector<string>&                 Combos() { return m_Combos; }

    //解析经理数据
    void                            Deserialize(CUtlBuffer& buffer);
    void                            DeserializeFromFile(ifstream& reader);

protected:

    /// 表示了经理的Id
    unsigned int m_Id;

    /// 表示了经理的中文名
    string m_Name;

    /// 表示了经理的英文名
    string m_SpellName;

    /// 表示了经理的Logo
    string m_Logo;

    /// 表示了经理当前使用的阵型id
    int m_FormationId;

    /// 表示了经理是否需要SP
    bool m_IsNoSp;

    /// 表示了该经理的球员
    vector<TransferPlayerEntity*> m_Players;

    /// 表示了经理装备的Opener技能
    vector<string> m_OpenerSkills;

    /// 表示了经理的被动技能
    vector<string> m_PassiveSkills;

    /// 表示了经理的意志
    vector<string> m_Wills;

    /// 表示了场上的球员组合
    vector<string> m_Combos;
};

/// 表示了一个用来传递的球员对象
class TransferPlayerEntity
{
public:

    TransferPlayerEntity()
        : m_Speed(0.0f)
        , m_Shooting(0.0f)
        , m_FreeKick(0.0f)
        , m_Balance(0.0f)
        , m_Stamina(0.0f)
        , m_Strength(0.0f)
        , m_Aggression(0.0f)
        , m_Disturb(0.0f)
        , m_Interception(0.0f)
        , m_Dribble(0.0f)
        , m_Passing(0.0f)
        , m_Mentality(0.0f)
        , m_Reflexes(0.0f)
        , m_Positioning(0.0f)
        , m_Handling(0.0f)
        , m_Acceleration(0.0f)
    {

    }

    unsigned int    Id() { return m_Id; }
    void            SetId(unsigned int id) { m_Id = id; }
    
    unsigned int    Pid() { return m_Pid; }
    void            SetPid(unsigned int id) { m_Pid = id; }

    int             CardLevel() { return m_CardLevel; }
    void            SetCardLevel(int level) { m_CardLevel = level; }

    int             Strengthen() { return m_Strengthen; }
    void            SetStrengthen(int value) { m_Strengthen = value; }

    Position        GetPosition() { return m_Position; }
    void            SetPosition(Position pos) { m_Position = pos; }

    int             HeadStyle() { return m_HeadStyle; }
    void            SetHeadStyle(int style) { m_HeadStyle = style; }

    int             BodyStyle() { return m_BodyStyle; }
    void            SetBodyStyle(int style) { m_BodyStyle = style; }

    string          FirstName() { return m_FirstName; }
    void            SetFirstName(string name) { m_FirstName = name; } 

    string          FamilyName() { return m_FamilyName; }
    void            SetFamilyName(string name) { m_FamilyName = name; }
    
    double          Speed() { return m_Speed; }
    void            SetSpeed(double speed) { m_Speed = speed; }

    double          Shooting() { return m_Shooting; }
    void            SetShooting(double value) { m_Shooting = value; }

    double          FreeKick() { return m_FreeKick; }
    void            SetFreeKick(double value) { m_FreeKick = value; }

    double          Balance() { return m_Balance; }
    void            SetBalance(double value) { m_Balance = value; }

    double          Stamina() { return m_Stamina; }
    void            SetStamina(double value) { m_Stamina = value; }

    double          Strength() { return m_Strength; }
    void            SetStrength(double value) { m_Strength = value; }

    double          Aggression() { return m_Aggression; }
    void            SetAggression(double value) { m_Aggression = value; }

    double          Disturb() { return m_Disturb; }
    void            SetDisturb(double value) { m_Disturb = value; }

    double          Interception() { return m_Interception; }
    void            SetInterception(double value) { m_Interception = value; }

    double          Dribble() { return m_Dribble; }
    void            SetDribble(double value) { m_Dribble = value; } 

    double          Passing() { return m_Passing; }
    void            SetPassing(double value) {  m_Passing = value; }

    double          Mentality() { return m_Mentality; }
    void            SetMentality(double value) { m_Mentality = value; }

    double          Reflexes() { return m_Reflexes; }
    void            SetReflexes(double value) { m_Reflexes = value; }

    double          Positioning() { return m_Positioning; }
    void            SetPositioning(double value) { m_Positioning = value; }

    double          Handling() { return m_Handling; }
    void            SetHandling(double value) { m_Handling = value; }

    double          Acceleration() { return m_Acceleration; }
    void            SetAcceleration(double value) { m_Acceleration = value; }

    vector<string>& Skills() { return m_Skills; }
    void            SetSkills(vector<string>& vl) { m_Skills = vl; }

    //解析球员的数据
    void            Deserialize(CUtlBuffer& buffer);
    void            DeserializeFromFile(ifstream& reader);

protected:

    /// 表示了球员的Id.
    unsigned int m_Id;

    /// 表示了球员在卡库中的编号
    unsigned int m_Pid;

    /// 表示了卡的级别
    int m_CardLevel;

    /// 表示了球员的加成
    int m_Strengthen;

    /// 表示了球员的场上位置
    Position m_Position;


    /// 表示了球员的头造型
    int m_HeadStyle;

    /// 表示了球员的身体造型
    int m_BodyStyle;

    /// 表示了球员的名字
    string m_FirstName;

    /// 表示了球员的姓
    string m_FamilyName;

    /// 速度
    double m_Speed;

    /// 射门
    double m_Shooting;

    /// 任意球
    double m_FreeKick;

    /// 控制
    double m_Balance;

    /// 体能
    double m_Stamina;

    /// 力量
    double m_Strength;

    /// 侵略性
    double m_Aggression;

    /// 干扰
    double m_Disturb;

    /// 抢断
    double m_Interception;

    /// 控球
    double m_Dribble;

    /// 传球
    double m_Passing;

    /// 意识
    double m_Mentality;

    /// 反应
    double m_Reflexes;

    /// 位置感
    double m_Positioning;

    /// 手控球
    double m_Handling;

    /// 加速度
    double m_Acceleration;

    /// 表示了球员装备的技能
    vector<string> m_Skills;
};

#endif //__TRANSFERENTITY_H__
