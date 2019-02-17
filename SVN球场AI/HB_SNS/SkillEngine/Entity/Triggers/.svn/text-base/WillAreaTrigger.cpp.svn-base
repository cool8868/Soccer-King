/********************************************************************************
 * 文件名：WillAreaTrigger.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "WillAreaTrigger.h"
#include "../Effects/TargetSelector.h"
#include "../../../common/Structs/Region.h"

WillAreaTrigger::WillAreaTrigger()
{
    InitVariables();
}

WillAreaTrigger::WillAreaTrigger(WillAreaTrigger& rf)
{
    InitVariables();

    m_Target            = rf.Target();
    m_TargetPosition    = rf.TargetPosition();
    m_Area              = rf.Area();
}

bool WillAreaTrigger::IsSkillTriggered(IPlayer* player)
{
    vector<IPlayer*> targets = singleton_default<TargetSelector>::instance().GetTarget(m_Target, m_TargetPosition, player);

    Region region;

    if (m_Area == 0) // 前场
    {
        region = (player->GetSide() == Side_Home) ?
            Region(Defines_Pitch.MAX_WIDTH / 2, 0, Defines_Pitch.MAX_WIDTH, Defines_Pitch.MAX_HEIGHT) :
            Region(0, 0, Defines_Pitch.MAX_WIDTH / 2, Defines_Pitch.MAX_HEIGHT);
    }
    else // 后场
    {
        region = (player->GetSide() == Side_Home) ?
            Region(0, 0, Defines_Pitch.MAX_WIDTH / 2, Defines_Pitch.MAX_HEIGHT) :
            Region(Defines_Pitch.MAX_WIDTH / 2, 0, Defines_Pitch.MAX_WIDTH, Defines_Pitch.MAX_HEIGHT);
    }

    foreach (IPlayer* p, targets)
    {
        if (region.IsCoordinateInRegion(p->Current()))
        {
            return true;
        }
    }

    return false;
}

void WillAreaTrigger::SetAttribute(xml_node& node)
{
    if (!node)
    {
        return;
    }

    string target                   = "target";
    string targetPosition           = "targetPosition";
    string area                     = "area";

    string value;

    if (Common::GetAttribute(node, target, value))
    {
        SetTarget(lexical_cast<int>(value));
    }

    if (Common::GetAttribute(node, targetPosition, value))
    {
        SetTargetPosition(value);
    }

    if (Common::GetAttribute(node, area, value))
    {
        SetArea(lexical_cast<int>(value));
    }
}

void WillAreaTrigger::InitVariables()
{
    m_Target        = 0;
    m_Area          = 0;

    m_TargetPosition.clear();
}
