/********************************************************************************
 * 文件名：Pitch.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PITCH_H__
#define __PITCH_H__

#include "../Interface/IPitch.h"

/// 表示了球场的接口
class Pitch: public IPitch
{
public:

    Pitch();

    virtual ~Pitch() {}

public:

    IGoal*              GetGoal() { return m_Goal; }
    void                SetGoal(IGoal* goal) { m_Goal = goal; }

    Coordinate          HomeGoal() { return m_HomeGoal; }

    Coordinate          AwayGoal() { return m_AwayGoal; }

    Region              HomeShootRegion() { return m_HomeShootRegion; }
    
    Region              AwayShootRegion() { return m_AwayShootRegion; }

    Region              HomePenaltyRegion() { return m_HomePenaltyRegion; }

    Region              AwayPenaltyRegion() { return m_AwayPenaltyRegion; }

    Region              HomeForcePassRegion() { return m_HomeForcePassRegion; }

    Region              AwayForcePassRegion() { return m_AwayPenaltyRegion; }

    MapDirection_Line&  HomeDestinations() { return m_HomeDestinations; }
    MapDirection_Line&  AwayDestinations() { return m_AwayDestinations; }

protected:

    /// 表示了一个通用的球门
    IGoal* m_Goal;

    /// 表示了主队球门的中心
    Coordinate m_HomeGoal;

    /// 表示了客队球门的中心
    Coordinate m_AwayGoal;

    /// 表示了客队球员直接射门区域
    Region m_HomeShootRegion;

    /// 表示了主队球员直接射门区域
    Region m_AwayShootRegion;

    /// 表示了主队的禁区
    Region m_HomePenaltyRegion;

    /// 表示了客队的禁区
    Region m_AwayPenaltyRegion;

    /// 表示了主队的行动目标点
    MapDirection_Line m_HomeDestinations;

    /// 表示了客队的行动目标点
    MapDirection_Line m_AwayDestinations;

    /// 表示了客队球员强制传球的区域
    Region m_HomeForcePassRegion;

    /// 表示了主队球员强制传球的区域
    /// (表示了右侧客队的区域)
    Region m_AwayForcePassRegion;
};

#endif //__PITCH_H__
