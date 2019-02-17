/********************************************************************************
 * 文件名：DiveBallState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DIVEBALLSTATE_H__
#define __DIVEBALLSTATE_H__

#include "State.h"

/// Represents the goal keeper's dive ball state.
class DiveBallState : public State
{
public:

    DiveBallState();

public:

    static DiveBallState* Instance();

    void Initialize();

    /// 扑救
    void Action(IPlayer* player);

    string ToString();

    /// 保存扑救状态
    Process* Save(IPlayer* player);

    /// Decides the nex <see cref="IState"/>
    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:
    
    static bool ValidateDiveBallToGKHoldBall(IPlayer* player, IState* preview);
};

#endif //__DIVEBALLSTATE_H__
