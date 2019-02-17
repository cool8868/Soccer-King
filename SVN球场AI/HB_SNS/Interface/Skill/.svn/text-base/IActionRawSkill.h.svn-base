/********************************************************************************
 * 文件名：IActionRawSkill.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IACTIONRAWSKILL_H__
#define __IACTIONRAWSKILL_H__

#include "IRawSkill.h"
#include "ISkillPart.h"

#include "../../common/Enum/SkillClass.h"

/// 表示了一个Action技能的接口
class IActionRawSkill : public IRawSkill
{
public:
    
    /// 表示了技能的冷却时间
    virtual int             CoolDown() = 0;
    virtual void            SetCoolDown(int time) = 0;

    /// 表示了技能的分类
    virtual SkillClass      Class() = 0;
    virtual void            SetClass(SkillClass type) = 0;

    virtual ISkillPart*     TriggerConditions() = 0;
    virtual void            SetTriggerConditions(ISkillPart* skill) = 0;

    virtual ISkillPart*     PropertyChanges() = 0;
    virtual void            SetPropertyChanges(ISkillPart* skill) = 0;

    virtual ISkillPart*     Displacements() = 0;
    virtual void            SetDisplacements(ISkillPart* skill) = 0;

    virtual ISkillPart*     FoulRelated() = 0;
    virtual void            SetFoulRelated(ISkillPart* skill) = 0;

    virtual ISkillPart*     BallRelated() = 0;
    virtual void            SetBallRelated(ISkillPart* skill) = 0;

    virtual ISkillPart*     Debuffs() = 0;
    virtual void            SetDebuffs(ISkillPart* skill) = 0;

    virtual ISkillPart*     Models() = 0;
    virtual void            SetModels(ISkillPart* skill) = 0;

    virtual ISkillPart*     SpecialEffects() = 0;
    virtual void            SetSpecialEffects(ISkillPart* skill) = 0;

    virtual void            SetSkillPart(string name, ISkillPart* skillPart) = 0;
};

#endif //__IACTIONRAWSKILL_H__
