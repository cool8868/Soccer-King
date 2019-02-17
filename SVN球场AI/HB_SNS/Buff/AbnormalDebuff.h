/********************************************************************************
 * 文件名：AbnormalDebuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ABNORMALDEBUFF_H__
#define __ABNORMALDEBUFF_H__

#include "AbsBuff.h"
#include "../common/Enum/DebuffType.h"
#include "../Interface/IAction.h"

/// 表示了所有的异常状态的基类
class AbnormalDebuff : public AbsBuff
{
public:

    /// Initialize a new abnormal debuff.
    AbnormalDebuff(int triggerId, int time, DebuffType type);

    /// 表示了异常状态的类型
    DebuffType  GetDebuffType() { return m_DebuffType; }
    void        SetDebuffType(DebuffType type) { m_DebuffType = type; }

protected:

   DebuffType   m_DebuffType;
};

/// 表示了所有的影响行动的异常状态的基类
class DisableDebuff : public AbnormalDebuff, public IAction
{
public:

    /// 初始化一个异常状态
    DisableDebuff(int triggerId, int time, DebuffType type);
};

#endif //__ABNORMALDEBUFF_H__
