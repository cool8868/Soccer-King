/********************************************************************************
 * 文件名：Utility.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __UTILITY_H__
#define __UTILITY_H__

#include "../../common/utils.h"

class Utility
{
public:

    /// Get a point that is in the line between the player's current <see cref="Coordinate"/> and the target's <see cref="Coordinate"/>, 
    /// which is in the range of the <see cref="IPlayer"/>'s width.
    static Coordinate GetWidthPoint(IPlayer* player, Coordinate target)
    {
        player->Rotate(target);

        double x = player->Current().X + player->BuildinAttribute()->Width() * cos(player->Status()->Angle() * MATH::PI / 180);
        double y = player->Current().Y + player->BuildinAttribute()->Width() * sin(player->Status()->Angle() * MATH::PI / 180);

        return Coordinate(x, y);
    }

    /// Get a point that is in the line between the player's current <see cref="Coordinate"/> and the target's <see cref="Coordinate"/>, 
    /// which is in the range.
    static Coordinate GetPointWithRange(IPlayer* player, Coordinate target, int range)
    {
        player->Rotate(target);

        double x = player->Current().X + range * cos(player->Status()->Angle() * MATH::PI / 180);
        double y = player->Current().Y + range * sin(player->Status()->Angle() * MATH::PI / 180);

        return Coordinate(x, y);
    }

    /// Get a point that is the player's defence target.
    static Coordinate GetDefencePosition(IPlayer* player, IPlayer* target)
    {
        if (target == NULL)
        {
            return player->Current();
        }

        if (player->GetSide() == Side_Home)
        {
            return Region(Coordinate(target->Current().X - 15, target->Current().Y - 5), Coordinate(target->Current().X, target->Current().Y + 5)).GetRandomPoint();
        }
        else
        {
            return Region(Coordinate(target->Current().X, target->Current().Y - 5), Coordinate(target->Current().X + 15, target->Current().Y + 5)).GetRandomPoint();
        }
    }
};

#endif //__UTILITY_H__
