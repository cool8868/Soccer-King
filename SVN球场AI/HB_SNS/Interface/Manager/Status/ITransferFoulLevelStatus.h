/********************************************************************************
 * 文件名：ITransferFoulLevelStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ITRANSFERFOULLEVELSTATUS_H__
#define __ITRANSFERFOULLEVELSTATUS_H__

/// 表示了犯规时转移给对方的概率
class ITransferFoulLevelStatus 
{
public:

    virtual ~ITransferFoulLevelStatus() {}

public:

    /// Current probability.
    virtual double      Probability() = 0;
    virtual void        SetProbability(double value) = 0;

    /// Raw probability.
    virtual double      RawProbability() = 0;
    virtual void        SetRawProbability(double value) = 0;
};

#endif //__ITRANSFERFOULLEVELSTATUS_H__
