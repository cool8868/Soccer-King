/********************************************************************************
 * 文件名：ManagerData.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ManagerData.h"

ManagerData::ManagerData()
{
    InitVariables();
}

ManagerData::~ManagerData()
{
    foreach (IRawPlayer* player, m_Players)
    {
        DeletePtrSafe(player);
    }
    m_Players.clear();
}

void ManagerData::InitVariables()
{
    m_Id            = 0;
    m_FormationId   = 0;

    m_Players.reserve(Defines_Match.MAX_PLAYER_COUNT);
    m_Players.clear();

    // 这里不要调用clear
    m_PassiveSkills.reserve(8);
    m_OpenerSkills.reserve(2);
}
