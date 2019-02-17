/********************************************************************************
 * 文件名：PositionExchangeEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __POSITIONEXCHANGEEFFECTOR_H__
#define __POSITIONEXCHANGEEFFECTOR_H__

/// 交叉换位特殊效果
class PositionExchangeEffector : public ISpecialEffector
{
public:

    /// 交叉换位特殊效果
    void Effect(IPlayer* player, Special* skill)
    {
        // 当阵型中只有一个后卫时,后卫的防守属性将获得加成，数值为所有前锋以及中场进攻属性和组织属性之和的value%。
        if (player->GetManager()->GetPlayersByPosition(Position_Fullback).size() > 1)
        {
            return;
        }

        double total = 0.0;
        foreach (IPlayer* p, player->GetManager()->GetPlayersByPosition(Position_Midfielder))
        {
            total += p->CurrProperty()[PlayerProperty_Dribble];
            total += p->CurrProperty()[PlayerProperty_Passing];
            total += p->CurrProperty()[PlayerProperty_Mentality];
        }

        foreach (IPlayer* p, player->GetManager()->GetPlayersByPosition(Position_Forward))
        {
            total += p->CurrProperty()[PlayerProperty_Speed];
            total += p->CurrProperty()[PlayerProperty_Shooting];
            total += p->CurrProperty()[PlayerProperty_FreeKick];
        }

        double perUpgrade = total / 3 * lexical_cast<double>(skill->Value()) / 100;
        player->GetManager()->Players()[1]->CurrProperty()[PlayerProperty_Aggression] += perUpgrade;
        player->GetManager()->Players()[1]->RawProperty[PlayerProperty_Aggression] += perUpgrade;

        player->GetManager()->Players()[1]->CurrProperty()[PlayerProperty_Disturb] += perUpgrade;
        player->GetManager()->Players()[1]->RawProperty()[PlayerProperty_Disturb] += perUpgrade;

        player->GetManager()->Players()[1]->CurrProperty()[PlayerProperty_Interception] += perUpgrade;
        player->GetManager()->Players()[1]->RawProperty()[PlayerProperty_Interception] += perUpgrade;
    }

    string ToString()
    {
        return "PositionExchangeEffector";
    }
};

#endif //__POSITIONEXCHANGEEFFECTOR_H__
