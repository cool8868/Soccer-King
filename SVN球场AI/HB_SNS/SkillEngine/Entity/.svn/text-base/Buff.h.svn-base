/********************************************************************************
 * 文件名：Buff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __BUFF_H__
#define __BUFF_H__

class Buff : public AbsBuff
{
public:

    /// Initialize a new instance of the <see cref="Buff"/> class.
    /// <param name="triggerId">Represents the trigger man's id.</param>
    /// <param name="time">Represents the buff's lasting time.(round)</param>
    /// <param name="type">Represents the buff's type.</param>
    Buff(int triggerId, int time, BuffType type)
        : AbsBuff(triggerId, time, type)
    {
        SetIsBuff(true);
    }
};

#endif //__BUFF_H__
