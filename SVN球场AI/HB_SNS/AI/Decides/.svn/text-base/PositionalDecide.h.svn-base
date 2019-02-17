/********************************************************************************
 * 文件名：PositionalDecide.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __POSITIONALDECIDE_H__
#define __POSITIONALDECIDE_H__

#include "Utility.h"

class PositionalDecide: public IPositionalDecide
{
public:

    Coordinate DecideTarget(IPlayer* player)
    {
        player->DecideEnd();

        return (player->Status()->IsAttackSide()) ? OffenseSideDecide(player) : DefenceSideDecide(player);
    }

    /// 防守人决策自己的防守目标
    virtual Coordinate DefenceSideDecide(IPlayer* player)
    {
        if (player->GetMatch()->Status()->IsNoBallHandler())
        {
            return player->GetMatch()->GetFootball()->Current();
        }

        if (player->GetMatch()->Status()->BallHandler()->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None)
        {
            return player->GetMatch()->GetFootball()->Current();
        }

        if (player->GetMatch()->Status()->BallHandler()->Status()->Enable() == false)
        {
            return player->GetMatch()->GetFootball()->Current();
        }

        //没有防守人时，跟随整体移动
        if (player->Status()->GetDefenceStatus()->DefenceTarget() == NULL)
        {
            return InternalDefenceDecide(player);
        }

        if (!player->Status()->GetDefenceStatus()->DefenceTarget()->Status()->Hasball())
        {
            return InternalDefenceDecide(player);
        }

        return Utility::GetDefencePosition(player, player->Status()->GetDefenceStatus()->DefenceTarget());
    }

    /// Decide the moving target while player's in the offense side.
    virtual Coordinate OffenseSideDecide(IPlayer* player)
    {
        if (player->GetMatch()->Status()->IsNoBallHandler())
        {
            return player->GetMatch()->GetFootball()->Current();
        }

        //持球人行动
        if (player->Status()->Hasball())
        {
            if (player->InSightPlayers().size() == 0)
            {
                Coordinate goal = (player->GetSide() == Side_Home) ? 
                    player->GetMatch()->GetPitch()->AwayGoal() : player->GetMatch()->GetPitch()->HomeGoal();

                return Utility::GetPointWithRange(player, goal, Defines_Player.SHORT_MOVE_RANGE);
            }

            int index = RandomHelper::GetInt32(0, 2);
            IPitch* pitch = player->GetMatch()->GetPitch();

            map<Direction, Line> lines = (player->GetSide() == Side_Home) ? pitch->AwayDestinations() : pitch->HomeDestinations();

            Coordinate target;

            if (index == 0)
            {
                target = lines[Direction_Left].GetRandomPoint();
            }

            if (index == 1)
            {
                target = lines[Direction_Center].GetRandomPoint();
            }

            if (index == 2)
            {
                target = lines[Direction_Right].GetRandomPoint();
            }

            player->Rotate(target);
            double x = player->Current().X + Defines_Player.SHORT_MOVE_RANGE * cos(player->Status()->Angle() * MATH::PI / 180);
            double y = player->Current().Y + Defines_Player.SHORT_MOVE_RANGE * sin(player->Status()->Angle() * MATH::PI / 180);

            return Coordinate(x, y);
        }

        const double lx_min = 1.0 / 4.0 * Defines_Pitch.MAX_WIDTH;
        const double rx_min = 8.0 / 6.0 * Defines_Pitch.MAX_WIDTH;
        const double lx_max = 1.0 / 3.0 * Defines_Pitch.MAX_WIDTH;
        const double rx_max = 8.0 / 6.0 * Defines_Pitch.MAX_WIDTH;

        const double uy_min = (-1.0 / 8.0) * Defines_Pitch.MAX_HEIGHT;
        const double dy_min = 7.0 / 8.0 * Defines_Pitch.MAX_HEIGHT;
        const double uy_max = 1.0 / 8.0 * Defines_Pitch.MAX_HEIGHT;
        const double dy_max = (9.0 / 8.0) * Defines_Pitch.MAX_HEIGHT;

        double lxmin = (player->GetSide() == Side_Home) ? lx_min : Defines_Pitch.MAX_WIDTH - rx_max;
        double lxmax = (player->GetSide() == Side_Home) ? lx_max : Defines_Pitch.MAX_WIDTH - rx_min;
        double rxmin = (player->GetSide() == Side_Home) ? rx_min : Defines_Pitch.MAX_WIDTH - lx_max;
        double rxmax = (player->GetSide() == Side_Home) ? rx_max : Defines_Pitch.MAX_WIDTH - lx_min;

        double lux = lxmin + player->GetMatch()->GetFootball()->Current().X / Defines_Pitch.MAX_WIDTH * (lxmax - lxmin);
        double luy = uy_min + player->GetMatch()->GetFootball()->Current().Y / Defines_Pitch.MAX_HEIGHT * (uy_max - uy_min);
        double rdx = rxmin + player->GetMatch()->GetFootball()->Current().X / Defines_Pitch.MAX_WIDTH * (rxmax - rxmin);
        double rdy = dy_min + player->GetMatch()->GetFootball()->Current().Y / Defines_Pitch.MAX_HEIGHT * (dy_max - dy_min);

        double rx = lux + player->Status()->GetDefault().X / Defines_Pitch.MAX_WIDTH * (rdx - lux);
        double ry = luy + player->Status()->GetDefault().Y / Defines_Pitch.MAX_HEIGHT * (rdy - luy);

        return Coordinate(rx, ry);
    }

private:

    Coordinate InternalDefenceDecide(IPlayer* player)
    {
        const double lx_min = 0.0;
        const double rx_min = 4.0 / 8.0 * Defines_Pitch.MAX_WIDTH;
        const double lx_max = 3.0 / 8.0 * Defines_Pitch.MAX_WIDTH;
        const double rx_max = 1.0 / 1.0 * Defines_Pitch.MAX_WIDTH;

        const double uy_min = 0.0;
        const double dy_min = 3.0 / 4.0 * Defines_Pitch.MAX_HEIGHT;
        const double uy_max = 1.0 / 4.0 * Defines_Pitch.MAX_HEIGHT;
        const double dy_max = Defines_Pitch.MAX_HEIGHT;

        double lxmin = (player->GetSide() == Side_Home) ? lx_min : Defines_Pitch.MAX_WIDTH - rx_max;
        double lxmax = (player->GetSide() == Side_Home) ? lx_max : Defines_Pitch.MAX_WIDTH - rx_min;
        double rxmin = (player->GetSide() == Side_Home) ? rx_min : Defines_Pitch.MAX_WIDTH - lx_max;
        double rxmax = (player->GetSide() == Side_Home) ? rx_max : Defines_Pitch.MAX_WIDTH - lx_min;

        double lux = lxmin + player->GetMatch()->GetFootball()->Current().X / Defines_Pitch.MAX_WIDTH * (lxmax - lxmin);
        double luy = uy_min + player->GetMatch()->GetFootball()->Current().Y / Defines_Pitch.MAX_HEIGHT * (uy_max - uy_min);
        double rdx = rxmin + player->GetMatch()->GetFootball()->Current().X / Defines_Pitch.MAX_WIDTH * (rxmax - rxmin);
        double rdy = dy_min + player->GetMatch()->GetFootball()->Current().Y / Defines_Pitch.MAX_HEIGHT * (dy_max - dy_min);

        double x = lux + player->Status()->GetDefault().X / Defines_Pitch.MAX_WIDTH * (rdx - lux);
        double y = luy + player->Status()->GetDefault().Y / Defines_Pitch.MAX_HEIGHT * (rdy - luy);

        return Coordinate(x, y);
    }
};

#endif //__POSITIONALDECIDE_H__
