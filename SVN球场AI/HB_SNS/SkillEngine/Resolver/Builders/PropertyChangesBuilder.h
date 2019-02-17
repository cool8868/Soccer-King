/********************************************************************************
 * 文件名：PropertyChangesBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PROPERTYCHANGESBUILDER_H__
#define __PROPERTYCHANGESBUILDER_H__

#include "../../../Interface/Skill/Resolver/ISkillPartBuilder.h"

#include "../../Entity/Parts/PropertyChangesSkillPart.h"

/// Represents the builder that to build the property changes part.
class PropertyChangesBuilder : ISkillPartBuilder 
{
public:

    /// Build a skill's part with the <see cref="XElement"/>.
    ISkillPart* Build(xml_node& xelement);

private:

    PropertyChange* CreatePropertyChange(xml_node& xelement);
};

#endif //__PROPERTYCHANGESBUILDER_H__

