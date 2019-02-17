/********************************************************************************
 * 文件名：ImmunityBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IMMUNITYBUILDER_H__
#define __IMMUNITYBUILDER_H__

class ImmunityBuilder : ISkillPartBuilder 
{
public:

    /// Build a <see cref="ISkillPart"/> with the <see cref="XElement"/>.
    ISkillPart* Build(xml_node& xelement) {
        var skillPart = new ImmunitySkillPart();
        var items = from item in xelement.Descendants("Immunity")
            select item;

        foreach (var item in items) {
            skillPart.Immunities.Add(base.BuildPartRecord<Immunity>(item));
        }
        return skillPart;
    }  

private:

    Immunity* CreateImmunity(xml_node& xelement);
};

#endif //__IMMUNITYBUILDER_H__

