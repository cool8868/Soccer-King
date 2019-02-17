/********************************************************************************
 * 文件名：ImmunitySkillPart.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IMMUNITYSKILLPART_H__
#define __IMMUNITYSKILLPART_H__

#include "../../../common/common.h"

#include "../../../Interface/Skill/ISkillPart.h"
#include "../../../Interface/Skill/IManagerSkill.h"

class Immunity;

/// Represents a skill part that defines the immunity infos.
class ImmunitySkillPart : public ISkillPart
{
public:
    
    ImmunitySkillPart();

    ImmunitySkillPart(ImmunitySkillPart& rf);

    ~ImmunitySkillPart();

public:

    IManagerSkill*          SkillReference() { return m_SkillReference; }
    void                    SetSkillReference(IManagerSkill* vl) { m_SkillReference = vl; }

    vector<Immunity*>&      Immunities() { return m_Immunities; }

    ImmunitySkillPart*      Clone() { return new ImmunitySkillPart(*this); }

private:

    /// Represents the reference of the <see cref="IRawSkill"/>.
    IManagerSkill*          m_SkillReference;

    /// Represents the list of the <see cref="Immunity"/>.
    vector<Immunity*>       m_Immunities;

private:

    void                    InitVariables();
};

/// Represents a record of the <see cref="Immunity"/>.
class Immunity
{
public:

    Immunity();

    Immunity(Immunity& rf);

    virtual ~Immunity() {}

public:

    int         Type() { return m_Type; }
    void        SetType(int vl) { m_Type = vl; }

    double      Rate() { return m_Rate; }
    void        SetRate(double valule) { m_Rate = valule; }

    int         TargetDebuff() { return m_TargetDebuff; }
    void        SetTargetDebuff(int vl) { m_TargetDebuff = vl; }

    int         Probability() { return m_Probability; }
    void        SetProbability(int vl) { m_Probability = vl; }

    Immunity*   Clone() { return new Immunity(*this); }

protected:

    /// Represents the <see cref="Immunity"/>'s type.
    int         m_Type;

    /// Represents the <see cref="Immunity"/>'s rate.
    double      m_Rate;

    /// Represents the target debuff id.
    int         m_TargetDebuff;

    /// Represents the probability.
    int         m_Probability;

private:

    void        InitVariables();
};

#endif //__IMMUNITYSKILLPART_H__
