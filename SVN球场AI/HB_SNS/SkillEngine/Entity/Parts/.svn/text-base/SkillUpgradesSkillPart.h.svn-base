/********************************************************************************
 * 文件名：SkillUpgradesSkillPart.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SKILLUPGRADESKILLPART_H__
#define __SKILLUPGRADESKILLPART_H__

#include "../../../common/common.h"
#include "../../../common/Utility.h"

#include "../../../Interface/Skill/ISkillPart.h"
#include "../../../Interface/Skill/IManagerSkill.h"

class SkillUpgrade;

/// Represents a skill part that defines the skill upgrades effect.
class SkillUpgradesSkillPart : public ISkillPart 
{
public:

    SkillUpgradesSkillPart();

    SkillUpgradesSkillPart(SkillUpgradesSkillPart& rf);

    ~SkillUpgradesSkillPart();

public:

    /// Represents the reference of the <see cref="IRawSkill"/>.
    IManagerSkill*          SkillReference() { return m_SkillReference; }
    void                    SetSkillReference(IManagerSkill* vl) { m_SkillReference = vl; }

    /// Represents the list of the <see cref="SkillUpgrade"/>.
    vector<SkillUpgrade*>&  SkillUpgrades() { return m_Upgrades; }

    SkillUpgradesSkillPart* Clone() { return new SkillUpgradesSkillPart(*this); }

private:

    IManagerSkill*          m_SkillReference;
    vector<SkillUpgrade*>   m_Upgrades;

private:

    void                    InitVariables();
};

/// Represents a record of the <see cref="SkillUpgradesSkillPart"/>.
class SkillUpgrade
{  
public:

    SkillUpgrade();

    SkillUpgrade(SkillUpgrade& rf);

    virtual ~SkillUpgrade() {}

public:

    int                 Type() { return m_Type; }
    void                SetType(int vl) { m_Type = vl; }

    vector<string>&     SkillType() { return m_SkillType; }
    void                SetSkillType(vector<string>& vl) { m_SkillType = vl; }
    void                SetSkillType(string vl) { m_SkillType = Common::TransToStringVector(vl, ","); }

    string              SkillTarget() { return m_SkillTarget; }
    void                SetSkillTarget(string vl) { m_SkillTarget = vl; }

    int                 Target() { return m_Target; }
    void                SetTarget(int vl) { m_Target = vl; }

    vector<int>&        TargetPosition() { return m_TargetPosition; }
    void                SetTargetPosition(vector<int>& vl) { m_TargetPosition = vl; }
    void                SetTargetPosition(string vl) { m_TargetPosition = Common::TransToValueVector<int>(vl, ","); }

    vector<int>&        TargetDebuff() { return m_TargetDebuff; }
    void                SetTargetDebuff(vector<int>& vl) { m_TargetDebuff = vl; }

    vector<int>&        TargetSkillProperty() { return m_TargetSkillProperty; }
    void                SetTargetSkillProperty(vector<int>& vl) { m_TargetSkillProperty = vl; }

    double              Rate() { return m_Rate; }
    void                SetRate(double vl) { m_Rate = vl; }

    int                 FixType() { return m_FixType; }
    void                SetFixType(int vl) { m_FixType = vl; }

    SkillUpgrade*       Clone() { return new SkillUpgrade(*this); }

protected:

    int                 m_Type;

    /// 表示了技能的分类
    /// * - 所有
    /// 0 - 射门
    /// 1 - 防守
    /// 2 - 组织
    /// 3 - 过人
    /// 4 - 守门
    vector<string>      m_SkillType;

    /// 表示了影响的技能id.
    string              m_SkillTarget;

    /// Represents the effected player.
    int                 m_Target;

    /// Represents the effected player's positions.
    vector<int>         m_TargetPosition;

    /// Represents the effected debuff.
    /// 影响的debuff编号
    vector<int>         m_TargetDebuff;

    /// 影响的技能效果id
    vector<int>         m_TargetSkillProperty;

    /// 表示了影响的目标技能的效果的额度
    double              m_Rate;

    /// 表示了修正方式
    int                 m_FixType;

private:

    void                InitVariables();
};

#endif //__SKILLUPGRADESKILLPART_H__
