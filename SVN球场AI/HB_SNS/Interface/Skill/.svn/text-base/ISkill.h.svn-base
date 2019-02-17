/********************************************************************************
 * 文件名：ISkill.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ISKILL_H__
#define __ISKILL_H__

/// 表示了一个技能
class ISkill 
{
public:

    virtual ~ISkill() {}

public:

    /// 表示了技能ID
    virtual string      SkillId() = 0;

    /// 表示了技能的名称
    virtual string      SkillName() = 0;

    /// 表示了技能的原数据
    virtual IRawSkill*  GetRawSkill() = 0;

    /// 表示技能的时间流逝
    virtual void        TimeLapse() = 0;
};

#endif //__ISKILL_H__
