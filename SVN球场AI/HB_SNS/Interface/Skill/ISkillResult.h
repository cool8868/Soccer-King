/********************************************************************************
 * 文件名：ISkillResult.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ISKILLRESULT_H__
#define __ISKILLRESULT_H__

/// 表示了某个回合触发的技能
class ISkillResult 
{
public:

    virtual ~ISkillResult() {} 

public:

    /// 表示技能触发的回合
    virtual int     Round() = 0;
    virtual void    SetRound(int round) = 0;

    /// 表示了触发的技能名称
    virtual string  SkillName() = 0;
    virtual void    SetSkillName(string name) = 0;

    /// 表示了触发的技能ID
    virtual string  SkillId() = 0;
    virtual void    SetSkillId(string id) = 0;

    /// 表示了技能的目标人(clientId, clientId, clientId)
    virtual string  SkillTargets() = 0;
    virtual void    SetSkillTargets(string targets) = 0;
};

#endif //__ISKILLRESULT_H__
