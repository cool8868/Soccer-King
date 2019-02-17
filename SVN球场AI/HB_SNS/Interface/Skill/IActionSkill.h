/********************************************************************************
 * 文件名：IActionRawSkill.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IACTIONSKILL_H__
#define __IACTIONSKILL_H__

/// 表示了Action技能的接口
class IActionSkill : public ISkill 
{
public:

    /// 表示了技能的冷却时间
    virtual int                 CoolDown() = 0;

    /// 表示了技能的最大冷却时间
    virtual int                 MaxCoolDown() = 0;

    /// 技能的可触发状态
    virtual vector<IState*>&    TriggerStates() = 0;

    /// 表示了技能的分类
    virtual SkillClass          Class() = 0;

    /// 技能触发
    virtual void                SkillTriggered() = 0;

    /// 获取技能影响的目标
    virtual vector<IPlayer*>&   GetSkillTargets(IPlayer* triggerman) = 0;

    /// 将球员技能影响目标转化为字符串
    virtual string              GetSkillTargetsToString(IPlayer* triggerman) = 0;

    /// 更新技能的CD时间
    virtual void                UpdateMaxCoolDown(int cd) = 0;
};

#endif //__IACTIONSKILL_H__
