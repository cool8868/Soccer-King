/********************************************************************************
 * 文件名：TriggersBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __TRIGGERSBUILDER_H__
#define __TRIGGERSBUILDER_H__

/// Represents a builder that to builds the Will's triggers part.
/// 表示了意志的触发部分的构建器
class TriggersBuilder : ISkillPartBuilder
{
public:

    /// Builds the will's triggers part.
    /// 构建意志的触发部分
    ISkillPart* Build(xml_node& xelement)
    {
        WillTriggersSkillPart* skillPart = new WillTriggersSkillPart();

        // 仅仅针对"Trigger"
        for (xml_node_iterator it = xelement.begin(); it != xelement.end(); ++it)
        {
            if (it->name() == "Trigger")
            {
                skillPart->Triggers().push_back(singleton_default<WillTriggerFactory>::instance().Build(*it));
            }
        }
    }
};

#endif //__TRIGGERSBUILDER_H__
