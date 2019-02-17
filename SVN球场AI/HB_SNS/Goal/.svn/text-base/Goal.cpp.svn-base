/********************************************************************************
 * 文件名：Goal.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "Goal.h"

#include "../common/RandomHelper/RandomHelper.h"

Goal* Goal::Instance()
{
    static Goal instance;

    return &instance;
}

Goal::Goal()
{
    m_ShootTargets.clear();

    m_ShootTargets[0]   = vector<ShootTarget>(4);
    m_ShootTargets[1]   = vector<ShootTarget>();
    m_ShootTargets[2]   = vector<ShootTarget>();
    m_ShootTargets[3]   = vector<ShootTarget>();
    m_ShootTargets[4]   = vector<ShootTarget>();

    for (int i = 0; i < 4; i++) 
    {
        m_ShootTargets[i].clear();
        m_ShootTargets[0].push_back(ShootTarget(i + 1, 0, true));
    }

    for (int x = 0; x <= 20; x++) 
    {
        for (int y = 0; y <= 7; y++) 
        {
            if ((y > 1) && ((x >= 2 && x <= 3) || (x >= 17 && x <= 18))) 
            {
                m_ShootTargets[1].push_back(ShootTarget(x, y));
                continue;
            }

            if ((y > 1) && ((x >= 4 && x <= 6) || (x >= 14 && x <= 16))) 
            {
                m_ShootTargets[2].push_back(ShootTarget(x, y));
                continue;
            }

            if ((y > 1) && ((x >= 7 && x <= 13))) 
            {
                m_ShootTargets[3].push_back(ShootTarget(x, y));
                continue;
            }

            m_ShootTargets[4].push_back(ShootTarget(x, y));                    
        }
    }
}

vector<ShootTarget>& Goal::DoorFrame() 
{
    return m_ShootTargets[0];
}


ShootTarget Goal::GetShootTargetByIndex(int index)
{
    return m_ShootTargets[index][RandomHelper::GetInt32(0, m_ShootTargets[index].size() - 1)];
}

ShootTarget Goal::GetRandomDoorFrame() 
{
    return m_ShootTargets[0][RandomHelper::GetInt32(0, m_ShootTargets[0].size() - 1)];
}
