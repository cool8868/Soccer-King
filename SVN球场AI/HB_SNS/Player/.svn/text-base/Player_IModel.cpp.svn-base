/********************************************************************************
 * 文件名：Player_IModel.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "Player.h"

/// 为一个球员添加一个模型
void Player::AddModel(int model, int last, bool isHoldBall)
{
    m_Status->GetModelStatus()->SetIsHoldBall(isHoldBall);
    m_Status->GetModelStatus()->SetMid(model);
    m_Status->GetModelStatus()->SetRemainTime(last);
}

/// 为一个球员添加一个模型
void Player::AddModel(int model, int last)
{
    m_Status->GetModelStatus()->SetIsHoldBall(false);
    m_Status->GetModelStatus()->SetMid(model);
    m_Status->GetModelStatus()->SetRemainTime(last);
}

/// 刷新模型效果
void Player::RefreshModel()
{
    if (m_Status->GetModelStatus()->RemainTime() <= 0)
    {
        m_Status->GetModelStatus()->SetMid(0);
        m_Status->GetModelStatus()->SetIsHoldBall(false);
        return;
    }

    if (m_Status->GetModelStatus()->IsHoldBall()) // 持球效果
    {
        if (!m_Status->Hasball())
        {
            m_Status->GetModelStatus()->SetIsHoldBall(false);
            m_Status->GetModelStatus()->SetRemainTime(0);
            m_Status->GetModelStatus()->SetMid(0);
        }

        return;
    }

    m_Status->GetModelStatus()->DecRemainTime();            
}
