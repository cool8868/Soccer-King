/********************************************************************************
 * 文件名：AbsBuff.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "AbsBuff.h"

AbsBuff::AbsBuff(int triggerId, int time, BuffType type) 
{
    InitVariables();

    m_TriggerId     = triggerId;
    m_Time          = time;
    m_Last          = time;
    m_Type          = type;
}

void AbsBuff::SetPropertyId(vector<int>& vecIds)
{
    //直接赋值
    m_VectorPropertyId = vecIds;
}

void AbsBuff::TimeLapse() 
{
    m_Time--;

    if (m_Time < 0) 
    {
        m_Time = 0;
    }
}

void AbsBuff::InitVariables()
{
    m_Id            = 0;
    m_TriggerId     = 0;
    m_Isbuff        = false;
    m_Time          = 0;
    m_Last          = 0;
    m_Rate          = 0.0;

    m_VectorPropertyId.clear();
}
