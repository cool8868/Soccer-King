/********************************************************************************
 * 文件名：DecreaseStaminaRateStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DECREASESTAMINARATESTATUS_H__
#define __DECREASESTAMINARATESTATUS_H__

#include "../../Interface/Manager/Status/IDecreaseStaminaRateStatus.h"

class DecreaseStaminaRateStatus : public IDecreaseStaminaRateStatus 
{
public:

    DecreaseStaminaRateStatus()
    {
        InitVariables();
    }

public:

    double  RawRate() { return m_RawRate; }
    void    SetRawRate(double value) { m_RawRate = value; }

    double  Rate() { return m_Rate; }
    void    SetRate(double value) { m_Rate = value; }

protected:

    /// Raw rate.
    double m_RawRate;

    /// Current rate
    double m_Rate;

private:

    void InitVariables()
    {
        m_RawRate   = 0.0;
        m_Rate      = 0.0;
    }
};

#endif //__DECREASESTAMINARATESTATUS_H__
