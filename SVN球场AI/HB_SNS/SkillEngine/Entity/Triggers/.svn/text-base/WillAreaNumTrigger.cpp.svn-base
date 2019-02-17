/********************************************************************************
 * 文件名：WillAreaNumTrigger.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "WillAreaNumTrigger.h"

#include "../../../common/Structs/Region.h"
#include "../../../common/Utility.h"

#include "../../../Interface/IMatch.h"


WillAreaNumTrigger::WillAreaNumTrigger()
{
    InitVariables();
}

WillAreaNumTrigger::WillAreaNumTrigger(WillAreaNumTrigger& rf)
{
    InitVariables();

    m_Area          = rf.Area();
    m_Position      = rf.Position();
    m_Num           = rf.Num();
    m_Condition     = rf.Condition();
}

bool WillAreaNumTrigger::IsSkillTriggered(IPlayer* player)
{
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

    vector<IPlayer*> targets(Defines_Match.MAX_PLAYER_COUNT * 2);
    targets.clear();

    foreach (IManager* m, player->GetMatch()->Managers())
    {
        foreach (IPlayer* p, m->Players())
        {                    
            if (m_Side == 1) // 本方
            {
                if (p->GetSide() != player->GetSide())
                {
                    continue;
                }                       
            }

            if (m_Side == 2) // 对方
            {
                if (p->GetSide() == player->GetSide())
                {
                    continue;
                }
            }

            if (region.IsCoordinateInRegion(p->Current()))
            {
                targets.push_back(p);
            }
        }
    }

    if (m_Condition == 0) // 大于
    {
        return targets.size() > (size_t)m_Num;
    }

    if (m_Condition == 1) // 小于
    {
        return targets.size() < (size_t)m_Num;
    }

    if (m_Condition == 2) // 等于
    {
        return targets.size() == (size_t)m_Num;
    }

    return false;
}

void WillAreaNumTrigger::SetAttribute(xml_node& node)
{
    if (!node)
    {
        return;
    }

    // <Trigger type="3" area="1" side="1" position="0" condition="1" num="4" />
    string area             = "area";
    string side             = "side";
    string position         = "position";
    string condition        = "condition";
    string num              = "num";

    string value;

    if (Common::GetAttribute(node, area, value))
    {
        SetArea(lexical_cast<int>(value));
    }

    if (Common::GetAttribute(node, side, value))
    {
        SetSide(lexical_cast<int>(value));
    }

    if (Common::GetAttribute(node, position, value))
    {
        SetPosition(lexical_cast<int>(value));
    }

    if (Common::GetAttribute(node, condition, value))
    {
        SetCondition(lexical_cast<int>(value));
    }

    if (Common::GetAttribute(node, num, value))
    {
        SetNum(lexical_cast<int>(value));
    }
}

void WillAreaNumTrigger::InitVariables()
{
    m_Area          = 0;
    m_Side          = 0;
    m_Position      = 0;
    m_Num           = 0;
    m_Condition     = 0;
}
