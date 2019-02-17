/********************************************************************************
 * 文件名：IDecreaseFoulLevelStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IDECREASEFOULLEVELSTATUS_H__
#define __IDECREASEFOULLEVELSTATUS_H__

/// 表示了犯规时降低惩罚级别的状态
class IDecreaseFoulLevelStatus 
{
public:

    virtual ~IDecreaseFoulLevelStatus() {}

public:

    /// Current rate.
    virtual double      Rate() = 0;
    virtual void        SetRate(double rate) = 0;

    /// Raw rate.
    virtual double      RawRate() = 0;
    virtual void        SetRawRate(double rate) = 0;

    /// Current probability.
    virtual double      Probability() = 0;
    virtual void        SetProbability(double value) = 0;

    /// Raw probability.
    virtual double      RawProbability() = 0;
    virtual void        SetRawProbability(double value) = 0; 
};

#endif //__IDECREASEFOULLEVELSTATUS_H__
