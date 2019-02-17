/********************************************************************************
 * 文件名：ShortPassState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SHORTPASSSTATE_H__
#define __SHORTPASSSTATE_H__

#include "../PassState.h"

class ShortPassState : public PassState
{
public:

    ShortPassState();

public:

    static ShortPassState* Instance();

    void Initialize();
    
    void Action(IPlayer* player);
    
    string ToString();
    
    void Enter(IPlayer* player);

    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateShortPassToPass(IPlayer* player, IState* preview);
};

#endif //__SHORTPASSSTATE_H__
