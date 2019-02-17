/********************************************************************************
 * 文件名：ParseEntity.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ParseEntity.h"
#include "../MatchProcess/FootballProcess.h"
#include "../MatchProcess/PassProcess.h"
#include "../MatchProcess/ShootProcess.h"
#include "../MatchProcess/MatchProcess.h"
#include "../MatchProcess/GoalkeeperProcess.h"
#include "../common/Utility.h"
#include "../common/DisplayUtility.h"
#include <iomanip>

ParseEntity::ParseEntity()
{
    InitVariables();
}

ParseEntity::~ParseEntity()
{
    m_FileStream.close();
    m_TranslateFileStream.close();

    DeletePtrSafe(m_MatchEntity);
}

ParseMatchEntity* ParseEntity::Attach(string fileName)
{
    m_FileStream.open(fileName, ios::in | ios::binary);

    if (!m_FileStream)
    {
        return (ParseMatchEntity*)NULL;
    }

    ParseMatchEntity* matchEntity = ParseMatch(m_FileStream);

    //读完后，关闭文件
    m_FileStream.close();

    //返回解析实体的指针
    return matchEntity;
}

ParseMatchEntity* ParseEntity::Attach(PacketBase& packet)
{
    CUtlBuffer& buffer = packet.GetContentHandler();

    return ParseMatch(buffer);
}

void ParseEntity::TranslateOutput(string fileName)
{
    m_TranslateFileStream.open(fileName, ios::out);

    if (!m_TranslateFileStream)
    {
        return;
    }

    Translate();

    //关闭文件
    m_TranslateFileStream.close();
}

ParseMatchEntity* ParseEntity::ParseMatch(ifstream& reader)
{
    m_MatchEntity = new ParseMatchEntity(this);

    m_MatchEntity->Read(reader);

    return m_MatchEntity;
}

ParseMatchEntity* ParseEntity::ParseMatch(CUtlBuffer& buff)
{
    m_MatchEntity = new ParseMatchEntity(this);

    m_MatchEntity->Read(buff);

    return m_MatchEntity;
}

void ParseEntity::Translate()
{
    if (!m_MatchEntity)
    {
        return;
    }

    m_MatchEntity->Translate(m_TranslateFileStream);
}

void ParseEntity::InitVariables()
{
    m_MatchEntity           = NULL;
    m_ManagerEntity         = NULL;

    m_HomeScore             = 0;
    m_AwayScore             = 0;
}

//////////////////////////////////////////////////////////////////////////
ParseMatchEntity::ParseMatchEntity(ParseEntity* entity)
{
    InitVariables();

    m_ParseEntity = entity;
}

ParseMatchEntity::~ParseMatchEntity()
{
    DeletePtrSafe(m_HomeManager);
    DeletePtrSafe(m_AwayManager);

    m_MatchProcesses.clear();
    m_FootballProcesses.clear();
}

void ParseMatchEntity::Read(ifstream& reader)
{
    m_Version       = reader.get() & 0xff;
    m_MatchType     = reader.get() & 0xff;
    m_MapId         = reader.get() & 0xff;

    m_HomeScore     = reader.get() & 0xff;
    m_AwayScore     = reader.get() & 0xff;

    m_HomeLogo      = Common::GetUTF(reader);
    m_AwayLogo      = Common::GetUTF(reader);

    //创建比赛经理对象
    m_HomeManager   = new ParseManagerEntity();
    m_AwayManager   = new ParseManagerEntity();

    m_Managers.push_back(m_HomeManager);
    m_Managers.push_back(m_AwayManager);

    m_HomeManager->Read(reader);
    m_AwayManager->Read(reader);

    // football process
    int length = reader.get() & 0xff;
    length = (reader.get() & 0xff) << 8 | length;
    for (int i = 0; i < length; ++i)
    {
        //读出id和round
        int id = 0;
        int round = 0;

        Common::ReadIdAndRound(reader, id, round);

        FootballProcess* process = new FootballProcess();
        process->SetRound(round);
        process->Read(reader);

        m_FootballProcesses.push_back(process);
    }

    // match process
    length = reader.get() & 0xff;
    length = (reader.get() & 0xff) << 8 | length;
    for (int i = 0; i < length; ++i)
    {
        //读出id和round
        int id      = 0;
        int round   = 0;
        Common::ReadIdAndRound(reader, id, round);

        MatchProcess* process = new MatchProcess();
        process->SetRound(round);
        process->Read(reader);

        m_MatchProcesses.push_back(process);
    }
}

void ParseMatchEntity::Read(CUtlBuffer& reader)
{
    m_Version       = reader.GetChar() & 0xff;
    m_MatchType     = reader.GetChar() & 0xff;
    m_MapId         = reader.GetChar() & 0xff;

    m_HomeScore     = reader.GetChar() & 0xff;
    m_AwayScore     = reader.GetChar() & 0xff;

    char str[64]    = {};
    reader.GetUTF(str, sizeof(str) - 1);
    m_HomeLogo      = str;

    memset(str, sizeof(str), 0);
    reader.GetUTF(str, sizeof(str) - 1);
    m_AwayLogo      = str;

    //创建比赛经理对象
    m_HomeManager   = new ParseManagerEntity();
    m_AwayManager   = new ParseManagerEntity();

    m_Managers.push_back(m_HomeManager);
    m_Managers.push_back(m_AwayManager);

    m_HomeManager->Read(reader);
    m_AwayManager->Read(reader);

    // football process
    int length = reader.GetChar() & 0xff;
    length = (reader.GetChar() & 0xff) << 8 | length;
    for (int i = 0; i < length; ++i)
    {
        //读出id和round
        int id = 0;
        int round = 0;

        Common::ReadIdAndRound(reader, id, round);

        FootballProcess* process = new FootballProcess();
        process->SetRound(round);
        process->Read(reader);

        m_FootballProcesses.push_back(process);
    }

    // match process
    length = reader.GetChar() & 0xff;
    length = (reader.GetChar() & 0xff) << 8 | length;
    for (int i = 0; i < length; ++i)
    {
        //读出id和round
        int id      = 0;
        int round   = 0;
        Common::ReadIdAndRound(reader, id, round);

        MatchProcess* process = new MatchProcess();
        process->SetRound(round);
        process->Read(reader);

        m_MatchProcesses.push_back(process);
    }
}

void ParseMatchEntity::Translate(ofstream& writer)
{
    TranslateFootball(writer);
}

void ParseMatchEntity::TranslateFootball(ofstream& writer)
{
    writer << "Football Total Round:" << m_FootballProcesses.size() << '\n';
    
    for (size_t i = 0; i < m_FootballProcesses.size(); ++i)
    {
        FootballProcess* process = static_cast<FootballProcess*>(m_FootballProcesses[i]);

        TranslateFootballProcess(process, writer);
    }
}

void ParseMatchEntity::TranslateFootballProcess(FootballProcess* process, ofstream& writer)
{
    writer  << "Round:" << setw(3) << setiosflags(ios::right) << process->Round()
            <<  setiosflags(ios::fixed) << setprecision(1)
            << "\tFootball Position:(" << setw(5) << setiosflags(ios::right) << process->GetCoordinate2().X << ", " << setw(5) << setiosflags(ios::right) << process->GetCoordinate2().Y << ")"
            << "\tDestination:(" << setw(5) << setiosflags(ios::right) << process->Destination2().X  << ", " <<  setw(5) << setiosflags(ios::right) << process->GetCoordinate2().Y << ")\n";
}

void ParseMatchEntity::InitVariables()
{
    m_MatchId       = 0;
    m_MatchType     = 0;
    m_MapId         = 0;

    m_HomeScore     = 0;
    m_AwayScore     = 0;

    m_HomeLogo      = String::Empty();
    m_AwayLogo      = String::Empty();

    m_MatchTime     = 0;

    m_HomeManager   = NULL;
    m_AwayManager   = NULL;

    m_MatchProcesses.clear();
    m_FootballProcesses.clear();
}
//////////////////////////////////////////////////////////////////////////
ParseManagerEntity::ParseManagerEntity()
{
    InitVariables();
}

ParseManagerEntity::~ParseManagerEntity()
{
    foreach (ParsePlayerEntity* player, m_Players)
    {
        DeletePtrSafe(player);
    }

    m_Players.clear();
}

void ParseManagerEntity::Read(ifstream& reader)
{
    m_Name          = Common::GetUTF(reader);

    m_FormationId   = reader.get() & 0xff;

    int size        = reader.get() & 0xff;

    for (int i = 0; i < size; ++i)
    {
        ParsePlayerEntity* p = new ParsePlayerEntity();

        p->Read(reader);

        //将p添加到玩家列表中
        m_Players.push_back(p);
    }
}

void ParseManagerEntity::Read(CUtlBuffer& reader)
{
    char str[64]    = {};
    reader.GetUTF(str, sizeof(str) - 1);
    m_Name          = str;

    m_FormationId   = reader.GetChar() & 0xff;

    int size        = reader.GetChar() & 0xff;

    for (int i = 0; i < size; ++i)
    {
        ParsePlayerEntity* p = new ParsePlayerEntity();

        p->Read(reader);

        //将p添加到玩家列表中
        m_Players.push_back(p);
    }
}

void ParseManagerEntity::Translate(ofstream& writer)
{

}

void ParseManagerEntity::InitVariables()
{
    m_Id            = 0;
    m_Name          = String::Empty();
    m_FormationId   = 0;

    m_Players.clear();
}

//////////////////////////////////////////////////////////////////////////
ParsePlayerEntity::ParsePlayerEntity()
{
    InitVariables();
}

void ParsePlayerEntity::Read(ifstream& reader)
{
    //
    // ClientId     0 - 13     4bits
    // CardLevel    0 - 7      4bits
    // Strengthen   0 - 9      4bits
    // PID          int32      4Bytes
    // Name
    // count        int16      2Bytes
    //
    m_ClientId      = reader.get() & 0xff;

    char value      = reader.get();
    m_CardLevel     = value >> 4 & 0x0f;
    m_Strengthen    = value & 0xf;

    reader.read((char*)&m_PID, sizeof(unsigned int));

    m_FamilyName    = Common::GetUTF(reader);

    //设定下是红蓝方
    m_Side = (m_ClientId < MyDefine_Player_Count) ? Side_Home : Side_Away;

    int lower_8     = reader.get() & 0xff;
    int heigh_8     = reader.get() & 0xff;
    int processSize = (heigh_8 & 0xff) << 8 | (lower_8 & 0xff);

    for (int i = 0; i < processSize; ++i)
    {
        int id      = 0;
        int round   = 0;
        Common::ReadIdAndRound(reader, id, round);

        switch (id)
        {
        case 0:
            {
                MakePlayerProcess(reader, round);
            }
            break;

        case 3:
            {
                MakeGoalKeeperProcess(reader, round);
            }
            break;

        case 4:
            {
                MakePassProcess(reader, round);
            }
            break;

        case 5:
            {
                MakeShootProcess(reader, round);
            }
            break;
        }
    }
}

void ParsePlayerEntity::Read(CUtlBuffer& reader)
{
    //
    // ClientId     0 - 13     4bits
    // CardLevel    0 - 7      4bits
    // Strengthen   0 - 9      4bits
    // PID          int32      4Bytes
    // Name
    // count        int16      2Bytes
    //
    m_ClientId      = reader.GetChar() & 0xff;

    char value      = reader.GetChar();
    m_CardLevel     = value >> 4 & 0x0f;
    m_Strengthen    = value & 0xf;

    m_PID           = reader.GetUnsignedInt();

    char str[64]    = {};
    reader.GetUTF(str, sizeof(str) - 1);
    m_FamilyName    = str;

    //设定下是红蓝方
    m_Side = (m_ClientId < MyDefine_Player_Count) ? Side_Home : Side_Away;

    int lower_8     = reader.GetChar() & 0xff;
    int heigh_8     = reader.GetChar() & 0xff;
    int processSize = (heigh_8 & 0xff) << 8 | (lower_8 & 0xff);

    for (int i = 0; i < processSize; ++i)
    {
        int id      = 0;
        int round   = 0;
        Common::ReadIdAndRound(reader, id, round);

        switch (id)
        {
        case 0:
            {
                MakePlayerProcess(reader, round);
            }
            break;

        case 3:
            {
                MakeGoalKeeperProcess(reader, round);
            }
            break;

        case 4:
            {
                MakePassProcess(reader, round);
            }
            break;

        case 5:
            {
                MakeShootProcess(reader, round);
            }
            break;
        }
    }
}

void ParsePlayerEntity::InitVariables()
{
    m_FamilyName        = String::Empty();

    m_PID               = 0;
    m_ClientId          = 0;

    m_CardLevel         = 0;
    m_Strengthen        = 0;

    m_Processes.clear();
}

void ParsePlayerEntity::MakePlayerProcess(ifstream& reader, int round)
{
    PlayerProcess* process = new PlayerProcess();

    process->SetRound(round);
    process->Read(reader);

    m_Processes.push_back(process);
}

void ParsePlayerEntity::MakePlayerProcess(CUtlBuffer& reader, int round)
{
    PlayerProcess* process = new PlayerProcess();

    process->SetRound(round);
    process->Read(reader);

    m_Processes.push_back(process);
}

void ParsePlayerEntity::MakeGoalKeeperProcess(ifstream& reader, int round)
{
    GoalkeeperProcess* process = new GoalkeeperProcess();

    process->SetRound(round);
    process->Read(reader);

    m_Processes.push_back(process);
}

void ParsePlayerEntity::MakeGoalKeeperProcess(CUtlBuffer& reader, int round)
{
    GoalkeeperProcess* process = new GoalkeeperProcess();

    process->SetRound(round);
    process->Read(reader);

    m_Processes.push_back(process);
}

void ParsePlayerEntity::MakePassProcess(ifstream& reader, int round)
{
    PassProcess* process = new PassProcess();

    process->SetRound(round);
    process->Read(reader);

    m_Processes.push_back(process);
}

void ParsePlayerEntity::MakePassProcess(CUtlBuffer& reader, int round)
{
    PassProcess* process = new PassProcess();

    process->SetRound(round);
    process->Read(reader);

    m_Processes.push_back(process);
}

void ParsePlayerEntity::MakeShootProcess(ifstream& reader, int round)
{
    ShootProcess* process = new ShootProcess();

    process->SetRound(round);
    process->Read(reader);

    m_Processes.push_back(process);
}

void ParsePlayerEntity::MakeShootProcess(CUtlBuffer& reader, int round)
{
    ShootProcess* process = new ShootProcess();

    process->SetRound(round);
    process->Read(reader);

    m_Processes.push_back(process);
}

