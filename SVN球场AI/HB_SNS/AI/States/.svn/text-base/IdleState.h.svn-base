/********************************************************************************
 * 文件名：IdleState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IDLESTATES_H__
#define __IDLESTATES_H__

#include "State.h"

class IdleState : public State
{
public:
    
    IdleState();

public:
    
    static IdleState* Instance();

    string ToString();

    /// Initializes the new instance of the
    void Initialize();

    /// Action.
    void Action(IPlayer* player);

    /// Decides the nex <see cref="IState"/>
    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateIdleToIdle(IPlayer* player, IState* preview);
    static bool ValidateIdleToChace(IPlayer* player, IState* preview);
    static bool ValidateIdleToAction(IPlayer* player, IState* preview);
};

#endif //__IDLESTATES_H__
