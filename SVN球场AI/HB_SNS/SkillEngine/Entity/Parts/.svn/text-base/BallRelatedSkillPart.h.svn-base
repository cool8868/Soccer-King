/********************************************************************************
 * 文件名：BallRelatedSkillPart.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __BALLRELATEDSKILLPART_H__
#define __BALLRELATEDSKILLPART_H__

#include "../../../common/common.h"

#include "../../../Interface/Skill/ISkillPart.h"

class BallRelated;

/// Represents a part of the skill that defines the ball related data.
class BallRelatedSkillPart : public ISkillPart 
{
public:

    BallRelatedSkillPart();

    BallRelatedSkillPart(BallRelatedSkillPart& rf);

    ~BallRelatedSkillPart();

public:

    /// Represents records of the <see cref="BallRelated"/>.
    vector<BallRelated*>&   BallRelateds() { return m_BallRealateds; }

    BallRelatedSkillPart*   Clone()  { return new BallRelatedSkillPart(*this); }

private:

    vector<BallRelated*>    m_BallRealateds;

private:

    void                    InitVariables();
};

/// Represents a ball realated record.
class BallRelated
{
public:

    BallRelated();

    BallRelated(BallRelated& rf);

    virtual ~BallRelated() {}

public:

    int             Action() { return m_Action; }
    void            SetAction(int vl) { m_Action = vl; }

    int             Last() { return m_Last; }
    void            SetLast(int vl) { m_Last = vl; }

    BallRelated*    Clone() { return new BallRelated(*this); }

protected:

    /// Represents a action.
    int             m_Action;

    /// Represents the lasting time.
    int             m_Last;

private:

    void            InitVariables();
};

#endif //__BALLRELATEDSKILLPART_H__
