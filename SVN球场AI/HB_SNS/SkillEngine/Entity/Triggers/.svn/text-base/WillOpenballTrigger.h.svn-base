/********************************************************************************
 * 文件名：WillOpenballTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WILLOPENBALLTRIGGER_H__
#define __WILLOPENBALLTRIGGER_H__

#include "../../../common/common.h"

#include "../../../Interface/Skill/ITrigger.h"
#include "../../../Interface/Player/IPlayer.h"

/// 开场触发的意志
class WillOpenballTrigger : public ITrigger
{
public:

    /// 验证一个开场触发类型的意志是否能够触发
    bool                    IsSkillTriggered(IPlayer* player) { return (player->GetMatch()->Status()->Round() == 1); }

    WillOpenballTrigger*    Clone() { return this; }

    bool                    IsKindOf(string name) { return typeid(*this).name() == name; }

    void                    SetAttribute(xml_node& node) { }
};

#endif //__WILLOPENBALLTRIGGER_H__
