/********************************************************************************
 * 文件名：IRawSkill.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IRAWSKILL_H__
#define __IRAWSKILL_H__

#include "../../common/common.h"
#include "../../common/Enum/SkillType.h"

/// 表示了一个技能的接口
class IRawSkill
{
public:

    virtual ~IRawSkill() {}

public:

    /// 表示了技能的唯一标示符
    virtual string      Id() = 0;

    /// 表示了技能的名称
    virtual string      Name() = 0;

    /// 表示了技能的类型
    virtual SkillType   Type() = 0;

    virtual IRawSkill*  Clone() = 0;
};

#endif //__IRAWSKILL_H__
