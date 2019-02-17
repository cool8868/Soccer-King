/********************************************************************************
 * 文件名：PlayerRule.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PLAYERRULE_H__
#define __PLAYERRULE_H__

#include "../../common/common.h"
#include "../../common/Enum/Position.h"
#include "../../common/Structs/Coordinate.h"
#include "../../common/Structs/Region.h"
#include "../../common/Enum/Side.h"
#include "../../common/Utility.h"

#include "../../Interface/Player/IPlayer.h"
#include "../../Interface/Player/Status/IPlayerStatus.h"

/// 封装了球员的业务规则
class PlayerRule
{
public:

    /// 守门员的移动区域
    static Region GetGoalKeeperMoveRegion(Position position, Side side)
    {
        return (side == Side_Home) ? Region::ParseByStr(Defines_Pitch.HOME_PENALTY_AREA) : Region::ParseByStr(Defines_Pitch.AWAY_PENALTY_AREA);
    }

    /// 球员的射门区域，将赋值给球员的Status中
    static Region GetPlayerShootRegion(double strength, Side side)
    {
        // （60+力量属性/5）
        Region region = Region(0, 20, (int)(20 + strength / 5), Defines_Pitch.MAX_HEIGHT - 20);   // away side region

        return (side == Side_Home) ? region.Mirror() : region;
    }

    /// 初始化球员的可传球列表
    static void InitPassTarget(IPlayer* player)
    {
        vector<IPlayer*>& passTargets = player->PassTargets();
        Common::AddRange<IPlayer>(passTargets, player->GetManager()->Players());

        vector<IPlayer*>::iterator& it = find(passTargets.begin(), passTargets.end(), player);
        if (it != passTargets.end())
        {
            passTargets.erase(it);
        }
    }

    /// 初始化球员的关注球员列表
    static void InitCarePlayers(IPlayer* player)
    {
        player->CarePlayers().clear();

        if (player->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
        {
            Common::AddRange<IPlayer>(player->CarePlayers(), player->GetManager()->Opponent()->Players());
        }

        if (player->BuildinAttribute()->GetPosition() == Position_Fullback)
        {
            Common::AddRange<IPlayer>(player->CarePlayers(), player->GetManager()->Opponent()->GetPlayersByPosition(Position_Forward));
            Common::AddRange<IPlayer>(player->CarePlayers(), player->GetManager()->Opponent()->GetPlayersByPosition(Position_Midfielder));
        }

        if (player->BuildinAttribute()->GetPosition() == Position_Midfielder)
        {
            Common::AddRange<IPlayer>(player->CarePlayers(), player->GetManager()->Opponent()->GetPlayersByPosition(Position_Forward));
            Common::AddRange<IPlayer>(player->CarePlayers(), player->GetManager()->Opponent()->GetPlayersByPosition(Position_Midfielder));
            Common::AddRange<IPlayer>(player->CarePlayers(), player->GetManager()->Opponent()->GetPlayersByPosition(Position_Goalkeeper));
        }

        if (player->BuildinAttribute()->GetPosition() == Position_Forward)
        {
            Common::AddRange<IPlayer>(player->CarePlayers(), player->GetManager()->Opponent()->GetPlayersByPosition(Position_Fullback));
            Common::AddRange<IPlayer>(player->CarePlayers(), player->GetManager()->Opponent()->GetPlayersByPosition(Position_Midfielder));
            Common::AddRange<IPlayer>(player->CarePlayers(), player->GetManager()->Opponent()->GetPlayersByPosition(Position_Goalkeeper));
        }
    }

    /// 获取球员的速度
    static double GetSpeed(IPlayer* player, double speed)
    {
        double max = player->Status()->MaxSpeed();
        double maxSpeed = max;

        if (player->Status()->Hasball() == false)
        {
            if (player->Status()->GetDefenceStatus()->DefenceTarget() != NULL) // 存在防守人
            {
                maxSpeed = max;
            }
            else // 没有防守人
            {
                maxSpeed = max * 0.8;
            }
        }
        else if (player->Status()->Holdball()) // 持球只能以90%的最高速度
        {
            maxSpeed = max * 0.8;
        }

        if (speed > maxSpeed)
        {
            return maxSpeed;
        }

        return speed;
    }

    /// 获取球员的最大速度
    static double GetMaxSpeed(double speed)
    {
        // (10 + (x-100) / 50) * 2
        return (10 + (speed - 100) / 50) * 2;
    }

    /// 获取球员的加速度
    static double GetAcceleration(double speed, double acceleration)
    {
        return GetMaxSpeed(speed) / Defines_Player.PLAYER_ACCELERATION[GetAccelerationIndex(acceleration)];
    }

    /// 获取球员的传球偏差
    static Coordinate GetPassOffset(double passProperty, Coordinate target)
    {
        int min = 10;
        int max = 30;
        max = (int)(max - 0.1 * passProperty);

        if (max <= min) // 没有偏差
        {
            return target;
        }
        else // 有偏差
        {
            double offset = RandomHelper::GetInt32(min, max);
            if (offset < min)
            {
                offset = min;
            }

            int x = RandomHelper::GetInt32(static_cast<int>(target.X - offset), static_cast<int>(target.X + offset));
            int y = RandomHelper::GetInt32(static_cast<int>(target.Y - offset), static_cast<int>(target.Y + offset));

            return Coordinate(x, y);
        }
    }

    /// 获取射门的目标区域编号
    /// </summary>
    /// <param name="property">属性值</param>
    /// <param name="player">射门的球员</param>
    /// <param name="fix">修正值</param>
    /// <returns>返回目标区域编号</returns>
    static int GetShootTargetIndex(double property, IPlayer* player, double fix)
    {
        int r = RandomHelper::GetInt32(0, static_cast<int>((property / 4) * pow(property / 100, 3) + 25 * pow(property / 100, 2) + 25 * (property / 100) + fix));

        if (r >= 0 && r <= fix)
        {
            return 4;
        }

        if (r > fix && r <= 25 * (property / 100) + fix)
        {
            return 3;
        }

        // 25(x/100)+10<r≤15(x/100)2+25(x/100)+10
        if (r > 25 * (property / 100) + fix && r <= 25 * pow(property / 100, 2) + 25 * (property / 100) + fix)
        {
            return 2;
        }

        // 25(x/100)2+25(x/100)+10<r≤25（x/100）3+25(x/100)2+25(x/100)+10
        if (r > 25 * pow(property / 100, 2) + 25 * (property / 100) + fix &&
            r <= static_cast<int>((property / 4) * pow(property / 100, 3) + 25 * pow(property / 100, 2) + 25 * (property / 100) + fix))
        {
            return 1;
        }

        return 4;

        // ---tony
    }

private:
    static int GetAccelerationIndex(double acceleration)
    {
        if (acceleration >= 0 && acceleration < 40)
        {
            return 0;
        }

        if (acceleration >= 40 && acceleration < 80)
        {
            return 1;
        }

        if (acceleration >= 80 && acceleration < 120)
        {
            return 2;
        }

        if (acceleration >= 120 && acceleration < 160)
        {
            return 3;
        }

        return 4;
    }
};

#endif //__PLAYERRULE_H__
