/********************************************************************************
 * 文件名：ModelsSkillPart.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MODELSSKILLPART_H__
#define __MODELSSKILLPART_H__

#include "../../../common/common.h"
#include "../../../common/Utility.h"

#include "../../../Interface/Skill/ISkillPart.h"

class ModelRecord;

/// Represents a part of the skill that defines the model part.
class ModelsSkillPart : public ISkillPart 
{
public:

    ModelsSkillPart();

    ModelsSkillPart(ModelsSkillPart& rf);

public:

    vector<ModelRecord*>&   Models() { return m_Models;}

    ModelsSkillPart*        Clone() { return  new ModelsSkillPart(*this); }

protected:

    /// Represents the list of the model records.
    vector<ModelRecord*>    m_Models;

private:

    void                    InitVariables();
};

/// Represents a model record.
class ModelRecord
{
public:

    ModelRecord();

    ModelRecord(ModelRecord& rf);

    virtual ~ModelRecord() {}

public:

    int             Mid() { return m_Mid; }
    void            SetMid(int vl) { m_Mid = vl; }

    short           Last() { return m_Last; }
    void            SetLast(short vl) { m_Last = vl; }

    int             Target() { return m_Target; }
    void            SetTarget(int vl) { m_Target = vl; }

    vector<int>&    TargetPosition() { return m_TargetPosition; }
    void            SetTargetPosition(vector<int>& vl) { m_TargetPosition = vl; }
    void            SetTargetPosition(string vl) { m_TargetPosition = Common::TransToValueVector<int>(vl, ","); }

    ModelRecord*    Clone() { return new ModelRecord(*this); }

protected:

    /// Represents the model id.
    int             m_Mid;

    /// Represents the model's lasting time.
    short           m_Last;

    /// Represents the target.
    /// [target]0-自己,1-自己相对的优先级最高的那一个人，2-自己相对的人(可能有2个或以上),3-多名队友,4-多名对方
    int             m_Target;

    /// Represents the target's <see cref="Position"/>
    /// [targetposition]0-任意,1-门将,2-后卫,3-中场,4-前锋
    vector<int>     m_TargetPosition;

private:

    void            InitVariables();
};

#endif //__MODELSSKILLPART_H__
