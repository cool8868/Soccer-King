/********************************************************************************
 * 文件名：OpenballRule.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __OPENBALLRULE_H__
#define __OPENBALLRULE_H__

#include "../../../Interface/Player/Status/IPlayerStatus.h"
#include "../../States/IdleState.h"
#include "../../States/Pass/ShortPassState.h"

#include "../../Rules/PassTargetDecideRule.h"

/// 表示了开球的逻辑规则
class OpenballRule
{
public:

    /// 开启一个开球回合
    static void Start(IManager* openBallManager)
    {
        //重置所有的物体
        IMatch* match = openBallManager->GetMatch();
        match->GetFootball()->Reset();

        foreach (IManager* manager, match->Managers())
        {
            foreach (IPlayer* player, manager->Players())
            {
                // 下场及有异常状态的球员不移动位置
                if (player->Status()->Enable() == false) continue;
                if (player->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None)
                {
                    if (player->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_InertiaDebuff)
                    {
                        continue;
                    }
                }

                player->Reset();
                player->MoveTo(player->Status()->GetDefault());
                player->Status()->SetState(IdleState::Instance());
                player->Status()->SetSpeed(0);
                player->Rotate(match->GetFootball()->Current());
            }
        }

        // 防止对方站入中圈内
        foreach (IPlayer* player, openBallManager->Opponent()->Players())
        {
            if (player->BuildinAttribute()->GetPosition() == Position_Forward)
            {
                if (player->GetSide() == Side_Away && player->Current().X < 130)
                {
                    player->MoveTo(130, player->Current().Y);
                }

                if (player->GetSide() == Side_Home && player->Current().X > 80)
                {
                    player->MoveTo(80, player->Current().Y);
                }
            }
        }
        
        // 找出1个发球人和1个接应人
        vector<IPlayer*> openballArray(2);
        openballArray.clear();
        for (int i = openBallManager->Players().size() - 1; i >= 0; --i)
        {
            if (openBallManager->Players()[i]->Status()->Enable() == false)
            {
                continue;
            }

            if (openBallManager->Players()[i]->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None) continue;

            if (openBallManager->Players()[i]->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
            {
                continue;
            }

            openballArray.push_back(openBallManager->Players()[i]);

            if (openballArray.size() == 2)
            {
                break;
            }
        }

        if (openballArray.size() == 0) // 没有任何发球人
        {
            openballArray.push_back(openBallManager->Players()[0]);
            match->Save();
            return;
        }
        else // 有足够的发球人
        {
            if (openballArray.size() == 1) // 只有1个发球人
            {
                openballArray[0]->MoveTo(_openballPosition1);
                openballArray[0]->Rotate(match->GetFootball()->Current());

                openballArray.push_back(openBallManager->Players()[0]); // 添加守门员为第二接球人

                match->Save();
                return;
            }
            else // 拥有2个发球人
            {
                openballArray[0]->MoveTo(_openballPosition1);
                openballArray[1]->MoveTo(_openballPosition2);
                openballArray[0]->Rotate(match->GetFootball()->Current());
                openballArray[1]->Rotate(match->GetFootball()->Current());

                openballArray[0]->Status()->SetHasball(true);
            }
        }

        // 发球
        match->Save();

        match->Status()->IncRound();
        match->RoundInit();
        openballArray[1]->Status()->SetHasball(true);
        match->Save();

        match->Status()->IncRound();
        match->RoundInit();
        openballArray[1]->Status()->SetState(ShortPassState::Instance());
        openballArray[1]->Status()->GetPassStatus()->SetPassTarget(PassTargetDecideRule::OpenballDecide(openballArray[1]));
        openballArray[1]->Action();
        match->Save();
    }

private:
    static Coordinate _openballPosition1;
    static Coordinate _openballPosition2;
};

Coordinate OpenballRule::_openballPosition1 = Coordinate::Parse(Defines_Position.OPENBALL_POSITION_1);
Coordinate OpenballRule::_openballPosition2 = Coordinate::Parse(Defines_Position.OPENBALL_POSITION_2);


#endif //__OPENBALLRULE_H__
