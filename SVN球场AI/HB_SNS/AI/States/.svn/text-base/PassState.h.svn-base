/********************************************************************************
 * 文件名：PassState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PASSSTATE_H__
#define __PASSSTATE_H__

#include "State.h"

class PassState : public State
{
public:

    PassState();

public:

    static PassState* Instance();
    
    void Initialize();
    
    void Enter(IPlayer* player);
    
    string ToString();
    
    Process* Save(IPlayer* player);
    
    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidatePassToHoldBall(IPlayer* player, IState* preview);
    static bool ValidatePassToShortPass(IPlayer* player, IState* preview);
    static bool ValidatePassToLongPass(IPlayer* player, IState* preview);
};

#endif //__PASSSTATE_H__