/********************************************************************************
 * 文件名：GKHoldBallState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __GKHOLDBALLSTATE_H__
#define __GKHOLDBALLSTATE_H__

#include "../IdleState.h"

/// 表示了守门员持球不动的动作
class GKHoldBallState : public IdleState
{
public:

    GKHoldBallState();

public:

    static GKHoldBallState* Instance();

    void Initialize();

    string ToString();
    
    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateGKHoldToAction(IPlayer* player, IState* preview);
};

#endif //__GKHOLDBALLSTATE_H__
