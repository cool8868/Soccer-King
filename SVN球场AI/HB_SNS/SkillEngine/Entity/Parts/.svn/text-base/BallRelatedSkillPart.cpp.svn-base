/********************************************************************************
 * 文件名：BallRelatedSkillPart.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "BallRelatedSkillPart.h"

BallRelatedSkillPart::BallRelatedSkillPart()
{
    InitVariables();
}

BallRelatedSkillPart::BallRelatedSkillPart(BallRelatedSkillPart& rf)
{
    InitVariables();

    foreach (BallRelated* b, rf.BallRelateds())
    {
        m_BallRealateds.push_back(b->Clone());
    }
}

BallRelatedSkillPart::~BallRelatedSkillPart()
{
    foreach (BallRelated* b, m_BallRealateds)
    {
        delete b;
    }

    m_BallRealateds.clear();
}

void BallRelatedSkillPart::InitVariables()
{
    m_BallRealateds.reserve(1);
    m_BallRealateds.clear();
}

//////////////////////////////////////////////////////////////////////////
BallRelated::BallRelated()
{
    InitVariables();
}

BallRelated::BallRelated(BallRelated& rf)
{
    InitVariables();

    m_Action    = rf.Action();
    m_Last      = rf.Last();
}

void BallRelated::InitVariables()
{
    m_Action    = 0;
    m_Last      = 0;
}
