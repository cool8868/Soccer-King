/********************************************************************************
 * 文件名：DefaultDribbleState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DEFAULTDRIBBLESTATE_H__
#define __DEFAULTDRIBBLESTATE_H__

#include "../DribbleState.h"

/// Represents a state that defines the default dribble state.
class DefaultDribbleState : public DribbleState
{
public:

    DefaultDribbleState();

public:

    static DefaultDribbleState* Instance();

    void Initialize();
    
    string ToString();

    void Enter(IPlayer* player);

    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:
    
    static bool ValidateDefaultDribbleToDribble(IPlayer* player, IState* preview);
    static bool ValidateDefaultDribbleToDefaultDribble(IPlayer* player, IState* preview);
};

#endif //__DEFAULTDRIBBLESTATE_H__
