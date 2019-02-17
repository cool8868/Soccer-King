/********************************************************************************
 * 文件名：HoldBallState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __HOLDBALLSTATE_H__
#define __HOLDBALLSTATE_H__

#include "State.h"

/// Represents the player who have the ball.
class HoldBallState : public State
{
public:

    HoldBallState();

public:

    static HoldBallState* Instance();
    
    string ToString();

    void Initialize();

    /// While enter the "HoldBallState", to validate the ball distance.
    /// if the ball distance is larget than zero, the player will decide to chace ball.
    void Enter(IPlayer* player);

    /// Decides the next
    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateHoldBallToAction(IPlayer* player, IState* preview);
    static bool ValidateHoldBallToPass(IPlayer* player, IState* preview);
    static bool ValidateHoldBallToDribble(IPlayer* player, IState* preview);
    static bool ValidateHoldBallToShoot(IPlayer* player, IState* preview);
    static bool ValidateHoldBallToStopBall(IPlayer* player, IState* preview);
};

#endif //__HOLDBALLSTATE_H__
