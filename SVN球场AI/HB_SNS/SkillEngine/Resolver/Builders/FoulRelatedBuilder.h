/********************************************************************************
 * 文件名：FoulRelatedBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FOULRELATEDBUILDER_H__
#define __FOULRELATEDBUILDER_H__

/// Represents the builder that to build the foul related part.
class FoulRelatedBuilder : public ISkillPartBuilder 
{
public:

    /// Build a skill's part with the <see cref="XElement"/>.
    ISkillPart* Build(xml_node& xelement);

private:
    
    Foul*           CreateFoul(xml_node& xelement);

    FoulChange*     CreateFoulChange(xml_node& xelement);
};

#endif //__FOULRELATEDBUILDER_H__

