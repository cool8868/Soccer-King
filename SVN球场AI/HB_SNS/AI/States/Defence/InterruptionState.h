/********************************************************************************
 * 文件名：InterruptionState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __INTERRUPTIONSTATE_H__
#define __INTERRUPTIONSTATE_H__

#include "../DefenceState.h"

class InterruptionState : public DefenceState
{
public:

    InterruptionState();

public:

    static InterruptionState* Instance();

    void Initialize();
    
    string ToString();
    
    void Enter(IPlayer* player);
    
    void Action(IPlayer* player);
    
    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateInterruptionToDefence(IPlayer* player, IState* preview);
};

#endif //__INTERRUPTIONSTATE_H__
