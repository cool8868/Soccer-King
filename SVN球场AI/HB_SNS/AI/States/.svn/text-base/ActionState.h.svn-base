/********************************************************************************
 * 文件名：ActionState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ACTIONSTATE_H__
#define __ACTIONSTATE_H__

#include "State.h"

/// 行动状态
class ActionState : public State
{
public:

    ActionState();
    ~ActionState();

public:

    static ActionState* Instance();

    void Initialize();

    void Enter(IPlayer* player);

    string ToString();

    /// Decides the nex <see cref="IState"/>
    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateActionToIdle(IPlayer* player, IState* preview);
    static bool ValidateActionToChace(IPlayer* player, IState* preview);
    static bool ValidateActionToHoldBall(IPlayer* player, IState* preview);
    static bool ValidateActionToOffBall(IPlayer* player, IState* preview);
};

#endif //__ACTIONSTATE_H__
