/********************************************************************************
 * 文件名：ManagerData.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MANAGERDATA_H__
#define __MANAGERDATA_H__

#include "../../Interface/Manager/IManager.h"
#include "../../Interface/Manager/IManagerData.h"

#include "../../common/Defines/Defines.h"
#include "../../common/common.h"

class ManagerData : public IManagerData 
{
public:

    ManagerData();

    ~ManagerData();

public:
    unsigned int            Id() { return m_Id; }
    void                    SetId(unsigned int id) { m_Id = id; }

    string                  Name() { return m_Name; }
    void                    SetName(string name) { m_Name = name; }

    string                  SpellName() { return m_SpellName; }
    void                    SetSpellName(string name) { m_SpellName = name; }

    int                     FormationId() { return m_FormationId; }
    void                    SetFormationId(int id) { m_FormationId = id; }

    vector<IRawPlayer*>&    Players() { return m_Players; }

    vector<string>&         PassiveSkills() { return m_PassiveSkills; }
    void                    SetPassiveSkills(vector<string>& vl) { m_PassiveSkills = vl; }

    vector<string>&         OpenerSkills() { return m_OpenerSkills; }
    void                    SetOpenerSkills(vector<string>& vl) { m_OpenerSkills = vl; }

protected:

    /// 表示了经理的编号
    unsigned int            m_Id;

    /// 表示了经理的中文名
    string                  m_Name;

    /// 表示了经理的拼写名
    string                  m_SpellName;

    /// 表示了阵型Id
    int                     m_FormationId;

    /// 表示了球员的集合
    vector<IRawPlayer*>     m_Players;

    /// 表示了经理的被动技能
    vector<string>          m_PassiveSkills;

    /// 表示了经理的Opener技能
    vector<string>          m_OpenerSkills;

private:

    void                    InitVariables();
};

#endif //__MANAGERDATA_H__
