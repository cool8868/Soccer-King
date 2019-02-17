/********************************************************************************
 * 文件名：PropertyChangesSkillPart.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PROPERTYCHANGESSKILLPART_H__
#define __PROPERTYCHANGESSKILLPART_H__

#include "../../../common/common.h"
#include "../../../common/Utility.h"

#include "../../../Interface/Skill/ISkillPart.h"

class PropertyChange;

/// Represents a part of the skill that contains the property's changes.
class PropertyChangesSkillPart : public ISkillPart 
{
public:

    PropertyChangesSkillPart();

    PropertyChangesSkillPart(PropertyChangesSkillPart& rf);

    ~PropertyChangesSkillPart();

public:

    /// Represents the records of the <see cref="PropertyChange"/>.
    vector<PropertyChange*>&    PropertyChanges() { return m_PropertyChanges; }

    PropertyChangesSkillPart*   Clone() { return new PropertyChangesSkillPart(*this); }

private:

    vector<PropertyChange*>     m_PropertyChanges;

private:

    void                        InitVariables();
};

/// Represents a record of <see cref="PropertyChangesSkillPart"/>.
class PropertyChange
{
public:

    PropertyChange();

    PropertyChange(PropertyChange& rf);

    virtual ~PropertyChange() {}

public:

    string          Property() { return m_Property; }
    void            SetProperty(string vl) { m_Property = vl; }

    double          Rate() { return m_Rate; }
    void            SetRate(double vl) { m_Rate = vl; }

    short           Last() { return m_Last; }
    void            SetLast(short vl) { m_Last = vl; }

    double          Gradient() { return m_Gradient; }
    void            SetGradient(double vl) { m_Gradient = vl; }

    int             Target() { return m_Target; }
    void            SetTarget(int vl) { m_Target = vl; }

    vector<int>&    TargetPosition() { return m_TargetPosition; }
    void            SetTargetPosition(vector<int>& vl) { m_TargetPosition = vl; }
    void            SetTargetPosition(string vl) { m_TargetPosition = Common::TransToValueVector<int>(vl, ","); }

    PropertyChange* Clone() { return new PropertyChange(*this); }

protected:

    /// Represents the which property to change.
    /// It is the property's index.
    string          m_Property;
    
    /// Represents the property change rate.
    double          m_Rate;

    /// Represents the property change lasting time. (in-game time, min)
    short           m_Last;

    /// Represents the property change gradient.
    /// buff's gradient (while the gradient not zero, it is a dot type buff)
    double          m_Gradient;

    /// Represents the property change's target.
    /// 0-自己,
    /// 1-自己相对的优先级最高的那一个人，
    /// 2-自己相对的人(可能有2个或以上),
    /// 3-多名队友,
    /// 4-多名对方
    int             m_Target;

    /// Represents the <see cref="PropertyChange"/> effected targets's position.
    /// 0-任意,
    /// 1-门将,
    /// 2-后卫,
    /// 3-中场,
    /// 4-前锋
    vector<int>     m_TargetPosition;

private:

    void            InitVariables();
};

#endif //__PROPERTYCHANGESSKILLPART_H__
