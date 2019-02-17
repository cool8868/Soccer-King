/********************************************************************************
 * 文件名：FootballProcess.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "FootballProcess.h"

FootballProcess::FootballProcess()
    : m_Id(1)
{
    InitVariables();
}

void FootballProcess::InitVariables()
{
    m_Round         = 0;
    m_Angle         = 0;

    m_Visible       = false;
    m_IsInAir       = false;

    m_Acceleration  = 0;

    m_Destination   = String::Empty();
    m_Coordinate    = String::Empty();
}

void FootballProcess::Output(CUtlBuffer& writer)
{
    MoveableProcess::Output(writer, m_Id);

    //
    // angle    6bits   2^6 = 64
    // isInair  1bit
    // visible  1bit
    //
    writer.PutChar(m_Angle << 2 | m_IsInAir << 1 | m_Visible);
    writer.PutChar(m_Destination2.X);
    writer.PutChar(m_Destination2.Y);
}

void FootballProcess::Output(ofstream& writer)
{
    MoveableProcess::Output(writer, m_Id);

    //
    // angle    6bits   2^6 = 64
    // isInair  1bit
    // visible  1bit
    //
    writer.put(m_Angle << 2 | m_IsInAir << 1 | m_Visible);
    writer.put(m_Destination2.X);
    writer.put(m_Destination2.Y);
}

void FootballProcess::Read(ifstream& reader)
{
    MoveableProcess::ReadInvoke(reader);

    //
    // angle    6bits   2^6 = 64
    // isInair  1bit
    // visible  1bit
    //
    int value           = reader.get() & 0xff;
    m_Angle             = value >> 2 & 0x3f;
    m_IsInAir           = value & 0x2;
    m_Visible           = value & 0x1;

    m_Destination2.X    = reader.get() & 0xff;
    m_Destination2.Y    = reader.get() & 0xff;
}

void FootballProcess::Read(CUtlBuffer& reader)
{
    MoveableProcess::ReadInvoke(reader);

    //
    // angle    6bits   2^6 = 64
    // isInair  1bit
    // visible  1bit
    //
    int value           = reader.GetChar() & 0xff;
    m_Angle             = value >> 2 & 0x3f;
    m_IsInAir           = value & 0x2;
    m_Visible           = value & 0x1;

    m_Destination2.X    = reader.GetChar() & 0xff;
    m_Destination2.Y    = reader.GetChar() & 0xff;
}

