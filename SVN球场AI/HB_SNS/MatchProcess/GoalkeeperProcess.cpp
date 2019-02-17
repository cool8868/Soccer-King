/********************************************************************************
 * 文件名：GoalkeeperProcess.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "GoalkeeperProcess.h"

GoalkeeperProcess::GoalkeeperProcess()
    : PlayerProcess(3)
{
    InitVariables();
}

void GoalkeeperProcess::InitVariables()
{
    m_IsBackward        = false;
    m_IsStandUp         = false;
    m_DiveDirection     = 0;
}

void GoalkeeperProcess::Output(CUtlBuffer& writer)
{
    PlayerProcess::Output(writer, m_Id);

    writer.PutChar(m_IsBackward << 7 | m_IsStandUp << 6 | m_DiveDirection);
}

void GoalkeeperProcess::Output(ofstream& writer)
{
    PlayerProcess::Output(writer, m_Id);

    writer.put(m_IsBackward << 7 | m_IsStandUp << 6 | m_DiveDirection);
}

void GoalkeeperProcess::Read(ifstream& reader)
{
    PlayerProcess::ReadInvoke(reader);

    int value       = reader.get() & 0xff;
    m_IsBackward    = value >> 7 & 0x1;
    m_IsStandUp     = value >> 6 & 0x1;
    m_DiveDirection = value & 0x3f;
}

void GoalkeeperProcess::Read(CUtlBuffer& reader)
{
    PlayerProcess::ReadInvoke(reader);

    int value       = reader.GetChar() & 0xff;
    m_IsBackward    = value >> 7 & 0x1;
    m_IsStandUp     = value >> 6 & 0x1;
    m_DiveDirection = value & 0x3f;
}

