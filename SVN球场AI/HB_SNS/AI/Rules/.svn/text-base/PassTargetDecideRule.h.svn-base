/********************************************************************************
 * 文件名：PassTargetDecideRule.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PASSTARGETDECIDERULE_H__
#define __PASSTARGETDECIDERULE_H__

#include "../../common/Utility.h"

/// 表示了封装选择传球目标逻辑的类
class PassTargetDecideRule
{
public:

    /// 决定传球的目标
    static IPlayer* Decide(IPlayer* player)
    {
        // 如果该球员为最前的球员，不允许带球
        if (player->ClientId() == player->GetManager()->Status()->HeadMostPlayer()->ClientId())
        {
            return NULL;
        }

        return InternalDecide(player);
    }

    /// 强制传球选择
    static IPlayer* ForcePassDecide(IPlayer* player)
    {
        IPlayer* target = InternalDecide(player);

        if (target != NULL)
        {
            return target;                
        }
        else
        {
            vector<IPlayer*>    array(player->PassTargets().size());
            double              targets[MyDefine_Player_PassTargets_Count] = {};
            array.clear();

            int length = 0;

            for (size_t i = 0; i < player->PassTargets().size(); i++)
            {
                if (!player->PassTargets()[i]->Status()->Enable())
                {
                    continue;
                }

                if (player->PassTargets()[i]->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None)
                {
                    continue;
                }

                array.push_back(player->PassTargets()[i]);
                targets[i] = player->PassTargets()[i]->Current().Distance(player->GetMatch()->GetFootball()->Current());
                length++;
            }

            if (length == 0)
            {
                return NULL;
            }

            return array[Common::GetDoubleMinIndexQuick(targets, length)];
        }
    }

    /// 传给最近的球员
    static IPlayer* PassClosest(IPlayer* player)
    {
        double              distances[MyDefine_MAX_PLAYER_COUNT] = {};
        vector<IPlayer*>    array(Defines_Match.MAX_PLAYER_COUNT);
        array.clear();

        for (size_t i = 0; i < player->GetManager()->Players().size(); i++)
        {
            if (player->GetManager()->Players()[i] == player) continue;
            if (!player->GetManager()->Players()[i]->Status()->Enable()) continue;
            if (player->GetManager()->Players()[i]->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None) continue;

            distances[i] = player->Current().SimpleDistance(player->GetManager()->Players()[i]->Current());
            array.push_back(player->GetManager()->Players()[i]);
        }
        if (array.size() == 0)
        {
            return player;
        }
        else
        {
            return array[Common::GetDoubleMinIndexQuick(distances, array.size())];
        }
    }

    /// 守门员开球门球
    /// <param name="player">发球的守门员</param>
    /// <returns>发球的目标</returns>
    static IPlayer* GoalKickDecide(IPlayer* player)
    {
        vector<IPlayer*> array(Macro_PlayerCount);
        array.clear();

        foreach (IPlayer* p, player->GetManager()->Players())
        {
            if (p == player) continue;
            if (!p->Status()->Enable()) continue;
            if (p->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None) continue;

            if (RandomHelper::GetPercentage() < 70)
            {
                if (p->BuildinAttribute()->GetPosition() == Position_Midfielder || p->BuildinAttribute()->GetPosition() == Position_Forward)
                {
                    array.push_back(p);
                }
            }
            else
            {
                array.push_back(p);
            }
        }

        if (array.size() == 0)
        {
            return NULL;
        }

        return array[RandomHelper::GetInt32(0, array.size() - 1)];
    }

    /// 决定开球回合的传球目标
    /// <param name="player">Represents the player who needs to decide a pass target.</param>
    /// <returns>Returns the pass target.</returns>
    static IPlayer* OpenballDecide(IPlayer* player)
    {
        // Random the target list for avoid always pass to the same player.
        vector<IPlayer*> targets(Defines_Match.MAX_PLAYER_COUNT);
        vector<IPlayer*> midArray(player->GetManager()->GetPlayersByPosition(Position_Midfielder)); //用vector初始化
        targets.clear();

        foreach (IPlayer* p, midArray)
        {
            if (!p->Status()->Enable()) continue;
            if (p->Current().X == player->Current().X) continue;
            
            targets.push_back(p);
        }

        if (targets.size() > 0)
        {
            return targets[RandomHelper::GetInt32(0, targets.size() - 1)];
        }

        foreach (IPlayer* p, player->GetManager()->Players())
        {
            if (!p->Status()->Enable()) continue;
            if (p->BuildinAttribute()->GetPosition() == Position_Goalkeeper) continue;
            if (p->Current().X == player->Current().X) continue;

            targets.push_back(p);
        }

        if (targets.size() == 0)
        {
            foreach (IPlayer* p, player->GetManager()->Opponent()->Players())
            {
                if (!p->Status()->Enable()) continue;
                if (p->BuildinAttribute()->GetPosition() == Position_Goalkeeper) continue;                    

                targets.push_back(p);
            }
        }

        if (targets.size() == 0)
        {
            return player->GetManager()->Players()[0];
        }

        return targets[RandomHelper::GetInt32(0, targets.size() - 1)];
    }

private:

    /// 选择传球人的内部实现
    static IPlayer* InternalDecide(IPlayer* player)
    {
        vector<IPlayer*>    passTargets(player->PassTargets().size());                  // 可传球员的数组
        double              distanceArray[MyDefine_Player_PassTargets_Count] = {};      // 可传球员离自己的防守人的距离
        passTargets.clear();

        // 将所有球员和他的防守距离加入至内存中
        for (size_t i = 0; i < player->PassTargets().size(); i++)
        {
            if (player->PassTargets()[i] == player) continue;

            if (!player->PassTargets()[i]->Status()->Enable())
            {
                continue;
            }

            if (player->PassTargets()[i]->Status()->GetModelStatus()->Mid() == (int)ModelType_FreezeEffect)
            {
                continue;
            }

            if (player->GetMatch()->GetFootball()->LastTouchMan() != NULL)
            {
                if (player->PassTargets()[i]->ClientId() == player->GetMatch()->GetFootball()->LastTouchMan()->ClientId())
                {
                    continue;
                }
            }               

            // 插入距离数组中
            if (player->IsCoordinateEnablePass(player->PassTargets()[i]->Current()))
            {                    
                if (player->Current().Distance(player->PassTargets()[i]->Current()) > Defines_Player.PASS_MAX_RANGE)
                {
                    continue;
                }

                if (player->PassTargets()[i]->Status()->DefenderDistance() < 7)
                {
                    continue;
                }

                passTargets.push_back(player->PassTargets()[i]);
                distanceArray[passTargets.size() - 1] = player->PassTargets()[i]->Status()->DefenderDistance();
            }
        }

        if (passTargets.size() == 0) // 无人可以传
        {
            return NULL;
        }

        // 优先传给无球人
        vector<IPlayer*> nonDefenderArray(Macro_NonDefenders);
        nonDefenderArray.clear();
        for (size_t i = 0; i < passTargets.size(); i++)
        {
            if (passTargets[i]->Status()->GetDefenceStatus()->Defenders().size() == 0)
            {
                nonDefenderArray.push_back(passTargets[i]);
            }
        }

        if (nonDefenderArray.size() > 0)
        {
            return nonDefenderArray[RandomHelper::GetInt32(0, nonDefenderArray.size() - 1)];
        }
        else
        {
            // 没有无球人则传给离防守人最远的球员
            return passTargets[Common::GetDoubleMaxIndexQuick(distanceArray, passTargets.size())];
        }
    }
};

#endif //__PASSTARGETDECIDERULE_H__
