/********************************************************************************
 * 文件名：ManagerStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MANAGERSTATUS_H__
#define __MANAGERSTATUS_H__

#include "../../Interface/Manager/Status/IManagerStatus.h"
#include "DecreaseStaminaRateStatus.h"

class ManagerStatus : public IManagerStatus
{
public:

    ManagerStatus() 
    {
        InitVariables();
    }

public:

    int                             HelpDefenseRate() { return m_HelpDefenseRate; }
    void                            SetHelpDefenseRate (int rate) { m_HelpDefenseRate = rate; }

    IPlayer*                        HeadMostPlayer() { return m_HeadMostPlayer; }
    void                            SetHeadMostPlayer(IPlayer* player) { m_HeadMostPlayer = player; }

    IDecreaseDebuffRateStatus*      DecreaseDebuffRateStatus() { return m_DecreaseDebuffStatus; }

    IImmunityDebuffSkillStatus*     ImmunityDebuffSkillStatus() { return m_ImmunityDebuffSkillStatus; }

    IDecreaseFoulLevelStatus*       DecreaseFoulLevelStatus() { return m_DecreaseFoulLevelStatus; }

    INoFoulLevelStatus*             NoFoulLevelStatus() { return m_NoFoulLevelStatus; }

    ITransferFoulLevelStatus*       TransferFoulLevelStatus() { return m_TransferFoulLevelStatus; }

    INoInjuredStatus*               NoInjuredStatus() { return m_NoInjuredStatus; }

    IDecreaseStaminaRateStatus*     DecreaseStaminaRateStatus() { return m_DecreaseStaminaRateStatus; }

    IImmunityDebuffsStatus*         ImmunityDebuffsStatus() { return m_ImmunityDebuffsStatus; }

    IMadmanStatus*                  MadmanStatus() { return m_MadmanStatus; }

protected:

    /// 协防的概率
    int                             m_HelpDefenseRate;

    /// 表示了最前面的球员
    IPlayer*                        m_HeadMostPlayer;

    /// 表示了减免异常效果的状态
    IDecreaseDebuffRateStatus*      m_DecreaseDebuffStatus;

    /// 表示了对对方减益效果的抵抗率
    IImmunityDebuffSkillStatus*     m_ImmunityDebuffSkillStatus;

    /// 表示了犯规时降低惩罚级别的状态
    IDecreaseFoulLevelStatus*       m_DecreaseFoulLevelStatus;

    /// 表示了犯规不受任何制裁
    INoFoulLevelStatus*             m_NoFoulLevelStatus;

    /// 表示了犯规时转移给对方的概率
    ITransferFoulLevelStatus*       m_TransferFoulLevelStatus;

    /// 表示了球员有一定概率不受伤的状态
    INoInjuredStatus*               m_NoInjuredStatus;

    /// 表示了对体力下降影响的状态
    IDecreaseStaminaRateStatus*     m_DecreaseStaminaRateStatus;

    /// 表示了各个异常状态和它的抵抗概率
    IImmunityDebuffsStatus*         m_ImmunityDebuffsStatus;
    
    /// 狂人技能的相关状态，当对方进球后，对方的属性下降，本方的属性提升
    IMadmanStatus*                  m_MadmanStatus;

private:

    void InitVariables()
    {
        m_HelpDefenseRate = 50;

        m_HeadMostPlayer                = NULL;

        m_DecreaseDebuffStatus          = NULL;

        m_ImmunityDebuffSkillStatus     = NULL;

        m_DecreaseFoulLevelStatus       = NULL;

        m_NoFoulLevelStatus             = NULL;

        m_TransferFoulLevelStatus       = NULL;

        m_NoInjuredStatus               = NULL;

        m_DecreaseStaminaRateStatus     = NULL;

        m_ImmunityDebuffsStatus         = NULL;

        m_MadmanStatus                  = NULL;
    }
};


#endif //__MANAGERSTATUS_H__
