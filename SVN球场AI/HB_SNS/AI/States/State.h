/********************************************************************************
 * 文件名：State.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __STATE_H__
#define __STATE_H__

#include "../../Interface/IState.h"
#include "../../Interface/IMatch.h"
#include "../../Interface/Player/IPlayer.h"
#include "../../Interface/Player/Status/IPlayerStatus.h"
#include "../../Interface/Player/Property/IPlayerAttribute.h"

#include "../../common/common.h"

#include "../../MatchProcess/PlayerProcess.h"
#include "../../MatchProcess/GoalkeeperProcess.h"

typedef bool (*ConditionDelegate)(IPlayer* player, IState* preview);

class State : public IState
{
public:

    State();
    virtual ~State();

    /// 1. Find the next states from the state chain.
    /// 2. Find the next state that pass the condition.
    IState* Decide(IPlayer* player, IState* preview);

    void Enter(IPlayer* player);

    /// Invoked while exit current state.
    void Exit(IPlayer* player);

    /// Current State to make a action.
    void Action(IPlayer* player);

    /// Player to save current state.
    Process* Save(IPlayer* player);

    //是否还是在当前状态
    virtual bool IsInState(IState* st) = 0;

    /// Represents the state chain.
    vector<IState*>& StateChain();

    /// Represents the next states's conditions.
    map<IState*, ConditionDelegate>& StateCondition();

    //////////////////////////////////////////////////////////////////////////
    unsigned int    Id() { return m_Id; }

    int             ClientId() { return m_ClientId; }
    void            SetClientId(int id) { m_ClientId = id; }

    int             TimeLast() { return m_TimeLast; }
    void            SetTimeLast(int time) { m_TimeLast = time; }

    bool            Stopable() { return m_Stopable; }
    void            SetStopable(bool stopable) { m_Stopable = stopable; }

protected:

    //保存自身类数据到process中
    void Save(Process* process, IPlayer* player);

protected:

    /// 内部编号
    unsigned int m_Id;

    /// 客户端编号
    int m_ClientId;

    /// 表示了该动作的持续时间
    int m_TimeLast;

    /// 表示了该状态是否可以成为最终状态
    bool m_Stopable;

    //////////////////////////////////////////////////////////////////////////
    //状态序列
    vector<IState*> m_StateChain;

    //状态条件
    map<IState*, ConditionDelegate> m_StateCondition;
};

#endif //__STATE_H__
