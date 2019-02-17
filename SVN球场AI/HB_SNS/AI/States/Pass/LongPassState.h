/********************************************************************************
 * 文件名：LongPassState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __LONGPASSSTATE_H__
#define __LONGPASSSTATE_H__

#include "../PassState.h"

class LongPassState : public PassState
{
public:

    LongPassState();

public:

    static LongPassState* Instance();

    void Initialize();
    
    void Action(IPlayer* player);
    
    string ToString();
    
    void Enter(IPlayer* player);
    
    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateLongPassToPass(IPlayer* player, IState* preview);
};

#endif //__LONGPASSSTATE_H__
