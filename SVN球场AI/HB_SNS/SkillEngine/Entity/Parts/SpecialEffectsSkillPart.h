/********************************************************************************
 * 文件名：SpecialEffectsSkillPart.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SPECIALEFFECTSSKILLPART_H__
#define __SPECIALEFFECTSSKILLPART_H__

#include "../../../common/common.h"

#include "../../../Interface/Skill/ISkillPart.h"

class Special;

class SpecialEffectsSkillPart : public ISkillPart 
{
public:

    SpecialEffectsSkillPart();

    SpecialEffectsSkillPart(SpecialEffectsSkillPart& rf);

    ~SpecialEffectsSkillPart();

public:

    vector<Special*>&           Specials() { return m_Specials; }

    SpecialEffectsSkillPart*    Clone() { return new SpecialEffectsSkillPart(*this); }

private:

    vector<Special*>            m_Specials;

private:

    void                        InitVariables();
};

class Special
{
public:

    Special();

    Special(Special& rf);

    virtual ~Special() {} 

public:

    string      Name() { return m_Name; }
    void        SetName(string vl) { m_Name = vl; }

    string      Value() { return m_Value; }
    void        SetValue(string vl) { m_Value = vl; }

    Special*    Clone() { return new Special(*this); }

protected:

    string      m_Name;
    string      m_Value;

private:

    void        InitVariables();
};

#endif //__SPECIALEFFECTSSKILLPART_H__
