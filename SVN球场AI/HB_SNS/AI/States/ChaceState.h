/********************************************************************************
 * 文件名：ChaceState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __CHACESTATE_H__
#define __CHACESTATE_H__

#include "State.h"

/// 跑动状态
class ChaceState : public State
{
public:

    ChaceState();
    ~ChaceState();

public:

    static ChaceState* Instance();

    /// Outputs the ChaceState's name.
    string ToString();

    /// Initializes the new instance of the <see cref="ChaceState"/> class.
    void Initialize();

    /// Chace State action.
    void Action(IPlayer* player);

    /// Decides the nex
    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateChaceToAction(IPlayer* player, IState* preview);
    static bool ValidateChaceToChace(IPlayer* player, IState* preview);
    static bool ValidateChaceToIdle(IPlayer* player, IState* preview);
};

#endif //__CHACESTATE_H__
