/********************************************************************************
 * 文件名：ManagerChangesEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MANAGERCHANGESEFFECTOR_H__
#define __MANAGERCHANGESEFFECTOR_H__

class ManagerChangesEffector : public IEffect
{
public:

    void Effect(IPlayer* player, ISkillPart* skill)
    {
        if (skill == NULL)
        {
            return;
        }

        ManagerChangesSkillPart* skillPart = PointerCastSafe(ManagerChangesSkillPart, skill);

        if (skillPart == NULL)
        {
            throw ArgumentException("Invoke the ManagerChangesEffector to Effect a skill with error skill part. Argument need ManagerChangesSkillPart but is NULL");
        }

        foreach (ManagerPropertyChange* propertyChange, skillPart->ManagerPropertyChanges())
        {
            if (propertyChange->Property() == "22") // 协防效果，临时添加
            {
                player->GetManager()->Status()->HelpDefenseRate() += (int)(player->GetManager()->Status()->HelpDefenseRate() * propertyChange->Rate() / 100);
                continue;
            }

            vector<IPlayer*> targets;
            if (propertyChange->Coordinate().size() != 0)
            {
                targets = singleton_default<TargetSelector>::instance().GetTarget(propertyChange->Target(), propertyChange->TargetPosition(), player, propertyChange->Coordinate());
            }
            else
            {
                vector<int> coordinates;
                coordinates += 0, Defines_Pitch.MAX_HEIGHT;

                targets = singleton_default<TargetSelector>::instance().GetTarget(propertyChange->Target(), propertyChange->TargetPosition(), player, coordinates);
            }

            AbsBuff* buff = singleton_default<BuffFactory>::instance().CreateBuff(propertyChange, player, skillPart->SkillReference()->Last());

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
};

#endif //__MANAGERCHANGESEFFECTOR_H__
