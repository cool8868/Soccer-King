/********************************************************************************
 * 文件名：State.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "State.h"

State::State()
    : m_StateChain(5)
{
    m_StateChain.clear();
}

State::~State()
{

}

IState* State::Decide(IPlayer* player, IState* preview)
{
    Enter(player);

    if (m_StateChain.size() == 0 || m_StateCondition.size() == 0)
    {
        return (IState*)NULL;
    }

    foreach (IState* next, m_StateChain)
    {
        if (m_StateCondition.find(next) == m_StateCondition.end())
        {
            return (IState*)NULL;
        }

        if ((*m_StateCondition[next])(player, preview))
        {
            return next;
        }
    }

    return (IState*)NULL;
}

void State::Enter(IPlayer* player)
{
    // LogHelper.Insert(String.Format("{0} Enter.", this));
}

void State::Exit(IPlayer* player)
{
    // LogHelper.Insert(String.Format("{0} Exit.", this));
}

void State::Action(IPlayer* player)
{
    //throw new NotSupportedException(this.ToString());
}

Process* State::Save(IPlayer* player)
{
    Process *process = NULL;

    if (player->BuildinAttribute()->GetPosition() != Position_Goalkeeper)
    {
        process = new PlayerProcess();
    }
    else
    {
        process = new GoalkeeperProcess();

        GoalkeeperProcess* pGoalKeeperProcess = PointerCastSafe(GoalkeeperProcess, process);

        pGoalKeeperProcess->SetIsBackward(player->Status()->IsBackward());
        pGoalKeeperProcess->SetIsStandUp(player->Status()->IsStandUp());
    }

    //基类初始化
    PlayerProcess* pPlayerProcess = PointerCastSafe(PlayerProcess, process);

    if (player->Status()->IsAttackSide())
    {
        pPlayerProcess->SetNameVisible(player->Status()->Hasball() ? true : false);
    }
    else
    {
        pPlayerProcess->SetNameVisible((player->Status()->GetDefenceStatus()->DefenceTarget() == player->GetMatch()->Status()->BallHandler()) ? true : false);
    }

    pPlayerProcess->SetRound(player->GetMatch()->Status()->Round());
    pPlayerProcess->SetAcceleration((int)player->Status()->Acceleration());
    pPlayerProcess->SetCoordinate(player->Status()->Current().Output());
    pPlayerProcess->SetCoordinate2(player->Status()->Current());
    pPlayerProcess->SetAngle(PlayerProcess::GetPlayerAngleIndex(player->Status()->Angle()));
    pPlayerProcess->SetState(PlayerProcess::GetClientStateIndex(player->Status()->State()->ClientId()));
    pPlayerProcess->SetFoulLevel(player->Status()->GetFoulStatus()->FoulLevel());
    pPlayerProcess->SetHasBall(player->Status()->Hasball());
    pPlayerProcess->SetModel(player->Status()->GetModelStatus()->Mid());

    return process;
}

void State::Save(Process* process, IPlayer* player)
{
    //将数据写入到process中
    PlayerProcess *pPlayerProcess = static_cast< PlayerProcess* >(process);

    //基类初始化
    if (player->Status()->IsAttackSide())
    {
        pPlayerProcess->SetNameVisible(player->Status()->Hasball() ? true : false);
    }
    else
    {
        pPlayerProcess->SetNameVisible((player->Status()->GetDefenceStatus()->DefenceTarget() == player->GetMatch()->Status()->BallHandler()) ? true : false);
    }

    pPlayerProcess->SetRound(player->GetMatch()->Status()->Round());
    pPlayerProcess->SetAcceleration((int)player->Status()->Acceleration());
    pPlayerProcess->SetCoordinate(player->Status()->Current().Output());
    pPlayerProcess->SetCoordinate2(player->Status()->Current());
    pPlayerProcess->SetAngle(PlayerProcess::GetPlayerAngleIndex(player->Status()->Angle()));
    pPlayerProcess->SetState(PlayerProcess::GetClientStateIndex(player->Status()->State()->ClientId()));
    pPlayerProcess->SetFoulLevel(player->Status()->GetFoulStatus()->FoulLevel());
    pPlayerProcess->SetHasBall(player->Status()->Hasball());
}

vector<IState*>& State::StateChain()
{
    return m_StateChain;
}

map<IState*, ConditionDelegate>& State::StateCondition()
{
    return m_StateCondition;
}
