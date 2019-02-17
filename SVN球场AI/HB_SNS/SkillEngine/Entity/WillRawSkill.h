/********************************************************************************
 * 文件名：WillRawSkill.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WILLRAWSKILL_H__
#define __WILLRAWSKILL_H__

#include "../../Skill/RawSkill.h"
#include "../../Interface/Skill/IWillRawSkill.h"

#include "../Entity/WillRawSkill.h"

/// Represents the raw data of the will.
class WillRawSkill : public RawSkill, public IWillRawSkill
{
public:

    /// Initializes a new instance of the <see cref="WillRawSkill"/> class.
    WillRawSkill(string id, string name);

public:
    
    ISkillPart*         Triggers() { return m_Triggers; }
    void                SetTriggers(ISkillPart* vl) { m_Triggers = vl; }

    ISkillPart*         PropertyChanges() { return m_PropertyChanges; }
    void                SetPropertyChanges(ISkillPart* vl) { m_PropertyChanges = vl; }

    void                SetSkillPart(string name, ISkillPart* skillPart);

protected:

    /// 表示了意志的触发条件
    ISkillPart*         m_Triggers;

    /// 表示了意志的效果
    ISkillPart*         m_PropertyChanges;
};

#endif //__WILLRAWSKILL_H__
