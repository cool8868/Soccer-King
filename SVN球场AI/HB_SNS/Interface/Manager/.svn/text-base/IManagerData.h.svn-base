/********************************************************************************
 * 文件名：IManagerData.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IMANAGERDATA_H__
#define __IMANAGERDATA_H__

#include "../../common/common.h"

#include "../Player/Property/IRawPlayer.h"

class IManagerData 
{
public:

    virtual ~IManagerData() {}

public:

    /// 表示了经理的编号
    virtual unsigned int            Id() = 0;
    virtual void                    SetId(unsigned int id) = 0;

    /// 表示了经理的中文名
    virtual string                  Name() = 0;
    virtual void                    SetName(string name) = 0;

    /// 表示了经理的拼写名
    virtual string                  SpellName() = 0;
    virtual void                    SetSpellName(string name) = 0;

    /// 表示了阵型Id
    virtual int                     FormationId() = 0;
    virtual void                    SetFormationId(int id) = 0;

    /// 表示了球员的集合
    virtual vector<IRawPlayer*>&    Players() = 0;

    /// 表示了经理的被动技能
    virtual vector<string>&         PassiveSkills() = 0;
    virtual void                    SetPassiveSkills(vector<string>& vl) = 0;

    /// 表示了经理的Opener技能
    virtual vector<string>&         OpenerSkills() = 0;
    void                            SetOpenerSkills(vector<string>& vl) = 0;
};

#endif //__IMANAGERDATA_H__
