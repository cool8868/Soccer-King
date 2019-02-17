/********************************************************************************
 * 文件名：PositionalState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __POSITIONALSTATE_H__
#define __POSITIONALSTATE_H__

#include "State.h"

/// 表示了寻位状态
class PositionalState : public State
{
public:

    PositionalState();

public:

    static PositionalState* Instance();

    string ToString();
    
    void Initialize();
    
    void Enter(IPlayer* player);

    void Exit(IPlayer* player);
    
    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidatePositionalToOffBall(IPlayer* player, IState* preview);
};

#endif //__POSITIONALSTATE_H__
