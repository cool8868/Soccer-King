/********************************************************************************
 * 文件名：MatchEntity.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "MatchEntity.h"
#include "../common/Defines/Defines.h"
#include "../common/RandomHelper/RandomHelper.h"
#include "../common/Enum/OpenballSide.h"
#include "../common/Enum/PlayerProperty.h"
#include "../common/Utility.h"

#include "../Manager/Manager.h"
#include "../Pitch/Pitch.h"
#include "../FootBall/Football.h"

#include "../AI/Rules/FreeKickRules/OpenballRule.h"
#include "../AI/Rules/FreeKickRules/FreeKickRuleFactory.h"
#include "../AI/StateInitializer.h"

MatchEntity::MatchEntity()
    : m_Processes(Macro_MatchEntityProcess)
{
    InitVariables();

    m_Pitch         = new Pitch();
    m_Football      = new Football();

    m_Status        = new MatchStatus();

    //初始化状态
    StateInitializer::Initialize();
    SkillFacade::Instance()->Initialize();

    m_Processes.clear();
}

MatchEntity::~MatchEntity()
{
    DeletePtrSafe(m_Status);
    DeletePtrSafe(m_Football);
    DeletePtrSafe(m_Pitch);

    //删除Manager对象
    Release();
}

void MatchEntity::Release()
{
    DeletePtrSafe(m_HomeManager);
    DeletePtrSafe(m_AwayManager);

    foreach (Process* p, m_Processes) 
    {
        DeletePtrSafe(p);
    }
    m_Processes.clear();
}

void MatchEntity::InitMatch(int homeId, int awayId, int matchType, double time)
{
    InitBasicInfo(matchType, time);

    m_HomeManager = new Manager(homeId, Side_Home, this);
    m_AwayManager = new Manager(awayId, Side_Away, this);

    m_Managers.push_back(m_HomeManager);
    m_Managers.push_back(m_AwayManager);
}

void MatchEntity::InitMatch(TransferMatchEntity* entity)
{
    m_MapId = entity->MapId();
    InitBasicInfo(entity->MatchType(), entity->Time());

    m_HomeManager = new Manager(Side_Home, this, entity->HomeManager());
    m_AwayManager = new Manager(Side_Away, this, entity->AwayManager());

    m_Managers.push_back(m_HomeManager);
    m_Managers.push_back(m_AwayManager);

    InitPlayerPropertyBalance();

    m_HomeManager->Initialize();
    m_AwayManager->Initialize();
}

IManager* MatchEntity::GetManagerBySide(Side side)
{
    return (side == Side_Home) ? m_HomeManager : m_AwayManager;
}

IManager* MatchEntity::GetManagerBySide(OpenballSide side)
{
    if (side == OpenballSide_Home) return m_HomeManager;
    if (side == OpenballSide_Away) return m_AwayManager;

    //throw new NotSupportedException("Invoke GetManagerBySide but the side is Side.None.");

    return (IManager*)NULL;
}

IPlayer* MatchEntity::GetPlayerByCoordiante(Coordinate c)
{
    foreach (IManager* manager, m_Managers)
    {
        foreach (IPlayer* player, manager->Players())
        {
            if (MATH::FloatEqual(player->Current().X, c.X) && MATH::FloatEqual(player->Current().Y, c.Y))
            {
                return player;
            }
        }
    }

    throw NotSupportedException("通过坐标没有找到球员");
    
    // +++ tony:
    return (IPlayer*)NULL;
}

// 开球
void MatchEntity::Openball(IManager* openballManager)
{
    OpenballRule::Start(openballManager);
}

// 进球
void MatchEntity::Goal(Side side)
{
    LogHelper::Insert(str(format("[进球] Side:%d 进球了!") % side), LogType_Debug);

    if (side == Side_Home)
    {
        ++m_HomeScore;
    }
    else
    {
        ++m_AwayScore;
    }

    m_Status->SetIsGoal(true);
    m_Status->SetBreak(false);
    m_Status->SetInterruption(false);
    m_Status->SetNeedSave(true);            
    
    Save();

    m_Status->IncRound();
    RoundInit();
    m_Status->SetInterruption(false);
    m_Status->SetBreak(true);
    m_Status->SetNeedSave(true);
    
    Openball((side == Side_Home) ? m_AwayManager : m_HomeManager);
}

/// 射偏或者射飞
void MatchEntity::MissShoot()
{            
    Save();

    m_Status->IncRound();
    RoundInit();
    m_Status->SetInterruption(false);
    m_Status->SetBreak(true);
    m_Status->SetNeedSave(true);
    
    foreach (IManager* manager, m_Managers)
    {
        foreach (IPlayer* p, manager->Players())
        {
            p->Reset();
            p->Status()->SetState(IdleState::Instance());
        }
    }

    Save();

    for (int i = 0; i < 3; i++)
    {
        m_Status->IncRound();
        RoundInit();
        foreach (IManager* manager, m_Managers)
        {
            foreach (IPlayer* p, manager->Players())
            {
                p->Reset();
                p->Status()->SetState(IdleState::Instance());
            }
        }

        Save();
    }
}

/// 保存当前回合，如果标记了该回合不需要保存比赛数据则不保存比赛数据
void MatchEntity::Save()
{
    if (m_Status->NeedSave())
    {
        if (m_Processes.size() == 0)
        {
            InternalSave();
        }
        else
        {
            if (m_Processes[m_Processes.size() - 1]->Round() != m_Status->Round())
            {
                InternalSave();
            }
        }
    }

    foreach (IManager* manager, m_Managers)
    {
        foreach (IPlayer* player, manager->Players())
        {
            if (player->Status()->Enable() == false)
            {
                continue;
            }

            player->Save();
        }
    }

    m_Football->Save(m_Status->Round());
}

void MatchEntity::RoundInit()
{
    m_Status->SetIsGoal(false);
    m_Status->SetInterruption(false);
    m_Status->SetFoul(false);
    m_Status->SetBreak(false);
    m_Status->SetNeedSave(false);
    m_Status->SetHomeOpener(String::Empty());
    m_Status->SetAwayOpener(String::Empty());

    int time = CalculateGameTime();

    if (m_Status->Round() == 0) // first half start triggered the open ball.
    {
        foreach (IManager* m, m_Managers)
        {
            m->TriggerOpenerSkill(true);
        }

        IManager* openballManager = (m_Status->HalfOpenballSide() == OpenballSide_Home)? m_HomeManager : m_AwayManager;
        Openball(openballManager);
    }

    if (time != m_Status->Time()) // 一个新的分种开始
    {
        m_Status->SetTime(time);

        // close the first half 下半场开始
        if (time == 46)
        {
            m_Status->SetNeedSave(true);
            m_Status->SetInterruption(true);
            m_Status->SetIsFirstHalf(false);

            // 下半场开始，清除所有的异常状态
            foreach (IManager* m, m_Managers)
            {
                m->ClearDisable();
            }

            IManager* openballManager = (m_Status->HalfOpenballSide() == OpenballSide_Home) ? m_AwayManager : m_HomeManager;
            Openball(openballManager);

            m_HomeManager->SecondHalfStart();
            m_AwayManager->SecondHalfStart();
        }

        m_HomeManager->MinuteInit();
        m_AwayManager->MinuteInit();                
    }

    if (m_Status->IsNoBallHandler())
    {
        RefreshBallHandler();
    }

    m_HomeManager->RoundInit();
    m_AwayManager->RoundInit();
    
    m_Football->RoundInit();
}

/// 表示了犯规的经理
void MatchEntity::Foul(IManager* manager)
{
    // 保存当前的状态
    m_Status->SetNeedSave(true);
    m_Status->SetInterruption(false);
    m_Status->SetFoul(true);
    Save();

    m_Status->SetNeedSave(false);

    // 执行定位球逻辑
    FreeKickRuleFactory::Instance()->Start(manager->Opponent(), m_Football->Current());
}

/// 出界
/// 表示了让球出界的经理
void MatchEntity::OutBorder(IManager* manager)
{
    Foul(manager); // 出界执行了和犯规同样的定位球逻辑
}

void MatchEntity::SaveOpenerSkill(Side side, string skillId)
{
    if (side == Side_Home)
    {
        m_Status->SetHomeOpener(skillId);
    }
    else
    {
        m_Status->SetAwayOpener(skillId);
    }

    m_Status->SetNeedSave(true);
}

IManager* MatchEntity::OpenballManager()
{
    return GetManagerBySide(m_Status->GetOpenballSide());
}

void MatchEntity::InitBasicInfo(int matchType, double time)
{
    m_MatchType             = matchType;
    m_TotalRound            = static_cast<int>(time * 60 / Defines_Match.ROUND_TIME);
    m_HalfMatchRoundCount   = m_TotalRound / 2;

    m_Status->SetRound(-1);
    m_Status->SetInterruption(false);
    m_Status->SetIsFirstHalf(true);            
    m_Status->SetHalfOpenballSide((RandomHelper::GetBoolean()) ? OpenballSide_Home : OpenballSide_Away);
}

void MatchEntity::InitPlayerPropertyBalance()
{
    vector<double> array(Macro_SumPropertyNum);
    array.clear();

    foreach (IManager* m, m_Managers)
    {
        foreach (IPlayer* p, m->Players())
        {
            foreach (int& property, PlayerPropertyInitializer::PlayerPropertyIds())
            {
                array.push_back(p->RawProperty()[property]);
            }
        }
    }

    double max = Common::Utility<double>::GetMax(array);

    if (max > 200)
    {
        foreach (IManager* m, m_Managers)
        {
            foreach (IPlayer* p, m->Players())
            {
                foreach (int& property, PlayerPropertyInitializer::PlayerPropertyIds())
                {
                    double raw = p->RawProperty()[property];

                    p->RawProperty()[i] = raw / max * 200;
                    p->CurrProperty()[i] = p->RawProperty()[i];
                }
            }
        }
    }
}

void MatchEntity::InternalSave()
{
    MatchProcess* processes = new MatchProcess();

    processes->SetIsGoal(m_Status->IsGoal());
    processes->SetRound(m_Status->Round());
    processes->SetHomeScore(m_HomeScore);
    processes->SetAwayScore(m_AwayScore);
    processes->SetScore(lexical_cast<string>(m_HomeScore) + ',' + lexical_cast<string>(m_AwayScore));
    processes->SetFoul (m_Status->Foul());
    processes->SetInterruption(m_Status->GetInterruption());
    processes->SetBreak(m_Status->Break());
    processes->SetHomeOpener(m_Status->HomeOpener());
    processes->SetAwayOpener(m_Status->AwayOpener());

    m_Processes.push_back(processes);
}

/// 刷新持球人
void MatchEntity::RefreshBallHandler()
{
    double              distances[Macro_BallHanderRefreshCounter] = {};
    vector<IPlayer*>    parray(Macro_BallHanderRefreshCounter);
    parray.clear();

    foreach (IManager* m, m_Managers)
    {
        foreach (IPlayer* p, m->Players())
        {
            if (p->Status()->Enable() == false)
            {
                continue;
            }

            if (p->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None)
            {
                continue;
            }

            distances[parray.size()] = p->Current().Distance(m_Football->Current());
            parray.push_back(p);
        }
    }

    if (parray.size() == 0)
    {
        return;
    }

    IPlayer* ballhandler = parray[Common::GetDoubleMinIndexQuick(distances, parray.size())];

    ballhandler->Status()->SetHasball(true);

    if (ballhandler->Status()->Holdball())
    {
        m_Status->SetIsNoBallHandler(false);
    }
}

int MatchEntity::CalculateGameTime() 
{
    return static_cast<int>((double)m_Status->Round() / m_TotalRound * 90); 
}

void MatchEntity::InitVariables()
{
    m_MatchType             = 0;
    m_MapId                 = 0;

    m_Pitch                 = NULL;
    m_Football              = NULL;

    m_OpenballManager       = NULL;
    m_HomeManager           = NULL;
    m_AwayManager           = NULL;

    m_Status                = NULL;

    m_HomeScore             = 0;
    m_AwayScore             = 0;

    m_TotalRound            = 0;
    m_HalfMatchRoundCount   = 0;

    m_Managers.clear();
    m_Processes.clear();
}
