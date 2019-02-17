/********************************************************************************
 * 文件名：DefaultShootState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DEFAULTSHOOTSTATE_H__
#define __DEFAULTSHOOTSTATE_H__

#include "../ShootState.h"

/// 表示了普通射门状态
class DefaultShootState : public ShootState
{
public:

    DefaultShootState();

public:

    static DefaultShootState* Instance();

    void Initialize();

    /// 普通射门行动
    void Action(IPlayer* player);

    string ToString();

    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:
    
    static bool ValidateDefaultShootToShoot(IPlayer* player, IState* preview);
};

#endif //__DEFAULTSHOOTSTATE_H__
