/********************************************************************************
 * 文件名：Manager.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MANAGER_H__
#define __MANAGER_H__

#include "../Interface/Manager/IManager.h"
#include "../Interface/Manager/IManagerData.h"
#include "../common/Enum/Position.h"

class Manager: public IManager, public Creature
{
public:

    /// 这个是过期的接口,只在模拟器中使用
    Manager(unsigned int id, Side side, IMatch* match);

    Manager(Side side, IMatch* match, TransferManagerEntity* entity);

    virtual ~Manager();

public:

    /// 进球
    void                    Goal();

    /// 新的回合到来时初始化
    void                    RoundInit();

    /// 新的分钟开始时初始化
    void                    MinuteInit();

    /// 下半场开始
    void                    SecondHalfStart();

    /// 表示了当前队伍的球员犯规
    void                    Foul();

    /// 清除所有球员的所有异常状态
    void                    ClearDisable();

    /// 守门员开球门球
    void                    GkOpenball();

    /// Initializes current entity.
    void                    Initialize();

    vector<IPlayer*>        GetPlayersByPosition(Position position);

    void                    TriggerOpenerSkill(bool isFirstHalf);

public:

    unsigned int            Id() { return m_Id; }
    string                  Name() { return m_Name; }

    string                  SpellName() { return m_SpellName; }

    int                     FormationId() { return m_FormationId; }

    string                  Logo() { return m_Logo; }
    void                    SetLogo(string logo) { m_Logo = logo; }

    vector<IPlayer*>&       Players() { return m_Players; };

    IMatch*                 GetMatch() { return m_Match; }

    IManager*               Opponent();

    Side                    GetSide() { return m_Side; }

    vector<string>&         PassiveSkills() { return m_PassiveSkills; }
    void                    SetPassiveSkills(vector<string>& vl) { m_PassiveSkills = vl; }

    vector<IOpenerSkill*>&  OpenerSkills() { return m_OpenerSkills; }
    void                    SetOpenerSkills(vector<IOpenerSkill*>& vl) { m_OpenerSkills = vl; }

    vector<IWill*>&         Wills() { return m_Wills; }
    void                    SetWills(vector<IWill*>& vl) { m_Wills = vl; }

    IManagerStatus*         Status() { return m_Status; }

protected:

    /// 表示了经理的ID
    unsigned int m_Id;

    /// 表示了经理的中文名
    string m_Name;

    /// 表示了经理的英文名
    string m_SpellName;

    /// 表示了阵型id
    int m_FormationId;

    /// 表示了经理的Logo
    string m_Logo;

    /// 表示了球员的集合
    vector<IPlayer*> m_Players;

    /// 表示了对正常比赛的引用
    IMatch* m_Match;

    /// 表示了对手经理
    IManager* m_Opponent;

    /// 表示了经理是主队或客队
    Side m_Side;

    /// 表示了该经理的被动技能
    vector<string> m_PassiveSkills;

    /// 表示了该经理的主动经理技能
    vector<IOpenerSkill*> m_OpenerSkills;

    /// 表示了该经理的意志
    vector<IWill*>  m_Wills;

    /// 表示了经理的当前状态
    IManagerStatus* m_Status;

private:

    vector<IPlayer*>            InternalGetPlayerByPosition(Position position);

    /// 刷新本方的最前的一个球员
    void                        RefreshHeadMostPlayer();

    /// 刷新每个球员的防守目标
    void                        RefreshDefenceStatus();

    /// 找出一个球员的最近的防守人
    static IPlayer*             GetClosestDefender(IPlayer* target, vector<IPlayer*>& players);

    static vector<IPlayer*>     GetNoneDefenceTargetPlayer(vector<IPlayer*>& players);

    //初始化变量
    void                        InitVariables();

private:

    //用于保存指针，垃圾回收
    IManagerData*   m_Entity;
};

#endif //__MANAGER_H__