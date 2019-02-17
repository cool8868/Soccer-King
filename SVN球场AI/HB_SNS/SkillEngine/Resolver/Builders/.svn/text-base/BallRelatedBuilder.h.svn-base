/********************************************************************************
 * 文件名：BallRelatedBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __BALLRELATEDBUILDER_H__
#define __BALLRELATEDBUILDER_H__

class BallRelatedBuilder : public ISkillPartBuilder 
{
public:

    /// Build a skill's part with the <see cref="XElement"/>.
    ISkillPart* Build(xml_node& xelement);

private:

    BallRelated* CreateBallRelated(xml_node& xelement);
};

#endif //__BALLRELATEDBUILDER_H__
