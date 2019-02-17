/********************************************************************************
 * 文件名：DefenceState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DEFENCESTATE_H__
#define __DEFENCESTATE_H__

#include "State.h"

/// 表示了防守状态
class DefenceState : public State
{
public:

    DefenceState();

public:

    static DefenceState* Instance();

    void Initialize();
    
    void Enter(IPlayer* player);
    
    string ToString();
    
    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateDefenceToInterruption(IPlayer* player, IState* preview);
    
    static bool ValidateDefenceToOffBall(IPlayer* player, IState* preview);
    
    static bool ValidateDefenceToSlideTackle(IPlayer* player, IState* preview);
};

#endif //__DEFENCESTATE_H__
