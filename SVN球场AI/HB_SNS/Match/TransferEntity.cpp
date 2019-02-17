/********************************************************************************
 * 文件名：TransferEntity.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "TransferEntity.h"
#include "../common/Defines/Defines.h"
#include "../common/Utility.h"

TransferMatchEntity::TransferMatchEntity()
    : m_HomeManager(NULL)
    , m_AwayManager(NULL)
    , m_Time(0.0f)
{
    m_HomeManager   = new TransferManagerEntity();
    m_AwayManager   = new TransferManagerEntity();
}

TransferMatchEntity::~TransferMatchEntity()
{
    DeletePtrSafe(m_HomeManager);
    DeletePtrSafe(m_AwayManager);
}

void TransferMatchEntity::Deserialize(CUtlBuffer& buffer)
{
    m_MapId         = buffer.GetInt();
    m_MatchType     = buffer.GetInt();
    m_Time          = buffer.GetInt();

    //解析主客队数据
    m_HomeManager->Deserialize(buffer);
    m_AwayManager->Deserialize(buffer);
}

void TransferMatchEntity::DeserializeFromFile(ifstream& reader)
{
    //跳过12个头
    for (int i = 0; i < 12; ++i)
    {
        int a = reader.get() & 0xff;
        int b = 0;
    }

    reader.read((char*)&m_MatchType, sizeof(int));

    int time = 0;
    reader.read((char*)&time, sizeof(int));
    m_Time = time;

    //解析主客队数据
    m_HomeManager->DeserializeFromFile(reader);
    m_AwayManager->DeserializeFromFile(reader);
}
//////////////////////////////////////////////////////////////////////////
TransferManagerEntity::TransferManagerEntity()
    : m_Players(Macro_PlayerCount)
{
    m_Name          = String::Empty();
    m_SpellName     = String::Empty();
    m_Logo          = String::Empty();

    m_Players.clear();
}

TransferManagerEntity::~TransferManagerEntity()
{
    foreach (TransferPlayerEntity* p, m_Players)
    {
        delete p;
    }

    m_Players.clear();
}

void TransferManagerEntity::Deserialize(CUtlBuffer& buffer)
{
    m_Id            = buffer.GetUnsignedInt();
    m_SpellName     = Common::GetUTF(buffer);
    m_Name          = Common::GetUTF(buffer);
    m_Logo          = Common::GetUTF(buffer);
    m_FormationId   = buffer.GetInt();

    int nCount      = buffer.GetInt();

    for (int i = 0; i < nCount; ++i)
    {
        TransferPlayerEntity* player = new TransferPlayerEntity();

        player->Deserialize(buffer);

        m_Players.push_back(player);
    }
}

void TransferManagerEntity::DeserializeFromFile(ifstream& reader)
{
    reader.read((char*)&m_Id, sizeof(unsigned int));
    m_SpellName     = Common::GetUTF(reader);
    m_Name          = Common::GetUTF(reader);
    m_Logo          = Common::GetUTF(reader);
    reader.read((char*)&m_FormationId, sizeof(int));

    int nCount      = 0;
    reader.read((char*)&nCount, sizeof(int));

    for (int i = 0; i < nCount; ++i)
    {
        TransferPlayerEntity* player = new TransferPlayerEntity();

        player->DeserializeFromFile(reader);

        m_Players.push_back(player);
    }
}

//////////////////////////////////////////////////////////////////////////
void TransferPlayerEntity::Deserialize(CUtlBuffer& buffer)
{
    m_Id            = buffer.GetUnsignedInt();
    m_Pid           = buffer.GetUnsignedInt();
    m_CardLevel     = buffer.GetInt();
    m_Strengthen    = buffer.GetInt();
    m_HeadStyle     = buffer.GetInt();
    m_BodyStyle     = buffer.GetInt();

    m_FirstName     = Common::GetUTF(buffer);
    m_FamilyName    = Common::GetUTF(buffer);

    m_Speed         = buffer.GetDouble();
    m_Shooting      = buffer.GetDouble();
    m_FreeKick      = buffer.GetDouble();
    m_Balance       = buffer.GetDouble();
    m_Stamina       = buffer.GetDouble();
    m_Strength      = buffer.GetDouble();
    m_Aggression    = buffer.GetDouble();
    m_Disturb       = buffer.GetDouble();
    m_Interception  = buffer.GetDouble();
    m_Dribble       = buffer.GetDouble();
    m_Passing       = buffer.GetDouble();
    m_Mentality     = buffer.GetDouble();
    m_Reflexes      = buffer.GetDouble();
    m_Positioning   = buffer.GetDouble();
    m_Handling      = buffer.GetDouble();
    m_Acceleration  = buffer.GetDouble();
}

void TransferPlayerEntity::DeserializeFromFile(ifstream& reader)
{
    reader.read((char*)&m_Id, sizeof(unsigned int));
    reader.read((char*)&m_Pid, sizeof(unsigned int));
    reader.read((char*)&m_CardLevel, sizeof(int));
    reader.read((char*)&m_Strengthen, sizeof(int));
    reader.read((char*)&m_HeadStyle, sizeof(int));
    reader.read((char*)&m_BodyStyle, sizeof(int));

    m_FirstName     = Common::GetUTF(reader);
    m_FamilyName    = Common::GetUTF(reader);

    reader.read((char*)&m_Speed, sizeof(double));
    reader.read((char*)&m_Shooting, sizeof(double));
    reader.read((char*)&m_FreeKick, sizeof(double));
    reader.read((char*)&m_Balance, sizeof(double));
    reader.read((char*)&m_Stamina, sizeof(double));
    reader.read((char*)&m_Strength, sizeof(double));
    reader.read((char*)&m_Aggression, sizeof(double));
    reader.read((char*)&m_Disturb, sizeof(double));
    reader.read((char*)&m_Interception, sizeof(double));
    reader.read((char*)&m_Dribble, sizeof(double));
    reader.read((char*)&m_Passing, sizeof(double));
    reader.read((char*)&m_Mentality, sizeof(double));
    reader.read((char*)&m_Reflexes, sizeof(double));
    reader.read((char*)&m_Positioning, sizeof(double));
    reader.read((char*)&m_Handling, sizeof(double));
    reader.read((char*)&m_Acceleration, sizeof(double));
}
