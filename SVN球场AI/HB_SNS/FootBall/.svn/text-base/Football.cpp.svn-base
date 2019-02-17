/********************************************************************************
 * 文件名：Football.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "Football.h"
#include "../common/Defines/Defines.h"
#include "../MatchProcess/FootballProcess.h"

Football::Football()
    : m_Processes(Macro_PlayerProcess)
{
    InitVariables();

    m_Acceleration  = Defines_Pitch.BALL_ACCELERATION;
    m_Default       = Coordinate::Parse(Defines_Position.BALL_DEFAULT);
}

Football::~Football()
{
    foreach (Process* p, m_Processes)
    {
        DeletePtrSafe(p);
    }

    m_Processes.clear();
}

// 把球踢往某一点
void Football::Kick(Coordinate destination, double speed, IPlayer* lastMan)
{
    m_LastTouchMan  = lastMan;

    if (m_ForceInAir)
    {
        m_ForceInAir = false;
        
        Kick(destination, speed, 45, lastMan);

        return;
    }

    m_Destination           = destination;
    m_Speed                 = speed;
    m_Recalculate           = true;
    m_Angle                 = 0;
    m_IsInAir               = false;
    m_IsPassDestination     = false;
}

/// 将球踢往某一点（球是在空中的）
void Football::Kick(Coordinate destination, double speed, int angle, IPlayer* lastMan)
{
    m_LastTouchMan = lastMan;

    Kick(destination, speed, lastMan);

    m_IsInAir   = true;
    m_Angle     = angle;
}

void Football::Hide(int round)
{
    m_HideTime  = round;
}

bool Football::Visible()
{
    return (m_HideTime == 0);
}

void Football::ForceInAir()
{
    m_ForceInAir = true;
}

void Football::RoundInit()
{
    if (m_HideTime > 0)
    {
        m_HideTime--;
    }
}

// 物体根据自身的趋势移动
void Football::Move()
{
    LogHelper::Insert(str(format("[Football]Current:(%.2f, %.2f), destination:(%.2f, %.2f)") 
        % m_Current.X % m_Current.Y % m_Destination.X % m_Destination.Y), Color_Brown);

    if (m_Speed <= 0) // 防止球反向移动
    {
        ValidateOutBorder();
        return;
    }

    double d = m_Current.Distance(m_Destination); // 防止被除数为0
    if (d == 0 && m_Recalculate)
    {
        ValidateOutBorder();
        return;
    }

    if (m_Recalculate)
    {
        m_cosA = (m_Destination.X - m_Current.X) / d;
        m_sinA = (m_Destination.Y - m_Current.Y) / d;

        m_Recalculate = false;
    }

    double vx = m_Speed * m_cosA;
    double vy = m_Speed * m_sinA;

    double ax = m_Acceleration * m_cosA;
    double ay = m_Acceleration * m_sinA;

    double t = Defines_Match.ROUND_TIME;

    double x = m_Current.X + vx * t + 0.5 * ax * pow(t, 2);
    double y = m_Current.Y + vy * t + 0.5 * ay * pow(t, 2);            

    double nextDistance = m_Current.Distance(Coordinate(x, y));
    if (nextDistance > d)
    {
        m_IsPassDestination = true;
        m_IsInAir           = false;
    }

    m_Current.X = x;
    m_Current.Y = y;

    m_Speed = m_Speed + m_Acceleration * t;

    LogHelper::Insert(str(format("[Football]After moving:(%.2f, %.2f)") % m_Current.X, % m_Current.Y), Color_Brown);

    ValidateOutBorder();
}

// 保存当前回合的数据
void Football::Save(int round)
{
    if (m_Processes.size() == 0)
    {
        InternalSave(round);
    }
    else
    {
        if (m_Processes[m_Processes.size() - 1]->Round() != round)
        {
            InternalSave(round);
        }
    }
}

void Football::Save()
{
    throw NotSupportedException();
}

void Football::Reset()
{
    m_Current       = m_Default;
    m_Destination   = m_Default;
    m_Speed         = 0;
    m_Acceleration  = Defines_Pitch.BALL_ACCELERATION;
}

void Football::MoveTo(Coordinate target)
{
    m_Current       = target;
    m_Destination   = target;
    m_Speed         = 0;
    m_IsInAir       = false;

    LogHelper::Insert("[MoveTo]Football is move to " + target->Output(), Color_Green);
}

void Football::MoveTo(double x, double y)
{
    m_Current.X     = x;
    m_Current.Y     = y;

    m_Destination.X = x;
    m_Destination.Y = y;
    m_Speed         = 0;
    m_IsInAir       = false;

    LogHelper::Insert(str(format("[MoveTo]Football is move to %.2f, %.2f") % x % y), Color_Green);
}

// 内部保存方法
void Football::InternalSave(int round)
{
    FootballProcess* process = new FootballProcess();

    process->SetRound(round);
    process->SetCoordinate(m_Current.Output());
    process->SetCoordinate2(m_Current);
    process->SetAcceleration((int)m_Acceleration);
    process->SetDestination(m_Destination.Output());
    process->SetDestination2(m_Destination);
    process->SetAngle(m_Angle);
    process->SetIsInAir(m_IsInAir);

    m_Processes.push_back(process);

    m_Angle = 0;
}

void Football::ValidateOutBorder()
{
    if (m_LastTouchMan == NULL)
    {
        LogHelper::Insert("Validate Out the border with null last touch man.", LogType_Error);
        return;
    }

    //角球
    // 足球出了左侧底线
    if (m_Current.X < 0)
    {
        if (m_LastTouchMan->GetSide() == Side_Away)
        { 
            // 门将开门球  
            GKOpenball(Side_Home);            
            return;
        }
        else
        { 
            // 角球
            if (m_Current.Y < Defines_Pitch.MAX_HEIGHT / 2)
            {
                MoveTo(0, 0);
            }
            else
            {
                MoveTo(0, Defines_Pitch.MAX_HEIGHT);
            }
        }

        m_LastTouchMan->GetMatch()->OutBorder(m_LastTouchMan->GetManager());
        return;
    }

    // 足球出了右侧底线
    if (m_Current.X > Defines_Pitch.MAX_WIDTH)
    {
        if (m_LastTouchMan->GetSide() == Side_Home)
        { 
            // 门将开门球
            GKOpenball(Side_Away);
            return;
        }
        else
        { 
            // 角球
            if (m_Current.Y < Defines_Pitch.MAX_HEIGHT / 2)
            {
                MoveTo(Defines_Pitch.MAX_WIDTH, 0);
            }
            else
            {
                MoveTo(Defines_Pitch.MAX_WIDTH, Defines_Pitch.MAX_HEIGHT);
            }
        }

        m_LastTouchMan->GetMatch()->OutBorder(m_LastTouchMan->GetManager());

        return;
    }

    // 边线球
    double y = m_Current.Y;
    bool isOutBorder = false;

    if (m_Current.Y < 0)
    {
        y = 0;
        isOutBorder = true;
    }

    if (m_Current.Y > Defines_Pitch.MAX_HEIGHT)
    {
        y = Defines_Pitch.MAX_HEIGHT;
        isOutBorder = true;
    }

    if (isOutBorder)
    {
        MoveTo(m_Current.X, y);
        m_LastTouchMan->GetMatch()->OutBorder(m_LastTouchMan->GetManager());
    }
}

void Football::GKOpenball(Side side)
{
    IManager* manager = (side == Side_Home) ? m_LastTouchMan->GetMatch()->HomeManager() : m_LastTouchMan->GetMatch()->AwayManager();
    manager->GkOpenball();
}

void Football::InitVariables()
{
    m_IsInAir               = false;
    m_IsPassDestination     = false;
    m_Visible               = false;
    m_ForceInAir            = false;
    m_Recalculate           = false;

    m_LastTouchMan          = NULL;

    m_Angle                 = 0;
    m_HideTime              = 0;

    m_Speed                 = 0.0f;
    m_Acceleration          = 0.0f;

    m_cosA                  = 0.0f;
    m_sinA                  = 0.0f;

    m_Processes.clear();
}
