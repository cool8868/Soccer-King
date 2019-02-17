/********************************************************************************
 * 文件名：ParseEntity.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef _PARSEENTITY_H__
#define _PARSEENTITY_H__

#include "../Interface/IMatch.h"
#include "../Interface/Player/Property/IPlayerAttribute.h"
#include "../Interface/IBinaryReadIn.h"

#include "../common/Package/packetbase.h"

#include "../MatchProcess/Process.h"


class ParseMatchEntity;
class ParseManagerEntity;
class ParsePlayerEntity;
class FootballProcess;

/// 表示了比赛服务的输出返回结果
class ParseEntity
{
public:

    ParseEntity();

    ~ParseEntity();

    ParseMatchEntity*           Attach(string fileName);

    //挂接到buff上
    ParseMatchEntity*           Attach(PacketBase& packet);

    //将结果输出到用户文本中
    void                        TranslateOutput(string fileName);

public:

    /// 表示了输出的返回结果
    ParseMatchEntity*           GetMatchEntity() { return m_MatchEntity; }
    void                        SetMatchEntity(ParseMatchEntity* entity) { m_MatchEntity = entity; }

private:

    /// Create a entity for output.
    ParseMatchEntity*           ParseMatch(ifstream& reader);

    ParseMatchEntity*           ParseMatch(CUtlBuffer& buff);

    //将数据导出到文件中
    void                        Translate();

    //初始化变量
    void                        InitVariables();

protected:

    /// 表示了输出的返回结果
    ParseMatchEntity*      m_MatchEntity;

    int                     m_HomeScore;
    int                     m_AwayScore;

private:

    ParseManagerEntity*    m_ManagerEntity;

    ifstream                m_FileStream;

    //用户需求数据
    ofstream                m_TranslateFileStream;
};

/// 表示了一个用来输出的比赛对象
class ParseMatchEntity: public IBinaryReadIn
{
public:

    ParseMatchEntity(ParseEntity* entity);

    ~ParseMatchEntity();

public:

    /// 表示了比赛的Id
    int                             MatchId() { return m_MatchId;}
    void                            SetMatchId(int id) { m_MatchId = id; }

    /// 表示了比赛类型
    int                             MatchType() { return m_MatchType; }
    void                            SetMatchType(int type) { m_MatchType = type; }

    /// 表示了地图的编号
    int                             MapId() { return m_MapId; }
    void                            SetMapId(int id) { m_MapId = id; }

    /// 表示了主队的比分
    int                             HomeScore() { return m_HomeScore; }
    void                            SetHomeScore(int score) { m_HomeScore = score; }

    /// 表示了客队的比分
    int                             AwayScore() { return m_AwayScore; }
    void                            SetAwayScore(int score) { m_AwayScore = score; }

    /// 表示了主队的队标
    string                          HomeLogo() { return m_HomeLogo; }
    void                            SetHomeLogo(string logo) { m_HomeLogo = logo; }

    /// 表示了客队的队标
    string                          AwayLogo() { return m_AwayLogo; }
    void                            SetAwayLogo(string logo) { m_AwayLogo = logo; }

    /// 表示了比赛的时长
    int                             MatchTime() { return m_MatchTime; }
    void                            SetMatchTime(int time) { m_MatchTime = time; }

    /// 表示了主队的输出经理对象    
    ParseManagerEntity*             HomeManager() { return m_HomeManager; }
    void                            SetHomeManager(ParseManagerEntity* manager) { m_HomeManager = manager; }

    /// 表示了客队的输出经理对象    
    ParseManagerEntity*             AwayManager() { return m_AwayManager; }
    void                            SetAwayManager(ParseManagerEntity* manager) { m_AwayManager = manager; }

    vector<ParseManagerEntity*>&    Managers() { return m_Managers; }

    /// 表示了比赛的过程
    vector<Process*>&               MatchProcesses() { return m_MatchProcesses; }
    void                            SetMatchProcesses(vector<Process*>& process) { m_MatchProcesses =  process; }

    /// 表示了足球的过程
    vector<Process*>&               FootballProcesses() { return m_FootballProcesses; }
    void                            SetFootballProcesses(vector<Process*>& process) { m_FootballProcesses = process; }

    // 将当前数据输出到文件流中
    void                            Read(ifstream& reader);

    // 将当前的内存数据输出
    void                            Read(CUtlBuffer& reader);

    void                            Translate(ofstream& writer);

private:

    //初始化变量
    void                            InitVariables();

    //输出足球的数据
    void                            TranslateFootball(ofstream& writer);

    //单回合的足球数据输出
    void                            TranslateFootballProcess(FootballProcess* process, ofstream& writer);

protected:

    //版本号
    int m_Version;

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
    ParseManagerEntity* m_HomeManager;

    /// 表示了客队的输出经理对象
    ParseManagerEntity* m_AwayManager;

    /// 表示了比赛的过程
    vector<Process*> m_MatchProcesses;

    /// 表示了足球的过程
    vector<Process*> m_FootballProcesses;

    // 比赛的经理对象
    vector<ParseManagerEntity*> m_Managers;

    //结果的实体
    ParseEntity*    m_ParseEntity;
};

/// 表示了经理的输出对象
class ParseManagerEntity: public IBinaryReadIn
{
public:

    ParseManagerEntity();

    ~ParseManagerEntity();

public:

    /// 表示了经理的ID
    unsigned int                Id() { return m_Id; }
    void                        SetId(unsigned int id) { m_Id = id; }

    /// 表示了经理的名称
    string                      Name() { return m_Name; }
    void                        SetName(string name) { m_Name = name; }

    /// 表示了阵型id
    int                         FormationId() { return m_FormationId; }
    void                        SetFormationId(int id) { m_FormationId = id; }

    /// 表示了输出的球员对象集合
    vector<ParsePlayerEntity*>& Players() { return m_Players; }
    void                        SetPlayers(vector<ParsePlayerEntity*> entity) { m_Players = entity; }

    /// 将当前对象的内容填充入二进制流中
    void                        Read(ifstream& reader);

    /// 从内存中读取数据
    void                        Read(CUtlBuffer& reader);

    /// 将比赛数据解析成用户可以看的数据
    void                        Translate(ofstream& writer);

private:

    //初始化变量
    void                        InitVariables();

protected:

    /// 表示了经理的ID
    unsigned int                m_Id;

    /// 表示了经理的名称
    string                      m_Name;

    /// 表示了阵型id
    int                         m_FormationId;

    /// 表示了输出的球员对象集合
    vector<ParsePlayerEntity*>  m_Players;
};

/// 表示了球员的输出对象
class ParsePlayerEntity: public IBinaryReadIn
{
public:

    ParsePlayerEntity();

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

    //  红蓝方
    Side                        GetSide() { return m_Side; }
    void                        SetSide(Side side) { m_Side = side; }

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

    /// 将当前对象的内容填充入二进制流中
    void                        Read(ifstream& reader);

    void                        Read(CUtlBuffer& reader);

private:

    //初始化变量
    void                        InitVariables();

protected:

    //普通球员记录
    void                        MakePlayerProcess(ifstream& reader, int round);
    void                        MakePlayerProcess(CUtlBuffer& reader, int roud);

    //守门员记录
    void                        MakeGoalKeeperProcess(ifstream& reader, int round);
    void                        MakeGoalKeeperProcess(CUtlBuffer& reader, int roud);

    //传球回合
    void                        MakePassProcess(ifstream& reader, int round);
    void                        MakePassProcess(CUtlBuffer& reader, int round);

    //射门回合数据
    void                        MakeShootProcess(ifstream& reader, int round);
    void                        MakeShootProcess(CUtlBuffer& reader, int round);

    /// 表示了球员的性名
    string          m_FamilyName;

    /// 表示了球员的PID.
    unsigned int    m_PID;

    /// 表示了球员的客户端ID.
    int             m_ClientId;

    //球队是哪一边的
    Side            m_Side;

    /// 表示了球员的卡级别.
    int             m_CardLevel;

    /// 表示了球员的卡加成
    int             m_Strengthen;

    /// 表示了球员的场上位置
    Position        m_Position;

    /// 表示了球员的比赛过程
    vector<Process*> m_Processes;
};

#endif //_PARSEENTITY_H__
