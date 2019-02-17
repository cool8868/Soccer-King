/********************************************************************************
 * 文件名：ManagerChangesBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MANAGERCHANGESBUILDER_H__
#define __MANAGERCHANGESBUILDER_H__

class ManagerChangesBuilder : public ISkillPartBuilder 
{
public:

    /// Build a <see cref="ISkillPart"/> with the <see cref="XElement"/>.
    ISkillPart*             Build(xml_node& xelement);

private:

    ManagerPropertyChange*  CreateManagerPropertyChange(xml_node& xelement);
};

#endif //__MANAGERCHANGESBUILDER_H__
