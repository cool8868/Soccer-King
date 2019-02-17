/********************************************************************************
 * 文件名：Player_IStopball.cpp
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

void Player::Stopball() 
{
    if (m_Status->Hasball() == false || m_Status->BallDistance() > m_Attribute->DefenceRange())
    {
        throw NotSupportedException("[停球]球在不在这个球员身上：" + lexical_cast<string>(m_Id));
    }

    LogHelper::Insert("[停球]" + lexical_cast<string>(m_Id) + "停球");

    m_Manager->GetMatch()->GetFootball()->Kick(m_Manager->GetMatch()->GetFootball()->Current(), 0, this);
}
