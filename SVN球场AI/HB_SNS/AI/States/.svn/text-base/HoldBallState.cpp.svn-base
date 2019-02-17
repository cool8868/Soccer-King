/********************************************************************************
 * 文件名：HoldBallState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "HoldBallState.h"
#include "OffBallState.h"
#include "ChaceState.h"
#include "ActionState.h"
#include "ShootState.h"
#include "PassState.h"
#include "DribbleState.h"

#include "../../common/Enum/PlayerProperty.h"

HoldBallState::HoldBallState()
{
    m_Stopable = false;
}

HoldBallState* HoldBallState::Instance()
{
    static HoldBallState instance;

    return &instance;
}

string HoldBallState::ToString()
{
    return "HoldBallState";
}

void HoldBallState::Initialize()
{
    m_StateChain.push_back(ActionState::Instance());
    m_StateChain.push_back(ShootState::Instance());
    m_StateChain.push_back(PassState::Instance());
    m_StateChain.push_back(DribbleState::Instance());

    m_StateCondition[PassState::Instance()]         = ValidateHoldBallToPass;
    m_StateCondition[ActionState::Instance()]       = ValidateHoldBallToAction;
    m_StateCondition[DribbleState::Instance()]      = ValidateHoldBallToDribble;
    m_StateCondition[ShootState::Instance()]        = ValidateHoldBallToShoot;
}

void HoldBallState::Enter(IPlayer* player)
{
    IMatch* match = player->GetMatch();

    // while the ball distance is larger than zero, the player will decide to chace ball
    if (player->Status()->Hasball() && player->Status()->BallDistance() > 0)
    {
        if (match->GetFootball()->Speed() < 20)
        {
            player->SetTargetBall(true);
        }
        else
        {
            if (match->GetFootball()->IsPassDestination()) // 球已经滚动过了目标点
            {
                player->SetTargetBall(true);
            }
            else
            {
                player->SetTargetBall(false);
            }
        }
    }
}

IState* HoldBallState::QuickDecide(IPlayer* player, IState* preview)
{
    if (player->Status()->Hasball() == false)
    {
        return OffBallState::Instance();
    }
    else
    {
        if (player->Status()->Holdball() == false)
        {
            return ChaceState::Instance();
        }

        if (player->Status()->NeedRedecide() == false)
        {
            return ActionState::Instance();
        }
        else
        {
            //验证射门
            Region shootRegion = (player->GetSide() == Side_Home) ? 
                player->GetMatch()->GetPitch()->AwayShootRegion() : player->GetMatch()->GetPitch()->HomeShootRegion();

            if (shootRegion.IsCoordinateInRegion(player->Current()))
            {
                player->AddFinishing();

                return ShootState::Instance();
            }

            Region region = player->Status()->GetShootStatus()->ShootRegion();                    

            // while the player is not in the shooting area, can't to shoot
            if (player->Status()->GetShootStatus()->ShootRegion().IsCoordinateInRegion(player->Current()))
            {
                Coordinate target = (player->GetSide() == Side_Home) ? 
                    player->GetMatch()->GetPitch()->AwayGoal() : player->GetMatch()->GetPitch()->HomeGoal();

                double distance = player->Current().Distance(target);
                double percentange = 0.0;

                percentange = (1 - pow(1 - player->CurrProperty()[PlayerProperty_Shooting] / 250, 0.25)) * 100;

                percentange += 10;

                // while the player is in the shooting area, still need a percentage to decide shoot. 1-((射门属性/400))1/4 
                if (RandomHelper::GetPercentage() < percentange)
                {
                    return ShootState::Instance();
                }
            }

            //验证传球
            if (player->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
            {
                return PassState::Instance();
            }

            double percentage = player->CurrProperty()[PlayerProperty_PassProbability];

            if (player->BuildinAttribute()->GetPosition() == Position_Midfielder ||
                player->BuildinAttribute()->GetPosition() == Position_Forward)
            {
                if (player->CurrProperty()[PlayerProperty_Dribble] - player->CurrProperty()[PlayerProperty_Passing] >= 18)
                {
                    percentage = 30;
                }
            }

            if (RandomHelper::GetPercentage() < percentage)
            {
                return PassState::Instance();
            }

            return DribbleState::Instance();
        }
    }
}

bool HoldBallState::ValidateHoldBallToAction(IPlayer* player, IState* preview)
{
    if (!player->Status()->Hasball())
    {
        return true;
    }

    if (player->Status()->NeedRedecide())
    {
        return false;
    }

    return true;
}

bool HoldBallState::ValidateHoldBallToPass(IPlayer* player, IState* preview)
{
    if (!player->Status()->Hasball())
    {
        return false;
    }

    if (!player->Status()->NeedRedecide())
    {
        return false;
    }

    if (!player->Status()->Holdball())
    {
        return false;
    }

    if (player->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
    {
        return true;
    }           

    Region region = (player->GetSide() == Side_Home) ? 
        player->GetMatch()->GetPitch()->AwayForcePassRegion() : player->GetMatch()->GetPitch()->HomeForcePassRegion();

    if (region.IsCoordinateInRegion(player->Current()))
    {
        return true;
    }

    return (RandomHelper::GetPercentage() < player->CurrProperty()[PlayerProperty_PassProbability]);
}

bool HoldBallState::ValidateHoldBallToDribble(IPlayer* player, IState* preview)
{
    if (!player->Status()->NeedRedecide())
    {
        return false;
    }

    return true;
    //return DribbleConditionFactory.Create(player.Property.Position).Decide(player, preview);
}

bool HoldBallState::ValidateHoldBallToShoot(IPlayer* player, IState* preview)
{
    // while the ball handler is no need to decide, can't to the shoot
    if (!player->Status()->NeedRedecide())
    {
        return false;
    }

    Region region = (player->GetSide() == Side_Home) ? 
        player->GetMatch()->GetPitch()->AwayShootRegion() : player->GetMatch()->GetPitch()->HomeShootRegion();

    if (region.IsCoordinateInRegion(player->Current()))
    {
        player->AddFinishing();

        return true;
    }

    // while the player is not in the shooting area, can't to shoot
    if (!player->Status()->GetShootStatus()->ShootRegion().IsCoordinateInRegion(player->Current()))
    {
        return false;
    }

    // while the player is in the shooting area, still need a percentage to decide shoot. 1-((射门属性/400))1/4 
    return (RandomHelper::GetPercentage() < (1 - pow(player->CurrProperty()[PlayerProperty_Shooting] / 400, 0.25)) * 100); 
    // return (RandomHelper.GetPercentage() < (1 - Math.Pow(player.CurrProperty[PlayerProperty.Shooting] / 400, 0.25)) * 100);            
}

bool HoldBallState::ValidateHoldBallToStopBall(IPlayer* player, IState* preview)
{
    if (player->Status()->BallDistance() < 2)
    {
        return true;
    }

    return false;
}

bool HoldBallState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    return false;
}
