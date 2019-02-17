/********************************************************************************
 * 文件名：DebuffsBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DEBUFFSBUILDER_H__
#define __DEBUFFSBUILDER_H__

/// Represents a skill builder that to build a part of skill 
/// which contains the debuffs info.
class DebuffsBuilder : public ISkillPartBuilder 
{
public:

    /// Build a skill's part with the <see cref="XElement"/>.
    ISkillPart*     Build(xml_node& xelement);

private:

    Debuff*         CreateDebuff(xml_node& xelement);
};

#endif //__DEBUFFSBUILDER_H__
