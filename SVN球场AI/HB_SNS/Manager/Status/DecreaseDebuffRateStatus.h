/********************************************************************************
 * 文件名：DecreaseDebuffRateStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DECREASEDEBUFFRATESTATUS_H__
#define __DECREASEDEBUFFRATESTATUS_H__

/// Represents the decreases of the debuffs's rate status.    
class DecreaseDebuffRateStatus : public IDecreaseDebuffRateStatus 
{
public:

    DecreaseDebuffRateStatus()
    {
        InitVariables();
    }

public:

    double  Rate() { return m_Rate; }
    void    SetRate(double vl) { m_Rate = vl; }

    double  RawRate() { return m_RawRate; }
    void    SetRawRate(double vl) { m_RawRate = vl; }

protected:

    /// Current rate.
    double  m_Rate;

    /// Raw rate.
    double  m_RawRate;

private:

    void InitVariables()
    {
        m_Rate      = 0.0;
        m_RawRate   = 0.0;
    }
};

#endif //__DECREASEDEBUFFRATESTATUS_H__
