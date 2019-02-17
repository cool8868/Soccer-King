/********************************************************************************
 * 文件名：IDebuffStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IDEBUFFSTATUS_H__
#define __IDEBUFFSTATUS_H__

#include "../../../common/Enum/DebuffType.h"

class DisableDebuff;

/// 表示了球员的异常状态的接口
class IDebuffStatus 
{
public:

    virtual ~IDebuffStatus() {}

public:

    /// 表示了球员受到的异常状态类型
    virtual DebuffType          GetDebuffType() = 0;
    virtual void                SetDebuffType(DebuffType vl) = 0;

    /// 表示了球员的当前异常行动状态
    virtual DisableDebuff*      GetDisableDebuff() = 0;
    virtual void                SetDisableDebuff(DisableDebuff* vl) = 0;
};

#endif //__IDEBUFFSTATUS_H__
