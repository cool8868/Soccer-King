/********************************************************************************
 * 文件名：SpecialEffectsBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SPECIALEFFECTSBUILDER_H__
#define __SPECIALEFFECTSBUILDER_H__

/// Represents a skill builder that to build a part of skill 
/// which contains the special effect info.
class SpecialEffectsBuilder : ISkillPartBuilder 
{
public:

    /// Build a skill's part with the <see cref="XElement"/>.
    ISkillPart* Build(xml_node& xelement) {
        var skillPart = new SpecialEffectsSkillPart();

        var items = from item in xelement.Descendants("SpecialEffect")
            select item;
        foreach (var item in items) {
            skillPart.Specials.Add(CreateSpecial(item));
        }

        return skillPart;
    }

private:
    
    Special* CreateSpecial(xml_node& xelement) {
        return base.BuildPartRecord<Special>(xelement);
    }
};

#endif //__SPECIALEFFECTSBUILDER_H__
