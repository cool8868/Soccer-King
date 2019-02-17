/********************************************************************************
 * 文件名：HoldBallBuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __HOLDBALLBUFF_H__
#define __HOLDBALLBUFF_H__

class HoldBallBuff : public AbsHoldBallBuff
{
public:

    HoldBallBuff(int triggerId)
        : AbsHoldBallBuff(triggerId)
    {
        SetIsBuff(true);
    }
};

class HoldBallDebuff : public AbsHoldBallBuff
{
public:

    HoldBallDebuff(int triggerId)
        : AbsHoldBallBuff(triggerId)
    {
        SetIsBuff(false);
    }
};

#endif //__HOLDBALLBUFF_H__
