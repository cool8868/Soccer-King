/********************************************************************************
 * 文件名：VolleyShootState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __VOLLEYSHOOTSTATE_H__
#define __VOLLEYSHOOTSTATE_H__

#include "../ShootState.h"

/// 表示了大力抽射的状态
class VolleyShootState : public ShootState
{
public:

    VolleyShootState();

public:

    static VolleyShootState* Instance();

    void Initialize();

    /// 触发一次大力射门
    void Action(IPlayer* player);

    string ToString();

    IState* QuickDecide(IPlayer* player, IState* preview);

    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateVolleyShootToShoot(IPlayer* player, IState* preview);
};

#endif //__VOLLEYSHOOTSTATE_H__
