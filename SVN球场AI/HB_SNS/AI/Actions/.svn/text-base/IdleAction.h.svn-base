/********************************************************************************
 * 文件名：IdleAction.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IDLEACTION_H__
#define __IDLEACTION_H__

#include "../../Interface/IAction.h"

class IdleAction: public IAction
{
public:

    /// Player to make a action.
    void Action(IPlayer* player) 
    {
        player->Redecide();
    }
};

#endif //__IDLEACTION_H__
