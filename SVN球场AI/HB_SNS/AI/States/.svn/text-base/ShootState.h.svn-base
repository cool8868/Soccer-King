/********************************************************************************
 * 文件名：ShootState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SHOOTSTATE_H__
#define __SHOOTSTATE_H__

#include "State.h"

/// Represents the shoot action state.
class ShootState : public State
{
public:

    ShootState();

public:

    static ShootState* Instance();

    void Initialize();

    void Enter(IPlayer* player);

    Process* Save(IPlayer* player);

    string ToString();

    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:
    
    static bool ValidateShootToVolleyShoot(IPlayer* player, IState* state);
    static bool ValidateShootToDefaultShoot(IPlayer* player, IState* state);
    static bool ValidateShootToHoldBall(IPlayer* player, IState* state);
};

#endif //__SHOOTSTATE_H__
