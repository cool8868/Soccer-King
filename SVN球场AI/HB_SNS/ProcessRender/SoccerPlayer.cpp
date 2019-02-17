/********************************************************************************
 * 文件名：SoccerPlayer.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "SoccerPlayer.h"
#include "../MatchProcess/PlayerProcess.h"
#include "../Interface/Player/Status/IPlayerStatus.h"
#include "../Interface/Player/Property/IPlayerAttribute.h"

#include "../common/DisplayUtility.h"
#include "../common/misc/Cgdi.h"

SoccerPlayer::SoccerPlayer(SoccerPitch* sp)
    : m_SoccerPitch(sp)
{
    InitVariables();
}

SoccerPlayer::~SoccerPlayer()
{

}

void SoccerPlayer::Attach(IPlayer* player)
{
    m_Player = player;

    m_dBoundingRadius               = m_Player->BuildinAttribute()->Width() * SCALE_SIZE;
    m_DefenceRangeRadius            = m_Player->BuildinAttribute()->DefenceRange() * SCALE_SIZE;
    m_InterRuptionRangeRadius       = Defines_Player.INTERRUPTION_RANGE * SCALE_SIZE;
    m_SightRangeRadius              = m_Player->BuildinAttribute()->SightRange() * SCALE_SIZE;
}

void SoccerPlayer::Attach(ParsePlayerEntity* player)
{
    m_ParsePlayer = player;

    //以下的球员的属性都是模拟的
    m_dBoundingRadius               = 2 * SCALE_SIZE;
    m_DefenceRangeRadius            = Defines_Player.DEFENCE_RANGE * SCALE_SIZE;
    m_InterRuptionRangeRadius       = Defines_Player.INTERRUPTION_RANGE * SCALE_SIZE;
    m_SightRangeRadius              = Defines_Player.SIGHT_RANGE * SCALE_SIZE;
}

void SoccerPlayer::Render()
{
    Coordinate coordinate2      = m_Player->Status()->Current();
    int angle                   = m_Player->Status()->Angle();
    int angleIndex              = PlayerProcess::GetPlayerAngleIndex(angle);
    double double_angle         = TransIndexToAngle(angleIndex);

    Coordinate pos              = TransCoordinate(coordinate2);

    //绘制球员
    gdi->TransparentText();

    if (m_Player->GetSide() == Side_Away)
    {
        gdi->BluePen();
        gdi->TextColor(Cgdi::blue);
    }
    else
    {
        gdi->RedPen();
        gdi->TextColor(Cgdi::red);
    }

    //显示ID
    if (DisPlayerController.PlayerId)
    {
        gdi->TextAtPos(pos.X - 20, pos.Y - 20, lexical_cast<string>(m_Player->ClientId()));    
    }

    //球员的朝向
    if (DisPlayerController.PlayerDirection)
    {
        gdi->TextAtPos(pos.X, pos.Y + 20, lexical_cast<string>(angleIndex));
    }

    //显示当前的状态
    if (DisPlayerController.PlayerState)
    {
        int nState = PlayerProcess::GetClientStateIndex(m_Player->Status()->State()->ClientId());
        string strHoldBallState = "持球人当前状态:";

        if (m_Player->Status()->Holdball())
        {
            strHoldBallState += Common::DisplayState(nState);
        }

        //持球人的当前状态
        gdi->TextAtPos(PITCH_BOUNDS, 10, strHoldBallState);

        //球员当前的状态
        if (nState != 0)
        {
            gdi->TextColor(Cgdi::white); 
        }

        gdi->TextAtPos(pos.X, pos.Y - 20, Common::DisplayState(nState));
    }

    gdi->HollowBrush();

    //绘制视野半径
    if (DisPlayerController.PlayerSightRange)
    {
        //gdi->GreenPen();
        //gdi->HollowBrush();

        gdi->Circle(pos, m_SightRangeRadius);
    }

    //绘制干扰半径
    if (DisPlayerController.PlayerDefenceRange)
    {
        //gdi->BrownPen();
        //gdi->HollowBrush();

        gdi->Circle(pos, m_DefenceRangeRadius);
    }

    //绘制抢断半径
    if (DisPlayerController.PlayerInterRuptionRange)
    {
        //gdi->PinkPen();
        //gdi->HollowBrush();

        gdi->Circle(pos, m_InterRuptionRangeRadius);
    }

    //画头
    gdi->HollowBrush();
    gdi->Circle(pos, m_dBoundingRadius);

    Coordinate directionPos = Coordinate(pos.X + m_dBoundingRadius * cos(double_angle), pos.Y - m_dBoundingRadius * sin(double_angle));
    gdi->Line(pos, directionPos);
}

void SoccerPlayer::Render(size_t round)
{
    vector<Process*>& vec_process = m_Player->Processes();
    
    if (round < vec_process.size() && round != m_Round)
    {
        m_Round = round;
    }

    PlayerProcess* process      = static_cast<PlayerProcess*>(vec_process[m_Round]);

    Coordinate coordinate2      = process->GetCoordinate2();
    int angleIndex              = process->Angle();
    double double_angle         = TransIndexToAngle(angleIndex);

    Coordinate pos              = TransCoordinate(coordinate2);

    //绘制球员
    gdi->TransparentText();

    if (m_Player->GetSide() == Side_Away)
    {
        gdi->BluePen();
        gdi->TextColor(Cgdi::blue);
    }
    else
    {
        gdi->RedPen();
        gdi->TextColor(Cgdi::red);
    }

    //显示ID
    if (DisPlayerController.PlayerId)
    {
        gdi->TextAtPos(pos.X - 20, pos.Y - 20, lexical_cast<string>(m_Player->ClientId()));    
    }

    //球员的朝向
    if (DisPlayerController.PlayerDirection)
    {
        gdi->TextAtPos(pos.X, pos.Y + 20, lexical_cast<string>(angleIndex));
    }

    //显示当前的状态
    if (DisPlayerController.PlayerState)
    {
        int nState = process->GetState();
        string strHoldBallState = "持球人当前状态:";

        if (process->HasBall())
        {
            strHoldBallState += Common::DisplayState(nState);
        }

        //持球人的当前状态
        gdi->TextAtPos(PITCH_BOUNDS, 10, strHoldBallState);

        //球员当前的状态
        if (nState != 0)
        {
            gdi->TextColor(Cgdi::white); 
        }

        gdi->TextAtPos(pos.X, pos.Y - 20, Common::DisplayState(nState));
    }

    gdi->HollowBrush();

    //绘制视野半径
    if (DisPlayerController.PlayerSightRange)
    {
        //gdi->GreenPen();
        //gdi->HollowBrush();

        gdi->Circle(pos, m_SightRangeRadius);
    }

    //绘制干扰半径
    if (DisPlayerController.PlayerDefenceRange)
    {
        //gdi->BrownPen();
        //gdi->HollowBrush();

        gdi->Circle(pos, m_DefenceRangeRadius);
    }

    //绘制抢断半径
    if (DisPlayerController.PlayerInterRuptionRange)
    {
        //gdi->PinkPen();
        //gdi->HollowBrush();

        gdi->Circle(pos, m_InterRuptionRangeRadius);
    }

    //画头
    gdi->HollowBrush();
    gdi->Circle(pos, m_dBoundingRadius);

    Coordinate directionPos = Coordinate(pos.X + m_dBoundingRadius * cos(double_angle), pos.Y - m_dBoundingRadius * sin(double_angle));
    gdi->Line(pos, directionPos);
}

void SoccerPlayer::RenderParser(size_t round)
{
    vector<Process*>& vec_process = m_ParsePlayer->Processes();

    if (round < vec_process.size() && round != m_Round)
    {
        m_Round = round;
    }

    PlayerProcess* process      = static_cast<PlayerProcess*>(vec_process[m_Round]);

    Coordinate coordinate2      = process->GetCoordinate2();
    int angleIndex              = process->Angle();
    double double_angle         = TransIndexToAngle(angleIndex);

    Coordinate pos              = TransCoordinate(coordinate2);

    //绘制球员
    gdi->TransparentText();

    if (m_ParsePlayer->GetSide() == Side_Away)
    {
        gdi->BluePen();
        gdi->TextColor(Cgdi::blue);
    }
    else
    {
        gdi->RedPen();
        gdi->TextColor(Cgdi::red);
    }

    //显示ID
    if (DisPlayerController.PlayerId)
    {
        gdi->TextAtPos(pos.X - 20, pos.Y - 20, lexical_cast<string>(m_ParsePlayer->ClientId()));    
    }

    //球员的朝向
    if (DisPlayerController.PlayerDirection)
    {
        gdi->TextAtPos(pos.X, pos.Y + 20, lexical_cast<string>(angleIndex));
    }

    //显示当前的状态
    if (DisPlayerController.PlayerState)
    {
        int nState = process->GetState();
        string strHoldBallState = "持球人当前状态:";

        if (process->HasBall())
        {
            strHoldBallState += Common::DisplayState(nState);
        }

        //持球人的当前状态
        gdi->TextAtPos(PITCH_BOUNDS, 10, strHoldBallState);

        //球员当前的状态
        if (nState != 0)
        {
            gdi->TextColor(Cgdi::white); 
        }

        gdi->TextAtPos(pos.X, pos.Y - 20, Common::DisplayState(nState));
    }

    gdi->HollowBrush();

    //绘制视野半径
    if (DisPlayerController.PlayerSightRange)
    {
        //gdi->GreenPen();
        //gdi->HollowBrush();

        gdi->Circle(pos, m_SightRangeRadius);
    }

    //绘制干扰半径
    if (DisPlayerController.PlayerDefenceRange)
    {
        //gdi->BrownPen();
        //gdi->HollowBrush();

        gdi->Circle(pos, m_DefenceRangeRadius);
    }

    //绘制抢断半径
    if (DisPlayerController.PlayerInterRuptionRange)
    {
        gdi->Circle(pos, m_InterRuptionRangeRadius);
    }

    //画头
    gdi->HollowBrush();
    gdi->Circle(pos, m_dBoundingRadius);

    Coordinate directionPos = Coordinate(pos.X + m_dBoundingRadius * cos(double_angle), pos.Y - m_dBoundingRadius * sin(double_angle));
    gdi->Line(pos, directionPos);
}

void SoccerPlayer::InitVariables()
{
    m_dBoundingRadius           = 0.0f;
    m_DefenceRangeRadius        = 0.0f;
    m_InterRuptionRangeRadius   = 0.0f;
    m_SightRangeRadius          = 0.0f;

    m_Round                     = 0;

    m_Player                    = NULL;
}
