/********************************************************************************
 * 文件名：FreeKickRule.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FREEKICKRULE_H__
#define __FREEKICKRULE_H__

class FreeKickRule
{
public:

    /// Initializes a new instance of the <see cref="FreeKickRule"/> class.
    /// <param name="manager">Represents a player who will take the kick.</param>
    /// <param name="point">Represents a <see cref="Coordinate"/> that is the open ball point.</param>
    FreeKickRule(IManager* manager, Coordinate point)
    {
        m_Manager   = manager;
        m_Point     = point;
    }

    /// Starts a free kick.
    virtual void Start() = 0;

protected:

    Coordinate CloseMove(double x, double y)
    {
        double distance = m_Point.Distance(Coordinate(x, y));
        if (distance < 100)
        {
            if (distance == 0) // 修正除0错误 Add by Jiawei Cheng 6/27/2010
            {
                return Coordinate(x, y);
            }

            double move = (distance + 50) / 10;
            if ((distance - move) < 19)
            {
                move = distance - 19;
            }

            x = x + (m_Point.X - x) * move / distance;
            y = y + (m_Point.Y - y) * move / distance;
        }

        return Coordinate(x, y);
    }

protected:

    IManager*   m_Manager;
    Coordinate  m_Point;
};

#endif //__FREEKICKRULE_H__
