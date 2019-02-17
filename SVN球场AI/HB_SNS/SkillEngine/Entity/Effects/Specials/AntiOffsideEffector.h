/********************************************************************************
 * 文件名：AntiOffsideEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ANTIOFFSIDEEFFECTOR_H__
#define __ANTIOFFSIDEEFFECTOR_H__

#include "../SpecialEffectsEffector.h"

/// Represents the anti-offset effector.
class AntiOffsideEffector : public ISpecialEffector
{
public:

    /// Effects the anti-offside effect.
    void Effect(IPlayer* player, Special* skill)
    {
        if (player->GetManager()->Opponent()->GetPlayersByPosition(Position_Fullback).size() >= 2)
        {
            double value = lexical_cast<double>(skill->Value());

            vector<IPlayer*> targets = player->GetManager()->GetPlayersByPosition(Position_Forward);
            foreach (IPlayer* p, targets)
            {
                p->CurrProperty()[PlayerProperty_Speed]     += p->CurrProperty[PlayerProperty_Speed] * value / 100;
                p->CurrProperty()[PlayerProperty_Shooting]  += p->CurrProperty()[PlayerProperty_Shooting] * value / 100;
                p->CurrProperty()[PlayerProperty_FreeKick]  += p->CurrProperty()[PlayerProperty_FreeKick] * value / 100;

                p->CurrProperty()[PlayerProperty_Dribble]   += p->CurrProperty()[PlayerProperty_Dribble] * value / 100;
                p->CurrProperty()[PlayerProperty_Passing]   += p->CurrProperty()[PlayerProperty_Passing] * value / 100;
                p->CurrProperty()[PlayerProperty_Mentality] += p->CurrProperty()[PlayerProperty_Mentality] * value / 100;
            }
        }
    }

    string ToString()
    {
        return "AntiOffsideEffector";
    }
};

#endif //__ANTIOFFSIDEEFFECTOR_H__
