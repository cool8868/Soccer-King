/********************************************************************************
 * 文件名：Creature.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/

#include "Creature.h"
#include "../Player/Player.h"
#include "../Interface/Player/IPlayer.h"
#include "AbsHoldBallBuff.h"

Creature::Creature()
    : m_MAX_BUFF_COUNT(20)
    , m_BuffBar(m_MAX_BUFF_COUNT)
    , m_DebuffBar(m_MAX_BUFF_COUNT)
{
    InitVariables();
}

void Creature::AddBuff(AbsBuff* buff)
{
    if (buff->IsBuff() == false)
    {
        throw ErrorBuffTypeException(String::Format("Insert a debuff to buff bar. arguments buff:{%d}", buff->Id()));
    }

    if (m_OnAddBuff != NULL)
    {
        m_OnAddBuff->Invoke(AddBuffEventArgs(buff));
    }

    m_BuffBar.push_back(buff);
}

void Creature::AddDebuff(AbsBuff* debuff)
{
    if (debuff->IsBuff())
    {
        throw ErrorBuffTypeException(String::Format("Insert a buff to debuff bar. arguments debuff:{%f}", debuff->Id()));
    }

    if (ValidateImmunityAddDebuff())
    {
        return;
    }

    if (m_OnAddDebuff != NULL)
    {
        m_OnAddDebuff->Invoke(AddDebuffEventArgs(debuff));
    }

    m_DebuffBar.push_back(debuff);
}

void Creature::RefreshBuff()
{
    vector<AbsBuff*> removeList(m_BuffBar.size());
    removeList.clear();

    for (size_t i = 0; i < m_BuffBar.size(); i++)
    {
        if (m_BuffBar[i]->Type() == BuffType_HoldBall)
        {
            if (PointerCastSafe(IPlayer, this)->Status()->Hasball() == false)
            {
                PointerCastSafe(AbsHoldBallBuff, m_BuffBar[i])->RemoveBuff();
            }
        }

        if (m_BuffBar[i]->Type() != BuffType_Dot)
        {
            m_BuffBar[i]->TimeLapse();
        }

        if (m_BuffBar[i]->Time() == 0)
        {
            removeList.push_back(m_BuffBar[i]);
        }
    }

    foreach (AbsBuff* buff, removeList)
    {
        if (m_OnRemoveBuff != NULL)
        {
            m_OnRemoveBuff->Invoke(RemoveBuffEventArgs(buff));

            vector<AbsBuff*>::iterator iter = find(m_BuffBar.begin(), m_BuffBar.end(), buff);
            if (iter != m_BuffBar.end())
            {
                m_BuffBar.erase(iter);
            }
        }
    }
}

void Creature::RefreshDebuff()
{
    vector<AbsBuff*> removeList(m_DebuffBar.size());
    removeList.clear();

    for (size_t i = 0; i < m_DebuffBar.size(); i++)
    {
        if (m_DebuffBar[i]->Type() != BuffType_Dot)
        {
            m_DebuffBar[i]->TimeLapse();
        }

        if (m_DebuffBar[i]->Time() == 0)
        {
            removeList.push_back(m_DebuffBar[i]);
        }
    }

    foreach (AbsBuff* buff, removeList)
    {
        if (m_OnRemoveDebuff != NULL)
        {
            m_OnRemoveDebuff->Invoke(RemoveDebuffEventArgs(buff));

            vector<AbsBuff*>::iterator iter = find(m_DebuffBar.begin(), m_DebuffBar.end(), buff);
            if (iter != m_DebuffBar.end())
            {
                m_DebuffBar.erase(iter);
            }
        }
    }
}

void Creature::InitVariables()
{
    m_BuffBar.clear();
    m_DebuffBar.clear();
    
    m_OnAddDebuff       = NULL;
    m_OnRemoveDebuff    = NULL;
    m_OnAddBuff         = NULL;
    m_OnRemoveBuff      = NULL;
}


