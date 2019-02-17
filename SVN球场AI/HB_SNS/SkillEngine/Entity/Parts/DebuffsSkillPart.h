/********************************************************************************
 * 文件名：DebuffsSkillPart.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DEBUFFSSKILLPART_H__
#define __DEBUFFSSKILLPART_H__

#include "../../../common/common.h"
#include "../../../common/Utility.h"

#include "../../../Interface/Skill/ISkillPart.h"

class Debuff;

/// Represents a part of the skill config defines the debuff records.
class DebuffsSkillPart : public ISkillPart 
{
public:

    DebuffsSkillPart();

    DebuffsSkillPart(DebuffsSkillPart& rf);

    ~DebuffsSkillPart();

public:

    /// Represents the list of the debuff records.
    vector<Debuff*>&    Debuffs() { return m_Debuffs; }

    DebuffsSkillPart*   Clone() { return new DebuffsSkillPart(*this); }

private:

    vector<Debuff*>     m_Debuffs;

private:

    void                InitVariables();
};

/// 表示了配置文件中的一条异常记录
class Debuff
{
public:

    Debuff();

    Debuff(Debuff& rf);

    virtual ~Debuff() {}

public:

    int             Type() { return m_Type; }
    void            SetType(int vl) { m_Type = vl; }

    int             Level() { return m_Level; }
    void            SetLevel(int vl) { m_Level = vl; }

    int             Target() { return m_Target; }
    void            SetTarget(int vl) { m_Target = vl; }

    vector<int>&    TargetPosition() { return m_TargetPosition; }
    void            SetTargetPosition(vector<int>& vl) { m_TargetPosition = vl; }
    void            SetTargetPosition(string vl) { m_TargetPosition = Common::TransToValueVector<int>(vl, ","); }

    double          Probability() { return m_Probability; }
    void            SetProbability(double vl) { m_Probability = vl; }

    Debuff*         Clone() { return new Debuff(*this); }

protected:

    /// Represents the debuff's type.
    /// 异常状态的类型Id。
    int             m_Type;

    /// Represents the debuff's level.
    /// 异常状态的等级。
    int             m_Level;

    /// Represents the target.
    /// [target]0-自己,1-自己相对的优先级最高的那一个人，2-自己相对的人(可能有2个或以上),3-多名队友,4-多名对方
    int             m_Target;

    /// Represents the target's <see cref="Position"/>
    /// [targetposition]0-任意,1-门将,2-后卫,3-中场,4-前锋
    vector<int>     m_TargetPosition;

    /// Represents the debuff's trigger probability.
    /// Debuff触发的概率
    double          m_Probability;

private:

    void            InitVariables();
};

#endif //__DEBUFFSSKILLPART_H__
