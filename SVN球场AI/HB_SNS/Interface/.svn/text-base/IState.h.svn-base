/********************************************************************************
 * 文件名：IState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ISTATE_H__
#define __ISTATE_H__

#include "../common/common.h"

class IPlayer;
class Process;

class IState
{
public:

    virtual ~IState() {};

public:
    /// 初始化状态
    virtual void Initialize() = 0;

    /// 思考下一个状态
    virtual IState* Decide(IPlayer* player, IState* preview) = 0;

    /// 高速思考下一个状态
    virtual IState* QuickDecide(IPlayer* player, IState* preview) = 0;  

    /// 进入一个状态
    virtual void Enter(IPlayer* player) = 0;

    virtual void Exit(IPlayer* player) = 0;

    //是否还是在当前状态
    virtual bool IsInState(IState* st) = 0;

    /// 保存当前状态
    virtual Process* Save(IPlayer* player) = 0;

    //IAction
    virtual void Action(IPlayer* player) = 0;

    virtual string ToString() = 0;

public:

    virtual unsigned int    Id() = 0;

    virtual int             ClientId() = 0;
    virtual void            SetClientId(int id) = 0;

    virtual int             TimeLast() = 0;
    virtual void            SetTimeLast(int time) = 0;

    virtual bool            Stopable() = 0;
    virtual void            SetStopable(bool stopable) = 0;
};


#endif //__ISTATE_H__
