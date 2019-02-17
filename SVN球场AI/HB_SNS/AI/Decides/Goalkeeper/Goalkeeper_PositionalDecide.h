/********************************************************************************
 * 文件名：Goalkeeper_PositionalDecide.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __GOALKEEPER_POSITIONALDECIDE_H__
#define __GOALKEEPER_POSITIONALDECIDE_H__

#include "../PositionalDecide.h"

/// 守门员移动时逻辑
class Goalkeeper_PositionalDecide : public PositionalDecide
{
public:

    /// 本方进攻时的逻辑
    Coordinate OffenseSideDecide(IPlayer* player)
    {            
        player->Rotate(player->GetMatch()->GetFootball()->Current());
        
        return player->Status()->GetDefault();
    }

    /// 防守时的逻辑
    Coordinate DefenceSideDecide(IPlayer* player)
    {
        if (player->Status()->IsAttackSide() != false)
        {
            Region region = (player->GetSide() == Side_Home) ?
                player->GetMatch()->GetPitch()->HomePenaltyRegion() :
                player->GetMatch()->GetPitch()->AwayPenaltyRegion();

            if (region.IsCoordinateInRegion(player->GetMatch()->GetFootball()->Current()))
            {
                return player->GetMatch()->GetFootball()->Current();
            }
        }

        player->Rotate(player->GetMatch()->GetFootball()->Current());

        if (player->GetMatch()->Status()->BallHandler()->Status()->GetZone() == Zone_OpposingHalf)
        {

            // 当对方进攻球员比较近时，守门员应该蹲下并且退回至门线上
            player->Status()->SetIsStandUp(false);

            Coordinate target;
            target.X = player->Status()->GetDefault().X;
            target.Y = player->GetMatch()->GetFootball()->Current().Y;

            if (target.Y < player->Status()->GetDefault().Y - Defines_Player.GK_MOVE_RANGE)  target.Y = player->Status()->GetDefault().Y - Defines_Player.GK_MOVE_RANGE;
            if (target.Y > player->Status()->GetDefault().Y + Defines_Player.GK_MOVE_RANGE)  target.Y = player->Status()->GetDefault().Y + Defines_Player.GK_MOVE_RANGE;

            return target;
        }

        player->Status()->SetIsStandUp(true);
        
        return player->GetMatch()->GetFootball()->Current();
    }
};

#endif //__GOALKEEPER_POSITIONALDECIDE_H__
