/********************************************************************************
 * 文件名：ShootProcess.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "ShootProcess.h"

ShootProcess::ShootProcess()
    : PlayerProcess(5)
{
    InitVariables();
}

void ShootProcess::InitVariables()
{
    m_ShootTarget       = String::Empty();
    
    m_ShootTargetIndex  = 0;
    m_ShootSpeed        = 0;
}

void ShootProcess::Output(CUtlBuffer& writer)
{
    PlayerProcess::Output(writer, m_Id);

    writer.PutChar(m_ShootTargetIndex);

    if (m_ShootTargetIndex == 0)
    {
        writer.PutChar(m_ShootTarget2.X);
    }
    else
    {
        writer.PutChar(m_ShootTarget2.X);
        writer.PutChar(m_ShootTarget2.Y);
    }
}

void ShootProcess::Output(ofstream& writer)
{
    PlayerProcess::Output(writer, m_Id);

    writer.put(m_ShootTargetIndex);

    if (m_ShootTargetIndex == 0)
    {
        writer.put(m_ShootTarget2.X);
    }
    else
    {
        writer.put(m_ShootTarget2.X);
        writer.put(m_ShootTarget2.Y);
    }
}

void ShootProcess::Read(ifstream& reader)
{
    PlayerProcess::ReadInvoke(reader);

    m_ShootTargetIndex  = reader.get() & 0xff;
    if (m_ShootTargetIndex == 0)
    {
        m_ShootTarget2.X = reader.get() & 0xff;
    }
    else
    {
        m_ShootTarget2.X = reader.get() & 0xff;
        m_ShootTarget2.Y = reader.get() & 0xff;   
    }
}

void ShootProcess::Read(CUtlBuffer& reader)
{
    PlayerProcess::ReadInvoke(reader);

    m_ShootTargetIndex  = reader.GetChar() & 0xff;
    if (m_ShootTargetIndex == 0)
    {
        m_ShootTarget2.X = reader.GetChar() & 0xff;
    }
    else
    {
        m_ShootTarget2.X = reader.GetChar() & 0xff;
        m_ShootTarget2.Y = reader.GetChar() & 0xff;   
    }
}

