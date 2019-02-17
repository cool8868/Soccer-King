/********************************************************************************
 * 文件名：ChaceState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ChaceState.h"
#include "IdleState.h"
#include "ActionState.h"
#include "OffBallState.h"
#include "PositionalState.h"

#include "../Actions/ChaceAction.h"

ChaceState::ChaceState()
{
    m_Stopable = true;
}

ChaceState::~ChaceState()
{

}

ChaceState* ChaceState::Instance()
{
    static ChaceState instance;

    return &instance;
}

string ChaceState::ToString()
{
    return "ChaceState";
}

void ChaceState::Initialize()
{
    m_StateChain.push_back(this);
    m_StateChain.push_back(IdleState::Instance());
    m_StateChain.push_back(ActionState::Instance());

    m_StateCondition[ActionState::Instance()]       = ValidateChaceToAction;
    m_StateCondition[this]                          = ValidateChaceToChace;
    m_StateCondition[IdleState::Instance()]         = ValidateChaceToIdle;
}

void ChaceState::Action(IPlayer* player)
{
    ChaceAction::Instance()->Action(player);
}

IState* ChaceState::QuickDecide(IPlayer* player, IState* preview)
{
    if (player->Status()->NeedRedecide())
    {
        if (player->Status()->IsAttackSide() == false) // 防守方
        {
            if (player->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
            {
                return OffBallState::Instance();
            }

            if (player->Status()->GetDefenceStatus()->DefenceTarget() == NULL) // 没有防守对象
            {
                return PositionalState::Instance();
            }
        }
        else // 进攻方
        {
            if (player->Status()->Hasball() == false)
            {
                return PositionalState::Instance();
            }
        }

        return ActionState::Instance();
    }
    else
    {
        if (MATH::FloatEqual(player->Status()->DestinationDistance() ,0))
        {
            return IdleState::Instance();
        }
        else
        {
            return ChaceState::Instance();
        }
    }
}

bool ChaceState::ValidateChaceToAction(IPlayer* player, IState* preview)
{
    return player->Status()->NeedRedecide();
}

bool ChaceState::ValidateChaceToChace(IPlayer* player, IState* preview)
{
    if (player->Status()->NeedRedecide())
    {
        return false;
    }

    if (MATH::FloatEqual(player->Status()->DestinationDistance() , 0.0))
    {
        return false;
    }

    return true;
}

bool ChaceState::ValidateChaceToIdle(IPlayer* player, IState* preview)
{
    if (player->Status()->NeedRedecide())
    {
        return false;
    }

    if (player->Status()->DestinationDistance() > 0)
    {
        return false;
    }

    return true;
}

bool ChaceState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    return false;
}