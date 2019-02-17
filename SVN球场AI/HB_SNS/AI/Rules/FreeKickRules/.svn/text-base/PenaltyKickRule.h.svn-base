/********************************************************************************
 * 文件名：PenaltyKickRule.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PENALTYKICKRULE_H__
#define __PENALTYKICKRULE_H__

#include "FreeKickRule.h"
#include "../MatchRule.h"
#include "../../States/Shoot/VolleyShootState.h"

/// 点球规则    
class PenaltyKickRule : public FreeKickRule
{
public:

    /// Initializes a new instance of the <see cref="PenaltyKickRule"/> class.
    /// <param name="manager">Represents the take kick player manager.</param>
    /// <param name="point">Represents a <see cref="Coordinate"/> that is the open ball point.</param>
    PenaltyKickRule(IManager* manager, Coordinate point)
        : FreeKickRule(manager, point)
    {
    }

    /// 罚点球
    void Start()
    {
        // 站位回合
        // 当前回合加1
        m_Manager->GetMatch()->Status()->IncRound();

        // 找出罚球人-> 找出离球最近的人
        // 找出罚球人，罚球人为任意球属性最高的球员（不包含守门员）                        
        IPlayer* takeKickPlayer = MatchRule::GetHighestPropertyPlayer(m_Manager, PlayerProperty_FreeKick);
        if (takeKickPlayer == NULL) // 没有可以罚球的人，跳出逻辑
        {
            return;
        }

        takeKickPlayer->Status()->SetHasball(true);

        // 罚球人站到球面前            
        takeKickPlayer->MoveTo(m_Point);
        takeKickPlayer->Rotate((m_Manager->GetSide() == Side_Home) ? m_Manager->GetMatch()->GetPitch()->AwayGoal() : m_Manager->GetMatch()->GetPitch()->HomeGoal());

        // 防守方移动至防守位置
        Region region = (m_Manager->GetSide() == Side_Home) ? m_Manager->GetMatch()->GetPitch()->AwayPenaltyRegion() : m_Manager->GetMatch()->GetPitch()->HomePenaltyRegion();
        foreach (IPlayer* p, takeKickPlayer->GetManager()->Opponent()->Players())
        {
            Coordinate coor;
            if (p->BuildinAttribute()->GetPosition() != Position_Goalkeeper)
            {
                coor = CloseMove(p->Status()->GetDefault().X, p->Status()->GetDefault().Y);
            }
            else
            {
                coor = p->Status()->GetDefault();
            }

            if (p->BuildinAttribute()->GetPosition() == Position_Fullback) // 将防守人移动至禁区线上
            {
                if (region.Start.X == 0)
                {
                    coor = Coordinate(region.End.X, coor.Y);
                }
                else
                {
                    coor = Coordinate(region.Start.X, coor.Y);
                }
            }

            p->Status()->SetState(IdleState::Instance());
            p->MoveTo(coor);
            p->Rotate(m_Point);
        }

        // 进攻方除罚球人移动至进攻位置
        foreach (IPlayer* p, takeKickPlayer->GetManager()->Players())
        {
            if (p->ClientId() == takeKickPlayer->ClientId()) continue;
            if (p->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
            {
                p->MoveTo(p->Status()->GetDefault());
                p->Rotate(m_Point);
                p->Status()->SetState(IdleState::Instance());
                continue;
            }

            double x = (p->GetManager()->GetSide() == Side_Home) ? p->Status()->GetDefault().X * 1.6 : p->Status()->GetDefault().X * 1.6 - Defines_Pitch.MAX_WIDTH * 0.6;
            double y = p->Status()->GetDefault().Y;
            y = RandomHelper::GetBoolean() ? y + 5 : y - 5;

            Coordinate coor = CloseMove(x, y);

            p->Status()->SetState(IdleState::Instance());
            p->MoveTo(coor);
            p->Rotate(m_Point);
        }

        // 防止除罚球人外任何人进入禁区            
        foreach (IManager* m, m_Manager->GetMatch()->Managers())
        {
            foreach (IPlayer* player, m->Players())
            {
                if (player->ClientId() == takeKickPlayer->ClientId())
                {
                    continue;
                }

                if (player->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
                {
                    continue;
                }

                if (region.IsCoordinateInRegion(player->Current()))
                {
                    if (region.Start.X == 0)
                    {
                        player->MoveTo(region.End.X, player->Current().Y);
                    }
                    else
                    {
                        player->MoveTo(region.Start.X, player->Current().Y);
                    }
                }
            }
        }

        // 停顿时间
        for (int i = 0; i < 4; i++)
        {
            m_Manager->GetMatch()->Save();
            m_Manager->GetMatch()->Status()->IncRound();
            m_Manager->GetMatch()->RoundInit();
        }

        // 开球回合
        m_Manager->GetMatch()->RoundInit();
        takeKickPlayer->Status()->SetState(VolleyShootState::Instance());
        takeKickPlayer->AddFinishing();
        takeKickPlayer->Action();

        IPlayer* gk = m_Manager->Opponent()->GetPlayersByPosition(Position_Goalkeeper)[0];
        gk->QuickDecide();
        gk->Action();

        foreach (IPlayer* p, m_Manager->Players())
        {
            p->Save();
        }

        foreach (IPlayer* p, m_Manager->Opponent()->Players())
        {
            p->Save();
        }

        m_Manager->GetMatch()->GetFootball()->Save(m_Manager->GetMatch()->Status()->Round());
    }
};

#endif //__PENALTYKICKRULE_H__
