/********************************************************************************
 * 文件名：SoccerMatch.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "SoccerMatch.h"
#include "SoccerPitch.h"
#include "../MatchProcess/MatchProcess.h"
#include "../common/misc/Cgdi.h"
#include "../common/DisplayUtility.h"

SoccerMatch::SoccerMatch(SoccerPitch* sp)
    : m_SoccerPitch(sp)
{
    InitVariables();

    m_Round = 0;
}

SoccerMatch::~SoccerMatch()
{
}

void SoccerMatch::Attach(IMatch* match)
{
    m_Match = match;
}

void SoccerMatch::Attach(ParseMatchEntity* match)
{
    m_MatchProcess = match->MatchProcesses();
}

void SoccerMatch::Render(size_t round)
{
    vector<Process*>& vec_process = m_Match->Processes();

    foreach (Process* p, vec_process)
    {
        MatchProcess* process = static_cast<MatchProcess*>(p);

        if (process->Round() == round)
        {
            m_HomeScore = process->HomeScore();
            m_AwayScore = process->AwayScore();
        }
    }

    //比分
    gdi->TextColor(Cgdi::red);
    gdi->TextAtPos((m_SoccerPitch->m_PitchWidth / 2) - 10, m_SoccerPitch->m_PitchHeight + 50, "Red: " + lexical_cast<string>(m_HomeScore));

    gdi->TextColor(Cgdi::blue);
    gdi->TextAtPos((m_SoccerPitch->m_PitchWidth / 2) + 50, m_SoccerPitch->m_PitchHeight + 50, "Blue: " + lexical_cast<string>(m_AwayScore));
}

void SoccerMatch::RenderParser(size_t round)
{
    vector<Process*>& vec_process = m_MatchProcess;

    foreach (Process* p, vec_process)
    {
        MatchProcess* process = static_cast<MatchProcess*>(p);

        if (process->Round() == round)
        {
            m_HomeScore = process->HomeScore();
            m_AwayScore = process->AwayScore();
        }
    }

    //比分
    gdi->TextColor(Cgdi::red);
    gdi->TextAtPos((m_SoccerPitch->m_PitchWidth / 2) - 10, m_SoccerPitch->m_PitchHeight + 50, "Red: " + lexical_cast<string>(m_HomeScore));

    gdi->TextColor(Cgdi::blue);
    gdi->TextAtPos((m_SoccerPitch->m_PitchWidth / 2) + 50, m_SoccerPitch->m_PitchHeight + 50, "Blue: " + lexical_cast<string>(m_AwayScore));
}

void SoccerMatch::Render()
{
    //比分
    gdi->TextColor(Cgdi::red);
    gdi->TextAtPos((m_SoccerPitch->m_PitchWidth / 2) - 10, m_SoccerPitch->m_PitchHeight + 50, "Red: " + lexical_cast<string>(m_Match->HomeScore()));

    gdi->TextColor(Cgdi::blue);
    gdi->TextAtPos((m_SoccerPitch->m_PitchWidth / 2) + 50, m_SoccerPitch->m_PitchHeight + 50, "Blue: " + lexical_cast<string>(m_Match->AwayScore()));
}

void SoccerMatch::InitVariables()
{
    m_Round             = 0;

    m_Match             = NULL;

    m_HomeScore         = 0;
    
    m_AwayScore         = 0;

    m_MatchProcess.clear();
}

void SoccerMatch::Restart()
{
    m_Round         = 0;

    m_HomeScore     = 0;

    m_AwayScore     = 0;
}
