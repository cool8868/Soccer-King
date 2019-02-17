/********************************************************************************
 * 文件名：BuffFactory.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __BUFFFACTORY_H__
#define __BUFFFACTORY_H__

#include "../Entity/Parts/ManagerChangesSkillPart.h"

#include "../../Buff/AbsBuff.h"
#include "../../MatchData/PlayerData/PlayerPropertyInitializer.h"

class BuffFactory
{
public:

    /// Create a buff.
    /// <param name="skill">Represents a record of the <see cref="PropertyChange"/> class.</param>
    /// <param name="player">Represents the skill trigger <see cref="IPlayer"/>.</param>
    /// <returns>Represents a <see cref="AbsBuff"/></returns>
    AbsBuff* CreateBuff(PropertyChange* skill, IPlayer* player)
    {
        if (skill->Property() == "*")
        {
            return InternalCreateBuff(player, skill->Last(), skill->Rate(), skill->Gradient(), PlayerPropertyInitializer::PlayerPropertyIds());
        }
        else
        {
            return InternalCreateBuff(player, skill->Last(), skill->Rate(), skill->Gradient(), Common::TransToStringVector(skill->Property(), ","));
        }
    }

    /// Create a buff.
    /// <param name="skill">Represents a record of the <see cref="ManagerPropertyChange"/> class.</param>
    /// <param name="player">Represents the skill trigger <see cref="IPlayer"/></param>
    /// <param name="last">Represents the buff's lasting time.</param>
    /// <returns><see cref="AbsBuff"/></returns>
    AbsBuff* CreateBuff(ManagerPropertyChange* skill, IPlayer* player, short last)
    {
        if (skill->Property() == "*")
        {
            return InternalCreateBuff(player, last, skill->Rate(), skill->Gradient(), PlayerPropertyInitializer::PlayerPropertyIds());
        }
        else
        {
            return InternalCreateBuff(player, last, skill->Rate(), skill->Gradient(), Common::TransToStringVector(skill->Property(), ","));
        }
    }

private:

    /// Create a <see cref="AbsBuff"/> with the arguments: player id, lasting time,
    /// effect rate, effect gradient.
    /// <param name="player">Represents the trigger <see cref="IPlayer"/>.</param>
    /// <param name="last">Represents the buff lasting time.</param>
    /// <param name="rate">Represents the buff effected rate.</param>
    /// <param name="gradient">Represents the buff effected gradient.</param>
    /// <param name="propertyId">Represents the buff effected property's id.</param>
    /// <returns></returns>
    /// 1. 如果gradient为0，那么该BUFF为Dot类型
    /// 2. Dot类型的BUFF，gradient属性和last属性都是原始值(以min为单位)
    /// 3. Dot类型的BUFF，TimeLapse方法只在每分钟时调用
    AbsBuff* InternalCreateBuff(IPlayer* player, int last, double rate, double gradient, vector<int>& propertyId)
    {
        if (last == -1)
        { 
            // hold ball buff type.
            if (rate > 0.0)
            {
                HoldBallBuff* holdBallBuff = new HoldBallBuff(player->Id());
                holdBallBuff->SetPropertyId(propertyId);
                holdBallBuff->SetRate(rate);

                return holdBallBuff;
            }
            else
            {
                HoldBallDebuff* holdBallDebuff = new HoldBallDebuff(player->Id());
                holdBallDebuff->SetPropertyId(propertyId);
                holdBallDebuff->SetRate(rate);

                return holdBallDebuff;
            }
        }

        if (MATH::FloatEqual(gradient, 0.0) == false)
        { 
            // dot type
            if (gradient > 0.0)
            {
                DotBuff* dotBuff = new DotBuff(player->Id());
                dotBuff->SetGradient(gradient);
                dotBuff->SetPropertyId(propertyId);

                return dotBuff;
            }
            else
            {
                DotDebuff* dotDebuff = new DotDebuff(player->Id());
                dotDebuff->SetGradient(gradient);
                dotDebuff->SetPropertyId(propertyId);

                return dotDebuff;
            }
        }

        if (last == 0)
        { 
            // 0 means 1 round
            last = 1;
        }
        else
        {
            last = Common::ConvertTimeToRound(last, player->GetMatch()->GetTotalRound());   // convert to round
        }

        if (rate > 0.0)
        {
            Buff* buff = new Buff(player->Id(), last, BuffType_Skill);
            buff->SetRate(rate);
            buff->SetPropertyId(propertyId);

            return buff;
        }
        else
        {
            Debuff* debuff = new Debuff(player->Id(), last, BuffType_Skill);

            return debuff;
        }
    }

    /// Create a <see cref="AbsBuff"/> with the arguments: player id, lasting time,
    /// effect rate, effect gradient.
    /// <param name="player">Represents the trigger <see cref="IPlayer"/>.</param>
    /// <param name="last">Represents the buff lasting time.</param>
    /// <param name="rate">Represents the buff effected rate.</param>
    /// <param name="gradient">Represents the buff effected gradient.</param>
    /// <param name="propertyId">Represents the buff effected property's id.</param>
    /// 1. 如果gradient为0，那么该BUFF为Dot类型
    /// 2. Dot类型的BUFF，gradient属性和last属性都是原始值(以min为单位)
    /// 3. Dot类型的BUFF，TimeLapse方法只在每分钟时调用
    /// </remarks>
    AbsBuff InternalCreateBuff(IPlayer* player, int last, double rate, double gradient, vector<string>& propertyId)
    {
        vector<int> proIds(propertyId.size());

        for (size_t i = 0; i < propertyId.size(); i++)
        {
            proIds[i] = trim_copy(propertyId[i]);
        }

        return InternalCreateBuff(player, last, rate, gradient, proIds);
    }
};

#endif //__BUFFFACTORY_H__
