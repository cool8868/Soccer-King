/********************************************************************************
 * 文件名：DirectFreeKickRule.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DIRECTFREEKICKRULE_H__
#define __DIRECTFREEKICKRULE_H__

#include "FreeKickRule.h"
#include "../../States/Shoot/FreekickShootState.h"

class DirectFreeKickRule : public FreeKickRule
{
public:

    /// Initializes a new instance of the <see cref="DirectFreeKickRule"/> class.
    /// <param name="manager">Represents the take kick player manager.</param>
    /// <param name="point">Represents a <see cref="Coordinate"/> that is the open ball point.</param>
    DirectFreeKickRule(IManager* manager, Coordinate point)
        : FreeKickRule(manager, point)
    {
    }

    /// Starts a free kick.
    void Start()
    {
        // 站位回合
        // 当前回合加1
        m_Manager->GetMatch()->Status()->IncRound();
        m_Manager->GetMatch()->RoundInit();

        // 射门目标
        Coordinate target = (m_Manager->GetSide() == Side_Home) ? m_Manager->GetMatch()->GetPitch()->AwayGoal() : m_Manager->GetMatch()->GetPitch()->HomeGoal();
        // 已经移动位置后的球员列表
        vector<IPlayer*> finArray(Defines_Match.MAX_PLAYER_COUNT * 2);
        finArray.clear();

        // 罚球人相关
        // 找出罚球人，罚球人为任意球属性最高的球员（不包含守门员）            
        IPlayer* takeKickPlayer = MatchRule::GetHighestPropertyPlayer(m_Manager, PlayerProperty_FreeKick);
        if (takeKickPlayer == NULL) // 如果没有可以罚球的球员，返回
        {
            return;
        }
        takeKickPlayer->Status()->SetHasball(true);
        finArray.push_back(takeKickPlayer);

        // 保存足球回合
        takeKickPlayer->GetMatch()->GetFootball()->Save(takeKickPlayer->GetMatch()->Status()->Round());

        // 罚球人站到球面前            
        takeKickPlayer->MoveTo(m_Point);
        takeKickPlayer->Rotate(m_Point);

        // 找出人墙区
        const int distance = 20;  // 离人墙的距离
        const int pDistance = 5; // 人墙间的距离   

        double xdiff = m_Point.X - target.X;
        double ydiff = m_Point.Y - target.Y;
        if (xdiff == 0) xdiff = 1;
        if (ydiff == 0) ydiff = 1;

        double tDistance = target.Distance(m_Point); // 总距离
        double l = xdiff / -ydiff;  // 斜率

        double x0 = m_Point.X - xdiff / tDistance * distance;
        double y0 = m_Point.Y - ydiff / tDistance * distance;

        // 填充人墙坐标
        int wallPlayerCount = RandomHelper::GetBoolean() ? 2 : 3;
        vector<Coordinate> wallPoints(wallPlayerCount);
        wallPoints.clear();
        if (wallPlayerCount == 2)
        {
            double x = x0 - pDistance / 2 * xdiff / tDistance;
            double y = y0 + pDistance / 2 * ydiff / tDistance;
            wallPoints.push_back(Coordinate(x, y));

            x = x0 + pDistance / 2 * xdiff / tDistance;
            y = y0 - pDistance / 2 * ydiff / tDistance;
            wallPoints.push_back(Coordinate(x, y));
        }
        else
        { 
            // wall player count = 3
            double x = x0 - pDistance * xdiff / tDistance;
            double y = y0 + pDistance * ydiff / tDistance;
            wallPoints.push_back(Coordinate(x, y));

            x = x0;
            y = y0;
            wallPoints.push_back(Coordinate(x, y));

            x = x0 + pDistance * xdiff / tDistance;
            y = y0 - pDistance * ydiff / tDistance;
            wallPoints.push_back(Coordinate(x, y));
        }


        // 摆人墙
        // 从防守方找出离球最近的人摆人墙
        double              defenders[MyDefine_MAX_PLAYER_COUNT - 1] = {};
        vector<IPlayer*>    array(Defines_Match.MAX_PLAYER_COUNT - 1);
        array.clear();
        int length = 0;
        for (int i = 0; i < Defines_Match.MAX_PLAYER_COUNT; i++)
        {
            // 下场及有异常状态的球员不移动位置
            if (m_Manager->Opponent()->Players()[i]->Status()->Enable() == false) continue;
            if (m_Manager->Opponent()->Players()[i]->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None) continue;

            if (m_Manager->Opponent()->Players()[i]->BuildinAttribute()->GetPosition() == Position_Goalkeeper) continue;
            if (m_Manager->Opponent()->Players()[i]->BuildinAttribute()->GetPosition() == Position_Forward) continue;

            defenders[length] = m_Manager->Opponent()->Players()[i]->Current().Distance(m_Point);
            array.push_back(m_Manager->Opponent()->Players()[i]);
            length++;
        }

        int indexes[MyDefine_MAX_PLAYER_COUNT - 1] = {};
        Common::SortMinDoubleArrayIndexQuick(defenders, length, indexes);
        for (int i = 0; i < wallPlayerCount; i++)
        {
            if (i >= static_cast<int>(array.size())) continue; // 修正人墙人数不够引发异常

            Coordinate p = wallPoints[i];
            array[indexes[i]]->MoveTo(p);
            finArray.push_back(array[indexes[i]]);
        }

        // 防守方剩余球员站位
        foreach (IPlayer* p, m_Manager->Opponent()->Players())
        {
            // 下场及有异常状态的球员不移动位置
            if (p->Status()->Enable() == false) continue;
            if (p->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None) continue;

            if (find(finArray.begin(), finArray.end(), p) == finArray.end())
            {
                if (p->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
                { 
                    // 门将
                    p->MoveTo(p->Status()->GetDefault());
                }
                else
                { 
                    // 非门将
                    p->MoveTo(CloseMove(p->Status()->GetDefault().X, p->Status()->GetDefault().Y));
                }
            }
            p->Rotate(m_Manager->GetMatch()->GetFootball()->Current());
        }

        // 进攻方剩余球员站位
        IPlayer* lastMan = FindOutLastDefender(); // 最后一个防守人
        foreach (IPlayer* p, m_Manager->Players())
        {
            // 下场及有异常状态的球员不移动位置
            if (p->Status()->Enable() == false) continue;
            if (p->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None) continue;

            if (p->ClientId() != takeKickPlayer->ClientId())
            { 
                // 非罚球人
                if (p->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
                { 
                    // 门将
                    p->MoveTo(p->Status()->GetDefault());
                }
                else
                {
                    if (p->BuildinAttribute()->GetPosition() == Position_Forward)
                    {
                        // 前锋挤压阵型
                        int excursion = (m_Manager->GetSide() == Side_Home) ? -RandomHelper::GetInt32(0, 10) : RandomHelper::GetInt32(0, 10);
                        p->MoveTo(lastMan->Current().X + excursion, p->Status()->GetDefault().Y);
                    }
                    else
                    { 
                        // 其余进攻球员
                        double x = (p->GetManager()->GetSide() == Side_Home) ? 
                            p->Status()->GetDefault().X * 1.6 : p->Status()->GetDefault().X * 1.6 - Defines_Pitch.MAX_WIDTH * 0.6;
                        double y = p->Status()->GetDefault().Y;
                        p->MoveTo(CloseMove(x, y));
                    }
                }
            }

            p->Rotate(m_Manager->GetMatch()->GetFootball()->Current());
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
        takeKickPlayer->Status()->SetState(FreekickShootState::Instance());
        Player::ValidateSkillTriggered(takeKickPlayer);
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

    /// 找出最后一个防守者
private:
    
    IPlayer* FindOutLastDefender()
    {
        double array[MyDefine_MAX_PLAYER_COUNT];

        int length = 0;
        for (int i = 0; i < Defines_Match.MAX_PLAYER_COUNT; i++)
        {
            if (m_Manager->Opponent()->Players()[i]->BuildinAttribute()->GetPosition() == Position_Goalkeeper) continue;
            array[length++] = m_Manager->Opponent()->Players()[i]->Current().X;
        }

        int index = 0;
        if (m_Manager->Opponent()->GetSide() == Side_Home)
        { 
            // 主队的话需要找出X坐标做小
            index = Common::GetDoubleMinIndexQuick(array, length) + 1;
        }
        else
        { 
            // 客队需要找出X坐标最大
            index = Common::GetDoubleMaxIndexQuick(array, length) + 1;
        }

        return m_Manager->Opponent()->Players()[index];
    }
};

#endif //__DIRECTFREEKICKRULE_H__
