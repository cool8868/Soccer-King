/********************************************************************************
 * 文件名：ActionSkill.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ACTIONSKILL_H__
#define __ACTIONSKILL_H__

class ActionSkill : public IActionSkill 
{
public:

    ActionSkill(IRawSkill rawSkill, int totalRound);

public:

    string              SkillId() { return m_RawSkill->Id(); } 

    string              SkillName() { return m_RawSkill->Name(); } 

    IRawSkill*          GetRawSkill() { return m_RawSkill; }

    int                 CoolDown() { return m_CoolDown; }

    int                 MaxCoolDown(){ return m_MaxCoolDown; }

    SkillClass          Class() { return m_Class; }

    vector<IState*>&    TriggerStates() { return m_TriggerStates; }

    void                TimeLapse();

    void                SkillTriggered() { m_CoolDown = m_MaxCoolDown; }

    vector<IPlayer*>    GetSkillTargets(IPlayer triggerman);

    string              GetSkillTargetsToString(IPlayer* triggerman);

    void                UpdateMaxCoolDown(int cd);

protected:

    IActionRawSkill*    m_RawSkill;
    vector<IState*>     m_TriggerStates;
    int                 m_MaxCoolDown;
    int                 m_CoolDown;
    SkillClass          m_Class;

private:

    void    InitVariables();
};

#endif //__ACTIONSKILL_H__
