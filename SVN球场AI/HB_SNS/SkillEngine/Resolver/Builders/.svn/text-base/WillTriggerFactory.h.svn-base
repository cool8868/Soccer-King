/********************************************************************************
 * 文件名：WillTriggerFactory.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WILLTRIGGERFACTORY_H__
#define __WILLTRIGGERFACTORY_H__

/// Represents a factory that to build the will's trigger.
/// 意志的触发器构建工厂
class WillTriggerFactory : ISkillPartBuilder
{
public:

    ISkillPart* Build(xml_node& element);

private:

    ITrigger* CreateInstance(string name);
};

#endif //__WILLTRIGGERFACTORY_H__
