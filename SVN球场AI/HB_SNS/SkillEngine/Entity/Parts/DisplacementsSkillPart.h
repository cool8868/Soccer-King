/********************************************************************************
 * 文件名：DisplacementsSkillPart.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DISPLACEMENTSSKILLPART_H__
#define __DISPLACEMENTSSKILLPART_H__

#include "../../../common/common.h"

#include "../../../Interface/Skill/ISkillPart.h"

class Displacement;

/// Represents a part of the skill that contains the displacements info.
class DisplacementsSkillPart : public ISkillPart 
{
public:

    DisplacementsSkillPart();

    DisplacementsSkillPart(DisplacementsSkillPart& rf);

    ~DisplacementsSkillPart();

public:

    /// Represents the displacement list.
    vector<Displacement*>&      Displacements() { return m_Displacements; }

    DisplacementsSkillPart*     Clone() { return new DisplacementsSkillPart(*this); }

private:

    vector<Displacement*>       m_Displacements;

private:

    void                        InitVariables();
};

/// Represents a record of the <see cref="DisplacementsSkillPart"/>
class Displacement
{
public:

    Displacement();

    Displacement(Displacement& rf);

    virtual ~Displacement() {}

public:

    int             Type() { return m_Type; }
    void            SetType(int type) { m_Type = type; }

    int             Distance() { return m_Distance; }
    void            SetDistance(int vl) { m_Distance = vl; }

    short           Angle() { return m_Angle; }
    void            SetAngle(short vl) { m_Angle = vl; }

    int             Target() { return m_Target; }
    void            SetTarget(int vl) { m_Target = vl; }

    vector<int>&    TargetPosition() { return m_TargetPosition; }
    void            SetTargetPosition(vector<int>& vl) { m_TargetPosition = vl; }

    Displacement*   Clone() { return new Displacement(*this); }

protected:

    /// Represents the type.
    /// [type]=
    /// 0-做一段位移(距离+角度),
    /// 1-技能使用者移动到与目标Y坐标相等的坐标上后，距离目标固定的距离，再释放技能,
    /// 2-技能使用者移动到与目标相连直线与Distance规定的半径相交的坐标上后，再释放技能
    /// (三者的target是不同的定义，一定要区分，0的target是使用位移的人，1，2的target是指技能使用者的目标
    int             m_Type;

    /// Represent the curernt player's move distance.
    int             m_Distance;

    /// Represetns the curernt player's angle.
    short           m_Angle;

    /// Represents the record's effect target.
    /// 0-自己,
    /// 1-自己相对的优先级最高的那一个人，
    /// 2-自己相对的人(可能有2个或以上),
    /// 3-多名队友,
    /// 4-多名对方
    int             m_Target;

    /// Represetns the record's effect target's position.
    /// 0-任意,
    /// 1-门将,
    /// 2-后卫,
    /// 3-中场,
    /// 4-前锋
    vector<int>     m_TargetPosition;

private:

    void            InitVariables();
};

#endif //__DISPLACEMENTSSKILLPART_H__
