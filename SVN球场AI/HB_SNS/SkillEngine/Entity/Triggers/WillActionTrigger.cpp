/********************************************************************************
 * 文件名：WillActionTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "WillActionTrigger.h"

WillActionTrigger::WillActionTrigger()
{
    InitVariables();
}

WillActionTrigger::WillActionTrigger(WillActionTrigger& rf)
{
    InitVariables();

    m_Target                = rf.Target();
    m_TargetPosition        = rf.TargetPosition();
    m_IsSuccess             = rf.IsSuccess();
    m_TriggerStates         = rf.TriggerStates();
    m_States                = rf.State();
}

/// 从配置导入的状态
string WillActionTrigger::SetState(string vl)
{
    if (vl[0] == '*')
    {
        m_States = String::Empty();

        foreach (MapString_IState::value_type& state, StateSelecter::Instance()->States())
        {
            m_States    = state.first;
            m_States    += ",";
        }
    }
    else
    {
        m_States = vl;
    }

    m_TriggerStates = Common::TransToStringVector(m_States, ",");
}

/// 判断该意志是否能够触发
bool WillActionTrigger::IsSkillTriggered(IPlayer* player)
{
    if (find(m_TriggerStates.begin(), m_TriggerStates.end(), player->Status()->State()->ToString()) == m_TriggerStates.end())
    {
        return false;
    }

    if (m_IsSuccess == 1) // 要求技能成功
    {
        if (player->Status()->State()->IsInState(PassState::Instance())) // 传球状态
        {
            return !player->Status()->GetPassStatus()->IsPassFail();
        }

        if (player->Status()->State()->IsInState(ShootState::Instance())) // 射门状态
        {
            return player->Status()->GetShootStatus()->ShootTargetIndex() < 4;
        }

        if (player->Status()->State()->IsInState(DiveBallState::Instance())) // 扑救状态
        {
            return true;
        }

        if (player->Status()->State()->IsInState(DefenceState::Instance())) // 抢断状态
        {
            return player->Status()->Hasball();
        }

        if (player->Status()->State()->IsInState(BreakThroughState::Instance())) // 过人状态
        {
            return player->Status()->Hasball();
        }
    }            

    return true;            
}

void WillActionTrigger::SetAttribute(xml_node& node)
{
    if (!node)
    {
        return;
    }

    string target                   = "target";
    string targetPosition           = "targetPosition";
    string isSuccess                = "isSuccess";
    string state                    = "state";

    string value;

    if (Common::GetAttribute(node, target, value))
    {
        SetTarget(lexical_cast<int>(value));
    }

    if (Common::GetAttribute(node, targetPosition, value))
    {
        SetTargetPosition(value);
    }

    if (Common::GetAttribute(node, isSuccess, value))
    {
        SetIsSuccess(lexical_cast<int>(value));
    }
    
    if (Common::GetAttribute(node, state, value))
    {
        SetState(value);
    }
}

void WillActionTrigger::InitVariables()
{
    m_Target = 0;
    m_IsSuccess = false;

    m_TriggerStates.clear();
    m_TargetPosition.clear();

    m_States = String::Empty();
}

