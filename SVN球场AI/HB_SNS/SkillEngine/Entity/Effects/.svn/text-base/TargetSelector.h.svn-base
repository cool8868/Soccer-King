/********************************************************************************
 * 文件名：TargetSelector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __TARGETSELECTOR_H__
#define __TARGETSELECTOR_H__

#include "../../../common/common.h"
#include "../../../Interface/Player/IPlayer.h"

/// Represents the target's selector.
class TargetSelector
{
public:

    virtual ~TargetSelector() {}

public:

    /// Get the skill's target.
    /// <param name="target">
    /// 0-自己
    /// 1-自己相对的优先级最高的那一个人
    /// 2-自己相对的人（可能有2个或以上）
    /// 3-多名队友
    /// 4-多名对方
    /// 5-传球目标人
    /// <param name="targetPosition">
    /// Represents a string that defins the target's position.
    /// 0-任意
    /// 1-门将
    /// 2-后卫
    /// 3-中场
    /// 4-前锋
    vector<IPlayer*>    GetTarget(int target, vector<int>& targetPosition, IPlayer* player);

    /// Get the skill's target.
    /// <param name="target">
    /// 0-自己
    /// 1-自己相对的优先级最高的那一个人
    /// 2-自己相对的人（可能有2个或以上）
    /// 3-多名队友
    /// 4-多名对方
    /// <param name="targetPosition">
    /// 0-任意
    /// 1-门将
    /// 2-后卫
    /// 3-中场
    /// 4-前锋
    vector<IPlayer*>    GetTarget(int target, vector<int>& targetPosition, IPlayer* player, vector<int>& coordinates);
};

#endif //__TARGETSELECTOR_H__
