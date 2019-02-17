/********************************************************************************
 * 文件名：PlayerStatus.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "PlayerStatus.h"
#include "../../Exception/ApplicationException.h"
#include "../../Log/LogHelper.h"

PlayerStatus::PlayerStatus(IPlayer* player, Coordinate coordinate)
{
    //初始化变量
    InitVariables();

    m_Player            = player;
    
    //创建各个状态
    m_DefenceStatus     = new DefenceStatus(m_Player);
    m_PassStatus        = new PassStatus();
    m_ShootStatus       = new ShootStatus();
    m_FoulStatus        = new FoulStatus();
    m_DiveStatus        = new DiveStatus();
    m_DebuffStatus      = new DebuffStatus();
    m_ActionStatus      = new ActionStatus();
    m_ModelStatus       = new ModelStatus();

    m_Default           = coordinate;
    m_Current           = coordinate;
    m_Destination       = coordinate;

    Redecide();
}

PlayerStatus::~PlayerStatus()
{
    DeletePtrSafe(m_ModelStatus);
    DeletePtrSafe(m_ActionStatus);
    DeletePtrSafe(m_DebuffStatus);
    DeletePtrSafe(m_DiveStatus);
    DeletePtrSafe(m_FoulStatus);
    DeletePtrSafe(m_ShootStatus);
    DeletePtrSafe(m_PassStatus);
    DeletePtrSafe(m_DefenceStatus);
}

void PlayerStatus::Redecide()
{
    // LogHelper.Insert("[Redecide]Notify the player to redecide. [Player]" + _player.BuildinAttribute.FamilyName + ", " + _player.Side);
    m_NeedRedecide = true;
}

void PlayerStatus::DecideEnd()
{
    // LogHelper.Insert("[End]Notify the player is decided end. [Player]" + _player.BuildinAttribute.FamilyName + ", " + _player.Side);
    m_NeedRedecide = false;
}

bool PlayerStatus::Holdball()
{
    if (m_Hasball == false)
    {
        return false;
    }

    if (m_BallDistance > 2)
    {
        return false;
    }

    return true;
}

void PlayerStatus::SetHasball(bool hasBall)
{
    if (hasBall)
    {
        if (!m_Enable)
        {
            throw ApplicationException("[Error]Can't let the disable player to has ball. Player:" + lexical_cast<string>(m_Player->Id()));
        }

        if (m_Player->GetMatch()->Status()->BallHandler() != NULL)
        {
            m_Player->GetMatch()->Status()->BallHandler()->Status()->SetHasball(false);
        }

        m_Player->GetMatch()->Status()->SetBallHandler(m_Player);

        LogHelper::Insert("[HasBall]Player set to has ball." + lexical_cast<string>(m_Player->Id()), Color_Black);
    }

    m_Hasball = hasBall;
}

bool PlayerStatus::IsAttackSide()
{
    return m_Player->GetManager()->GetSide() == m_Player->GetMatch()->Status()->BallHandler()->GetManager()->GetSide();
}

Zone PlayerStatus::GetZone()
{
    return m_Player->GetTargetZone(m_Current);
}

bool PlayerStatus::Enable()
{
    return (!m_FoulStatus->IsInjured() && !m_FoulStatus->IsRedCard());
}

void PlayerStatus::RefreshDistance()
{
    /// 刷新该球员与球的距离
    RefreshBallDistance();

    /// 刷新球员和他的目标坐标的距离
    RefreshDestinationDistance();

    /// 刷新球员和他的传球目标的距离
    RefreshPassTargetDistance();

    /// 刷新该球员和他的防守人的距离
    RefreshDefenceTargetDistance();

    /// 刷新了该球员和他的最近的防守者之间的距离
    RefreshDefenderDistance();

    if (m_Player->Status()->Hasball())
    {
        /// 刷新该球员与球门的距离
        RefreshGoalDistance();
    }

    /// 刷新最近的防守人
    PointerCastSafe(DefenceStatus, m_DefenceStatus)->RefreshClosestDefender();
}

void PlayerStatus::RefreshBallDistance()
{
    if (m_Player->Status()->IsAttackSide() == false || m_Player->Status()->Hasball())
    {
        Coordinate football = m_Player->GetMatch()->GetFootball()->Current();
        double distance = football.Distance(m_Current) - m_Player->BuildinAttribute()->Width();

        m_BallDistance = static_cast<int>(distance);
    }
}

void PlayerStatus::RefreshDestinationDistance()
{
    m_DestinationDistance = m_Current.Distance(m_Destination);
}

void PlayerStatus::RefreshPassTargetDistance()
{
    if (m_Player->Status()->GetPassStatus()->PassTarget() == NULL)
    {
        m_PassTargetDistance = 0;
    }
    else
    {
        Coordinate start        = m_Player->Current();
        Coordinate end          = m_Player->Status()->GetPassStatus()->PassTarget()->Current();

        m_PassTargetDistance    = static_cast<int>(start.Distance(end));
    }
}

/// 刷新该球员和他的防守人的距离
void PlayerStatus::RefreshDefenceTargetDistance()
{
    if (m_Player->Status()->GetDefenceStatus()->DefenceTarget() == NULL)
    {
        m_DefenceTargetDistance = 0;
    }
    else
    {
        m_DefenceTargetDistance = m_Player->Current().Distance(m_Player->Status()->GetDefenceStatus()->DefenceTarget()->Current());
    }
}

/// 刷新了该球员和他的最近的防守者之间的距离
void PlayerStatus::RefreshDefenderDistance()
{
    if (m_DefenceStatus->Defenders().size() == 0) // 没有人防守，则给出最大的防守距离
    {
        m_DefenderDistance = Defines_Pitch.MAX_WIDTH;
    }
    else
    {
        double distances[MyDefine_Player_Defenders_Count] = {};
        for (size_t i = 0; i < m_DefenceStatus->Defenders().size(); i++)
        {
            distances[i] = m_Current.Distance(m_DefenceStatus->Defenders()[i]->Current());
        }

        m_DefenderDistance = Common::GetDoubleMinQuick(distances, m_DefenceStatus->Defenders().size());
    }
}

/// 刷新该球员与球门的距离
void PlayerStatus::RefreshGoalDistance()
{
    Coordinate goal = (m_Player->GetSide() == Side_Home) ? m_Player->GetMatch()->GetPitch()->AwayGoal() : m_Player->GetMatch()->GetPitch()->HomeGoal();

    m_GoalDistance = m_Player->Current().Distance(goal);
}

void PlayerStatus::InitVariables()
{
    m_Holdball              = false;
    m_Hasball               = false;
    m_Angle                 = 0;
    m_Speed                 = 0.0f;
    m_MaxSpeed              = 0.0f;
    m_State                 = NULL;
    m_Acceleration          = 0.0F;
    m_IsAttackSide          = false;
    m_IsBackward            = false;
    m_IsStandUp             = false;
    m_PerRoundDecrease      = 0.0f;

    m_ShootStatus           = NULL;
    m_PassStatus            = NULL;
    m_DefenceStatus         = NULL;
    m_FoulStatus            = NULL;
    m_DiveStatus            = NULL;
    m_DebuffStatus          = NULL;
    m_ActionStatus          = NULL;
    m_ModelStatus           = NULL;

    m_BallDistance          = 0;
    m_DestinationDistance   = 0.0f;
    m_PassTargetDistance    = 0;
    m_GoalDistance          = 0.0f;
    m_DefenceTargetDistance = 0.0f;
    m_DefenderDistance      = 0.0f;

    m_NeedRedecide          = false;
    m_Enable                = false;

    m_Player                = NULL;
}

//////////////////////////////////////////////////////////////////////////
DefenceStatus::DefenceStatus(IPlayer* player)
    : m_Defenders(Macro_PlayerCount)
    , m_DefenceTarget(NULL)
    , m_ClosestDefender(NULL)
{
    m_Defenders.clear();

    m_Player = player;
}

void DefenceStatus::SetDefenceTarget(IPlayer* player)
{
    if (m_DefenceTarget != NULL)
    {
        vector<IPlayer*>& players = m_DefenceTarget->Status()->GetDefenceStatus()->Defenders();
        vector<IPlayer*>::iterator i = find(players.begin(), players.end(), m_Player);

        if (i != players.end()) 
        {
            players.erase(i);
        }
    }

    if (player == NULL)
    {
        m_DefenceTarget = NULL;
        return;
    }

    player->Status()->GetDefenceStatus()->Defenders().push_back(m_Player);
    m_DefenceTarget = player;
}

/// 找出最近的防守人
IPlayer* DefenceStatus::GetClosestDefender()
{
    double distance = 10000.0;
    IPlayer* target = NULL;

    if (m_Defenders.size() == 0)
    {
        return NULL;
    }

    foreach (IPlayer* defender, m_Defenders)
    {
        double d = m_Player->Current().SimpleDistance(defender->Current());
        if (d < distance)
        {
            distance = d;
            target = defender;
        }
    }

    return target;
}

void DefenceStatus::RefreshClosestDefender()
{
    if (m_Player->Status()->Hasball())
    {
        if (m_Defenders.size() == 0)
        {
            m_ClosestDefender = NULL;
        }
        else
        {
            m_ClosestDefender = GetClosestDefender();
        }
    }
}

//////////////////////////////////////////////////////////////////////////
bool FoulStatus::IsYellowCard()
{
    return m_FoulLevel  == Defines_FoulLevel.YELLOW_CARD;
}

bool FoulStatus::IsRedCard()
{
    return m_FoulLevel == Defines_FoulLevel.RED_CARD;
}

bool FoulStatus::IsInjured()
{
    return m_FoulLevel == Defines_FoulLevel.INJURED;
}

