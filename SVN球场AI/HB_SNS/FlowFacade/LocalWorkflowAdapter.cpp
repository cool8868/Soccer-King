/********************************************************************************
 * 文件名：LocalWorkflowAdapter.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "LocalWorkflowAdapter.h"

#include "../Match/MatchEntity.h"
#include "../Interface/Player/Status/IPlayerStatus.h"

LocalWorkflowAdapter::LocalWorkflowAdapter()
    : m_Match(NULL)
{

}

LocalWorkflowAdapter::~LocalWorkflowAdapter()
{
    DeletePtrSafe(m_Match);
}

/// Create a match.
IMatch* LocalWorkflowAdapter::CreateMatch(int homeId, int awayId, int matchType, double time)
{
    m_Match = new MatchEntity();
    m_Match->InitMatch(homeId, awayId, matchType, time);

    return MainLoop(m_Match);
}

IMatch* LocalWorkflowAdapter::CreateMatchForDebug(int homeId, int awayId, int matchType, double time)
{
    m_Match = new MatchEntity();
    m_Match->InitMatch(homeId, awayId, matchType, time);

    foreach (IManager* m, m_Match->Managers())
    {
        foreach (IPlayer* p, m->Players())
        {
            p->Init();
        }
    }

    return m_Match;
}

IMatch* LocalWorkflowAdapter::CreateMatch(TransferMatchEntity* match)
{
    m_Match = new MatchEntity();
    m_Match->InitMatch(match);

    return MainLoop(m_Match);
}

IMatch* LocalWorkflowAdapter::CreateMatchForDebug(TransferMatchEntity* match)
{
    m_Match = new MatchEntity();
    m_Match->InitMatch(match);

    foreach (IManager* m, m_Match->Managers())
    {
        foreach (IPlayer* p, m->Players())
        {
            p->Init();
        }
    }

    return m_Match;
}

IMatch* LocalWorkflowAdapter::CreateMatchFromFile(string fileName)
{
    ifstream fileHander;

    fileHander.open(fileName.c_str(), ios::in | ios::binary);
    if (!fileHander)
    {
        return (IMatch*)NULL;
    }

    TransferMatchEntity entity;
    entity.DeserializeFromFile(fileHander);

    //关闭文件
    fileHander.close();

    return CreateMatchForDebug(&entity);
}

/// 游戏的主函数
IMatch* LocalWorkflowAdapter::MainLoop(IMatch* match)
{
    foreach (IManager* m, match->Managers())
    {
        foreach (IPlayer* p, m->Players())
        {
            p->Init();
        }
    }

    while (match->Status()->Round() < match->GetTotalRound())
    {
        // main loop.
        match->Status()->IncRound();
        match->Status()->SetIsGoal(false);

        if (match->Status()->BallHandler() == NULL)
        {
            match->HomeManager()->GetPlayersByPosition(Position_Goalkeeper)[0]->Status()->SetHasball(true);
        }

        // 回合初始化数据
        match->RoundInit();
        // 持球人决策
        match->Status()->BallHandler()->QuickDecide();

        // 进攻方决策
        foreach (IPlayer* player, match->Status()->BallHandler()->GetManager()->Players())
        {
            if (player == match->Status()->BallHandler())
            {
                continue;
            }

            player->QuickDecide();
        }

        // 防守方决策
        foreach (IPlayer* player, match->Status()->BallHandler()->GetManager()->Opponent()->Players())
        {
            player->QuickDecide();
        }

        // 持球人行动
        int ballHandlerId = match->Status()->BallHandler()->ClientId();
        match->Status()->BallHandler()->Action();

        // 所有球员一起行动
        foreach (IManager* manager, match->Managers())
        {
            foreach (IPlayer* player, manager->Players())
            {
                if (player->ClientId() == ballHandlerId)
                {
                    continue;
                }

                if (!player->Status()->Enable())
                {
                    continue;
                }

                player->Action();
            }
        }

        // 足球移动
        match->GetFootball()->Move();

        match->Save();
    }                       

    return match;
}

/// 游戏的主函数
void LocalWorkflowAdapter::MainLoopForDebug()
{
    IMatch* match = m_Match;

    if (match->Status()->Round() >= match->GetTotalRound())
    {
        return;
    }

    // main loop.
    match->Status()->IncRound();
    match->Status()->SetIsGoal(false);

    if (match->Status()->BallHandler() == NULL)
    {
        match->HomeManager()->GetPlayersByPosition(Position_Goalkeeper)[0]->Status()->SetHasball(true);
    }

    // 回合初始化数据
    match->RoundInit();
    // 持球人决策
    match->Status()->BallHandler()->QuickDecide();

    // 进攻方决策
    foreach (IPlayer* player, match->Status()->BallHandler()->GetManager()->Players())
    {
        if (player == match->Status()->BallHandler())
        {
            continue;
        }

        player->QuickDecide();
    }

    // 防守方决策
    foreach (IPlayer* player, match->Status()->BallHandler()->GetManager()->Opponent()->Players())
    {
        player->QuickDecide();
    }

    // 持球人行动
    int ballHandlerId = match->Status()->BallHandler()->ClientId();
    match->Status()->BallHandler()->Action();

    // 所有球员一起行动
    foreach (IManager* manager, match->Managers())
    {
        foreach (IPlayer* player, manager->Players())
        {
            if (player->ClientId() == ballHandlerId)
            {
                continue;
            }

            if (!player->Status()->Enable())
            {
                continue;
            }

            player->Action();
        }
    }

    // 足球移动
    match->GetFootball()->Move();

    match->Save();
}