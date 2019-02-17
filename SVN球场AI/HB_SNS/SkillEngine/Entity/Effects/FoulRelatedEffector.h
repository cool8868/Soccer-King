/********************************************************************************
 * 文件名：FoulRelatedEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FOULRELATEDEFFECTOR_H__
#define __FOULRELATEDEFFECTOR_H__

/// 犯规技能的解析效果器
class FoulRelatedEffector : public IEffect, public IGetSkillTarget
{
public:

    FoulRelatedEffector()
        : TARGET_SELF(0)            // 自己
        , TARGET_OPP(1)             // 对方
        , FOUL_CHANGE_DECREASE(0)   // 降低犯规
        , FOUL_CHANGE_TRANSFER(1)   // 转移至对方
    {
    }
    
    /// Effect a skill
    /// <param name="player">Represents the skill trigger man.</param>
    /// <param name="skill">Represents the triggering skill.</param>
    void Effect(IPlayer* player, ISkillPart* skill)
    {
        if (skill == NULL)
        {
            return;
        }

        FoulRelatedSkillPart* foulConfigPart = PointerCastSafe(FoulRelatedSkillPart, skill);

        if (foulConfigPart == NULL)
        {
            throw SkillErrorException("FoulRelated effects find exceptions: The incoming skill part is not FoulRelatedSkillPart.");
        }

        int triggerFoul = Defines_FoulLevel.NONE;
        int targetFoul  = Defines_FoulLevel.NONE;

        // 解析犯规相关的数据
        foreach (Foul* foul, foulConfigPart->Fouls()) // resolve the foul level 解析犯规相关的数据
        {
            if (RandomHelper::GetPercentage() >= foul->Probability())
            {
                continue;
            }

            if (foul->Target() == TARGET_SELF) // self (trigger man) 自己
            { 
                triggerFoul = foul->Type();
            }
            else // 对方
            {
                targetFoul = foul->Type()();
            }
        }

        // 解析犯规变化相关
        foreach (FoulChange* change, foulConfigPart->FoulChanges()) // resolve the foul changes 解析犯规变化相关
        {
            int type = change->Type();

            if (type == FOUL_CHANGE_DECREASE) // decrease the foul level 降低犯规等级
            { 
                if (triggerFoul == Defines_FoulLevel.INJURED)
                {
                    continue;
                }

                triggerFoul -= change->Lv();

                if (triggerFoul < Defines_FoulLevel.NORMAL) // the minimum is zero(Normal Foul) 
                {
                    triggerFoul = Defines_FoulLevel.NORMAL; // 最低降低到普通犯规
                }
            }

            if (type == FOUL_CHANGE_TRANSFER) // change my foul level to target.将犯规转移给对方
            { 
                targetFoul  = triggerFoul;
                triggerFoul = Defines_FoulLevel.NONE;
            }
        }

        IPlayer* skillTarget = InternalGetTarget(player);

        // 受伤相关
        if (triggerFoul == Defines_FoulLevel.INJURED)
        {
            player->Injured();
        }

        if (targetFoul == Defines_FoulLevel.INJURED)
        {
            skillTarget->Injured();
        }

        // 经理技能拥有一定概率将犯规转移给对方
        // manager skill has chance to transfer the foul level to the target.
        // +++ tony:
        if (player.Manager.Status.TransferFoulLevelStatus.Probability != 0)
        {
            if (RandomHelper::GetPercentage() < player.Manager.Status.TransferFoulLevelStatus.Probability)
            {
                if (skillTarget->Status()->Enable())
                {
                    targetFoul = triggerFoul;

                }

                triggerFoul = Defines_FoulLevel.NONE;
            }
        }

        if (triggerFoul != Defines_FoulLevel.NONE && triggerFoul != Defines_FoulLevel.INJURED)
        {
            player->Foul((int)triggerFoul);
        }

        if (targetFoul != Defines_FoulLevel.NONE && targetFoul != Defines_FoulLevel.INJURED)
        {
            skillTarget->Foul((int)targetFoul);
        }
    }

    /// Get the skill's effect targets.        
    /// <param name="player">Represents the trigger man.</param>
    /// <param name="part">Represents the <see cref="ISkillPart"/>.</param>
    /// <returns>The effected players.</returns>
    vector<IPlayer*> GetSkillTargets(IPlayer* player, ISkillPart* part)
    {
        FoulRelatedSkillPart* skill = PointerCastSafe(FoulRelatedSkillPart, part);
        vector<IPlayer*> targets(2);
        targets.clear();

        if (skill != NULL)
        {
            foreach (Foul* foul, skill->Fouls())
            {
                if (foul->Target() == TARGET_SELF)
                {
                    targets.push_back(player);
                }
                else // TARGET_OPP
                { 
                    targets.push_back(InternalGetTarget(player));
                }
            }
        }

        return targets;
    }

private:

    /// 获取犯规目标
    IPlayer* InternalGetTarget(IPlayer* player)
    {
        vector<int> targetPositon;
        targetPositon += 0;

        return singleton_default<TargetSelector>::instance().GetTarget(1, targetPositon, player)[0];
    }

private:
    
    const int TARGET_SELF;              // 自己
    const int TARGET_OPP;               // 对方

    const int FOUL_CHANGE_DECREASE;     // 降低犯规
    const int FOUL_CHANGE_TRANSFER;     // 转移至对方
};

#endif //__FOULRELATEDEFFECTOR_H__
