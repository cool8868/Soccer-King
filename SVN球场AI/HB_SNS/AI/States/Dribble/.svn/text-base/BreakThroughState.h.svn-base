/********************************************************************************
 * 文件名：BreakThroughState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __BREAKTHROUGHSTATE_H__
#define __BREAKTHROUGHSTATE_H__

#include "../DribbleState.h"

/// 表示了球员的过人状态
class BreakThroughState : public DribbleState
{
public:

    BreakThroughState();

public:

    static BreakThroughState* Instance();
    
    void Initialize();
    
    void Enter(IPlayer* player);

    string ToString();

    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateBreakThroughToAction(IPlayer* player, IState* preview);
};

#endif //__BREAKTHROUGHSTATE_H__
