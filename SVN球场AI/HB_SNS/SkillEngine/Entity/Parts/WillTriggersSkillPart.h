/********************************************************************************
 * 文件名：WillTriggersSkillPart.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WILLTRIGGERSKILLPART_H__
#define __WILLTRIGGERSKILLPART_H__

#include "../../../Interface/Skill/ITrigger.h"
#include "../../../Interface/Skill/ISkillPart.h"

#include "../../../common/common.h"

/// 表示了意志的触发部分对象
class WillTriggersSkillPart : public ISkillPart
{
public:

    WillTriggersSkillPart();

    WillTriggersSkillPart(WillTriggersSkillPart& rf);

    ~WillTriggersSkillPart();

public:

    /// 表示了所有的触发条件
    vector<ITrigger*>&      Triggers() { return m_Triggers; }

    WillTriggersSkillPart*  Clone() { return new WillTriggersSkillPart(*this); }

private:

    vector<ITrigger*>       m_Triggers;

private:

    void                    InitVariables();
};

#endif //__WILLTRIGGERSKILLPART_H__
