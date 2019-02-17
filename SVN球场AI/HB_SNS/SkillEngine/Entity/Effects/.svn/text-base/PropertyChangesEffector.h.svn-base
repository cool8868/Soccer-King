/********************************************************************************
 * 文件名：PropertyChangesEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PROPERTYCHANGESEFFECTOR_H__
#define __PROPERTYCHANGESEFFECTOR_H__

#include "../Parts/PropertyChangesSkillPart.h"
#include "../../Entity/BuffFactory.h"
#include "../../../Interface/Skill/IEffect.h"
#include "../../../Interface/Skill/IGetSkillTarget.h"
#include "../../../Exception/ArgumentException.h"
#include "../../../Buff/AbsBuff.h"

class PropertyChangesEffector : public IEffect, public IGetSkillTarget
{
public:

    void Effect(IPlayer* player, ISkillPart* skill)
    {
        if (skill == NULL)
        {
            return;
        }

        PropertyChangesSkillPart* skillPart = PointerCastSafe(PropertyChangesSkillPart, skill);

        if (skillPart == NULL)
        {
            throw ArgumentException("Invoke the PropertyChangesEffector to Effect a skill with error skill part. Argument need PropertyChangesSkillPart but is NULL");
        }

        foreach (PropertyChange* change, skillPart->PropertyChanges())
        {
            AbsBuff* buff               = singleton_default<BuffFactory>::instance().CreateBuff(change, player);
            vector<IPlayer*> targets    = singleton_default<TargetSelector>::instance().GetTarget(change->Target(), change->TargetPosition(), player);

            if (targets.size() == 0)
            {
                continue;
            }

            if (buff == NULL)
            {
                continue;
            }

            if (buff->IsBuff())
            {
                foreach (IPlayer* target, targets)
                {
                    target->AddBuff(buff->Clone());
                }
            }
            else
            {
                foreach (IPlayer* target, targets)
                {
                    target->AddDebuff(buff->Clone());
                }
            }
        }
    }

    /// Get the skill's effect targets.        
    /// <param name="player">Represents the trigger man.</param>
    /// <param name="part">Represents the <see cref="ISkillPart"/>.</param>
    /// <returns>The effected players.</returns>
    vector<IPlayer*> GetSkillTargets(IPlayer* player, ISkillPart* part)
    {
        PropertyChangesSkillPart* propertyChanges = PointerCastSafe(PropertyChangesSkillPart, part);
        vector<IPlayer*> targets(4);
        targets.clear();

        if (propertyChanges != NULL)
        {
            foreach (PropertyChange* change, propertyChanges->PropertyChanges())
            {
                Common::AddRange(targets, singleton_default<TargetSelector>::instance().GetTarget(change->Target(), change->TargetPosition(), player));
            }
        }

        return targets;
    }
};

#endif //__PROPERTYCHANGESEFFECTOR_H__
