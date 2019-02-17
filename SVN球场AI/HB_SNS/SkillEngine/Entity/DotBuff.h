/********************************************************************************
 * 文件名：DotBuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DOTBUFF_H__
#define __DOTBUFF_H__

class DotBuff : public AbsDotBuff
{
public:

    /// Initialize a new buff.        
    /// <param name="triggerId">Represents the trigger man's id.</param>
    /// <param name="time">Represents the buff's lasting time.(round)</param>        
    DotBuff(int triggerId, int time)
        : AbsDotBuff(triggerId, time)
    {
        SetIsBuff(true);
    }
};

#endif //__DOTBUFF_H__
