/********************************************************************************
 * 文件名：ResultEntity.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ResultEntity.h"
#include "../common/Utility.h"

ResultEntity::ResultEntity()
    : m_Packet(0)
{
    InitVariables();
}

ResultEntity::ResultEntity(int homeScore, int awayScore, IMatch* match)
    : m_Packet(0)
{
    InitVariables();

    m_HomeScore     = homeScore;
    m_AwayScore     = awayScore;

    if (match != NULL)
    {
        m_MatchEntity = CreateMatch(match);
    }
}

ResultEntity::ResultEntity(IMatch* match)
    : m_Packet(0)
{
    InitVariables();

    Attach(match);
}

ResultEntity::~ResultEntity()
{
    m_FileStream.close();

    DeletePtrSafe(m_MatchEntity);
}

void ResultEntity::Attach(IMatch* match)
{
    if (match == NULL)
    {
        return;
    }

    CreateMatch(match);

    m_HomeScore     = match->HomeScore();
    m_AwayScore     = match->AwayScore();
}

OutputMatchEntity* ResultEntity::CreateMatch(IMatch* match)
{
    m_MatchEntity = new OutputMatchEntity();

    m_MatchEntity->SetMatchProcesses(match->Processes());
    m_MatchEntity->SetFootballProcesses(match->GetFootball()->Processes());

    m_MatchEntity->SetMapId(match->MapId());
    m_MatchEntity->SetHomeScore(match->HomeScore());
    m_MatchEntity->SetAwayScore(match->AwayScore());
    m_MatchEntity->SetMatchType(match->MatchType());
    m_MatchEntity->SetMatchTime(90);

    m_MatchEntity->SetHomeManager(ParseManager(match->HomeManager()));
    m_MatchEntity->SetAwayManager(ParseManager(match->AwayManager()));

    m_MatchEntity->SetHomeLogo(match->HomeManager()->Logo());
    m_MatchEntity->SetAwayLogo(match->AwayManager()->Logo());

    return m_MatchEntity;
}

OutputManagerEntity* ResultEntity::ParseManager(IManager* manager)
{
    OutputManagerEntity* entity = new OutputManagerEntity();

    entity->SetId(manager->Id());
    entity->SetName(manager->Name());
    entity->SetFormationId(manager->FormationId());

    foreach (IPlayer* player, manager->Players())
    {
        entity->Players().push_back(ParsePlayer(player));
    }

    return entity;
}

OutputPlayerEntity* ResultEntity::ParsePlayer(IPlayer* player)
{
    OutputPlayerEntity* entity = new OutputPlayerEntity();
    entity->SetStrengthen(player->BuildinAttribute()->Strengthen());
    entity->SetPID(player->BuildinAttribute()->PID());
    entity->SetClientId(player->ClientId());
    entity->SetCardLevel(player->BuildinAttribute()->CardLevel());
    entity->SetFamilyName(player->BuildinAttribute()->FamilyName());
    entity->SetPosition(player->BuildinAttribute()->GetPosition());
    entity->SetProcesses(player->Processes());
    
    vector<OutputPlayerSkill*>& skillResults = entity->SkillResults();
    skillResults.reserve(player->SkillResults().size());
    skillResults.clear();

    foreach (MapInt_ISkillResult::value_type& skill, player->SkillResults())
    {
        skillResults.push_back(new OutputPlayerSkill(skill.second));
    }

    return entity;
}

void ResultEntity::InitVariables()
{
    m_MatchEntity           = NULL;
    m_ManagerEntity         = NULL;

    m_HomeScore             = 0;
    m_AwayScore             = 0;
}

void ResultEntity::Output(string fileName)
{
    m_FileStream.open(fileName.c_str(), ios::out | ios::binary);

    if (!m_MatchEntity || !m_FileStream)
    {
        return;
    }

    m_MatchEntity->Output(m_FileStream);

    //关闭文件
    m_FileStream.close();
}

void ResultEntity::OutputBuff(CUtlBuffer& writer)
{
    m_MatchEntity->Output(writer);
}

void ResultEntity::OutputBuff()
{
    CUtlBuffer& buffer = m_Packet.GetContentHandler();

    m_MatchEntity->Output(buffer);
}

//////////////////////////////////////////////////////////////////////////
OutputMatchEntity::OutputMatchEntity()
{
    InitVariables();
}

OutputMatchEntity::~OutputMatchEntity()
{
    DeletePtrSafe(m_HomeManager);
    DeletePtrSafe(m_AwayManager);

    m_MatchProcesses.clear();
    m_FootballProcesses.clear();
}

// 将当前对象的内容填充入二进制流中
void OutputMatchEntity::Output(CUtlBuffer& writer)
{
    writer.PutChar(Defines_System.OUTPUT_VERSION);
    writer.PutChar(m_MatchType);
    writer.PutChar(m_MapId);

    writer.PutChar(m_HomeScore);
    writer.PutChar(m_AwayScore);
    writer.PutUTF(m_HomeLogo.c_str());
    writer.PutUTF(m_AwayLogo.c_str());

    m_HomeManager->Output(writer);
    m_AwayManager->Output(writer);

    // football process
    writer.PutChar(m_FootballProcesses.size());
    writer.PutChar(m_FootballProcesses.size() >> 8);
    foreach (Process* p , m_FootballProcesses)
    {
        p->Output(writer);
    }

    // match process
    writer.PutChar(m_MatchProcesses.size());
    writer.PutChar(m_MatchProcesses.size() >> 8);
    foreach (Process* p, m_MatchProcesses)
    {
        p->Output(writer);
    }
}

void OutputMatchEntity::Output(ofstream& writer)
{
    writer.put(Defines_System.OUTPUT_VERSION);
    writer.put(m_MatchType);
    writer.put(m_MapId);

    writer.put(m_HomeScore);
    writer.put(m_AwayScore);

    Common::PutUTF(writer, m_HomeLogo);
    Common::PutUTF(writer, m_AwayLogo);

    m_HomeManager->Output(writer);
    m_AwayManager->Output(writer);

    // football process
    writer.put(m_FootballProcesses.size());
    writer.put(m_FootballProcesses.size() >> 8);
    foreach (Process* p , m_FootballProcesses)
    {
        p->Output(writer);
    }

    // match process
    writer.put(m_MatchProcesses.size());
    writer.put(m_MatchProcesses.size() >> 8);
    foreach (Process* p, m_MatchProcesses)
    {
        p->Output(writer);
    }
}

void OutputMatchEntity::InitVariables()
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
OutputManagerEntity::OutputManagerEntity()
{
    InitVariables();
}

OutputManagerEntity::~OutputManagerEntity()
{
    foreach (OutputPlayerEntity* player, m_Players)
    {
        DeletePtrSafe(player);
    }

    m_Players.clear();
}

/// 将当前对象的内容填充入二进制流中
void OutputManagerEntity::Output(CUtlBuffer& writer)
{
    writer.PutUTF(m_Name.c_str());
    writer.PutChar(m_FormationId);

    writer.PutChar(m_Players.size());
    foreach (OutputPlayerEntity* p, m_Players)
    {
        p->Output(writer);
    }
}

void OutputManagerEntity::Output(ofstream& writer)
{
    Common::PutUTF(writer, m_Name);
    writer.put(m_FormationId);

    writer.put(m_Players.size());
    foreach (OutputPlayerEntity* p, m_Players)
    {
        p->Output(writer);
    }
}

void OutputManagerEntity::InitVariables()
{
    m_Id            = 0;
    m_Name          = String::Empty();
    m_FormationId   = 0;

    m_Players.clear();
}

//////////////////////////////////////////////////////////////////////////
OutputPlayerEntity::OutputPlayerEntity()
{
    InitVariables();
}

OutputPlayerEntity::~OutputPlayerEntity()
{
    foreach (OutputPlayerEntity* skill, m_SkillResults)
    {
        DeletePtrSafe(skill);
    }
    m_SkillResults.clear();
}

void OutputPlayerEntity::Output(CUtlBuffer& writer)
{
    //
    // ClientId     0 - 13     4bits
    // CardLevel    0 - 7      4bits
    // Strengthen   0 - 9      4bits
    // PID          int32      4Bytes
    // Name
    // count        int16      2Bytes
    //
    writer.PutChar(m_ClientId);
    writer.PutChar(m_CardLevel << 4 | m_Strengthen);
    writer.PutUnsignedInt(m_PID);
    writer.PutUTF(m_FamilyName.c_str());
    writer.PutChar(m_Processes.size());
    writer.PutChar(m_Processes.size() >> 8);
    foreach (Process* p, m_Processes)
    {
        p->Output(writer);
    }

    writer.PutChar(m_SkillResults.size());
    writer.PutChar(m_SkillResults.size() >> 8);
    foreach (OutputPlayerSkill* skill, m_SkillResults)
    {
        skill->Output(writer);
    }
}

void OutputPlayerEntity::Output(ofstream& writer)
{
    //
    // ClientId     0 - 13     4bits
    // CardLevel    0 - 7      4bits
    // Strengthen   0 - 9      4bits
    // PID          int32      4Bytes
    // Name
    // count        int16      2Bytes
    //
    writer.put(m_ClientId);
    writer.put(m_CardLevel << 4 | m_Strengthen);
    writer.write((char*)&m_PID, sizeof(unsigned int));
    Common::PutUTF(writer, m_FamilyName);
    writer.put(m_Processes.size());
    writer.put(m_Processes.size() >> 8);
    foreach (Process* p, m_Processes)
    {
        p->Output(writer);
    }

    writer.put(m_SkillResults.size());
    writer.put(m_SkillResults.size() >> 8);
    foreach (OutputPlayerSkill* skill, m_SkillResults)
    {
        skill->Output(writer);
    }
}

void OutputPlayerEntity::InitVariables()
{
    m_FamilyName        = String::Empty();

    m_PID               = 0;
    m_ClientId          = 0;

    m_CardLevel         = 0;
    m_Strengthen        = 0;

    m_Processes.clear();
}

//////////////////////////////////////////////////////////////////////////
OutputPlayerSkill::OutputPlayerSkill()
{

}

OutputPlayerSkill::OutputPlayerSkill(ISkillResult* result)
{
    m_Round         = result->Round();
    m_SkillId       = result->SkillId();
    m_SkillName     = result->SkillName();
    m_SkillTargets  = result->SkillTargets();
}

void OutputPlayerSkill::Output(CUtlBuffer& writer)
{
    writer.PutChar(m_Round);
    writer.PutChar(m_Round >> 8);

    writer.PutUTF(m_SkillId);
    writer.PutUTF(m_SkillName);

    if (String::IsNullOrEmpty(m_SkillTargets.empty()))
    {
        writer.PutChar(0);
    }
    else
    {
        writer.PutUTF(m_SkillTargets);
    }
}

void OutputPlayerSkill::Output(ofstream& writer)
{
    writer.put(m_Round);
    writer.put(m_Round >> 8));

    Common::PutUTF(writer, m_SkillId);
    Common::PutUTF(writer, m_SkillName);

    if (String::IsNullOrEmpty(m_SkillTargets))
    {
        writer.put(0);
    }
    else
    {
        Common::PutUTF(writer, m_SkillTargets);
    }
}

