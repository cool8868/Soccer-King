/********************************************************************************
 * 文件名：DribbleState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DRIBBLESTATE_H__
#define __DRIBBLESTATE_H__

#include "State.h"

/// 带球状态
class DribbleState: public State
{
public:

    DribbleState();

public:

    static DribbleState* Instance();

    string ToString();

    void Initialize();

    void Action(IPlayer* player);

    void Enter(IPlayer* player);

    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateDribbleToDefaultDribble(IPlayer* player, IState* preview);
    static bool ValidateDribbleToHoldBall(IPlayer* player, IState* preview);
};

#endif //__DRIBBLESTATE_H__
