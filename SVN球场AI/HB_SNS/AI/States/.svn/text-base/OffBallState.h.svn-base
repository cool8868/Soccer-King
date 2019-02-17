/********************************************************************************
 * 文件名：OffBallState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __OFFBALLSTATE_H__
#define __OFFBALLSTATE_H__

#include "State.h"

/// Represents the player who dosen't have the ball.
/// This class implemented the Singleton pattern.
class OffBallState : public State
{
public:

    OffBallState();

public:

    static OffBallState* Instance();

    void Initialize();

    string ToString();

    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateOffBallToPositional(IPlayer* player, IState* preview);
    static bool ValidateOffBallToAction(IPlayer* player, IState* preview);
    static bool ValidateOffBallToStopball(IPlayer* player, IState* preview);
    static bool ValidateOffBallToDiveBall(IPlayer* player, IState* preview);
    static bool ValidateOffBallToDefence(IPlayer* player, IState* preview);
};

#endif //__OFFBALLSTATE_H__
