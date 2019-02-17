/********************************************************************************
 * 文件名：IManagerSkill.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IMANAGERSKILL_H__
#define __IMANAGERSKILL_H__

#include "IRawSkill.h"
#include "ISkillPart.h"

#include "../../common/common.h"

/// 表示了经理技能的接口
class IManagerSkill : public IRawSkill 
{
public:

    /// 表示了经理技能的持续时间
    virtual int                 Last() = 0;
    virtual void                SetLast(int last) = 0;

    /// 表示了该经理技能的Formation index.
    virtual vector<int>&        Formation() = 0;
    virtual void                SetFormation(vector<int>& foramtion) = 0;

    virtual ISkillPart*         ManagerChanges() = 0;
    virtual void                SetManagerChanges(ISkillPart* skill) = 0;

    virtual ISkillPart*         SkillUpgrades() = 0;
    virtual void                SetSkillUpgrades(ISkillPart* skill) = 0;

    virtual ISkillPart*         Immunity() = 0;
    virtual void                SetImmunity(ISkillPart* skill) = 0;

    virtual ISkillPart*         SpecialEffects() = 0;
    virtual void                SetSpecialEffects(ISkillPart* skill) = 0;

    virtual void                SetSkillPart(string name, ISkillPart* skillPart) = 0;
};

#endif //__IMANAGERSKILL_H__
