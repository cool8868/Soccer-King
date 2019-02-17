/********************************************************************************
 * 文件名：OffBallState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "OffBallState.h"
#include "StopballState.h"
#include "DiveBallState.h"
#include "DefenceState.h"
#include "ActionState.h"
#include "PositionalState.h"
#include "HoldBallState.h"
#include "ShootState.h"
#include "PassState.h"

#include "../../common/Enum/PlayerProperty.h"

OffBallState::OffBallState()
{
    m_Stopable = false;
}

OffBallState* OffBallState::Instance()
{
    static OffBallState instance;

    return &instance;
}

void OffBallState::Initialize()
{
    m_StateChain.push_back(StopballState::Instance());
    m_StateChain.push_back(DiveBallState::Instance());
    m_StateChain.push_back(DefenceState::Instance());
    m_StateChain.push_back(ActionState::Instance());
    m_StateChain.push_back(PositionalState::Instance());

    // initialize the state conditions.
    m_StateCondition[PositionalState::Instance()]       = ValidateOffBallToPositional;
    m_StateCondition[ActionState::Instance()]           = ValidateOffBallToAction;
    m_StateCondition[StopballState::Instance()]         = ValidateOffBallToStopball;
    m_StateCondition[DiveBallState::Instance()]         = ValidateOffBallToDiveBall;
    m_StateCondition[DefenceState::Instance()]          = ValidateOffBallToDefence;
}

string OffBallState::ToString()
{
    return "OffBallState";
}

IState* OffBallState::QuickDecide(IPlayer* player, IState* preview)
{
    if (player->Status()->Hasball())
    {
        player->Redecide();
        return HoldBallState::Instance();
    }
    else
    {
        IPlayer* ballHandler = player->GetMatch()->Status()->BallHandler();
        if (ballHandler == NULL)
        {
            return PositionalState::Instance();
        }

        if (player->Status()->IsAttackSide())
        {
            return PositionalState::Instance();
        }

        if (player->BuildinAttribute()->GetPosition() == Position_Goalkeeper) // 门将
        {
            //扑救状态
            if (player->GetMatch()->Status()->BallHandler()->GetSide() != player->GetSide())
            {
                // while the ball handler not make the shoot action, the goal keeper will not dive the ball.
                if (player->GetMatch()->Status()->BallHandler()->Status()->State()->IsInState(ShootState::Instance()))
                {
                    return DiveBallState::Instance();
                }
            }
        }
        else
        {
            if (player->Status()->GetDefenceStatus()->DefenceTarget() != NULL) // 有防守人
            {
                // 持球人不在射门并且不在传球状态
                if (!(ballHandler->Status()->State()->IsInState(ShootState::Instance())) && !(ballHandler->Status()->State()->IsInState(PassState::Instance())))
                {
                    if (player->GetMatch()->GetFootball()->IsInAir() == false) // 球不在空中
                    {
                        // 进入防守半径
                        if (player->Status()->BallDistance() < Defines_Player.INTERRUPTION_RANGE)
                        {
                            double probability = 0.0;

                            // 如果持球人有异常状态，则100%抢球
                            if (ballHandler->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None)
                            {
                                probability = 100.0;
                            }
                            else
                            {
                                probability = player->CurrProperty()[PlayerProperty_Aggression] * 0.3;

                                if (player->BuildinAttribute()->GetPosition() == Position_Fullback)
                                {
                                    probability *= 1.25;
                                }

                                if (player->BuildinAttribute()->GetPosition() == Position_Forward)
                                {
                                    probability *= 0.25;
                                }

                                if (probability < 0)
                                {
                                    probability = 0.0;
                                }
                            }

                            if (RandomHelper::GetPercentage() < probability)
                            {
                                return DefenceState::Instance();
                            }  
                        }
                    }
                }
            } // 是否有防守人                                                               
        }

        // 不能参与防守、扑救 -> 跑位
        return PositionalState::Instance();
    }
}

bool OffBallState::ValidateOffBallToPositional(IPlayer* player, IState* preview)
{
    // while any other state can't to enter, so the PositionalState is the default one.
    return true;
}

bool OffBallState::ValidateOffBallToAction(IPlayer* player, IState* preview)
{
    if (player->Status()->Hasball())
    {
        player->Redecide();
        return true;
    }

    return player->Status()->NeedRedecide() == false;
}

bool OffBallState::ValidateOffBallToStopball(IPlayer* player, IState* preview)
{
    return false;
}

bool OffBallState::ValidateOffBallToDiveBall(IPlayer* player, IState* preview)
{
    if (player->BuildinAttribute()->GetPosition() != Position_Goalkeeper)
    { 
        // only the goal keeper can dive ball
        return false;
    }

    if (player->GetMatch()->Status()->BallHandler() == NULL)
    { 
        // no ball handler can't dive
        return false;
    }

    // while the ball handler not make the shoot action, the goal keeper will not dive the ball.
    if (!(player->GetMatch()->Status()->BallHandler()->Status()->State()->IsInState(ShootState::Instance())))
    {
        return false;
    }

    // while the shooter is our side, it's also not necessary.
    if (player->GetMatch()->Status()->BallHandler()->GetSide() == player->GetSide())
    {
        return false;
    }

    return true;
}

bool OffBallState::ValidateOffBallToDefence(IPlayer* player, IState* preview)
{
    // goal keeper can't to make a defence, because the goal keeper can't interrup and slide tackle.
    if (player->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
    {
        return false;
    }

    if (player->Status()->Hasball())
    {
        return false;
    }

    if (!player->Status()->NeedRedecide())
    {
        return false;
    }

    if (player->Status()->IsAttackSide())
    {
        return false;
    }

    if (player->Status()->GetDefenceStatus()->DefenceTarget() == NULL)
    {
        return false;
    }

    if (player->GetMatch()->Status()->BallHandler()->Status()->State()->IsInState(ShootState::Instance()) ||
        player->GetMatch()->Status()->BallHandler()->Status()->State()->IsInState(PassState::Instance()))
    {
        return false;
    }

    if (player->GetMatch()->GetFootball()->IsInAir())
    {
        return false;
    }

    int ballDistance = player->Status()->BallDistance(); // 球距离
    if (ballDistance > Defines_Player.INTERRUPTION_RANGE)
    { 
        // 如果离球过远不判定         
        return false;
    }

    // 起脚率
    double probability = pow(player->CurrProperty()[PlayerProperty_Aggression] / 100, 2) * 100 * 0.05;
    if (probability > 20)
    {
        probability = 20;
    }

    return RandomHelper::GetPercentage() < probability;
}

bool OffBallState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    return false;
}
