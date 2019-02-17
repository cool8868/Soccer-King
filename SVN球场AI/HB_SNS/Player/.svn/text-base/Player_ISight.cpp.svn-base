/********************************************************************************
 * 文件名：Player_ISight.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "Player.h"


/// Refresh the player's sight.
void Player::RefreshSight()
{
    m_InSightPlayers.clear();

    foreach (IPlayer* p, m_CarePlayers)
    {
        if (Current().Distance(p->Current()) / 2 < m_Attribute->SightRange())
        {
            m_InSightPlayers.push_back(p);
        }
    }
}
