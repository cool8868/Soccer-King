/********************************************************************************
 * 文件名：ImmunityDebuffSkillStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IMMUNITYDEBUFFSKILLSTATUS_H__
#define __IMMUNITYDEBUFFSKILLSTATUS_H__

/// Represents the immunities the debuffs status.    
class ImmunityDebuffSkillStatus : public IImmunityDebuffSkillStatus 
{
public:

    ImmunityDebuffSkillStatus()
    {
        InitVariables();
    }

    /// Current probability.
    double  Probability() { return m_Probability; }
    void    SetProbability(double vl) { m_Probability = vl; }

    /// Raw probability.
    double  RawProbability() { return m_RawProbability; }
    void    SetRawProbability(double vl) { m_RawProbability = vl; }

protected:

    /// Current probability.
    double  m_Probability;

    /// Raw probability.
    double  m_RawProbability;

private:

    void InitVariables()
    {
        m_Probability       = 0.0;
        m_RawProbability    = 0.0;
    }
};

#endif //__IMMUNITYDEBUFFSKILLSTATUS_H__
