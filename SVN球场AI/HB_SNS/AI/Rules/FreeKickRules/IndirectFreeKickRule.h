/********************************************************************************
 * 文件名：IndirectFreeKickRule.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __INDIRECTFREEKICKRULE_H__
#define __INDIRECTFREEKICKRULE_H__

#include "../../States/Pass/LongPassState.h"

class IndirectFreeKickRule : public FreeKickRule
{
public:

    /// Initializes a new instance of the <see cref="IndirectFreeKickRule"/> class.
    /// <param name="manager">Represents the take kick player manager.</param>
    /// <param name="point">Represents a <see cref="Coordinate"/> that is the open ball point.</param>
    IndirectFreeKickRule(IManager* manager, Coordinate point)
        : FreeKickRule(manager, point)
    {

    }

    /// 执行一次间接任意球
    void Start()
    {
        // 站位回合
        m_Manager->GetMatch()->Status()->IncRound(); // 当前回合加1
        m_Manager->GetMatch()->RoundInit();

        // 找出罚球人-> 找出离球最近的人
        IPlayer* takeKickPlayer = MatchRule::GetClosestPlayerFromBallInMySide(m_Manager);
        if (takeKickPlayer == NULL)
        {
            return;
        }

        takeKickPlayer->Status()->SetHasball(true);

        // 罚球人站到球面前            
        takeKickPlayer->MoveTo(m_Point);
        takeKickPlayer->Rotate((m_Manager->GetSide() == Side_Home) ?
            m_Manager->GetMatch()->GetPitch()->AwayGoal() : m_Manager->GetMatch()->GetPitch()->HomeGoal());

        // 防守方移动至防守位置
        foreach (IPlayer* p, takeKickPlayer->GetManager()->Opponent()->Players())
        {
            // 下场及有异常状态的球员不移动位置
            if (p->Status()->Enable() == false) continue;
            if (p->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None) continue;

            Coordinate coor;
            if (p->BuildinAttribute()->GetPosition() != Position_Goalkeeper)
            {
                coor = CloseMove(p->Status()->GetDefault().X, p->Status()->GetDefault().Y);
            }
            else
            {
                coor = p->Status()->GetDefault();
            }

            p->Status()->SetState(IdleState::Instance());
            p->MoveTo(coor);
            p->Rotate(m_Point);
        }

        // 进攻方除罚球人移动至进攻位置
        foreach (IPlayer* p, takeKickPlayer->GetManager()->Players())
        {
            // 下场及有异常状态的球员不移动位置
            if (p->Status()->Enable() == false) continue;
            if (p->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None) continue;

            if (p->ClientId() == takeKickPlayer->ClientId()) continue;                
            if (p->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
            {
                p->MoveTo(p->Status()->GetDefault());
                p->Rotate(m_Point);
                p->Status()->SetState(IdleState::Instance());
                continue;
            }

            double x = (p->GetManager()->GetSide() == Side_Home) ?
                p->Status()->GetDefault().X * 1.6 : p->Status()->GetDefault().X * 1.6 - Defines_Pitch.MAX_WIDTH * 0.6;
            double y = p->Status()->GetDefault().Y;
            y = RandomHelper::GetBoolean() ? y + 5 : y - 5;

            Coordinate coor = CloseMove(x, y);

            p->Status()->SetState(IdleState::Instance());
            p->MoveTo(coor);
            p->Rotate(m_Point);
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
        takeKickPlayer->Status()->SetState(PassState::Instance());
        takeKickPlayer->Status()->State()->Enter(takeKickPlayer);

        // 如果没有合适的传球人
        if (takeKickPlayer->Status()->GetPassStatus()->PassTarget() == NULL) // 没有合适传球的人
        {
            takeKickPlayer->Status()->GetPassStatus()->SetPassTarget(PassTargetDecideRule::PassClosest(takeKickPlayer));
        }

        if (takeKickPlayer->Current().Distance(takeKickPlayer->Status()->GetPassStatus()->PassTarget()->Current()) < 50)
        {
            takeKickPlayer->Status()->SetState(ShortPassState::Instance());
        }
        else
        {
            takeKickPlayer->Status()->SetState(LongPassState::Instance());
        }

        takeKickPlayer->Rotate(takeKickPlayer->Status()->GetPassStatus()->PassTarget()->Current());
        takeKickPlayer->Action();

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

#endif //__INDIRECTFREEKICKRULE_H__
