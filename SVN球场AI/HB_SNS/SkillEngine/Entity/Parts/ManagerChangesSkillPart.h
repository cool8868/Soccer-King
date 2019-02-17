/********************************************************************************
 * 文件名：ManagerChangesSkillPart.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MANAGERCHANGESSKILLPART_H__
#define __MANAGERCHANGESSKILLPART_H__

#include "../../../common/common.h"
#include "../../../common/Utility.h"

#include "../../../Interface/Skill/ISkillPart.h"
#include "../../../Interface/Skill/IManagerSkill.h"

class ManagerPropertyChange;

/// Represents a part of the skill that defines the propety 
/// changes in the manager skill.
class ManagerChangesSkillPart : public ISkillPart
{
public:

    ManagerChangesSkillPart();

    ManagerChangesSkillPart(ManagerChangesSkillPart& rf);

    ~ManagerChangesSkillPart();

public:

    IManagerSkill*                  SkillReference() { return m_SkillReference; }
    void                            SetSkillReference(IManagerSkill* vl) { m_SkillReference = vl; }

    vector<ManagerPropertyChange*>& ManagerPropertyChanges() { return m_ManagerPropertyChanges; }

    ManagerChangesSkillPart*        Clone() { return new ManagerChangesSkillPart(*this); }

private:

    /// Represents the reference of the <see cref="IRawSkill"/>.
    IManagerSkill*                  m_SkillReference;

    /// Represents the list of the <see cref="ManagerPropertyChange"/>.
    vector<ManagerPropertyChange*>  m_ManagerPropertyChanges;

private:

    void                            InitVariables();
};

/// Represents a record of the property changes.
class ManagerPropertyChange
{
public:

    ManagerPropertyChange();

    ManagerPropertyChange(ManagerPropertyChange& rf);

    virtual ~ManagerPropertyChange() {}

public:

    string                  Property() { return m_Property; }
    void                    SetProperty(string vl) { m_Property = vl; }

    double                  Rate() { return m_Rate; }
    void                    SetRate(double vl) { m_Rate = vl; }

    double                  Gradient() { return m_Gradient; }
    void                    SetGradient(double vl) { m_Gradient = vl; }

    int                     Target() { return m_Target; }
    void                    SetTarget(int vl) { m_Target = vl; }

    vector<int>&            TargetPosition() { return m_TargetPosition; }
    void                    SetTargetPosition(vector<int>& vl) { m_TargetPosition = vl; }
    void                    SetTargetPosition(string vl) { m_TargetPosition = Common::TransToValueVector<int>(vl, ","); }

    vector<int>&            Coordinate() { return m_Coordinate; }
    void                    SetCoordinate(vector<int>& vl) { m_Coordinate = vl; }

    ManagerPropertyChange*  Clone() { return new ManagerPropertyChange(*this); }

protected:

    /// 表示了影响的属性编号
    string          m_Property;

    /// 表示了对影响的技能的变化率
    double          m_Rate;

    /// 表示了技能渐变率
    double          m_Gradient;

    /// Represents the <see cref="ManagerPropertyChange"/> effected targets.
    int             m_Target;

    /// Represents the <see cref="ManagerPropertyChange"/> effected targets's postition.
    vector<int>     m_TargetPosition;

    /// Represents the <see cref="ManagerPropertyChange"/> effected target's coordinate region.
    /// (y1 less or equal y less or equal y2) || (y3 less or equal y less or equal y4)
    vector<int>     m_Coordinate;

private:

    void            InitVariables();
};

#endif //__MANAGERCHANGESSKILLPART_H__
