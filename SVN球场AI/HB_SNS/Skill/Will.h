/********************************************************************************
 * 文件名：Will.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WILL_H__
#define __WILL_H__

/// 表示了意志对象
class Will : public IWill
{
public:

    Will()
    {
        InitVariables();
    }

public:

    string      SkillId() { return m_SkillId; }
    void        SetSkillId(string vl) { m_SkillId = vl; }

    string      SkillName() { return m_SkillName; }
    void        SetSkillName(string vl) { m_SkillName = vl; }

    IRawSkill*  GetRawSkill() { return m_RawSkill; }
    void        SetRawSkill(IRawSkill* vl) { m_RawSkill = vl; }

    // 不做任何处理
    void        TimeLapse() {}

protected:

    /// 表示了技能编号
    string  m_SkillId;

    /// 表示了技能的名称
    string  m_SkillName;

    /// 表示了技能的数据
    IRawSkill* m_RawSkill;

private:

    void InitVariables()
    {
        m_RawSkill      = NULL;
    }
};

#endif //__WILL_H__

