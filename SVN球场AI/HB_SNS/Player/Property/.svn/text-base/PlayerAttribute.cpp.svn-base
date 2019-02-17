/********************************************************************************
 * 文件名：PlayerAttribute.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "PlayerAttribute.h"
#include "../../common/String/String.h"

PlayerAttribute::PlayerAttribute()
{
    InitVariables();
}

int PlayerAttribute::Width()
{
    return Defines_Player.PLAYER_WIDTH;
}

int PlayerAttribute::DefenceRange()
{
    return Defines_Player.DEFENCE_RANGE;
}

int PlayerAttribute::SightRange()
{
    return Defines_Player.SIGHT_RANGE;
}

void PlayerAttribute::InitVariables()
{
    m_PID           = 0;
    m_CardLevel     = 0;

    m_Strengthen    = 0;
    m_HeadStyle     = 0;

    m_BodyStyle     = 0;
    m_FirstName     = String::Empty();
    m_FamilyName    = String::Empty();

    m_Width         = 0;
    m_DefenceRange  = 0;
    m_SightRange    = 0;
}
