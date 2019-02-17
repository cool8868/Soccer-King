/********************************************************************************
 * 文件名：IMadmanStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IMADMANSTATUS_H__
#define __IMADMANSTATUS_H__

/// 狂人技能的相关状态，当对方进球后，对方的属性下降，本方的属性提升
class IMadmanStatus 
{
public:

    virtual ~IMadmanStatus() {}

public:

    /// 表示了本方的提升幅度
    virtual int         SelfUpgradeRate() = 0;
    virtual void        SetSelfUpgradeRate(int rate) = 0;

    /// 表示了对方的降低幅度
    virtual int         OppDecreaseRate() = 0;
    virtual void        SetOppDecreaseRate(int rate) = 0;
};

#endif //__IMADMANSTATUS_H__

