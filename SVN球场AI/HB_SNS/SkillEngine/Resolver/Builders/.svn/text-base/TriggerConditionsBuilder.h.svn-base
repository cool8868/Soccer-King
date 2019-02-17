/********************************************************************************
 * 文件名：TriggerConditionsBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __TRIGGERCONDITIONSBUILDER_H__
#define __TRIGGERCONDITIONSBUILDER_H__

#include "../../../Interface/Skill/Resolver/ISkillPartBuilder.h"
#include "../../Entity/Parts/TriggerSkillPart.h"
#include "TriggerFactory.h"

/// 表示了球员技能的触发条件的构建器
class TriggerConditionsBuilder : ISkillPartBuilder
{
public:

    /// Build a skill's part with the <see cref="XElement"/>.
    /// 创建球员技能的触发条件构建器
    ISkillPart* Build(xml_node& xelement)
    {
        TriggerSkillPart* skillPart = new TriggerSkillPart();

        // 仅仅针对"Condition"
        for (xml_node_iterator it = xelement.begin(); it != xelement.end(); ++it)
        {
            if (it->name() == "Condition")
            {
                skillPart->Triggers().push_back(singleton_default<TriggerFactory>::instance().Build(*it));
            }
        }

        return skillPart;
    }
};

#endif //__TRIGGERCONDITIONSBUILDER_H__
