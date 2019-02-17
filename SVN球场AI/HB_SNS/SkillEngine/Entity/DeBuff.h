/********************************************************************************
 * 文件名：DeBuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DEBUFF_H__
#define __DEBUFF_H__

class DeBuff : public AbsBuff
{
public:

    /// Initializes a new instance of the <see cref="AbsBuff"/>.
    /// <param name="triggerId">Represents the trigger man's id.</param>
    /// <param name="time">Represents the buff's lasting time.(round)</param>
    /// <param name="type">Represents the buff's type.</param>
    DeBuff(int triggerId, int time, BuffType type)
        : AbsBuff(triggerId, time, type)
    {
        SetIsBuff(false);
    }
};

#endif //__DEBUFF_H__
