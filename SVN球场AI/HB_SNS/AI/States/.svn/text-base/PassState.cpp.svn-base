/********************************************************************************
 * 文件名：PassState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "PassState.h"
#include "HoldBallState.h"
#include "OffBallState.h"
#include "DribbleState.h"

#include "Pass/LongPassState.h"
#include "Pass/ShortPassState.h"

#include "../../MatchProcess/PassProcess.h"

PassState::PassState()
{
    m_Stopable = false;
}

PassState* PassState::Instance()
{
    static PassState instance;

    return &instance;
}

void PassState::Initialize()
{
    m_StateChain.push_back(LongPassState::Instance());
    m_StateChain.push_back(ShortPassState::Instance());
    m_StateChain.push_back(HoldBallState::Instance());

    m_StateCondition[HoldBallState::Instance()]         = ValidatePassToHoldBall;
    m_StateCondition[ShortPassState::Instance()]        = ValidatePassToShortPass;
    m_StateCondition[LongPassState::Instance()]         = ValidatePassToLongPass;
}

void PassState::Enter(IPlayer* player)
{
    if (player->Status()->Hasball())
    {
        player->DecideEnd();
        player->DecidePassTarget();
    }
}

string PassState::ToString()
{
    return "PassState";
}

Process* PassState::Save(IPlayer* player)
{
    PassProcess* process = new PassProcess();

    State::Save(process, player);

    process->SetIsPassFail(player->Status()->GetPassStatus()->IsPassFail());

    return process;
}

IState* PassState::QuickDecide(IPlayer* player, IState* preview)
{
    if (player->Status()->Hasball() == false)
    {
        return OffBallState::Instance();
    }
    else
    {
        if (player->Status()->Holdball())
        {
            if (player->Status()->GetPassStatus()->PassTarget() == NULL)
            {
                return DribbleState::Instance();
            }
            else
            {
                IPlayer* target = player->Status()->GetPassStatus()->PassTarget();
                if (target->BuildinAttribute()->GetPosition() == Position_Goalkeeper || target->BuildinAttribute()->GetPosition() == Position_Fullback)
                {
                    return ShortPassState::Instance();
                }

                if (player->Current().Distance(player->Status()->GetPassStatus()->PassTarget()->Current()) <= 50)
                {
                    return ShortPassState::Instance();
                }

                if (RandomHelper::GetPercentage() < Defines_Player.LONG_PASS_PERCENTAGE) // 长传概率
                {
                    return LongPassState::Instance();
                }
                else
                {
                    return ShortPassState::Instance();
                }
            }
        }
        else
        {
            return HoldBallState::Instance();
        }
    }
}

bool PassState::ValidatePassToHoldBall(IPlayer* player, IState* preview)
{
    return true;
}

bool PassState::ValidatePassToShortPass(IPlayer* player, IState* preview)
{
    if (!player->Status()->Hasball())
    {
        return false;
    }

    if (player->Status()->GetPassStatus()->PassTarget() == NULL)
    {
        player->Redecide();

        return false;
    }

    if (!player->Status()->Holdball())
    {
        return false;
    }

    return true;
}

bool PassState::ValidatePassToLongPass(IPlayer* player, IState* preview)
{
    if (!player->Status()->Hasball())
    {
        return false;
    }

    if (player->Status()->GetPassStatus()->PassTarget() == NULL)
    {
        player->Redecide();

        return false;
    }

    if (!player->Status()->Holdball())
    {
        return false;
    }

    if (player->Status()->GetPassStatus()->PassTarget()->BuildinAttribute()->GetPosition() == Position_Goalkeeper ||
        player->Status()->GetPassStatus()->PassTarget()->BuildinAttribute()->GetPosition() == Position_Fullback)
    {
        return false;
    }

    if (player->Current().Distance(player->Status()->GetPassStatus()->PassTarget()->Current()) <= 50)
    {
        return false;
    }

    return RandomHelper::GetPercentage() < Defines_Player.LONG_PASS_PERCENTAGE;
}

bool PassState::IsInState(IState* st)
{
    if (typeid(*this) == typeid(*st)) 
    {
        return true;
    }

    return false;
}
