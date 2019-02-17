/********************************************************************************
 * 文件名：DiveBallState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "DiveBallState.h"

#include "Idle/GKHoldBallState.h"

#include "../../Interface/IProcess.h"

DiveBallState::DiveBallState()
{
    m_Stopable = true;
}

DiveBallState* DiveBallState::Instance()
{
    static DiveBallState instance;

    return &instance;
}

void DiveBallState::Initialize()
{
    m_StateChain.push_back(GKHoldBallState::Instance());

    m_StateCondition[GKHoldBallState::Instance()]       = ValidateDiveBallToGKHoldBall;
}

/// 扑救
void DiveBallState::Action(IPlayer* player)
{
    player->DiveBall();
}

string DiveBallState::ToString()
{
    return "DiveBallState";
}

/// 保存扑救状态
Process* DiveBallState::Save(IPlayer* player)
{
    //调用基类的保存函数
    Process* process = State::Save(player);

    //派生类的属性
    GoalkeeperProcess* pGoalKeeperProcess = PointerCastSafe(GoalkeeperProcess, process);

    pGoalKeeperProcess->SetDiveDirection(player->Status()->GetDiveStatus()->DiveDirection());

    return process;
}

IState* DiveBallState::QuickDecide(IPlayer* player, IState* preview)
{
    return GKHoldBallState::Instance();
}

bool DiveBallState::ValidateDiveBallToGKHoldBall(IPlayer* player, IState* preview)
{
    return true;
}

bool DiveBallState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    return false;
}
