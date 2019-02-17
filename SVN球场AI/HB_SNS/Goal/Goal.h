/********************************************************************************
 * 文件名：Goal.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __GOAL_H__
#define __GOAL_H__

#include "../Interface/IGoal.h"

class Goal : public IGoal 
{
public:

    Goal();

    static Goal* Instance();

public:

    vector<ShootTarget>&        DoorFrame();

    //////////////////////////////////////////////////////////////////////////
    /// 通过index获取一个射门目标
    ShootTarget                 GetShootTargetByIndex(int index);

    /// 获取一个随机门框
    ShootTarget                 GetRandomDoorFrame();

protected:

    /// 表示了一个球门的多个门框区域
    vector<ShootTarget> m_DoorFrame;

    map<int, vector<ShootTarget> > m_ShootTargets;
};

#endif //__GOAL_H__
