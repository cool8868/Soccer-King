/********************************************************************************
 * 文件名：SoccerManager.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "SoccerManager.h"

SoccerManager::SoccerManager(SoccerPitch* sp)
    : m_SoccerPitch(sp)
{
    InitVariables();
}

SoccerManager::~SoccerManager()
{
    foreach (SoccerPlayer* p, m_Players)
    {
        DeletePtrSafe(p);
    }

    m_Players.clear();
}

void SoccerManager::Attach(IManager* manager)
{
    m_Manager = manager;

    Attach();
}

void SoccerManager::Attach(ParseManagerEntity* manager)
{
    m_ParseManager = manager;

    AttachParser();
}

void SoccerManager::Attach()
{
    m_Players.clear();

    foreach (IPlayer* player, m_Manager->Players())
    {
        SoccerPlayer* p = new SoccerPlayer(m_SoccerPitch);

        p->Attach(player);
        m_Players.push_back(p);
    }
}

void SoccerManager::AttachParser()
{
    m_Players.clear();

    foreach (ParsePlayerEntity* player, m_ParseManager->Players())
    {
        SoccerPlayer* p = new SoccerPlayer(m_SoccerPitch);

        p->Attach(player);
        m_Players.push_back(p);
    }
}

void SoccerManager::Render()
{
    foreach (SoccerPlayer* p, m_Players)
    {
        p->Render();
    }
}

void SoccerManager::Render(size_t round)
{
    foreach (SoccerPlayer* p, m_Players)
    {
        p->Render(round);
    }
}

void SoccerManager::RenderParser(size_t round)
{
    foreach (SoccerPlayer* p, m_Players)
    {
        p->RenderParser(round);
    }
}

void SoccerManager::InitVariables()
{
    m_Players.clear();

    m_Manager       = NULL;
}
