/********************************************************************************
 * 文件名：HoldBallTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __HOLDBALLTRIGGER_H__
#define __HOLDBALLTRIGGER_H__

#include "../../../Interface/Skill/ITrigger.h"
#include "../../../Interface/Player/Status/IPlayerStatus.h"

/// Represents a trigger that validate the skill needs to trigger in the first half.
class HoldBallTrigger : public ITrigger 
{
public:

    /// Validate the skill is triggered or not.
    /// <param name="player"><see cref="IPlayer"/></param>
    /// <returns>bool</returns>
    bool IsSkillTriggered(IPlayer* player) 
    {
        return player->Status()->Holdball();
    }

    HoldBallTrigger* Clone() 
    {
        return &singleton_default<HoldBallTrigger>::instance();
    }

    bool IsKindOf(string name) { return typeid(*this).name() == name; }

    void SetAttribute(xml_node& node) { }
};

#endif //__HOLDBALLTRIGGER_H__
