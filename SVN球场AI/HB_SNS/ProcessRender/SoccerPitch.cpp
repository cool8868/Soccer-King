/********************************************************************************
 * �ļ�����SoccerPitch.cpp
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "SoccerPitch.h"
#include "../Player/Player.h"
#include "../WindowUtils/DefineUtils.h"

#include "../common/misc/Cgdi.h"

SoccerPitch::SoccerPitch()
{
    InitVariables();

    //�򳡵�����
    m_Ori_PitchWidth            = Defines_Pitch.MAX_WIDTH;
    m_Ori_PitchHeight           = Defines_Pitch.MAX_HEIGHT;

    //���ŵ�����
    m_Ori_HomeGoal              = Coordinate::Parse(Defines_Pitch.HOME_GOAL);
    m_Ori_AwayGoal              = Coordinate::Parse(Defines_Pitch.AWAY_GOAL);

    //����
    m_Ori_Home_Penalty_Area     = Region::ParseByStr(Defines_Pitch.HOME_PENALTY_AREA);
    m_Ori_Away_Penalty_Area     = Region::ParseByStr(Defines_Pitch.AWAY_PENALTY_AREA);

    //���ŵĳ���
    m_Ori_GoalWidth             = 10;
    m_Ori_GoalHeight            = 16;

    ReSize();
}

SoccerPitch::~SoccerPitch()
{
    foreach (SoccerManager* p, m_Managers)
    {
        DeletePtrSafe(p);
    }

    m_Managers.clear();

    DeletePtrSafe(m_Football);
    DeletePtrSafe(m_SoccerMatch);
}

void SoccerPitch::Attach(IMatch* match)
{
    m_Match     = match;

    //���Ļغ���Ŀ
    m_MaxRound  = GetMaxRound();

    Attach();
}

void SoccerPitch::Attach(ParseMatchEntity* match)
{
    m_ParseMatch    = match;

    //���Ļغ���Ŀ
    m_MaxRound      = GetParserMaxRound();

    AttachParser();
}

void SoccerPitch::Attach()
{
    m_Managers.clear();

    foreach (IManager* m, m_Match->Managers())
    {
        SoccerManager* manager = new SoccerManager(this);

        manager->Attach(m);
        m_Managers.push_back(manager);
    }

    m_Football = new SoccerFootball(this);
    m_Football->Attach(m_Match);

    m_SoccerMatch = new SoccerMatch(this);
    m_SoccerMatch->Attach(m_Match);
}

void SoccerPitch::AttachParser()
{
    m_Managers.clear();

    foreach (ParseManagerEntity* m, m_ParseMatch->Managers())
    {
        SoccerManager* manager = new SoccerManager(this);

        manager->Attach(m);
        m_Managers.push_back(manager);
    }

    m_Football = new SoccerFootball(this);
    m_Football->Attach(m_ParseMatch);

    m_SoccerMatch   = new SoccerMatch(this);
    m_SoccerMatch->Attach(m_ParseMatch);
}

void SoccerPitch::RenderByPlayer()
{
    //�ݵ�
    gdi->DarkGreenPen();
    gdi->DarkGreenBrush();
    gdi->Rect(0, 0, (int)m_PitchRegion.End.X + PITCH_BOUNDS, (int)m_PitchRegion.End.Y + PITCH_BOUNDS);

    //�򳡱����
    gdi->WhitePen();
    gdi->Circle(m_PitchRegion.Center(), m_PitchWidth * 0.125);
    gdi->Line(m_PitchRegion.Center().X, m_PitchRegion.Top(), m_PitchRegion.Center().X, m_PitchRegion.Bottom());
    gdi->WhiteBrush();
    gdi->Circle(m_PitchRegion.Center(), 2.0);

    //�߽���
    gdi->HollowBrush();
    gdi->WhitePen();
    gdi->Rect(m_PitchRegion.Left(), m_PitchRegion.Top(), m_PitchRegion.Right(), m_PitchRegion.Bottom());

    //����
    gdi->HollowBrush();
    gdi->RedPen();
    gdi->Rect((int)m_HomeGoalRegion.Start.X, (int)m_HomeGoalRegion.Start.Y, (int)m_HomeGoalRegion.End.X, (int)m_HomeGoalRegion.End.Y);

    gdi->BluePen();
    gdi->Rect((int)m_AwayGoalRegion.Start.X, (int)m_AwayGoalRegion.Start.Y, (int)m_AwayGoalRegion.End.X, (int)m_AwayGoalRegion.End.Y);

    //����
    gdi->HollowBrush();
    gdi->RedPen();
    gdi->Rect((int)m_Home_Penalty_Area.Start.X, (int)m_Home_Penalty_Area.Start.Y, (int)m_Home_Penalty_Area.End.X, (int)m_Home_Penalty_Area.End.Y);

    gdi->HollowBrush();
    gdi->BluePen();
    gdi->Rect((int)m_Away_Penalty_Area.Start.X, (int)m_Away_Penalty_Area.Start.Y, (int)m_Away_Penalty_Area.End.X, (int)m_Away_Penalty_Area.End.Y);

    //�غ���
    gdi->TextColor(Cgdi::white);
    gdi->TextAtPos(m_PitchWidth / 2 - 50 + PITCH_BOUNDS, 10,"Round:  " + lexical_cast<string>(m_Match->Status()->Round()));

    //���
    foreach (SoccerManager* manager, m_Managers)
    {
        manager->Render();
    }

    //����
    m_Football->Render();

    //������Ϣ
    m_SoccerMatch->Render();
}

void SoccerPitch::RenderByProcess()
{
    //// �ݵ�
    gdi->DarkGreenPen();
    gdi->DarkGreenBrush();
    gdi->Rect(0, 0, (int)m_PitchRegion.End.X + PITCH_BOUNDS, (int)m_PitchRegion.End.Y + PITCH_BOUNDS);

    //// ����
    gdi->HollowBrush();
    gdi->RedPen();
    gdi->Rect((int)m_HomeGoalRegion.Start.X, (int)m_HomeGoalRegion.Start.Y, (int)m_HomeGoalRegion.End.X, (int)m_HomeGoalRegion.End.Y);

    gdi->BluePen();
    gdi->Rect((int)m_AwayGoalRegion.Start.X, (int)m_AwayGoalRegion.Start.Y, (int)m_AwayGoalRegion.End.X, (int)m_AwayGoalRegion.End.Y);

    //����
    gdi->HollowBrush();
    gdi->RedPen();
    gdi->Rect((int)m_Home_Penalty_Area.Start.X, (int)m_Home_Penalty_Area.Start.Y, (int)m_Home_Penalty_Area.End.X, (int)m_Home_Penalty_Area.End.Y);

    gdi->HollowBrush();
    gdi->BluePen();
    gdi->Rect((int)m_Away_Penalty_Area.Start.X, (int)m_Away_Penalty_Area.Start.Y, (int)m_Away_Penalty_Area.End.X, (int)m_Away_Penalty_Area.End.Y);

    //�򳡱����
    gdi->WhitePen();
    gdi->Circle(m_PitchRegion.Center(), m_PitchWidth * 0.125);
    gdi->Line(m_PitchRegion.Center().X, m_PitchRegion.Top(), m_PitchRegion.Center().X, m_PitchRegion.Bottom());
    gdi->WhiteBrush();
    gdi->Circle(m_PitchRegion.Center(), 2.0);

    //���
    foreach (SoccerManager* manager, m_Managers)
    {
        manager->Render(m_Round);
    }

    //����
    m_Football->Render(m_Round);

    //������Ϣ
    m_SoccerMatch->Render(m_Round);

    //�߽���
    gdi->HollowBrush();
    gdi->WhitePen();
    gdi->Rect(m_PitchRegion.Left(), m_PitchRegion.Top(), m_PitchRegion.Right(), m_PitchRegion.Bottom());

    //���ӿͶ���������

    //�غ���
    gdi->TextColor(Cgdi::white);
    gdi->TextAtPos(m_PitchWidth / 2 - 50 + PITCH_BOUNDS, 10,"Round:  " + lexical_cast<string>(m_Round));

    //����ʱ��
    gdi->TextColor(Cgdi::white);
    gdi->TextAtPos(m_PitchWidth / 2 + 50 + PITCH_BOUNDS, 10,"Time:  " + Common::TransDoubleToString((double)m_Round * 90 / m_MaxRound));
}

void SoccerPitch::RenderByParserProcess()
{
    //// �ݵ�
    gdi->DarkGreenPen();
    gdi->DarkGreenBrush();
    gdi->Rect(0, 0, (int)m_PitchRegion.End.X + PITCH_BOUNDS, (int)m_PitchRegion.End.Y + PITCH_BOUNDS);

    //// ����
    gdi->HollowBrush();
    gdi->RedPen();
    gdi->Rect((int)m_HomeGoalRegion.Start.X, (int)m_HomeGoalRegion.Start.Y, (int)m_HomeGoalRegion.End.X, (int)m_HomeGoalRegion.End.Y);

    gdi->BluePen();
    gdi->Rect((int)m_AwayGoalRegion.Start.X, (int)m_AwayGoalRegion.Start.Y, (int)m_AwayGoalRegion.End.X, (int)m_AwayGoalRegion.End.Y);

    //����
    gdi->HollowBrush();
    gdi->RedPen();
    gdi->Rect((int)m_Home_Penalty_Area.Start.X, (int)m_Home_Penalty_Area.Start.Y, (int)m_Home_Penalty_Area.End.X, (int)m_Home_Penalty_Area.End.Y);

    gdi->HollowBrush();
    gdi->BluePen();
    gdi->Rect((int)m_Away_Penalty_Area.Start.X, (int)m_Away_Penalty_Area.Start.Y, (int)m_Away_Penalty_Area.End.X, (int)m_Away_Penalty_Area.End.Y);

    //�򳡱����
    gdi->WhitePen();
    gdi->Circle(m_PitchRegion.Center(), m_PitchWidth * 0.125);
    gdi->Line(m_PitchRegion.Center().X, m_PitchRegion.Top(), m_PitchRegion.Center().X, m_PitchRegion.Bottom());
    gdi->WhiteBrush();
    gdi->Circle(m_PitchRegion.Center(), 2.0);

    //���
    foreach (SoccerManager* manager, m_Managers)
    {
        manager->RenderParser(m_Round);
    }

    //����
    m_Football->RenderParser(m_Round);

    //������Ϣ
    m_SoccerMatch->RenderParser(m_Round);

    //�߽���
    gdi->HollowBrush();
    gdi->WhitePen();
    gdi->Rect(m_PitchRegion.Left(), m_PitchRegion.Top(), m_PitchRegion.Right(), m_PitchRegion.Bottom());

    //���ӿͶ���������

    //�غ���
    gdi->TextColor(Cgdi::white);
    gdi->TextAtPos(m_PitchWidth / 2 - 50 + PITCH_BOUNDS, 10,"Round:  " + lexical_cast<string>(m_Round));

    //����ʱ��
    gdi->TextColor(Cgdi::white);
    gdi->TextAtPos(m_PitchWidth / 2 + 50 + PITCH_BOUNDS, 10,"Time:  " + Common::TransDoubleToString((double)m_Round * 90 / m_MaxRound));
}

int SoccerPitch::GetMaxRound()
{
    int round = 0;

    foreach (IManager* m, m_Match->Managers())
    {
        foreach (IPlayer* p, m->Players())
        {
            Player* player = static_cast<Player*>(p);

            if (player->Processes().size() > (size_t)round)
            {
                round = player->Processes().size();
            }
        }
    }

    return round;
}

int SoccerPitch::GetParserMaxRound()
{
    int round = 0;

    foreach (ParseManagerEntity* m, m_ParseMatch->Managers())
    {
        foreach (ParsePlayerEntity* p, m->Players())
        {
            if (p->Processes().size() > (size_t)round)
            {
                round = p->Processes().size();
            }
        }
    }

    return round;
}

void SoccerPitch::Restart()
{
    m_Round = 0;

    //��������Ϣ����
    m_SoccerMatch->Restart();
}

void SoccerPitch::Update()
{
    if (m_Round < m_MaxRound)
    {
        m_Round++;
    }
}

void SoccerPitch::InitVariables()
{
    m_Ori_PitchWidth        = 0;
    m_Ori_PitchHeight       = 0;

    m_Ori_GoalWidth         = 0;
    m_Ori_GoalHeight        = 0;

    m_PitchWidth            = 0;
    m_PitchHeight           = 0;

    m_GoalWidth             = 0;
    m_GoalHeight            = 0;

    m_Round                 = 0;
    m_MaxRound              = 0;

    m_Match                 = NULL;
    m_Football              = NULL;
    m_SoccerMatch           = NULL;
   
    m_Managers.clear();
}

void SoccerPitch::ReSize()
{
    //////////////////////////////////////////////////////////////////////////
    //ת����ʵ�ʵ�����
    m_PitchWidth                = static_cast<int>(m_Ori_PitchWidth * SCALE_SIZE);
    m_PitchHeight               = static_cast<int>(m_Ori_PitchHeight * SCALE_SIZE);

    m_PitchRegion.Start         = Coordinate(TransToWindow(0), TransToWindow(0));
    m_PitchRegion.End           = Coordinate(TransToWindow(m_Ori_PitchWidth), TransToWindow(m_Ori_PitchHeight));

    //���ŵ�����
    m_HomeGoal                  = Coordinate(TransToWindow(m_Ori_HomeGoal.X), TransToWindow(m_Ori_HomeGoal.Y));
    m_AwayGoal                  = Coordinate(TransToWindow(m_Ori_AwayGoal.X), TransToWindow(m_Ori_AwayGoal.Y));

    //����
    m_Home_Penalty_Area         = Region(TransToWindow(m_Ori_Home_Penalty_Area.Start.X),    TransToWindow(m_Ori_Home_Penalty_Area.Start.Y), 
                                         TransToWindow(m_Ori_Home_Penalty_Area.End.X),      TransToWindow(m_Ori_Home_Penalty_Area.End.Y));

    m_Away_Penalty_Area         = Region(TransToWindow(m_Ori_Away_Penalty_Area.Start.X),    TransToWindow(m_Ori_Away_Penalty_Area.Start.Y), 
                                         TransToWindow(m_Ori_Away_Penalty_Area.End.X),      TransToWindow(m_Ori_Away_Penalty_Area.End.Y));

    m_GoalWidth                 = static_cast<int>(m_Ori_GoalWidth * SCALE_SIZE);
    m_GoalHeight                = static_cast<int>(m_Ori_GoalHeight * SCALE_SIZE);

    m_HomeGoalRegion.Start      = Coordinate(m_HomeGoal.X,                  m_HomeGoal.Y - m_GoalHeight);
    m_HomeGoalRegion.End        = Coordinate(m_HomeGoal.X + m_GoalWidth,    m_HomeGoal.Y + m_GoalHeight);

    m_AwayGoalRegion.Start      = Coordinate(m_AwayGoal.X,                  m_AwayGoal.Y - m_GoalHeight);
    m_AwayGoalRegion.End        = Coordinate(m_AwayGoal.X - m_GoalWidth,    m_AwayGoal.Y + m_GoalHeight);
}
