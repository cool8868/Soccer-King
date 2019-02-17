/********************************************************************************
 * 文件名：StopballState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __STOPBALLSTATE_H__
#define __STOPBALLSTATE_H__

#include "State.h"

/// Represents the stopball state.
class StopballState : public State
{
public:
    StopballState();

public:

    static StopballState* Instance();

    void Initialize();

    string ToString();

    /// Stopball Action
    void Action(IPlayer* player);

    IState* QuickDecide(IPlayer* player, IState* preview);
    
    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateStopballToHoldBall(IPlayer* player, IState* preview);
};

class StopballHoldballCondition
{
public:

    /// Decide player to the hold ball state.
    bool Decide(IPlayer* player, IState* preview)
    {
        return true;
    }
};

#endif //__STOPBALLSTATE_H__
