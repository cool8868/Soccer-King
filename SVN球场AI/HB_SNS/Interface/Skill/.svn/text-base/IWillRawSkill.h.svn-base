/********************************************************************************
 * 文件名：IWillRawSkill.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IWILLRAWSKILL_H__
#define __IWILLRAWSKILL_H__

#include "IRawSkill.h"
#include "ISkillPart.h"

/// 表示了意志的接口
class IWillRawSkill : public IRawSkill
{
public:

    /// 表示了意志的触发条件
    virtual ISkillPart*     Triggers() = 0;
    virtual void            SetTriggers(ISkillPart* skill) = 0;

    /// 表示了意志的效果    
    virtual ISkillPart*     PropertyChanges() = 0;
    virtual void            SetPropertyChanges(ISkillPart* skill) = 0;

    virtual void            SetSkillPart(string name, ISkillPart* skillPart) = 0;
};

#endif //__IWILLRAWSKILL_H__
