/********************************************************************************
 * 文件名：MatchProcess.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "MatchProcess.h"
#include "../common/String/String.h"

MatchProcess::MatchProcess()
    : m_Id(2)
{
    InitVariables();
}

void MatchProcess::InitVariables()
{
    m_Score         = String::Empty();

    m_HomeScore     = 0;
    m_AwayScore     = 0;

    m_Goal          = false;

    m_Interruption  = false;
    m_Foul          = false;
    m_Break         = false;
}

void MatchProcess::Output(CUtlBuffer& writer)
{
    Process::Output(writer, m_Id);

    //
    // HomeScore    8bits
    // AwayScore    8bits
    // Goal         4bits
    // Interruption 2bit
    // Foul         1bit
    // Break        1bit
    //
    writer.PutChar(m_HomeScore);
    writer.PutChar(m_AwayScore);
    writer.PutChar(m_Goal << 4 | m_Interruption << 2 | m_Foul << 1 | m_Break);
}

void MatchProcess::Output(ofstream& writer)
{
    Process::Output(writer, m_Id);

    //
    // HomeScore    8bits
    // AwayScore    8bits
    // Goal         4bits
    // Interruption 2bit
    // Foul         1bit
    // Break        1bit
    //
    writer.put(m_HomeScore);
    writer.put(m_AwayScore);
    writer.put(m_Goal << 4 | m_Interruption << 2 | m_Foul << 1 | m_Break);
}

void MatchProcess::Read(ifstream& reader)
{
    //id和回合已经读取，不需要再读

    m_HomeScore     = reader.get() & 0xff;
    m_AwayScore     = reader.get() & 0xff;

    int value       = reader.get() & 0xff;
    m_Goal          = value >> 4 & 0xf;
    m_Interruption  = value & 0xc;
    m_Foul          = value & 0x2;
    m_Break         = value & 0x1;
}

void MatchProcess::Read(CUtlBuffer& reader)
{
    //id和回合已经读取，不需要再读

    m_HomeScore     = reader.GetChar() & 0xff;
    m_AwayScore     = reader.GetChar() & 0xff;

    int value       = reader.GetChar() & 0xff;
    m_Goal          = value >> 4 & 0xf;
    m_Interruption  = value & 0xc;
    m_Foul          = value & 0x2;
    m_Break         = value & 0x1;
}

