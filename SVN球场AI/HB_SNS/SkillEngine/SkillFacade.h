/********************************************************************************
 * 文件名：SkillFacade.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SKILLFACADE_H__
#define __SKILLFACADE_H__

#include "../Interface/Skill/ISkillFacade.h"
#include "../Interface/Skill/IActionRawSkill.h"
#include "../Interface/Skill/IOpenerSkill.h"
#include "../Interface/Skill/IWillRawSkill.h"

#include "../common/common.h"

/// Represents the facade of the skill engine.
class SkillFacade : public ISkillFacade
{
public:

    static SkillFacade*     Instance();

    void                    Initialize();

    //Action Skills
    IRawSkill*              GetActionSkill(string skillId);

    bool                    IsActionSkillTriggered(IPlayer* player, IActionSkill* skill);

    void                    ActionSkillEffect(IPlayer* player, IActionSkill* skill);

    vector<IPlayer*>        GetActionSkillTargets(IPlayer* player, IActionRawSkill* skill);

    //Opener Skills
    IRawSkill*              GetOpenerSkill(string skillId);

    void                    OpenerSkillEffect(IPlayer* player, IOpenerSkill* skill);

    //Passive Skills
    void                    PassiveSkillEffect(IPlayer* player, string skillId);

    //Will Skills
    /// 从缓存获取意志(意志原数据)
    IWillRawSkill*          GetWillSkill(string id);

    /// 验证意志是否触发
    bool                    IsWillTriggered(IPlayer* player, IWillRawSkill* will);

    /// 意志生效
    void                    WillEffect(IPlayer* player, IWillRawSkill* will);
};

#endif //__SKILLFACADE_H__
