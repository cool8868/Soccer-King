/********************************************************************************
 * 文件名：StopballAction.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __STOPBALLACTION_H__
#define __STOPBALLACTION_H__

#include "../../common/common.h"
#include "../../Interface/IAction.h"

class StopballAction: public IAction
{
public:

    /// The player to stop the football.
    void Action(IPlayer* player) 
    {
        double x = player->BuildinAttribute()->Width() * cos((double)player->Status()->Angle()) + player->Current().X;
        double y = player->BuildinAttribute()->Width() * sin((double)player->Status()->Angle()) + player->Current().Y;

        player->GetMatch()->GetFootball()->MoveTo(Coordinate(x, y));
        player->Status()->SetHasball(true);
        player->Status()->SetSpeed(player->Status()->Speed() / 2);
    }
};

#endif //__STOPBALLACTION_H__
