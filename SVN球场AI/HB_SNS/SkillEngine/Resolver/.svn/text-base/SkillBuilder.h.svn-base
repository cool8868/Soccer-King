/********************************************************************************
 * 文件名：SkillBuilder.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SKILLBUILDER_H__
#define __SKILLBUILDER_H__

#include "../../Interface/Skill/Resolver/ISkillBuilder.h"
#include "../../Interface/IRequestInitialize.h"
#include "../../Interface/Skill/Resolver/ISkillPartBuilder.h"

/// Represents a builder that to build the skills.
class SkillBuilder : public ISkillBuilder, public IRequestInitialize
{
public:

    SkillBuilder(string configKey);

    ~SkillBuilder();

public:

    void                                Initialize();

    MapString_ISkillPartBuilder&        Builders() { return m_Builders; }

protected:

    MapString_ISkillPartBuilder         m_Builders;

private:

    string m_ConfigKey;

    ISkillPartBuilder* CreateInstance(string name);
};

#endif //__SKILLBUILDER_H__
