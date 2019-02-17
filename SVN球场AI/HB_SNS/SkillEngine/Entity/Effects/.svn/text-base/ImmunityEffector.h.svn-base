/********************************************************************************
 * 文件名：ImmunityEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IMMUNITYEFFECTOR_H__
#define __IMMUNITYEFFECTOR_H__

/// 表示了免疫效果的解析器
class ImmunityEffector : public IEffect
{
public:
    
    /// Effect a <see cref="ImmunitySkillPart"/>.
    /// <param name="player"><see cref="IPlayer"/></param>
    /// <param name="skill"><see cref="ISkillPart"/></param>
    /// <remarks>
    /// 这里避免使用了多态和函数调用，提高了性能
    /// </remarks>
    void Effect(IPlayer* player, ISkillPart* skill)
    {
        if (skill == NULL)
        {
            return;
        }

        ImmunitySkillPart* skillPart = PointerCastSafe(ImmunitySkillPart, skill);

        if (skillPart == NULL)
        {
            throw ArgumentException("Invoke the ImmunityEffector to Effect a skill with error skill part. Argument need ImmunitySkillPart but is NULL");
        }

        foreach (Immunity* immunity, skillPart->Immunities())
        {
            if (immunity->Type() == 0) // 改变对方技能减益效果对本队的影响 (百分比方式, 包含正负号 exp:-5%, 5%)
            {
                // +++ tony
                player.Manager.Status.DecreaseDebuffRateStatus.Rate += immunity.Rate;

                if (skillPart->SkillReference()->Last() > 45)
                {
                    player.Manager.Status.DecreaseDebuffRateStatus.RawRate += immunity.Rate;
                }

                continue;
            }

            if (immunity->Type() == 1) // 对方减益技能的抵抗率
            {
                // +++ tony:
                player.Manager.Status.ImmunityDebuffSkillStatus.Probability += immunity.Probability;

                if (skillPart->SkillReference()->Last() > 45)
                {
                    player.Manager.Status.ImmunityDebuffSkillStatus.RawProbability += immunity.Probability;
                }

                continue;
            }

            if (immunity->Type() == 2) // 犯规时惩罚降低的级别
            {
                player.Manager.Status.DecreaseFoulLevelStatus.Rate += immunity.Rate;
                player.Manager.Status.DecreaseFoulLevelStatus.Probability += immunity.Probability;

                if (skillPart.SkillReference.Last > 45)
                {
                    player.Manager.Status.DecreaseFoulLevelStatus.RawRate += immunity.Rate;
                    player.Manager.Status.DecreaseFoulLevelStatus.RawProbability += immunity.Probability;
                }

                continue;
            }

            if (immunity.Type == 3) // 犯规时不受任何制裁的概率
            {
                player.Manager.Status.NoFoulLevelStatus.Probability += immunity.Probability;

                if (skillPart.SkillReference.Last > 45)
                {
                    player.Manager.Status.NoFoulLevelStatus.RawProbability += immunity.Probability;
                }

                continue;
            }

            if (immunity.Type == 4) // 犯规时转移给对方的概率
            {
                player.Manager.Status.TransferFoulLevelStatus.Probability += immunity.Probability;

                if (skillPart.SkillReference.Last > 45)
                {
                    player.Manager.Status.TransferFoulLevelStatus.RawProbability += immunity.Probability;
                }

                continue;
            }

            if (immunity.Type == 5) // 不受伤的概率
            {
                player.Manager.Status.NoInjuredStatus.Probability += immunity.Probability;

                if (skillPart.SkillReference.Last > 45)
                {
                    player.Manager.Status.NoInjuredStatus.RawProbability += immunity.Probability;
                }

                continue;
            }

            if (immunity.Type == 6) // 减少体力下降的幅度
            {
                player.Manager.Status.DecreaseStaminaRateStatus.Rate += immunity.Rate;

                if (skillPart.SkillReference.Last > 45)
                {
                    player.Manager.Status.DecreaseStaminaRateStatus.RawRate += immunity.Rate;
                }

                continue;
            }

            if (immunity.Type == 7) // 修改debuff的抵抗率
            {
                if (immunity.TargetDebuff == 0)
                {
                    continue;
                }

                player.Manager.Status.ImmunityDebuffsStatus.DebuffProperty[immunity.TargetDebuff] += immunity.Probability;

                if (skillPart.SkillReference.Last > 45)
                {
                    player.Manager.Status.ImmunityDebuffsStatus.RawDebuffProperty[immunity.TargetDebuff] += immunity.Probability;
                }
            }

            continue;
        }

    }
};

#endif //__IMMUNITYEFFECTOR_H__

