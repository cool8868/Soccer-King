/********************************************************************************
 * 文件名：ResultEntity.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __RESULTENTITY_H__
#define __RESULTENTITY_H__

#include "../Interface/IMatch.h"
#include "../Interface/Player/Property/IPlayerAttribute.h"
#include "../Interface/IBinaryOutput.h"

#include "../common/Package/packetbase.h"

#include "../MatchProcess/Process.h"


class OutputMatchEntity;
class OutputManagerEntity;
class OutputPlayerEntity;

/// 表示了比赛服务的输出返回结果
class ResultEntity
{
public:

    ResultEntity();

    ResultEntity(int homeScore, int awayScore, IMatch* match);

    ResultEntity(IMatch* match);

    ~ResultEntity();

    void                    Attach(IMatch* match);

    void                    Output(string fileName);
    void                    OutputBuff();
    void                    OutputBuff(CUtlBuffer& writer);

public:

    /// 表示了输出的返回结果
    OutputMatchEntity*      GetMatchEntity() { return m_MatchEntity; }
    void                    SetMatchEntity(OutputMatchEntity* entity) { m_MatchEntity = entity; }

    // 获取比赛的内存数据
    PacketBase&             GetMatchPacket() { return m_Packet; }

private:

    /// Create a entity for output.
    OutputMatchEntity*      CreateMatch(IMatch* match);

    OutputManagerEntity*    ParseManager(IManager* manager);

    OutputPlayerEntity*     ParsePlayer(IPlayer* player);

    //初始化变量
    void                    InitVariables();

protected:

    /// 表示了输出的返回结果
    OutputMatchEntity*      m_MatchEntity;

    int                     m_HomeScore;
    int                     m_AwayScore;

private:

    OutputManagerEntity*    m_ManagerEntity;

    ofstream                m_FileStream;

    PacketBase              m_Packet;
};

/// 表示了一个用来输出的比赛对象
class OutputMatchEntity: public IBinaryOutput
{
public:

    OutputMatchEntity();

    ~OutputMatchEntity();

public:

    /// 表示了比赛的Id
    int                     MatchId() { return m_MatchId;}
    void                    SetMatchId(int id) { m_MatchId = id; }

    /// 表示了比赛类型
    int                     MatchType() { return m_MatchType; }
    void                    SetMatchType(int type) { m_MatchType = type; }

    /// 表示了地图的编号
    int                     MapId() { return m_MapId; }
    void                    SetMapId(int id) { m_MapId = id; }

    /// 表示了主队的比分
    int                     HomeScore() { m_HomeScore; }
    void                    SetHomeScore(int score) { m_HomeScore = score; }

    /// 表示了客队的比分
    int                     AwayScore() { return m_AwayScore; }
    void                    SetAwayScore(int score) { m_AwayScore = score; }

    /// 表示了主队的队标
    string                  HomeLogo() { return m_HomeLogo; }
    void                    SetHomeLogo(string logo) { m_HomeLogo = logo; }

    /// 表示了客队的队标
    string                  AwayLogo() { return m_AwayLogo; }
    void                    SetAwayLogo(string logo) { m_AwayLogo = logo; }

    /// 表示了比赛的时长
    int                     MatchTime() { return m_MatchTime; }
    void                    SetMatchTime(int time) { m_MatchTime = time; }

    /// 表示了主队的输出经理对象
    OutputManagerEntity*    HomeManager() { return m_HomeManager; }
    void                    SetHomeManager(OutputManagerEntity* manager) { m_HomeManager = manager; }

    /// 表示了客队的输出经理对象
    OutputManagerEntity*    AwayManager() { return m_AwayManager; }
    void                    SetAwayManager(OutputManagerEntity* manager) { m_AwayManager = manager; }

    /// 表示了比赛的过程
    vector<Process*>&       MatchProcesses() { return m_MatchProcesses; }
    void                    SetMatchProcesses(vector<Process*>& process) { m_MatchProcesses =  process; }

    /// 表示了足球的过程
    vector<Process*>&       FootballProcesses() { return m_FootballProcesses; }
    void                    SetFootballProcesses(vector<Process*>& process) { m_FootballProcesses = process; }

    /// 将当前对象的内容填充入二进制流中
    void                    Output(CUtlBuffer& writer);

    // 将当前数据输出到文件流中
    void                    Output(ofstream& writer);

private:

    //初始化变量
    void                    InitVariables();

protected:

    /// 表示了比赛的Id
    int m_MatchId;

    /// 表示了比赛类型
    int m_MatchType;

    /// 表示了地图的编号
    int m_MapId;

    /// 表示了主队的比分
    int m_HomeScore;

    /// 表示了客队的比分
    int m_AwayScore;

    /// 表示了主队的队标
    string m_HomeLogo;

    /// 表示了客队的队标
    string m_AwayLogo;

    /// 表示了比赛的时长
    int m_MatchTime;

    /// 表示了主队的输出经理对象
    OutputManagerEntity* m_HomeManager;

    /// 表示了客队的输出经理对象
    OutputManagerEntity* m_AwayManager;

    /// 表示了比赛的过程
    vector<Process*> m_MatchProcesses;

    /// 表示了足球的过程
    vector<Process*> m_FootballProcesses;
};

/// 表示了经理的输出对象
class OutputManagerEntity: public IBinaryOutput
{
public:

    OutputManagerEntity();

    ~OutputManagerEntity();

public:

    /// 表示了经理的ID
    unsigned int                    Id() { return m_Id; }
    void                            SetId(unsigned int id) { m_Id = id; }

    /// 表示了经理的名称
    string                          Name() { return m_Name; }
    void                            SetName(string name) { m_Name = name; }

    /// 表示了阵型id
    int                             FormationId() { return m_FormationId; }
    void                            SetFormationId(int id) { m_FormationId = id; }

    /// 表示了输出的球员对象集合
    vector<OutputPlayerEntity*>&    Players() { return m_Players; }
    void                            SetPlayers(vector<OutputPlayerEntity*> entity) { m_Players = entity; }

    /// 将当前对象的内容填充入二进制流中
    void                            Output(CUtlBuffer& writer);
    void                            Output(ofstream& writer);

private:

    //初始化变量
    void                            InitVariables();

protected:

    /// 表示了经理的ID
    unsigned int                    m_Id;

    /// 表示了经理的名称
    string                          m_Name;

    /// 表示了阵型id
    int                             m_FormationId;

    /// 表示了输出的球员对象集合
    vector<OutputPlayerEntity*>     m_Players;
};

/// 表示了球员的输出对象
class OutputPlayerEntity: public IBinaryOutput
{
public:

    OutputPlayerEntity();

    ~OutputPlayerEntity();

public:

    /// 表示了球员的性名
    string                      FamilyName() { return m_FamilyName; }
    void                        SetFamilyName(string name) { m_FamilyName = name; }

    /// 表示了球员的PID.
    unsigned int                PID() { return m_PID; }
    void                        SetPID(unsigned int id) { m_PID = id; }

    /// 表示了球员的客户端ID.
    int                         ClientId() { return m_ClientId; }
    void                        SetClientId(int id) { m_ClientId = id; }

    /// 表示了球员的卡级别.
    int                         CardLevel() { return m_CardLevel; }
    void                        SetCardLevel(int level) { m_CardLevel = level; }

    /// 表示了球员的卡加成
    int                         Strengthen() { return m_Strengthen; }
    void                        SetStrengthen(int value) { m_Strengthen = value; }

    /// 表示了球员的场上位置
    Position                    GetPosition() { return m_Position; }
    void                        SetPosition(Position pos) { m_Position = pos; } 

    /// 表示了球员的比赛过程
    vector<Process*>&           Processes() { return m_Processes; }
    void                        SetProcesses(vector<Process*>& process) { m_Processes = process; }

    /// 表示了技能的触发结果
    vector<OutputPlayerSkill*>& SkillResults() { return m_SkillResults; }
    void                        SetSkillResults(vector<OutputPlayerSkill*>& vl) { m_SkillResults = vl; }

    /// 将当前对象的内容填充入二进制流中
    void                        Output(CUtlBuffer& writer);
    void                        Output(ofstream& writer);

private:

    //初始化变量
    void                        InitVariables();

protected:

    /// 表示了球员的性名
    string              m_FamilyName;

    /// 表示了球员的PID.
    unsigned int        m_PID;

    /// 表示了球员的客户端ID.
    int                 m_ClientId;

    /// 表示了球员的卡级别.
    int                 m_CardLevel;

    /// 表示了球员的卡加成
    int                 m_Strengthen;

    /// 表示了球员的场上位置
    Position            m_Position;

    /// 表示了球员的比赛过程
    vector<Process*>    m_Processes;

    /// 表示了技能的触发结果
    vector<OutputPlayerSkill*> m_SkillResults;

};

/// 表示了球员触发的技能结果
class OutputPlayerSkill : public IBinaryOutput
{
public:

    OutputPlayerSkill();

    OutputPlayerSkill(ISkillResult* result);

public:

    int     Round() { return m_Round; }
    void    SetRound(int vl) { m_Round = vl; }

    string  SkillId() { return m_SkillId; }
    void    SetSkillId(string vl) { m_SkillId = vl; }

    string  SkillName() { return m_SkillName; }
    void    SetSkillName(string vl) { m_SkillName = vl; }

    string  SkillTargets() { return m_SkillTargets; }
    void    SetSkillTargets(string vl) { m_SkillTargets = vl; }

    /// 将当前对象的内容填充入二进制流中
    void    Output(CUtlBuffer& writer);
    void    Output(ofstream& writer);

protected:

    /// 表示了技能触发的回合
    int     m_Round;

    /// 表示了触发的技能的ID
    string  m_SkillId;

    /// 表示了触发的技能的名称
    string  m_SkillName;

    /// 表示了技能发动的目标（目标为目标球员的client id）
    string  m_SkillTargets;
};

#endif //__RESULTENTITY_H__
