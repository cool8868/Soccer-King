/********************************************************************************
 * 文件名：FreekickShootState.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FREEKICKSHOOTSTATE_H__
#define __FREEKICKSHOOTSTATE_H__

#include "../ShootState.h"

/// 表示了任意球直接射门状态
class FreekickShootState : public ShootState
{
public:

    FreekickShootState();

public:
    
    static FreekickShootState* Instance();

    void Initialize();

    /// 触发一次任意球直接射门
    void Action(IPlayer* player);
    
    string ToString();

    IState* QuickDecide(IPlayer* player, IState* preview);
    
    //是否还是在当前状态
    bool IsInState(IState* st);

private:

    static bool ValidateFreekickShootToShoot(IPlayer* player, IState* preview);
};

#endif //__FREEKICKSHOOTSTATE_H__
