/********************************************************************************
 * 文件名：FinishingBuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FINISHINGBUFF_H__
#define __FINISHINGBUFF_H__

#include "../../common/common.h"

/// 表示了临门一脚BUFF（暂时提高50%射门属性)
class FinishingBuff : public AbsBuff
{
public:

    /// Initialize a new buff.        
    FinishingBuff(int triggerId)
        : AbsBuff(triggerId, 1, BuffType_Finishing)
    {
        m_Isbuff    = true;

        // 这里的经过基类的初始化，已经清空过
        m_VectorPropertyId += PlayerProperty_Shooting, PlayerProperty_FreeKick;

        m_Rate  = 50;
    }
};

#endif //__FINISHINGBUFF_H__

