/********************************************************************************
 * 文件名：ISkillPartBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ISKILLPARTBUILDER_H__
#define __ISKILLPARTBUILDER_H__

#include "../ISkillPart.h"
#include "../../../common/common.h"

/// Represents the contract of the skill part's builder.
class ISkillPartBuilder 
{
public:

    virtual ~ISkillPartBuilder() {};

public:

    /// Build a skill's part with the <see cref="XElement"/>.
    virtual ISkillPart* Build(xml_node& xelement) = 0;
};

#endif //__ISKILLPARTBUILDER_H__
