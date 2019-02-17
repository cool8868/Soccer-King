/********************************************************************************
 * 文件名：Player_IRotate.cpp
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

/// 让球员将自己的方向转为传递的目标
void Player::Rotate(Coordinate target)
{
    // 守门员由于没有素材而不转向
    if (m_Attribute->GetPosition() == Position_Goalkeeper)
    {
        target = m_Match->GetFootball()->Current();
    }

    // 当球员坐标与目标重合则转向查看足球
    if (m_Status->Current() == target)
    {
        if (Current() != m_Match->GetFootball()->Current())
        {
            Rotate(m_Match->GetFootball()->Current());
        }
        else
        {
            m_Status->SetAngle((m_Side == Side_Home) ? 0 : 180);
        }

        return;
    }

    // 球员转向
    double x = m_Status->Current().X;
    double y = m_Status->Current().Y;

    double d = Current().Distance(target);
    if (d < 1) // 避免距离相差太小导致运算后角度超出int范围
    {
        return;
    }

    double angle = (target.X > x) ? asin((target.Y - y) / d) : MATH::PI - asin((target.Y - y) / d);
    
    if (angle < 0)
    {
        angle = 2 * MATH::PI + angle;
    }

    m_Status->SetAngle((int)(angle * 180 / MATH::PI));
}
