/********************************************************************************
 * 文件名：OpenerRawSkill.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __OPENERRAWSKILL_H__
#define __OPENERRAWSKILL_H__

#include "../../Skill/RawSkill.h"
#include "../../Interface/Skill/IOpenerRawSkill.h"
#include "../../Interface/Skill/ISkillPart.h"

/// Represents a opener skill.
class OpenerRawSkill : public RawSkill , public IOpenerRawSkill 
{
public:

    /// Initializes a new instance of the <see cref="OpenerRawSkill"/> class.
    /// <param name="id">Represents the skill's id.</param>
    /// <param name="name">Represents the skill's name.</param>
    /// <param name="last">Represents the skill's lasting time.</param>
    /// <param name="formation">Represents the skill's formation id.</param>
    OpenerRawSkill(string id, string name, int last, vector<int>& formation);

public:

    string          Id() { return RawSkill::Id(); }

    string          Name() { return RawSkill::Name(); }

    SkillType       Type() { return RawSkill::Type(); }

    int             Last() { return m_Last; }
    void            SetLast(int vl) { m_Last = vl; }

    vector<int>&    Formation() { return m_Formation; }
    void            SetFormation(vector<int>& vl) { m_Formation = vl; }

    ISkillPart*     ManagerChanges() { return m_ManagerChanges; }
    void            SetManagerChanges(ISkillPart* vl) { m_ManagerChanges = vl; }

    ISkillPart*     SkillUpgrades() { return m_SkillUpgrades; }
    void            SetSkillUpgrades(ISkillPart* vl) { m_SkillUpgrades = vl; }

    ISkillPart*     Immunity() { return m_Immunity; }
    void            SetImmunity(ISkillPart* vl) { m_Immunity = vl; }

    ISkillPart*     SpecialEffects() { return m_SpecialEffects; }
    void            SetSpecialEffects(ISkillPart* vl) { m_SpecialEffects = vl; }

    void            SetSkillPart(string name, ISkillPart* skillPart);

protected:

    /// Represents the opener's lasting time.
    int             m_Last;

    /// Represents the opener's require formation index.
    vector<int>     m_Formation;

    /// Represents the manager changes skill part.
    ISkillPart*     m_ManagerChanges;

    /// Represents the skill upgrades skill part.
    ISkillPart*     m_SkillUpgrades;

    /// Represents the immunities skill part.
    ISkillPart*     m_Immunity;

    /// Represents the special effects skill part.
    ISkillPart*     m_SpecialEffects;
};

#endif //__OPENERRAWSKILL_H__
