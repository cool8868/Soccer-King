/********************************************************************************
 * 文件名：PassiveFacade.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PASSIVEFACADE_H__
#define __PASSIVEFACADE_H__

/// Represents the passive type skill's facade.
class PassiveFacade : public IRequestInitialize
{
public:

    /// Initializes all the passive skills.
    void Initialize();

    /// Get a passive skill by skill id and the skill level.
    IRawSkill* GetSkill(string skillId);

    /// Effect a passive type skill.
    void Effect(IPlayer* player, IRawSkill* skill);
};

#endif //__PASSIVEFACADE_H__
