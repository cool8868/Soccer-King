/********************************************************************************
 * 文件名：Manager.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "Manager.h"
#include "Status/ManagerStatus.h"

#include "../Player/Player.h"

#include "../MatchData/ManagerData/ManagerData.h"
#include "../MatchData/LocalManagerAdapter/LocalManagerAdapter.h"

#include "../AI/Rules/FreeKickRules/GkOpenballRule.h"
#include "../common/String/String.h"

Manager::Manager(unsigned int id, Side side, IMatch* match)
{
    InitVariables();

    m_Id                = id;
    m_Side              = side;
    m_Match             = match;

    m_Entity            = LocalManagerAdapter::Instance()->GetManagerById(id, match->MatchType(), m_Side);

    m_SpellName         = m_Entity->SpellName();
    m_Name              = m_Entity->Name();
    m_FormationId       = m_Entity->FormationId();
    m_Logo              = String::Empty();

    int clientId        = (m_Side == Side_Home) ?  0 : Defines_Match.MAX_PLAYER_COUNT;

    m_PassiveSkills.reserv(m_Entity->PassiveSkills().size());
    m_PassiveSkills.clear();

    foreach (string& skill, m_Entity->PassiveSkills())
    {
        m_PassiveSkills.push_back(skill);
    }

    if (!m_Entity->OpenerSkills()[0].empty())
    {
        IRawSkill* raw = SkillFacade::Instance()->GetOpenerSkill(m_Entity->OpenerSkills()[0]);
        if (raw != NULL)
        {
            m_OpenerSkills[0] = new OpenerSkill(raw);
        }
    }

    if (!m_Entity->OpenerSkills()[1].empty())
    {
        IRawSkill* raw = SkillFacade::Instance()->GetOpenerSkill(m_Entity->OpenerSkills()[1]);
        if (raw != NULL)
        {
            m_OpenerSkills[1] = new OpenerSkill(raw);
        }
    }

    for (size_t i = 0; i < m_Entity->Players().size(); i++)
    {
        IPlayer* player = new Player(m_Entity->Players()[i], i + clientId, this);

        m_Players.push_back(player);
    }

    m_Status = new ManagerStatus();
}

Manager::Manager(Side side, IMatch* match, TransferManagerEntity* entity)
{
    InitVariables();

    if (entity == NULL)
    {
        throw ApplicationException("Initializes a new manager by null TransferManagerEntity.");
    }

    if (entity->Players().size() == 0)
    {
        throw ApplicationException("The manager has no team members. Manager name:" + entity.Name);
    }

    if (entity->Players().size() != Macro_PlayerCount)
    {
        throw ApplicationException("The manager's team members count is not " + lexical_cast<int>(Macro_PlayerCount) + 
            " Manager name:" + m_Entity->Name() + ", Team member count:" + lexical_cast<int>(m_Entity->Players().size()));
    }

#ifdef Debug_Mode
    LogHelper::Insert("开始加载经理数据：" + m_Entity->Name(), LogType_Debug);
#endif

    m_Id                                = entity->Id();
    m_Side                              = side;
    m_SpellName                         = entity->SpellName();
    m_Name                              = entity->Name();
    m_Match                             = match;
    m_Logo                              = entity->Logo();
    m_FormationId                       = entity->FormationId();

    vector<FormationEntity> formation   = FormationFacade::GetFormation(entity->FormationId());
    int clientId                        = (m_Side == Side_Home) ? 0 : Defines_Match.MAX_PLAYER_COUNT;

    // Opener Skills
    if (entity->OpenerSkills().size() > 0)
    {
        if (!entity->OpenerSkills()[0].empty())
        {
            IRawSkill* raw = SkillFacade::Instance()->GetOpenerSkill(entity->OpenerSkills()[0]);
            if (raw != NULL)
            {
                m_OpenerSkills[0] = new OpenerSkill(raw);
            }
#ifdef Debug_Mode
            LogHelper::Insert("上半场Opener技能：" + entity->OpenerSkills()[0], LogType_Debug);
#endif
        }

        if (!entity->OpenerSkills()[1].empty())
        {
            IRawSkill* raw = SkillFacade::Instance()->GetOpenerSkill(entity->OpenerSkills()[1]);
            if (raw != NULL)
            {
                m_OpenerSkills[1] = new OpenerSkill(raw);
            }

#ifdef Debug_Mode
            LogHelper::Insert("下半场Opener技能：" + entity->OpenerSkills()[1], LogType_Debug);
#endif
        }
    }
#ifdef Debug_Mode
    else
    {
        LogHelper::Insert("没有装备Opener技能.", LogType_Debug);
    }
#endif

    //Passive Skills
    if (entity->PassiveSkills().size() > 0)
    {
        m_PassiveSkills.reserve(entity->PassiveSkills().size());
        m_PassiveSkills.clear();

        foreach (string& skill, entity->PassiveSkills())
        {
            m_PassiveSkills.push_back(skill);
#ifdef Debug_Mode
            LogHelper::Insert("加载被动经理技能：" + skill, LogType_Debug);
#endif
        }
    }
    else
    {
        m_PassiveSkills.reserve(0);
#ifdef Debug_Mode
        LogHelper::Insert("被动经理技能数量：0", LogType_Debug);
#endif
    }

    // Will Skills
    if (entity->Wills().size() > 0)
    {
        m_Wills.reserve(entity->Wills().size());
        m_Wills.clear();

        for (size_t i = 0; i < entity->Wills()->size(); i++)
        {
            IWillRawSkill* raw = SkillFacade::Instance()->GetWillSkill(entity->Wills()[i]);
            
            Will* will = new Will();
            will->SetSkillId(raw->Id());
            will->SetSkillName(raw->Name());
            will->SetRawSkill(raw);

            m_Wills.push_back(raw);

#ifdef Debug_Mode
            LogHelper::Insert("加载意志：" + raw->Id() + ", " + raw->Name(), LogType_Debug);
#endif
        }
    }
    else
    {
#ifdef Debug_Mode
        LogHelper::Insert("没有激活意志", LogType_Debug);
#endif
        m_Wills.reserv(0);
    }

    if (entity->Players().size() != Defines_Match.MAX_PLAYER_COUNT)
    {
        throw Exception("The manager's player count is not the standard player count. Client Player Num:" + lexical_cast<int>(entity->Players().size()));
    }

    for (size_t i = 0; i < entity->Players().size(); i++)
    {
        FormationEntity* formationEntity = &formation[i];
        entity->Players()[i]->SetPosition(formationEntity->GetPosition());
        IPlayer* player = new Player(entity->Players()[i], this, i + clientId, formationEntity->GetDefault());

        m_Players.push_back(player);
    }

    m_Status = new ManagerStatus();
}

Manager::~Manager()
{
    // 删除技能相关
    foreach (IOpenerSkill* skill, m_OpenerSkills)
    {
        DeletePtrSafe(skill);
    }
    m_OpenerSkills.clear();

    foreach (IWill* will, m_Wills)
    {
        DeletePtrSafe(will);
    }
    m_Wills.clear();

    // 删除球员
    foreach (IPlayer* player, m_Players)
    {
        DeletePtrSafe(player);
    }

    m_Players.clear();

    //删除数据源
    DeletePtrSafe(m_Entity);
}

void Manager::Initialize()
{
    try
    {
        //// 被动技能由于平衡性问题，关闭入口. 需要在平衡性调整后打开
        //foreach (string& skill, m_PassiveSkills)
        //{
        //    SkillFacade::Instance()->PassiveSkillEffect(m_Players[0], skill);
        //}
    }
    catch (MyException& ex)
    {
        LogHelper::Insert(&ex, "Manager.Initialize causes exceptions.");
    }
}

IManager* Manager::Opponent()
{
    if (m_Side == Side_Home)
    {
        return m_Match->AwayManager();
    }
    else
    {
        return m_Match->HomeManager();
    }
}

void Manager::Goal()
{
    m_Match->Goal(m_Side);            
}

void Manager::RoundInit()
{
    if (m_Wills.size() > 0)
    {
        foreach (string& will, m_Wills)
        {
            if (SkillFacade::Instance()->IsWillTriggered(m_Players[0], PointerCastSafe(IWillRawSkill, will->GetRawSkill())))
            {
                SkillFacade::Instance()->WillEffect(m_Players[0], PointerCastSafe(IWillRawSkill, will->GetRawSkill()));
            }
        }
    }            

    RefreshHeadMostPlayer();
    RefreshDefenceStatus();

    foreach (IPlayer* player, m_Players)
    {
        player->RoundInit();
    }
}

void Manager::MinuteInit()
{
    foreach (IPlayer* player, m_Players)
    {
        player->MinuteInit();
    }
}

void Manager::SecondHalfStart()
{
    m_Status->GetDecreaseFoulLevelStatus()->SetRate(m_Status->GetDecreaseFoulLevelStatus()->RawRate());
    m_Status->GetDecreaseFoulLevelStatus()->SetProbability(m_Status->GetDecreaseFoulLevelStatus()->RawProbability());
    m_Status->GetDecreaseStaminaRateStatus()->SetRate(m_Status->GetDecreaseStaminaRateStatus()->RawRate());
    m_Status->GetImmunityDebuffSkillStatus()->SetProbability(m_Status->GetImmunityDebuffSkillStatus()->RawProbability());
    m_Status->GetDecreaseDebuffRateStatus()->SetRate(m_Status->GetDecreaseDebuffRateStatus()->RawRate());
    m_Status->GetNoFoulLevelStatus()->SetProbability(m_Status->GetNoFoulLevelStatus()->RawProbability());
    m_Status->GetNoInjuredStatus()->SetProbability(m_Status->GetNoInjuredStatus()->RawProbability());
    m_Status->GetTransferFoulLevelStatus()->SetProbability(m_Status->GetTransferFoulLevelStatus()->RawProbability());

    foreach (MapInt_Double::value_type& property, m_Status->GetImmunityDebuffsStatus()->RawDebuffProperty())
    {
        m_Status->GetImmunityDebuffsStatus()->DebuffProperty()[property.first] = m_Status->GetImmunityDebuffsStatus()->RawDebuffProperty[property.first];
    }

    foreach (IPlayer* player, m_Players)
    {
        player->SecondHalfStart();
    }

    TriggerOpenerSkill(false);
}

void Manager::Foul()
{
    m_Match->Foul(this);
}

/// GK opens the ball.
void Manager::GkOpenball()
{
    GkOpenballRule::Start(this);
}

vector<IPlayer*> Manager::GetPlayersByPosition(Position position)
{
    return InternalGetPlayerByPosition(position);
}

vector<IPlayer*> Manager::InternalGetPlayerByPosition(Position position)
{
    vector<IPlayer*> players(Defines_Match.MAX_PLAYER_COUNT / 2);
    players.clear();

    foreach (IPlayer* p, m_Players)
    {
        if (p->BuildinAttribute()->GetPosition() == position)
        {
            players.push_back(p);
        }
    }

    return players;
}

/// 刷新本方的最前的一个球员
void Manager::RefreshHeadMostPlayer()
{
    double array[MyDefine_Player_Count] = {};
    for (size_t i = 0; i < m_Players.size(); i++)
    {
        array[i] = m_Players[i]->Current().X;
    }

    if (m_Side == Side_Home) // 最大
    {
        m_Status->SetHeadMostPlayer(m_Players[Common::GetDoubleMaxIndexQuick(array, m_Players.size())]);
    }
    else // 最小
    {
        m_Status->SetHeadMostPlayer(m_Players[Common::GetDoubleMinIndexQuick(array, m_Players.size())]);
    }       
}

/// 刷新每个球员的防守目标
void Manager::RefreshDefenceStatus()
{
    // clear all player's defence target
    foreach (IPlayer* player, m_Players)
    {
        player->Status()->GetDefenceStatus()->SetDefenceTarget(NULL);
    }

    if (m_Side == m_Match->Status()->BallHandler()->GetSide())
    {
        return;
    }

    if (m_Match->Status()->BallHandler()->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
    {
        return;
    }

    // find the ball handler's defender 找出持球人的防守人
    int count = (RandomHelper::GetPercentage() < m_Status->HelpDefenseRate()) ? 2 : 1;
    for (int i = 0; i < count; i++)
    {
        IPlayer* defender = GetClosestDefender(m_Match->Status()->BallHandler(), m_Players);
        if (defender != NULL)
        {
            defender->Status()->GetDefenceStatus()->SetDefenceTarget(m_Match->Status()->BallHandler());
        }
    }
}

/// 找出一个球员的最近的防守人
IPlayer* Manager::GetClosestDefender(IPlayer* target, vector<IPlayer*>& players)
{                        
    vector<IPlayer*> targets(players.size());
    double array[MyDefine_MAX_PLAYER_COUNT] = {};
    targets.clear();

    foreach (IPlayer* player, players)
    {
        if (!player->Status()->Enable()) // 球员无效
        {
            continue;
        }

        // 门将不参与防守
        if (player->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
        {
            continue;
        }

        // 已经分配了防守对象
        if (player->Status()->GetDefenceStatus()->DefenceTarget() != NULL)
        {
            continue;
        }

        //if (!player.BuildinAttribute.MoveRegion.IsCoordinateInRegion(target.Current))
        //{
        //    continue;
        //}

        array[targets.size()] = player->Current().SimpleDistance(target->Current());
        targets.push_back(player);
    }

    if (targets.size() == 0)
    {
        return NULL;
    }

    return targets[Common::GetDoubleMinIndexQuick(array, targets.size())];
}

vector<IPlayer*> Manager::GetNoneDefenceTargetPlayer(vector<IPlayer*>& players)
{
    vector<IPlayer*> list(Macro_NonDefenceTargetPlayer);
    list.clear();
    foreach (IPlayer* player, players)
    {
        if (!player->Status()->Enable())
        {
            continue;
        }

        if (player->BuildinAttribute()->GetPosition() == Position_Goalkeeper)
        {
            continue;
        }

        if (player->Status()->GetDefenceStatus()->DefenceTarget() == NULL)
        {
            list.push_back(player);
        }
    }

    return list;
}

void Manager::TriggerOpenerSkill(bool isFirstHalf)
{
    IOpenerSkill* opener = (isFirstHalf) ? m_OpenerSkills[0] : m_OpenerSkills[1];

    if (opener == NULL)
    {
        return;
    }

    SkillFacade::Instance()->OpenerSkillEffect(m_Players[0], opener);
    m_Match->SaveOpenerSkill(m_Side, opener->SkillId());
}

void Manager::ClearDisable()
{
    foreach (IPlayer* player, m_Players)
    {
        if (player->Status()->Enable() == false) continue;

        vector<AbsBuff*>& debuffBar = PointerCastSafe(Creature, player)->DebuffBar();
        for (size_t i = 0; i < debuffBar.size(); i++)
        {
            AbsBuff* debuff = debuffBar[i];
            if (debuff->Type() == BuffType_Disable)
            {
                vector<AbsBuff*>::it = find(debuffBar.begin(), debuffBar.end(), debuff);
                if (it != debuffBar.end())
                {
                    debuffBar.erase(it);
                }

                i--;
            }
        }

        player->Status()->GetModelStatus()->SetMid((int)ModelType_Normal);
        player->Status()->GetDebuffStatus()->SetDisableDebuff(NULL);
        player->Status()->GetDebuffStatus()->SetDebuffType(DebuffType_None);
    }
}

void Manager::InitVariables()
{
    m_Id            = 0;
    m_Name          = String::Empty();
    m_SpellName     = String::Empty();
    m_Logo          = String::Empty();

    m_FormationId   = 0;

    m_Players.reserve(Defines_Match.MAX_PLAYER_COUNT);
    m_Players.clear();

    m_OpenerSkills.reserve(2);

    m_Match         = NULL;    
    m_Opponent      = NULL;
    m_Status        = NULL;
    m_Entity        = NULL;
}
