/********************************************************************************
 * 文件名：TriggerSkillPart.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __TRIGGERSKILLPART_H__
#define __TRIGGERSKILLPART_H__

#include "../../../Interface/Skill/ITrigger.h"
#include "../../../Interface/Skill/ISkillPart.h"

#include "../../../common/common.h"

/// Represents a skill's part that contains the condition triggers.
class TriggerSkillPart : public ISkillPart 
{
public:

    TriggerSkillPart();

    TriggerSkillPart(TriggerSkillPart& rf);

public:

    /// Represents the skill's condition triggers.
    vector<ITrigger*>&  Triggers() { return m_Triggers; }

    TriggerSkillPart*   Clone() { return new TriggerSkillPart(*this); }

private:
    
    vector<ITrigger*>   m_Triggers;

private:

    void                InitVariables();
};

#endif //__TRIGGERSKILLPART_H__
