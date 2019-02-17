/********************************************************************************
 * 文件名：SkillResult.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SKILLRESULT_H__
#define __SKILLRESULT_H__

/// 表示了技能的结果
class SkillResult : public ISkillResult
{
public:

    SkillResult()
    {
        InitVariables();
    }

public:

    int         Round() { return m_Round; }
    void        SetRound(int vl) { m_Round = vl; }

    string      SkillId() { return m_SkillId; }
    void        SetSkillId(string vl) { m_SkillId = vl; }

    string      SkillTargets() { return m_SkillTargets; }
    void        SetSkillTargets(string vl) { m_SkillTargets = vl; }

    string      SkillName() { return m_SkillName; }
    void        SetSkillName(string vl) { m_SkillName = vl; }

protected:

    /// 表示了当前的回合
    int m_Round;

    /// 表示了发动的技能id.
    string m_SkillId;

    /// 表示了技能的目标
    string m_SkillTargets;

    /// 表示了技能的名称
    string m_SkillName;

private:

    void InitVariables()
    {
        m_Round         = 0;
        m_SkillId       = String::Empty();
        m_SkillTargets  = String::Empty();
        m_SkillName     = String::Empty();
    }
};

#endif //__SKILLRESULT_H__
