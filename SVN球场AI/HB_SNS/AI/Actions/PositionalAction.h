/********************************************************************************
 * 文件名：PositionalAction.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __POSITIONALACTION_H__
#define __POSITIONALACTION_H__

#include "../../Interface/IAction.h"

class PositionalAction: public IAction
{
public:

    PositionalAction* Instance()
    {
        static PositionalAction instance;

        return &instance;
    }

public:

    void Action(IPlayer* player) 
    {
        player->Move();
    }
};

#endif //__POSITIONALACTION_H__
