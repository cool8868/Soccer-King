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

#include "../../Entity/Effects/PropertyChangesEffector.h"
#include "../../../common/common.h"

/// Represents a action type skill effector.
class SkillEffector
{
public:

    virtual ~SkillEffector() {}

public:

    /// Effects a skill.
    void Effect(IPlayer* player, IActionRawSkill* skill)
    {
        singleton_default<PropertyChangesEffector>::instance().Effect(player, skill->PropertyChanges());
        singleton_default<DisplacementsEffector>::instance().Effect(player, skill->Displacements());
        singleton_default<BallRelatedEffector>::instance().Effect(player, skill->BallRelated());
        singleton_default<DebuffsEffector>::instance().Effect(player, skill->Debuffs());
        singleton_default<ModelEffector>::instance().Effect(player, skill->Models());
        singleton_default<SpecialEffectsEffector>::instance().Effect(player, skill->SpecialEffects());
        singleton_default<FoulRelatedEffector>::instance().Effect(player, skill->FoulRelated());
    }
};

#endif //__SKILLEFFECTOR_H__
