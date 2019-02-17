/********************************************************************************
 * 文件名：ISkillResult.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ITRIGGER_H__
#define __ITRIGGER_H__

#include "ISkillPart.h"

#include "../../common/common.h"

class IPlayer;

class ITrigger : public ISkillPart 
{
public:

    /// Validate the skill is triggered or not.
    virtual bool        IsSkillTriggered(IPlayer* player) = 0;

    //类型识别
    virtual bool        IsKindOf(string name) = 0;

    virtual void        SetAttribute(xml_node& node) = 0;

    virtual ITrigger*   Clone() = 0;
};

#endif //__ITRIGGER_H__
