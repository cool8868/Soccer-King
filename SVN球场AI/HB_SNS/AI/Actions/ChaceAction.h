/********************************************************************************
 * 文件名：ChaceAction.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __CHASEACTION_H__
#define __CHASEACTION_H__

class ChaceAction
{
public:

    virtual ~ChaceAction() {}

public:

    ChaceAction* Instance()
    {
        static ChaceAction instance;

        return &instance;
    }

public:

    void Action(IPlayer* player) 
    {
        player->Move();
    }
};

#endif //__CHASEACTION_H__
