/********************************************************************************
 * 文件名：SkillBuilder.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "SkillBuilder.h"
#include "Builders/TriggerConditionsBuilder.h"
#include "Builders/PropertyChangesBuilder.h"
#include "Builders/DisplacementsBuilder.h"
#include "Builders/FoulRelatedBuilder.h"
#include "Builders/BallRelatedBuilder.h"
#include "Builders/DebuffsBuilder.h"
#include "Builders/SpecialEffectsBuilder.h"
#include "Builders/ModelsBuilder.h"
#include "Builders/ManagerChangesBuilder.h"
#include "Builders/SkillUpgradesBuilder.h"
#include "Builders/ImmunityBuilder.h"
#include "Builders/TriggersBuilder.h"

#include "../Cache/ConfigCache/ConfigurationManager.h"

#include "../../Exception/ConfigurationErrorsException.h"
#include "../../Exception/SkillPartBuilderNotFoundException.h"

#include "../../common/Utility.h"

/// Initializes a new instance of the <see cref="SkillBuilder"/>.
SkillBuilder::SkillBuilder(string configKey)
{
    m_ConfigKey = configKey;
}

SkillBuilder::~SkillBuilder()
{
    foreach (MapString_ISkillPartBuilder::value_type& skillPart, m_Builders)
    {
        DeletePtrSafe(skillPart.second);
    }

    m_Builders.clear();
}

void SkillBuilder::Initialize()
{
    m_Builders.clear();
    string sectionName  = "SkillEngineConfig";
    xml_node& section   = singleton_default<ConfigurationManager>::instance().GetSection(sectionName);

    if (!section)
    {
        throw ConfigurationErrorsException("Can't find the config section. section name:" + sectionName);
    }

    xml_node node = Common::GetAttributeNode(section, "key", m_ConfigKey);
  
    if (!node)
    {
        throw ConfigurationErrorsException("Can't find the skill engine config. config name:" + m_ConfigKey);
    }

    vector<string> actionSkillConfig = Common::TransToStringVector(node.attribute("value").value(), ",");

    foreach (string& config, actionSkillConfig)
    {
        try
        {
            ISkillPartBuilder* builder = CreateInstance(trim_copy(config) + "Builder");

            if (builder == NULL)
            {
                throw SkillPartBuilderNotFoundException("Can't find the builder. builder name:" + config);
            }

            m_Builders[trim_copy(config)]       = builder;
        }
        catch (SkillPartBuilderNotFoundException& ex)
        {
            LogHelper::Insert(&ex);

            continue;
        }
    }

    LogHelper::Insert(m_ConfigKey + " has initialized.", LogType_Info);
}

ISkillPartBuilder* SkillBuilder::CreateInstance(string name)
{
    ISkillPartBuilder* builder = NULL;

    if (name == TypeId(TriggerConditionsBuilder))
    {
        builder = new TriggerConditionsBuilder();
    }
    else if (name == TypeId(PropertyChangesBuilder))
    {
        builder = new PropertyChangesBuilder();
    }
    else if (name == TypeId(DisplacementsBuilder))
    {
        builder = new DisplacementsBuilder;
    }
    else if (name == TypeId(FoulRelatedBuilder))
    {
        builder = new FoulRelatedBuilder();
    }
    else if (name == TypeId(BallRelatedBuilder))
    {
        builder = new BallRelatedBuilder();
    }
    else if (name == TypeId(DebuffsBuilder))
    {
        builder = new DebuffsBuilder();
    }
    else if (name == TypeId(SpecialEffectsBuilder))
    {
        builder = new SpecialEffectsBuilder();
    }
    else if (name == TypeId(ModelsBuilder))
    {
        builder = new ModelsBuilder();
    }
    else if (name == TypeId(ManagerChangesBuilder))
    {
        builder = new ManagerChangesBuilder();
    }
    else if (name == TypeId(SkillUpgradesBuilder))
    {
        builder = new SkillUpgradesBuilder();
    }
    else if (name ==TypeId(ImmunityBuilder))
    {
        builder = new ImmunityBuilder();
    }
    else if (name == TypeId(TriggersBuilder))
    {
        builder = new TriggersBuilder();
    }

    return builder;
}

