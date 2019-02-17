/********************************************************************************
 * 文件名：DecreaseFoulLevelStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DECREASEFOULLEVELSTATUS_H__
#define __DECREASEFOULLEVELSTATUS_H__

/// Represents the status that will to decrease the foul level while player has foul.
class DecreaseFoulLevelStatus : public IDecreaseFoulLevelStatus 
{
public:

    DecreaseFoulLevelStatus() 
    { 
        InitVariables(); 
    }

public:

    double      Rate() { return m_Rate; }
    void        SetRate(double vl) { m_Rate = vl; }

    double      RawRate() { return m_RawRate; }
    void        SetRawRate(double vl) { m_RawRate = vl; }

    double      Probability() { return m_Probability; }
    void        SetProbability(double vl) { m_Probability = vl; }

    double      RawProbability() { return m_RawProbability; }
    void        SetRawProbability(double vl) { m_RawProbability = vl; }

protected:

    /// Current rate.
    double  m_Rate;

    /// Raw rate.
    double  m_RawRate;

    /// Current probability.
    double  m_Probability;

    /// Raw probability.
    double  m_RawProbability;

private:

    void InitVariables()
    {
        m_Rate              = 0.0;
        m_RawRate           = 0.0;
        m_Probability       = 0.0;
        m_RawProbability    = 0.0;
    }
};

#endif //__DECREASEFOULLEVELSTATUS_H__
