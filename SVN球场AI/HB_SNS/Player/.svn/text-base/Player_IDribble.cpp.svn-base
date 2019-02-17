/********************************************************************************
 * 文件名：Player_IDribble.cpp
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

#include "../common/utils.h"

/// Dribble ball.
void Player::DribbleBall()
{
    Move();

    if (m_Status->Holdball())
    {
        double x = m_Status->Current().X + m_Attribute->Width() * cos(m_Status->Angle() * MATH::PI / 180);
        double y = m_Status->Current().Y + m_Attribute->Width() * sin(m_Status->Angle() * MATH::PI / 180);

        m_Match->GetFootball()->Kick(Coordinate(x, y), 25, this);
        //_match.Football.MoveTo(new Coordinate(x, y));
    }                
}