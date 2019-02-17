/********************************************************************************
 * 文件名：LocalManagerAdapter.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "LocalManagerAdapter.h"

#include "../PlayerData/PlayerPropertyInitializer.h"

#include "../../Player/Property/PlayerAttribute.h"
#include "../../Player/Property/RawPlayer.h"

LocalManagerAdapter* LocalManagerAdapter::Instance()
{
    static LocalManagerAdapter localManager;

    return &localManager;
}

LocalManagerAdapter::~LocalManagerAdapter()
{

}

IManagerData* LocalManagerAdapter::GetManagerById(int id, int type, Side side)
{
    IManagerData* manager = new ManagerData();

    manager->SetId((side == Side_Home) ? 0 : 1);
    manager->SetName((side == Side_Home) ? "TEST-01" : "TEST-02");
    manager->SetSpellName((side == Side_Home) ? "TEST-01" : "TEST-02");
    manager->SetFormationId(RandomHelper::GetInt32(1, Macro_FormationMax));

    vector<FormationEntity> formations = FormationFacade::GetFormation(manager->FormationId());

    // Initialize Debug Passive Skills
    xml_node& section = singleton_default<ConfigurationManagerData>::instance().GetSection("PassiveSkillDebuger");
    if (section != NULL)
    {
        string skillName;

        if (side == Side_Home)
        {
            manager->SetPassiveSkills(InitPassiveSkills(Common::GetAttribute(section, "key", "0", "value")));
        }
        else
        {
            manager->SetPassiveSkills(InitPassiveSkills(Common::GetAttribute(section, "key", "1", "value")));
        }
    }

    // Initialize Debug Opener Skills
    section = singleton_default<ConfigurationManagerData>::instance().GetSection("OpenerSkillDebuger");
    if (section != NULL)
    {
        if (side == Side_Home)
        {
            vector<string>& opener = InitOpenerSkills(Common::GetAttribute(section, "key", "0", "value"));
            manager->OpenerSkills()[0] = opener[0];
            manager->OpenerSkills()[1] = opener[1];
        }
        else
        {
            vector<string>& opener = InitOpenerSkills(Common::GetAttribute(section, "key", "1", "value"));
            manager->OpenerSkills()[0] = opener[0];
            manager->OpenerSkills()[1] = opener[1];
        }
    }

    //////////////////////////////////////////////////////////////////////////
    for (short i = 0; i < Defines_Match.MAX_PLAYER_COUNT; ++i)
    {
        ++m_PlayerIdCounter;

        IPlayerAttribute* attribute = new PlayerAttribute();

        attribute->SetFamilyName(String::Format("player_%d", m_PlayerIdCounter));  // family name
        attribute->SetFirstName(attribute->FamilyName());           // first name
        attribute->SetPosition(formations[i].GetPosition());        // position
        Coordinate defaultCoordinate = formations[i].GetDefault();  // default coordinate
        if (attribute->GetPosition() != Position_Goalkeeper)
        {
            // move region
            attribute->SetMoveRegion(MoveRegionFacade::GetMoveRegion(defaultCoordinate));
        }

        IRawPlayer* player = new RawPlayer();
        player->SetId(m_PlayerIdCounter);
        player->SetBuildinAttribute(attribute);
        player->BuildinAttribute()->SetStrengthen(9);
        player->SetDefault(defaultCoordinate);
        player->SetRawProperty(PlayerPropertyInitializer::Initialize());

        double property = 150.0f;

        MapInt_Double& playerPropertyMap = player->RawProperty();
        playerPropertyMap[PlayerProperty_Speed]         = property;
        playerPropertyMap[PlayerProperty_Shooting]      = property;
        playerPropertyMap[PlayerProperty_FreeKick]      = property;
        playerPropertyMap[PlayerProperty_Balance]       = property;
        playerPropertyMap[PlayerProperty_Stamina]       = property;
        playerPropertyMap[PlayerProperty_Strength]      = property;
        playerPropertyMap[PlayerProperty_Aggression]    = property;
        playerPropertyMap[PlayerProperty_Disturb]       = property;
        playerPropertyMap[PlayerProperty_Interception]  = property;
        playerPropertyMap[PlayerProperty_Dribble]       = property;
        playerPropertyMap[PlayerProperty_Passing]       = property;
        playerPropertyMap[PlayerProperty_Mentality]     = property;
        playerPropertyMap[PlayerProperty_Reflexes]      = property;
        playerPropertyMap[PlayerProperty_Positioning]   = property;
        playerPropertyMap[PlayerProperty_Handling]      = property;
        playerPropertyMap[PlayerProperty_Acceleration]  = property;

        if (player->BuildinAttribute()->GetPosition() == Position_Fullback)
        {
            playerPropertyMap[PlayerProperty_PassProbability] = Defines_Player_Fullback.PASS_PROBABILITY;
        }

        if (player->BuildinAttribute()->GetPosition() == Position_Midfielder)
        {
            playerPropertyMap[PlayerProperty_PassProbability] = Defines_Player_Midfielder.PASS_PROBABILITY;
        }

        if (player->BuildinAttribute()->GetPosition() == Position_Forward)
        {
            playerPropertyMap[PlayerProperty_PassProbability] = Defines_Player_Forward.PASS_PROBABILITY;
        }

        manager->Players().push_back(player);
    }

    // Initialize Debug Action Skill
    section = singleton_default<ConfigurationManagerData>::instance()().GetSection("SkillDebuger");
    if (section != NULL)
    {
        vector<vector<string> > skillList(Macro_PlayerCount);
        skillList.clear();

        // 在前面设置过Id为0或者1
        if (manager->Id() == 0)
        {
            for (int i = 0; i < Macro_PlayerCount; i++)
            {
                string skillstr = Common::GetAttribute(section, "key", lexical_cast<string>(i), "value");
                skillList.push_back(Common::TransToStringVector(skillstr, ","));
            }
        }
        else
        {
            for (int i = Macro_PlayerCount; i < 2 * Macro_PlayerCount; i++)
            {
                string skillstr = Common::GetAttribute(section, "key", lexical_cast<string>(i), "value");
                skillList.push_back(Common::TransToStringVector(skillstr, ","));
            }
        }

        for (size_t i = 0; i < manager->Players().size(); i++)
        {
            AddSkill(skillList[i], manager->Players[i]);
        }
    }

    return manager;
}

void LocalManagerAdapter::AddSkill(vector<string>& skills, IRawPlayer* player)
{
    foreach (string& skill, skills)
    {
        if (skill.empty())
        {
            continue;
        }

        player->Skills().push_back(trim_copy(skill));
    }
}

vector<string> LocalManagerAdapter::InitPassiveSkills(string skillconfig)
{
    if (skillconfig.empty())
    {
        return vector<string>(0);
    }

    return Common::TransToStringVector(skillconfig, ",");
}

vector<string> LocalManagerAdapter::InitOpenerSkills(string skillconfig)
{
    if (skillconfig.empty())
    {
        return vector<string>(2);
    }

    vector<string>& skills = Common::TransToStringVector(skillconfig, ",");
    vector<string> opener(2);

    opener[0] = skills[0];

    if (skills.size() == 1)
    {
        opener[1] = String::Empty();
    }
    else
    {
        opener[1] = skills[1];
    }

    return opener;
}


