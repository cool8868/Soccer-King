/********************************************************************************
 * 文件名：PlayerPropertyInitializer.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "PlayerPropertyInitializer.h"
#include "../../common/Enum/PlayerProperty.h"

PlayerPropertyInitializer::PlayerPropertyInitializer()
{
    m_PropertyIds.clear();
    m_PlayerPropertyIds.clear();

    m_PropertyIds +=    
        PlayerProperty_Speed,               PlayerProperty_Shooting,            PlayerProperty_FreeKick,            PlayerProperty_Balance,
        PlayerProperty_Stamina,             PlayerProperty_Strength,            PlayerProperty_Aggression,          PlayerProperty_Disturb,
        PlayerProperty_Interception,        PlayerProperty_Dribble,             PlayerProperty_Passing,             PlayerProperty_Mentality,
        PlayerProperty_Reflexes,            PlayerProperty_Positioning,         PlayerProperty_Handling,            PlayerProperty_Acceleration,
        PlayerProperty_FoulRate,            PlayerProperty_InnerProbability,    PlayerProperty_OuterProbability, 
        PlayerProperty_AttackProbability,   PlayerProperty_DefenceProbability,  PlayerProperty_PassProbability;

    m_PlayerPropertyIds += 
        PlayerProperty_Speed,               PlayerProperty_Shooting,            PlayerProperty_FreeKick,            PlayerProperty_Balance,
        PlayerProperty_Stamina,             PlayerProperty_Strength,            PlayerProperty_Aggression,          PlayerProperty_Disturb,
        PlayerProperty_Interception,        PlayerProperty_Dribble,             PlayerProperty_Passing,             PlayerProperty_Mentality,
        PlayerProperty_Reflexes,            PlayerProperty_Positioning,         PlayerProperty_Handling,            PlayerProperty_Acceleration;
}

MapInt_Double PlayerPropertyInitializer::Initialize()
{
	MapInt_Double propertys;
    propertys.clear();

    foreach (int key, m_PropertyIds)
    {
        propertys[key]  = 0;
    }

    return propertys;
}
