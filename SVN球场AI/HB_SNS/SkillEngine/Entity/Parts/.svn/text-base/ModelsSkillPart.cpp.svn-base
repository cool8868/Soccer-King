/********************************************************************************
 * 文件名：ModelsSkillPart.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ModelsSkillPart.h"

ModelsSkillPart::ModelsSkillPart()
{
    InitVariables();
}

ModelsSkillPart::ModelsSkillPart(ModelsSkillPart& rf)
{
    InitVariables();

    foreach (ModelRecord* record, rf.Models()) 
    {
        m_Models.push_back(record->Clone());
    }
}

void ModelsSkillPart::InitVariables()
{
    m_Models.reserve(4);
    m_Models.clear();
}

//////////////////////////////////////////////////////////////////////////
ModelRecord::ModelRecord()
{
    InitVariables();
}

ModelRecord::ModelRecord(ModelRecord& rf)
{
    m_Mid               = rf.Mid();
    m_Last              = rf.Last();
    m_Target            = rf.Target();
    m_TargetPosition    = rf.TargetPosition();
}

void ModelRecord::InitVariables()
{
    m_Mid       = 0;
    m_Last      = 0;
    m_Target    = 0;

    m_TargetPosition.clear();
}

