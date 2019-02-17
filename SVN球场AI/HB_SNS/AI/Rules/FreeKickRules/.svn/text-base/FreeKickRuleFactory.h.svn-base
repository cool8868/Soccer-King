/********************************************************************************
 * 文件名：FreeKickRuleFactory.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FREEKICKRULEFACTORY_H__
#define __FREEKICKRULEFACTORY_H__

#include "FreeKickRule.h"
#include "PenaltyKickRule.h"
#include "IndirectFreeKickRule.h"
#include "DirectFreeKickRule.h"

/// Represents the factory of the FreekickRule class.
class FreeKickRuleFactory
{
public:

    static FreeKickRuleFactory* Instance()
    {
        static FreeKickRuleFactory instance;

        return &instance;
    }

    /// 进行一次任意球
    /// 表示了开球方的经理
    /// 表示了当前的开球点
    void Start(IManager* manager, Coordinate point)
    {
        // 判定是直接任意球还是间接任意球
        FreeKickRule* rule = NULL;
        double distance = point.Distance((manager->GetSide() == Side_Home) ? manager->GetMatch()->GetPitch()->AwayGoal() : manager->GetMatch()->GetPitch()->HomeGoal());

        // 如果在禁区为点球
        Region region = (manager->GetSide() == Side_Home) ? manager->GetMatch()->GetPitch()->AwayPenaltyRegion() : manager->GetMatch()->GetPitch()->HomePenaltyRegion();
        if (region.IsCoordinateInRegion(point))
        {
            point = Coordinate(25, 68);
            if (manager->GetSide() == Side_Home)
            {
                point = point.Mirror();
            }

            manager->GetMatch()->GetFootball()->MoveTo(point);
            
            PenaltyKickRule kickRule(manager, point);
            rule = &kickRule;
            rule->Start();

            return;
        }

        // 角度太小则为间接任意球
        if (point.Y < manager->GetMatch()->GetPitch()->HomePenaltyRegion().Start.Y - 10 || point.Y > manager->GetMatch()->GetPitch()->HomePenaltyRegion().End.Y + 10)
        {
            if ((manager->GetSide() == Side_Home) && (point.X > manager->GetMatch()->GetPitch()->AwayPenaltyRegion().Start.X))
            {
                IndirectFreeKickRule kickRule(manager, point);
                rule = &kickRule;
                rule->Start();

                return;
            }

            if ((manager->GetSide() == Side_Away) && (point.X < manager->GetMatch()->GetPitch()->HomePenaltyRegion().End.X))
            {
                IndirectFreeKickRule kickRule(manager, point);
                rule = &kickRule;
                rule->Start();

                return;
            }
        }

        if (distance < 60)
        {
            DirectFreeKickRule kickRule(manager, point);
            rule = &kickRule;
            rule->Start();
        }
        else
        {
            IndirectFreeKickRule kickRule(manager, point);
            rule = &kickRule;
            rule->Start();
        }
    }
};

#endif //__FREEKICKRULEFACTORY_H__
