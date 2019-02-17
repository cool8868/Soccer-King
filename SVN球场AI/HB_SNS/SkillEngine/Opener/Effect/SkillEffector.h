/********************************************************************************
 * 文件名：SkillEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SKILLEFFECTOR_H__
#define __SKILLEFFECTOR_H__

/// Represents a opener type skill effector.
class SkillEffector 
{
public:

    virtual ~SkillEffector() {}

public:

    /// Effects a opener skill.
    void Effect(IPlayer* player, IOpenerRawSkill* skill) 
    {
        vector<int>& formation = skill->Formation();

        if (formation.size() == 0) 
        {
            throw SkillErrorException("Skill Config error. The manager skill has no formation config.");
        }

        if (formation[0] != 0) 
        {
            if (find(formation.begin(), formation.end(), player->GetManager()->FormationId()) == formation.end())
            {
                return;
            }
        }

        singleton_default<ManagerChangesEffector>::instance().Effect(player, skill->ManagerChanges());
        singleton_default<SkillUpgradesEffector>::instance().Effect(player, skill->SkillUpgrades());
        singleton_default<ImmunityEffector>::instance().Effect(player, skill->Immunity());
        singleton_default<SpecialEffectsEffector>::instance().Effect(player, skill->SpecialEffects());            
    }
};

#endif //__SKILLEFFECTOR_H__
