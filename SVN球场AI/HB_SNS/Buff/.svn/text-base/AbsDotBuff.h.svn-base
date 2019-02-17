/********************************************************************************
 * 文件名：AbsDotBuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ABSDOTBUFF_H__
#define __ABSDOTBUFF_H__

#include "../common/Enum/BuffType.h"

/// 表示了一个渐变型BUFF或者DEBUFF
class AbsDotBuff : public AbsBuff 
{
public:

    /// 初始化一个新的BUFF或DEBUFF
    AbsDotBuff(int triggerId, int time) 
        : AbsBuff(triggerId, time, BuffType_Dot) 
    {

    }

    /// 表示了渐变率
    double      Gradient() { return m_Gradient; }
    void        SetGradient(double value) { m_Gradient = value; }

protected:

    double  m_Gradient;
};

#endif //__ABSDOTBUFF_H__

