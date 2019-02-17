/********************************************************************************
 * 文件名：DebuffsEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DEBUFFSEFFECTOR_H__
#define __DEBUFFSEFFECTOR_H__

/// Represents the debuff skill part's effector.
class DebuffsEffector : public IEffect, public IGetSkillTarget
{
public:

    /// Effect a skill
    void Effect(IPlayer* player, ISkillPart* skill)
    {
        if (skill == NULL)
        {
            return;
        }

        DebuffsSkillPart* skillPart = PointerCastSafe(DebuffsSkillPart, skill);

        if (skillPart == NULL)
        {
            throw SkillErrorException("DebuffsSkillPart effects find exceptions: The incoming skill part is not DebuffsSkillPart.");
        }

        foreach (Debuff* debuff, skillPart->Debuffs())
        {
            vector<IPlayer*> targets = singleton_default<TargetSelector>::Instance().GetTarget(debuff->Target(), debuff->TargetPosition(), player);
            if (RandomHelper::GetPercentage() > debuff->Probability())
            {
                continue;
            }

            if (debuff->Type() == (int)DebuffType_DownDebuff)
            {
                int last = singleton_default<DebuffConfigCache>::Instance().GetDebuffLastTime(debuff->Level(), debuff->Type());
                last = Utility::ConvertTimeToRound(last, player->GetMatch()->GetTotalRound());
                foreach (IPlayer* target, targets)
                {
                    target->AddDownDebuff(player->Id(), last);
                }
            }

            if (debuff->Type() == (int)DebuffType_PuzzleDebuff)
            {
                int last = singleton_default<DebuffConfigCache>::Instance().GetDebuffLastTime(debuff->Level(), debuff->Type());
                last = Utility::ConvertTimeToRound(last, player->GetMatch()->GetTotalRound());
                foreach (IPlayer* target, targets)
                {
                    target->AddPuzzleDebuff(player->Id(), last);
                }
            }

            if (debuff->Type() == (int)DebuffType_OutOfHandDebuff)
            {
                foreach (IPlayer* target, targets)
                {
                    target->AddOutOfHandDebuff(player->Id(), debuff->Level());
                }
            }

            if (debuff->Type == (int)DebuffType_StunDebuff)
            {
                foreach (IPlayer* target, targets)
                {
                    target->AddStunDebuff(player->Id(), debuff->Level());
                }
            }

            if (debuff->Type() == (int)DebuffType_FreezeDebuff)
            {
                int last = singleton_default<DebuffConfigCache>::Instance().GetDebuffLastTime(debuff->Level(), debuff->Type());
                last = Utility::ConvertTimeToRound(last, player->GetMatch()->GetTotalRound());
                foreach (IPlayer* target, targets)
                {
                    target->AddFreezeDebuff(player->Id(), (int)last);
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
        DebuffsSkillPart* skill = PointerCastSafe(DebuffsSkillPart, part);
        vector<IPlayer*> targets(2);
        targets.clear();

        if (skill != NULL)
        {
            foreach (DeBuff* debuff, skill->Debuffs())
            {
                Common::AddRange(targets, singleton_default<TargetSelector>::Instance().GetTarget(debuff->Target(), debuff->TargetPosition(), player));
            }
        }

        return targets;
    }
};

#endif //__DEBUFFSEFFECTOR_H__
