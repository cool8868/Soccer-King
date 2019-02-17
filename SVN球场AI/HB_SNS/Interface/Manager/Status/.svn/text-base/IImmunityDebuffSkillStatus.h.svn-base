/********************************************************************************
 * 文件名：IDecreaseStaminaRateStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IIMMUNITYDEBUFFSKILLSTATUS_H__
#define __IIMMUNITYDEBUFFSKILLSTATUS_H__

/// 表示了对对方减益效果的抵抗率
class IImmunityDebuffSkillStatus 
{
public:

    virtual ~IImmunityDebuffSkillStatus() {}

public:

    /// Current probability.
    virtual double      Probability() = 0;
    virtual void        SetProbability(double value) = 0;

    /// Raw probability.
    virtual double      RawProbability() = 0;
    virtual void        SetRawProbability(double value) = 0;
};

#endif //__IIMMUNITYDEBUFFSKILLSTATUS_H__
