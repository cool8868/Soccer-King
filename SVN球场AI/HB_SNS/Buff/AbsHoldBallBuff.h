/********************************************************************************
 * 文件名：AbsHoldBallBuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ABSHOLDBALLBUFF_H__
#define __ABSHOLDBALLBUFF_H__

#include "AbsBuff.h"

/// 表示当球员持球时才有效的技能
class AbsHoldBallBuff : public AbsBuff
{
public:

    /// 初始化一个新的BUFF或DEBUFF
    AbsHoldBallBuff(int triggerId)
        : AbsBuff(triggerId, -1, BuffType_HoldBall)
    {

    }

    /// 当每个回合开始时调用
    void TimeLapse() { }

    /// 移除Buff
    void RemoveBuff() { m_Time = 0; }
};

#endif //__ABSHOLDBALLBUFF_H__
