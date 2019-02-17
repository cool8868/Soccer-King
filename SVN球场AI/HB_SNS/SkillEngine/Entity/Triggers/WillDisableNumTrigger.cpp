/********************************************************************************
 * 文件名：WillDisableNumTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "WillDisableNumTrigger.h"

WillDisableNumTrigger::WillDisableNumTrigger()
{
    InitVariables();
}

WillDisableNumTrigger::WillDisableNumTrigger(WillDisableNumTrigger& rf)
{
    InitVariables();

    m_Side          = rf.GetSide();
    m_Condition     = rf.Condition();
    m_Num           = rf.Num();
}

bool WillDisableNumTrigger::IsSkillTriggered(IPlayer* player)
{
    int count = 0;
    foreach (IManager* m, player->GetMatch()->Managers())
    {
        foreach (IPlayer* p, m->Players())
        {
            if (p->Status()->Enable() == false) // 离场球员
            {
                if (m_Side == 1) // 本方
                {
                    if (p->GetSide() != player->GetSide()) continue;
                }

                if (m_Side == 2) // 对方
                {
                    if (p->GetSide() == player->GetSide()) continue;
                }

                if (m_Condition == 1) // 红牌
                {
                    if (p->Status()->GetFoulStatus()->IsRedCard() == false) continue;
                }

                if (m_Condition == 2) // 受伤
                {
                    if (p->Status()->GetFoulStatus()->IsInjured() == false) continue;
                }

                if (p->Processes()[p->Processes().size() - 1]->Round() == player->GetMatch()->Status()->Round() - 1)
                {
                    count++;
                }
            }
        }
    }

    return count == m_Num;
}

void WillDisableNumTrigger::SetAttribute(xml_node& node)
{
    if (!node)
    {
        return;
    }

    string side             = "side";
    string condition        = "condition";
    string num              = "num";

    string value;

    if (Common::GetAttribute(node, side, value))
    {
        SetSide(lexical_cast<int>(value));
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

void WillDisableNumTrigger::InitVariables()
{
    m_Side          = 0;
    
    m_Condition     = 0;

    m_Num           = 0;
}
