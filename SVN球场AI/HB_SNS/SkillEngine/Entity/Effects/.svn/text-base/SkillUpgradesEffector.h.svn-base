/********************************************************************************
 * 文件名：SkillUpgradesEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SKILLUPGRADESEFFECTOR_H__
#define __SKILLUPGRADESEFFECTOR_H__

class SkillUpgradesEffector : public IEffect
{
public:

    void Effect(IPlayer* player, ISkillPart* skill)
    {
        if (skill == NULL)
        {
            return;
        }

        SkillUpgradesSkillPart* skillPart = PointerCastSafe(SkillUpgradesSkillPart, skill);

        if (skillPart == NULL)
        {
            throw ArgumentException("Invoke the SkillUpgradesEffector to Effect a skill with error skill part. Argument need SkillUpgradesSkillPart but is NULL");
        }

        foreach (SkillUpgrade* upgrade, skillPart->SkillUpgrades())
        {
            if (upgrade->Type() == 3)
            {
                singleton_default<ActionTypeEffector>::instance().Effect(player, upgrade);
            }
        }
    }

    void Effect(vector<IActionSkill*>& targetSkills, SkillUpgrade* upgrade)
    {
        // 0 means none debuff  
        if (upgrade->TargetDebuff() != NULL && upgrade->TargetDebuff().size() != 0 && upgrade->TargetDebuff()[0] != 0)
        {               
            singleton_default<DebuffStatusEffector>::instance().Effect(targetSkills, upgrade);
        }

        foreach (int type, upgrade->TargetSkillProperty())
        {
            if (type == 0)
            {
                continue;
            }

            ISkillPropertyEffector* effector = NULL;

            if (type == 1)
            {
                effector = singleton_default<CdEffector>::instance();
            }

            if (type == 2)
            {
                effector = singleton_default<InjureRateEffector>::instance();
            }

            if (type == 3)
            {
                effector = singleton_default<DebuffLastEffector>::instance();
            }

            if (type == 4)
            {
                effector = singleton_default<DebuffRateEffector>::instance();
            }

            if (type == 5)
            {
                effector = singleton_default<BuffLastEffector>::instance();
            }

            if (type == 6)
            {
                effector = singleton_default<BuffRateEffector>::instance();
            }

            if (type == 7)
            {
                effector = singleton_default<FoulRateEffector>::instance();
            }

            if (type == 8)
            {
                effector = singleton_default<TriggerRateEffector>::instance();
            }

            if (effector == NULL)
            {
                throw SkillErrorException("Can't find the skill upgrade sub effector. Type:" + lexical_cast<string>(type));
            }

            effector->Effect(targetSkills, upgrade);
        }
    }
};

class ISkillUpgradeEffector
{
public:

    virtual ~ISkillUpgradeEffector() {}

public:

    void Effect(IPlayer* player, SkillUpgrade* upgrade);
};

#endif //__SKILLUPGRADESEFFECTOR_H__

