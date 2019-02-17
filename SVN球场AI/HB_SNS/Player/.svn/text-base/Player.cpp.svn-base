/********************************************************************************
 * 文件名：Player.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "Player.h"
#include "Property/PlayerAttribute.h"
#include "../MatchData/MoveRegion/MoveRegionFacade.h"
#include "../MatchData/PlayerData/PlayerPropertyInitializer.h"

#include "../AI/Rules/PlayerRule.h"

#include "../SkillEngine/SkillFacade.h"

#include "../common/Enum/PlayerProperty.h"

Player::Player(IRawPlayer* entity, int clientId, IManager* manager)
    : m_AddBuff_Method(this, &Player::AddBuff)
    , m_AddDebuff_Method(this, &Player::AddDebuff)
    , m_RemoveBuff_Method(this, &Player::RemoveBuff)
    , m_RemoveDebuff_Method(this, &Player::RemoveDebuff)
{
    InitVariables();

    m_Id                = entity->Id();                 // id
    m_Manager           = manager;                      // manager
    m_Match             = manager->GetMatch();          // match
    m_Side              = manager->GetSide();           // side
    m_ClientId          = clientId;                     // client id
    m_Attribute         = entity->BuildinAttribute();   // attribute

    // Properties
    foreach (const MapInt_Double::value_type& pair, entity->RawProperty())
    {
        m_RawProperty[pair.first]   = pair.second;  // raw properties
        m_CurrProperty[pair.first]  = pair.second;  // curr properties
    }

    // MoveRegion
    Coordinate defaultCoordinate = entity->GetDefault();
    if (m_Side == Side_Away)
    {
        // mirror the coordinate while the player is in the guest team
        m_Attribute->SetMoveRegion(m_Attribute->MoveRegion().Mirror());

        defaultCoordinate = defaultCoordinate.Mirror();
    }

    if (m_Attribute->GetPosition() == Position_Goalkeeper)
    { 
        // initialize the goal keeper's move region
        m_Attribute->SetMoveRegion(PlayerRule::GetGoalKeeperMoveRegion(m_Attribute->GetPosition(), m_Side));
    }

    // 初始化球员的状态
    m_Status = new PlayerStatus(this, defaultCoordinate);
    m_Status->SetAcceleration(PlayerRule::GetAcceleration(m_CurrProperty[PlayerProperty_Speed], m_CurrProperty[PlayerProperty_Acceleration]));
    m_Status->SetMaxSpeed(PlayerRule::GetMaxSpeed(m_CurrProperty[PlayerProperty_Speed]));
    m_Status->SetSpeed(PlayerRule::GetSpeed(this, m_Status->MaxSpeed()));
    m_Status->GetShootStatus()->SetShootRegion(PlayerRule::GetPlayerShootRegion(m_CurrProperty[PlayerProperty_Strength], m_Manager->GetSide()));

    foreach (string& skillId, entity->Skills())
    {
        IRawSkill* rawSkill         = SkillFacade::Instance()->GetActionSkill(skillId);
        ActionSkill* actionSkill    = new ActionSkill(rawSkill, m_Match->GetTotalRound());

        m_SkillReferences.push_back(actionSkill);

        foreach (IState* state, actionSkill->TriggerStates())
        {

            if (m_Skills.find(state) == m_Skills.end())
            {
                m_Skills[state] = vector<IActionSkill*>(2);

                m_Skills[state]->clear();
            }

            m_Skills[state].push_back(actionSkill);
        }
    }

    // events binding
    SetOnAddBuff(m_AddBuff_Method);
    SetOnAddDebuff(m_AddDebuff_Method);
    SetOnRemoveBuff(m_RemoveBuff_Method);
    SetOnRemoveDebuff(m_RemoveDebuff_Method);

    Reset();
}

Player::Player(TransferPlayerEntity* entity, IManager* manager, int clientId, Coordinate currCoordinate)
    : m_AddBuff_Method(this, &Player::AddBuff)
    , m_AddDebuff_Method(this, &Player::AddDebuff)
    , m_RemoveBuff_Method(this, &Player::RemoveBuff)
    , m_RemoveDebuff_Method(this, &Player::RemoveDebuff)
{
    InitVariables();

    m_Id            = entity->Id();             // id
    m_Manager       = manager;                  // manager
    m_Match         = manager->GetMatch();      // match

    if (m_Match == NULL)
    {
        //LogHelper.Insert("Null Match", LogType.Error);
    }

    m_Side          = m_Manager->GetSide();     // side
    m_ClientId      = clientId;                 // client id
    
    m_Attribute     = new PlayerAttribute();

    m_Attribute->SetPID(entity->Pid());
    m_Attribute->SetCardLevel(entity->CardLevel());
    m_Attribute->SetStrengthen(entity->Strengthen());
    m_Attribute->SetPosition(entity->GetPosition());
    m_Attribute->SetHeadStyle(entity->HeadStyle());
    m_Attribute->SetBodyStyle(entity->BodyStyle());
    m_Attribute->SetFirstName(entity->FirstName());
    m_Attribute->SetFamilyName(entity->FamilyName());
    
    if (entity->FamilyName().empty())
    {
        //LogHelper.Insert("Player has a Null Family Name. Manager Name:" + manager.Name + ", player index:" + clientId, LogType.Error);
        m_Attribute->SetFamilyName("");
    }
    m_Attribute->SetMoveRegion(MoveRegionFacade::GetMoveRegion(currCoordinate));

    m_RawProperty   = PlayerPropertyInitializer::Initialize();
    m_CurrProperty  = PlayerPropertyInitializer::Initialize();

    m_RawProperty[PlayerProperty_Speed]             = entity->Speed();
    m_RawProperty[PlayerProperty_Shooting]          = entity->Shooting();
    m_RawProperty[PlayerProperty_FreeKick]          = entity->FreeKick();
    m_RawProperty[PlayerProperty_Balance]           = entity->Balance();
    m_RawProperty[PlayerProperty_Stamina]           = entity->Stamina();
    m_RawProperty[PlayerProperty_Strength]          = entity->Strength();
    m_RawProperty[PlayerProperty_Aggression]        = entity->Aggression();
    m_RawProperty[PlayerProperty_Disturb]           = entity->Disturb();
    m_RawProperty[PlayerProperty_Interception]      = entity->Interception();
    m_RawProperty[PlayerProperty_Dribble]           = entity->Dribble();
    m_RawProperty[PlayerProperty_Passing]           = entity->Passing();
    m_RawProperty[PlayerProperty_Mentality]         = entity->Mentality();
    m_RawProperty[PlayerProperty_Reflexes]          = entity->Reflexes();
    m_RawProperty[PlayerProperty_Positioning]       = entity->Positioning();
    m_RawProperty[PlayerProperty_Handling]          = entity->Handling();
    m_RawProperty[PlayerProperty_Acceleration]      = entity->Acceleration();

    m_CurrProperty[PlayerProperty_Speed]            = entity->Speed();
    m_CurrProperty[PlayerProperty_Shooting]         = entity->Shooting();
    m_CurrProperty[PlayerProperty_FreeKick]         = entity->FreeKick();
    m_CurrProperty[PlayerProperty_Balance]          = entity->Balance();
    m_CurrProperty[PlayerProperty_Stamina]          = entity->Stamina();
    m_CurrProperty[PlayerProperty_Strength]         = entity->Strength();
    m_CurrProperty[PlayerProperty_Aggression]       = entity->Aggression();
    m_CurrProperty[PlayerProperty_Disturb]          = entity->Disturb();
    m_CurrProperty[PlayerProperty_Interception]     = entity->Interception();
    m_CurrProperty[PlayerProperty_Dribble]          = entity->Dribble();
    m_CurrProperty[PlayerProperty_Passing]          = entity->Passing();
    m_CurrProperty[PlayerProperty_Mentality]        = entity->Mentality();
    m_CurrProperty[PlayerProperty_Reflexes]         = entity->Reflexes();
    m_CurrProperty[PlayerProperty_Positioning]      = entity->Positioning();
    m_CurrProperty[PlayerProperty_Handling]         = entity->Handling();
    m_CurrProperty[PlayerProperty_Acceleration]     = entity->Acceleration();

    // MoveRegion
    Coordinate defaultCoordinate = currCoordinate; // default coordinate
    if (m_Side == Side_Away) // mirror the coordinate while the player is in the guest team
    { 
        m_Attribute->SetMoveRegion(m_Attribute->MoveRegion().Mirror());
        defaultCoordinate = defaultCoordinate.Mirror();
    }
    if (m_Attribute->GetPosition() == Position_Goalkeeper) // initialize the goal keeper's move region
    { 
        m_Attribute->SetMoveRegion(PlayerRule::GetGoalKeeperMoveRegion(m_Attribute->GetPosition(), m_Side));
    }

    // Initializes PlayerStatus
    m_Status = new PlayerStatus(this, defaultCoordinate);
    m_Status->SetAcceleration(PlayerRule::GetAcceleration(m_CurrProperty[PlayerProperty_Speed], m_CurrProperty[PlayerProperty_Acceleration]));
    m_Status->SetMaxSpeed(PlayerRule::GetMaxSpeed(m_CurrProperty[PlayerProperty_Speed]));
    m_Status->SetSpeed(PlayerRule::GetSpeed(this, m_Status->MaxSpeed()));
    m_Status->GetShootStatus()->SetShootRegion(PlayerRule::GetPlayerShootRegion(m_CurrProperty[PlayerProperty_Strength], m_Manager->GetSide()));

    // Initializes The Pass Probability 
    if (m_Attribute->GetPosition() == Position_Fullback)
    {
        m_RawProperty[PlayerProperty_PassProbability]       = Defines_Player_Fullback.PASS_PROBABILITY;
        m_CurrProperty[PlayerProperty_PassProbability]      = Defines_Player_Fullback.PASS_PROBABILITY;
    }

    if (m_Attribute->GetPosition() == Position_Midfielder)
    {
        m_RawProperty[PlayerProperty_PassProbability]       = Defines_Player_Midfielder.PASS_PROBABILITY;
        m_CurrProperty[PlayerProperty_PassProbability]      = Defines_Player_Midfielder.PASS_PROBABILITY;
    }

    if (m_Attribute->GetPosition() == Position_Forward)
    {
        m_RawProperty[PlayerProperty_PassProbability]       = Defines_Player_Forward.PASS_PROBABILITY;
        m_CurrProperty[PlayerProperty_PassProbability]      = Defines_Player_Forward.PASS_PROBABILITY;
    }

    // Skills
    if (entity->Skills().size() != 0)
    {
        foreach (string& skill, entity->Skills())
        {
            if (skill.empty())
            {
                LogHelper::Insert("Transfers the null skill id from the main system, manager name:" + m_Manager->Name() + ", player name:" + m_Attribute->FamilyName(), LogType_Error);
                continue;
            }

            IRawSkill* rawSkill         = SkillFacade::Instance()->GetActionSkill(skill);
            ActionSkill* actionSkill    = new ActionSkill(rawSkill, m_Match->GetTotalRound());
            
            if (actionSkill == NULL)
            {
                LogHelper::Insert("Initializes a null action skill. Skill Id:" + skill);
            }

            m_SkillReferences.push_back(actionSkill);

#if Debug_Mode
            LogHelper::Insert("初始化技能球员技能:" + skill, LogType_Debug);
#endif

            if (actionSkill->TriggerStates().size() == 0)
            {
                LogHelper::Insert("Null TriggerStates", LogType_Error);
            }

            foreach (IState* state, actionSkill->TriggerStates())
            {
                if (m_Skills.find(state) == m_Skills.end())
                {
                    m_Skills[state]     =  vector<IActionSkill*>(2);

                    m_Skills[state]->clear();
                }

                m_Skills[state].push_back(actionSkill);
            }
        }
    }

    // events binding
    SetOnAddBuff(m_AddBuff_Method);
    SetOnAddDebuff(m_AddDebuff_Method);
    SetOnRemoveBuff(m_RemoveBuff_Method);
    SetOnRemoveDebuff(m_RemoveDebuff_Method);

    Reset();
}

Player::~Player()
{
    // 删除技能
    foreach (IActionSkill* skill, m_SkillReferences)
    {
        DeletePtrSafe(skill);
    }
    m_SkillReferences.clear();

    // 删除记录数据
    foreach (Process* p, m_Processes) 
    {
        DeletePtrSafe(p);
    }
    m_Processes.clear();
}

void Player::InitVariables()
{
    m_Id                = 0;
    m_ClientId          = 0;

    m_Attribute         = NULL;

    m_RawProperty.clear();
    m_CurrProperty.clear();
    m_Skills.clear();
    m_SkillResults.clear();

    m_Manager           = NULL;
    m_Match             = NULL;
    m_Status            = NULL;

    m_PassTargets.reserve(Macro_PlayerPassTargets);
    m_CarePlayers.reserve(Macro_CarePlayers);
    m_InSightPlayers.reserve(Macro_InSightPlayers);
    m_Processes.reserve(Macro_PlayerProcess);
    m_SkillReferences.reserve(4);

    m_PassTargets.clear();
    m_CarePlayers.clear();
    m_InSightPlayers.clear();
    m_Processes.clear();
}
