/********************************************************************************
 * 文件名：GkOpenballRule.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __GKOPENBALLRULE_H__
#define __GKOPENBALLRULE_H__

#include "../../States/Idle/GKHoldBallState.h"

/// 门球开球规则
class GkOpenballRule
{
public:

    /// 开始一个开球规则
    static void Start(IManager* openBallManager)
    {
        IPlayer* gk = openBallManager->Players()[0];
        if (gk->Status()->Enable() == false)
        {
            foreach (IPlayer* p, openBallManager->Players())
            {
                if (p->Status()->Enable())
                {
                    gk = p;
                    break;
                }
            }
        }

        gk->GetMatch()->Status()->SetInterruption(false);
        gk->GetMatch()->Status()->SetBreak(true);
        gk->GetMatch()->Status()->SetFoul(true);
        gk->GetMatch()->Save();
        gk->GetMatch()->Status()->IncRound();

        gk->MoveTo(openBallManager->Players()[0]->Status()->GetDefault());
        gk->GetMatch()->GetFootball()->MoveTo(gk->Current());
        gk->Status()->SetHasball(true);
        gk->Status()->GetActionStatus()->SetActionLast(0);
        gk->Status()->SetState(GKHoldBallState::Instance());

        /// TODO: 修正门球重置位置
        foreach (IManager* manager, openBallManager->GetMatch()->Managers())
        {
            foreach (IPlayer* player, manager->Players())
            {
                if (player->Status()->Enable() == false) continue;
                if (player->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None)
                {
                    if (player->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_InertiaDebuff)
                    {
                        continue;
                    }
                }

                player->Status()->SetState(IdleState::Instance());
                player->MoveTo(player->Status()->GetDefault());
                player->Rotate(player->GetMatch()->GetFootball()->Current());
            }
        }

        gk->GetMatch()->Save();

        // 停顿时间
        for (int i = 0; i < 4; i++)
        {
            openBallManager->GetMatch()->Save();
            openBallManager->GetMatch()->Status()->IncRound();
            openBallManager->GetMatch()->RoundInit();
        }
    }
};

#endif //__GKOPENBALLRULE_H__
