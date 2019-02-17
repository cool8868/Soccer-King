/********************************************************************************
 * 文件名：TargetSelector.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "TargetSelector.h"

vector<IPlayer*> TargetSelector::GetTarget(int target, vector<int>& targetPosition, IPlayer* player)
{
    vector<IPlayer*> targets(7);
    targets.clear();

    // Add Targets
    if (target == 0) // myself
    { 
        targets.push_back(player);
    }

    if (target == 1) // high priority in opponenters 
    { 
        if (player->Status()->State()->IsInState(ShootState::Instance()))
        {
            targets.push_back(player->GetManager()->Opponent()->GetPlayersByPosition(Position_Goalkeeper)[0]);
        }
        else
        {
            IPlayer* opponenter = (player->Status()->IsAttackSide()) ? 
                player->Status()->GetDefenceStatus()->GetClosestDefender() : player->Status()->GetDefenceStatus()->DefenceTarget();

            if (opponenter != NULL)
            {
                targets.push_back(opponenter);
            }
        }
    }

    if (target == 2)
    {
        // opponents
        if (player->Status()->IsAttackSide())
        {
            Common::AddRange(targets, player->Status()->GetDefenceStatus().Defenders());
        }
        else
        {
            targets.push_back(player->Status()->GetDefenceStatus()->DefenceTarget());
        }
    }

    if (target == 3)
    { 
        // team members
        foreach (IPlayer* p, player->GetManager()->Players())
        {
            if (!p->Status()->Enable()) continue;
            targets.push_back(p);
        }
    }

    if (target == 4)
    {
        // players in another team
        foreach (IPlayer* p, player->GetManager()->Opponent()->Players())
        {
            if (!p->Status()->Enable()) continue;
            targets.push_back(p);
        }
    }

    if (target == 5)
    {
        // the pass target
        if (player->Status()->GetPassStatus()->PassTarget() != NULL && player->Status()->GetPassStatus()->PassTarget()->Status()->Enable())
        {
            targets.push_back(player->Status()->GetPassStatus()->PassTarget());
        }
    }

    if (target == 6)
    { 
        // team members (Not include self)
        foreach (IPlayer* p, player->GetManager()->Players())
        {
            if (!p->Status()->Enable()) continue;
            if (p->Id() == player->Id()) continue;
            targets.push_back(p);
        }
    }

    // Add TargetPositions
    vector<IPlayer*> results(7);
    results.clear();

    foreach (int position, targetPosition)
    {
        foreach (IPlayer* t, targets)
        {
            if (position == 0)
            {
                results.push_back(t);
            }

            if (position == 1 && t->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
            {
                results.push_back(t);
            }

            if (position == 2 && t->BuildinAttribute()->GetPosition() == Position_Fullback)
            {
                results.push_back(t);
            }

            if (position == 3 && t->BuildinAttribute()->GetPosition() == Position_Midfielder)
            {
                results.push_back(t);
            }

            if (position == 4 && t->BuildinAttribute()->GetPosition() == Position_Forward)
            {
                results.push_back(t);
            }
        }
    }

    return results;
}

vector<IPlayer*> TargetSelector::GetTarget(int target, vector<int>& targetPosition, IPlayer* player, vector<int>& coordinates)
{
    if (coordinates.size() == 0)
    {
        return GetTarget(target, targetPosition, player);
    }

    vector<vector<int> > clist(coordinates.size() / 2 + 1);
    clist.clear();

    for (size_t i = 0; i < coordinates.size(); i++)
    {
        int index = (i / 2);
        if (clist.size() == index)
        {
            //压入2个元素
            clist.push_back(vector<int>(2));
            clist[index][0] = coordinates[i];
        }
        else
        {
            clist[index][1] = coordinates[i];
        }
    }

    vector<IPlayer*> targets = GetTarget(target, targetPosition, player);
    vector<IPlayer*> resultTargets(targets.size());
    resultTargets.clear();

    foreach (IPlayer* t, targets)
    {
        foreach (vector<int>& c, clist)
        {
            if (c[0] <= t->Status()->Default().Y && t->Status()->Default().Y <= c[1])
            {
                resultTargets.push_back(t);
                break;
            }
        }
    }

    return resultTargets;
}

