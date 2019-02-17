/********************************************************************************
 * 文件名：
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IMANAGER_H__
#define __IMANAGER_H__

#include "Status/IManagerStatus.h"
#include "../IRequestInitialize.h"

#include "../../common/common.h"
#include "../../common/Enum/Side.h"
#include "../../common/Enum/Position.h"

#include "../Player/IPlayer.h"
#include "../IMatch.h"

class IOpenerSkill;

/// 表示了一个经理(经理表示了玩家)
class IManager: public IRequestInitialize
{
public:

    virtual ~IManager() {}

public:

    /// 进球
    virtual void                Goal() = 0;

    /// 新的回合到来时初始化
    virtual void                RoundInit() = 0;

    /// 新的分钟开始时初始化
    virtual void                MinuteInit() = 0;

    /// 下半场开始
    virtual void                SecondHalfStart() = 0;

    /// 表示了当前队伍的球员犯规
    virtual void                Foul() = 0;

    /// 守门员开球门球
    virtual void                GkOpenball() = 0;

    /// 获取球员通过场上位置
    virtual vector<IPlayer*>    GetPlayersByPosition(Position position) = 0;

    /// 触发Opener技能
    virtual void                TriggerOpenerSkill(bool isFirstHalf) = 0;

    /// 清除所有球员的所有异常状态
    virtual void                ClearDisable() = 0;

public:

    /// 表示了经理的ID
    virtual unsigned int        Id() = 0;

    /// 表示了经理的中文名
    virtual string              Name() = 0;

    /// 表示了经理的英文名
    virtual string              SpellName() = 0;

    /// 表示了阵型id
    virtual int                 FormationId() = 0;

    /// 表示了经理的Logo
    virtual string              Logo() = 0;
    virtual void                SetLogo(string logo) = 0;

    /// 表示了对正常比赛的对象
    virtual IMatch*             GetMatch() = 0;

    /// 表示了对手经理
    virtual IManager*           Opponent() = 0;

    /// 表示了经理是主队或客队
    virtual Side                GetSide() = 0;

    /// 表示了经理的当前状态
    virtual IManagerStatus*     Status() = 0;

    /// 表示了球员的集合
    virtual vector<IPlayer*>&   Players() = 0;

    /// 表示了经理的被动技能集合
    virtual vector<string>&     PassiveSkills() = 0;

    /// 表示了经理的Opener技能
    /// 每个经理只有2个Opener技能
    /// 第一个表示上半场触发
    /// 第二个表示下半场触发
    virtual vector<IOpenerSkill*>&  OpenerSkills() = 0;
};

#endif  //__IMANAGER_H__
