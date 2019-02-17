/********************************************************************************
 * 文件名：IManagerStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IMANAGERSTATUS_H__
#define __IMANAGERSTATUS_H__

#include "../../Player/IPlayer.h"

class IDecreaseDebuffRateStatus;
class IImmunityDebuffSkillStatus;
class IDecreaseFoulLevelStatus;
class INoFoulLevelStatus;
class ITransferFoulLevelStatus;
class INoInjuredStatus;
class IDecreaseStaminaRateStatus;
class IImmunityDebuffsStatus;
class IMadmanStatus;

/// 表示了经理的当前回合状态
class IManagerStatus
{
public:

    virtual ~IManagerStatus() {}

public:

    /// 协防的概率
    virtual int                             HelpDefenseRate() = 0;
    virtual void                            SetHelpDefenseRate(int rate) = 0;

    /// 表示了最前面的球员
    virtual IPlayer*                        HeadMostPlayer() = 0;
    virtual void                            SetHeadMostPlayer(IPlayer* player) = 0;

    /// 表示了减免异常效果的状态
    virtual IDecreaseDebuffRateStatus*      GetDecreaseDebuffRateStatus() = 0;

    /// 表示了对对方减益效果的抵抗率
    virtual IImmunityDebuffSkillStatus*     GetImmunityDebuffSkillStatus() = 0;

    /// 表示了犯规时降低惩罚级别的状态
    virtual IDecreaseFoulLevelStatus*       GetDecreaseFoulLevelStatus() = 0;

    /// 表示了犯规不受任何制裁
    virtual INoFoulLevelStatus*             GetNoFoulLevelStatus() = 0;

    /// 表示了犯规时转移给对方的概率
    virtual ITransferFoulLevelStatus        GetTransferFoulLevelStatus() = 0;

    /// 表示了球员有一定概率不受伤的状态
    virtual INoInjuredStatus*               GetNoInjuredStatus() = 0;

    /// 表示了对体力下降影响的状态
    virtual IDecreaseStaminaRateStatus*     GetDecreaseStaminaRateStatus() = 0;

    /// 表示了各个异常状态和它的抵抗概率
    virtual IImmunityDebuffsStatus*         GetImmunityDebuffsStatus() = 0;

    /// 表示了狂人的状态
    virtual IMadmanStatus*                  GetMadmanStatus() = 0;
};

#endif //
