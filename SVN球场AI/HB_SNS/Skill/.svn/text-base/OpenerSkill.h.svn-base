/********************************************************************************
 * 文件名：OpenerSkill.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __OPENERSKILL_H__
#define __OPENERSKILL_H__

/// Represents the opener type skill.    
class OpenerSkill : public IOpenerSkill 
{
public:

    OpenerSkill(IRawSkill* skill) 
    {
        IOpenerRawSkill* openerSkill = PointerCastSafe(IOpenerRawSkill, skill);

        if (openerSkill == NULL)
        {
            LogHelper::Insert("Create a opener skill with null referenced raw skill.");
            return;
        }

        m_SkillId       = openerSkill->Id();
        m_SkillName     = openerSkill->Name();
        m_RawSkill      = skill;
    }

    string      SkillId() { return m_SkillId; }
    void        SetSkillId(string vl) { m_SkillId = vl; }

    string      SkillName() { return m_SkillName; }
    void        SetSkillName(string vl) { m_SkillName = vl; }

    IRawSkill*  GetRawSkill() { return m_RawSkill; }
    void        SetRawSkill(IRawSkill* vl) { m_RawSkill = vl; }

    // 这里未作任何处理
    void        TimeLapse() {}

protected:

    string      m_SkillId;

    string      m_SkillName;

    IRawSkill*  m_RawSkill;
};

#endif //__OPENERSKILL_H__
