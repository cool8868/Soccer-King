/********************************************************************************
 * 文件名：ActionRawSkill.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ACTIONRAWSKILL_H__
#define __ACTIONRAWSKILL_H__

#include "../../common/common.h"

#include "../../Skill/RawSkill.h"
#include "../../Interface/Skill/IActionRawSkill.h"

class ActionRawSkill : public RawSkill, public IActionRawSkill 
{
public:

    /// Initializes a new instance of the <see cref="RawSkill"/> class.
    /// <param name="id">Represents the skill's id.</param>
    /// <param name="name">Represents the skill's name.</param>        
    /// <param name="cd">Represents the skill's cd.(unit minute)</param>
    ActionRawSkill(string id, string name, int cd);

public:

    string          Id() { return RawSkill::Id(); }

    string          Name() { return RawSkill::Name(); }

    SkillType       Type() { return RawSkill::Type(); }

    int             CoolDown() { return m_CoolDown; }
    void            SetCoolDown(int vl) { m_CoolDown = vl; }

    SkillClass      Class() { return m_Class; }
    void            SetClass(SkillClass vl) { m_Class = vl; }

    ISkillPart*     TriggerConditions() { return m_TriggerConditions; }
    void            SetTriggerConditions(ISkillPart* vl) { m_TriggerConditions = vl; }

    ISkillPart*     PropertyChanges() { return m_PropertyChanges; }
    void            SetPropertyChanges(ISkillPart* vl) { m_PropertyChanges = vl; }

    ISkillPart*     Displacements() { return m_Displacements; }
    void            SetDisplacements(ISkillPart* vl) { m_Displacements = vl; }

    ISkillPart*     FoulRelated() { return m_FoulRelated; }
    void            SetFoulRelated(ISkillPart* vl) { m_FoulRelated = vl; }

    ISkillPart*     BallRelated() { return m_BallRelated; }
    void            SetBallRelated(ISkillPart* vl) { m_BallRelated = vl; }

    ISkillPart*     Debuffs() { return m_Debuffs; }
    void            SetDebuffs(ISkillPart* vl) { m_Debuffs = vl; }

    ISkillPart*     Models() { return m_Models; }
    void            SetModels(ISkillPart* vl) { m_Models = vl; }

    ISkillPart*     SpecialEffects() { return m_SpecialEffects; }
    void            SetSpecialEffects(ISkillPart* vl) { m_SpecialEffects = vl; }

    void            SetSkillPart(string name, ISkillPart* skillPart);

protected:

    /// Represents the skill's cool down time.(unit minute)
    int             m_CoolDown;

    /// Represents the skill's class.        
    SkillClass      m_Class;

    /// Represents the trigger conditions part.
    ISkillPart*     m_TriggerConditions;

    /// Represents the property changes part.
    ISkillPart*     m_PropertyChanges;

    /// Represents the displacements part.
    ISkillPart*     m_Displacements;

    /// Represents the foul related part.
    ISkillPart*     m_FoulRelated;

    /// Represents the ball related part.
    ISkillPart*     m_BallRelated;

    /// Represents the debuffs part.
    ISkillPart*     m_Debuffs;

    /// Represents the models part.
    ISkillPart*     m_Models;

    /// Represents the special effects part.
    ISkillPart*     m_SpecialEffects;
};

#endif //__ACTIONRAWSKILL_H__
