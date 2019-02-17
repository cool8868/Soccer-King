/********************************************************************************
 * 文件名：RawSkill.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __RAWSKILL_H__
#define __RAWSKILL_H__

#include "../Interface/Skill/IRawSkill.h"

/// 表示了一个技能的抽象基类
class RawSkill : public IRawSkill
{
public:

    /// Initializes a new instance of the <see cref="RawSkill"/>.
    RawSkill(string id, string name, SkillType type)
    {
        m_Id    = id;
        m_Name  = name;
        m_Type  = type;
    }

    /// 表示了技能的ID
    string Id () { return m_Id; }

    /// 表示了技能的名称
    string Name() { return m_Name; }

    /// Represents the skill's type.
    SkillType Type() { return m_Type; }

protected:

    string      m_Id;

    string      m_Name;

    SkillType   m_Type;
};

#endif //__RAWSKILL_H__
