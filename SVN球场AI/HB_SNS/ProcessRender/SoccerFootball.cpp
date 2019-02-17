/********************************************************************************
 * �ļ�����SoccerFootball.cpp
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "SoccerFootball.h"
#include "SoccerPitch.h"
#include "../MatchProcess/FootballProcess.h"
#include "../common/misc/Cgdi.h"
#include "../common/DisplayUtility.h"

SoccerFootball::SoccerFootball(SoccerPitch* sp)
    : m_SoccerPitch(sp)
{
    InitVariables();

    m_dBoundingRadius = 0.5 * SCALE_SIZE;

    m_Round = 0;
}

SoccerFootball::~SoccerFootball()
{
}

void SoccerFootball::Attach(IMatch* match)
{
    m_Football = match->GetFootball();
}

void SoccerFootball::Attach(ParseMatchEntity* match)
{
    m_FootballProcess = match->FootballProcesses();
}

void SoccerFootball::Render(size_t round)
{
    vector<Process*>& vec_process = m_Football->Processes();

    if (round < vec_process.size() && round != m_Round)
    {
        m_Round = round;
    }

    FootballProcess* process = static_cast<FootballProcess*>(vec_process[m_Round]);

    Coordinate coordinate2 = process->GetCoordinate2();
    Coordinate pos = TransCoordinate(coordinate2);

    if (DisPlayerController.FootBallPoint)
    {
        ////��ʾ���λ��
        format fmt("%.1f, %.1f");
        fmt % coordinate2.X % coordinate2.Y;
        gdi->TextColor(Cgdi::white);
        gdi->TextAtPos(pos.X - 20, pos.Y - 20, fmt.str());
    }

    //���Ƿ��ڿ���
    string footBall = "�����Ƿ��ڿ��У�";
    footBall += lexical_cast<string>(m_Football->IsInAir());
    gdi->TextColor(Cgdi::white);
    gdi->TextAtPos(PITCH_BOUNDS + m_SoccerPitch->m_PitchWidth / 4, 10, footBall);

    gdi->OrangePen();
    gdi->OrangeBrush();
    gdi->Circle(pos, m_dBoundingRadius + 2);

    gdi->BlackBrush();
    gdi->Circle(pos, m_dBoundingRadius);
}

void SoccerFootball::RenderParser(size_t round)
{
    vector<Process*>& vec_process = m_FootballProcess;

    if (round < vec_process.size() && round != m_Round)
    {
        m_Round = round;
    }

    FootballProcess* process = static_cast<FootballProcess*>(vec_process[m_Round]);

    Coordinate coordinate2 = process->GetCoordinate2();
    Coordinate pos = TransCoordinate(coordinate2);

    if (DisPlayerController.FootBallPoint)
    {
        ////��ʾ���λ��
        format fmt("%.1f, %.1f");
        fmt % coordinate2.X % coordinate2.Y;
        gdi->TextColor(Cgdi::white);
        gdi->TextAtPos(pos.X - 20, pos.Y - 20, fmt.str());
    }

    //���Ƿ��ڿ���
    string footBall = "�����Ƿ��ڿ��У�";
    footBall += lexical_cast<string>(process->IsInAir());
    gdi->TextColor(Cgdi::white);
    gdi->TextAtPos(PITCH_BOUNDS + m_SoccerPitch->m_PitchWidth / 4, 10, footBall);

    gdi->OrangePen();
    gdi->OrangeBrush();
    gdi->Circle(pos, m_dBoundingRadius + 2);

    gdi->BlackBrush();
    gdi->Circle(pos, m_dBoundingRadius);
}

void SoccerFootball::Render()
{
    Coordinate coordinate2 = m_Football->Current();
    Coordinate pos = TransCoordinate(coordinate2);

    if (DisPlayerController.FootBallPoint)
    {
        ////��ʾ���λ��
        format fmt("%.1f, %.1f");
        fmt % coordinate2.X % coordinate2.Y;
        gdi->TextColor(Cgdi::white);
        gdi->TextAtPos(pos.X - 20, pos.Y - 20, fmt.str());
    }

    //���Ƿ��ڿ���
    string footBall = "�����Ƿ��ڿ��У�";
    footBall += lexical_cast<string>(m_Football->IsInAir());
    gdi->TextColor(Cgdi::white);
    gdi->TextAtPos(PITCH_BOUNDS + m_SoccerPitch->m_PitchWidth / 4, 10, footBall);

    gdi->OrangePen();
    gdi->OrangeBrush();
    gdi->Circle(pos, m_dBoundingRadius + 2);

    gdi->BlackBrush();
    gdi->Circle(pos, m_dBoundingRadius);
}

void SoccerFootball::InitVariables()
{
    m_dBoundingRadius   = 0.0f;
    m_Round             = 0;

    m_Football          = NULL;

    m_FootballProcess.clear();
}
