/********************************************************************************
 * 文件名：Pitch.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "Pitch.h"
#include "../Goal/Goal.h"

Pitch::Pitch()
{
    m_HomeDestinations.clear();
    m_AwayDestinations.clear();

    m_Goal                                      = Goal::Instance();
    m_HomeGoal                                  = Coordinate::Parse(Defines_Pitch.HOME_GOAL);
    m_AwayGoal                                  = Coordinate::Parse(Defines_Pitch.AWAY_GOAL);

    m_HomeDestinations[Direction_Center]        = Line::ParseByStr(Defines_Pitch.HOME_DESTINATION_CENTER);
    m_HomeDestinations[Direction_Left]          = Line::ParseByStr(Defines_Pitch.HOME_DESTINATION_LEFT);
    m_HomeDestinations[Direction_Right]         = Line::ParseByStr(Defines_Pitch.HOME_DESTINATION_RIGHT);

    m_AwayDestinations[Direction_Center]        = Line::ParseByStr(Defines_Pitch.AWAY_DESTINATION_CENTER);
    m_AwayDestinations[Direction_Left]          = Line::ParseByStr(Defines_Pitch.AWAY_DESTINATION_LEFT);
    m_AwayDestinations[Direction_Right]         = Line::ParseByStr(Defines_Pitch.AWAY_DESTINATION_RIGHT);

    m_HomeShootRegion                           = Region::ParseByStr(Defines_Pitch.HOME_FORCE_SHOOT_REGION);
    m_AwayShootRegion                           = Region::ParseByStr(Defines_Pitch.HOME_FORCE_SHOOT_REGION).Mirror();
    m_HomePenaltyRegion                         = Region::ParseByStr(Defines_Pitch.HOME_PENALTY_AREA);
    m_AwayPenaltyRegion                         = Region::ParseByStr(Defines_Pitch.AWAY_PENALTY_AREA);
    m_HomeForcePassRegion                       = Region::ParseByStr(Defines_Pitch.HOME_FORCE_PASS_REGION);
    m_AwayForcePassRegion                       = Region::ParseByStr(Defines_Pitch.HOME_FORCE_PASS_REGION).Mirror();
}
