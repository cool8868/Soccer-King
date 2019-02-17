/********************************************************************************
 * 文件名：PlayerProcess.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "PlayerProcess.h"

PlayerProcess::PlayerProcess(int id)
    : m_Id(id)
{
    InitVariables();
}

string PlayerProcess::ToString()
{
    return String::Format("Round:%d, Coordinate:%s, Acceleration:%d, Angle:%d", m_Round, m_Coordinate.c_str(), m_Acceleration, m_Angle);
}

int PlayerProcess::GetPlayerAngleIndex(int angle)
{
    return static_cast<int>((angle + 22.5) / 45) % 8;
}

int PlayerProcess::GetClientStateIndex(int state)
{
    if (state == 1)
    { 
        // ChaceState
        return 0;
    }

    if (state == 3)
    { 
        // DiveBallState
        return 1;
    }

    if (state == 6 || state == 21)
    { 
        // IdleState
        return 2;
    }

    if (state == 11)
    { 
        // StopballState
        return 3;
    }

    if (state == 12)
    { 
        // InterruptionState
        return 4;
    }

    if (state == 13)
    { 
        // SlideTackleState
        return 5;
    }

    if (state == 14)
    { 
        // DefaultDribbleState
        return 6;
    }

    if (state == 15)
    { 
        // LongPassState
        return 7;
    }

    if (state == 16)
    { 
        // ShortPassstate
        return 8;
    }

    if (state == 17 || state == 20)
    { 
        // DefaultShootState | FreekickShootState
        return 9;
    }

    if (state == 18)
    { 
        // VolleyShootState
        return 10;
    }

    if (state == 19)
    { 
        // BreakThroughState
        return 11;
    }

    // +++ tony:
    throw ApplicationException("Can't convert the raw state index into the client state index. Raw Id:" + state);
}

void PlayerProcess::Output(CUtlBuffer& writer)
{
    Output(writer, m_Id);
}

void PlayerProcess::Output(CUtlBuffer& writer, int id)
{
    MoveableProcess::Output(writer, id);

    // byte angle = GetPlayerAngleIndex(this.Angle);
    // byte state = GetClientStateIndex(this.State);                      

    //////////////////////////////////////////////////
    //
    // angle            4bit
    // state            4bit
    // nameVisible      1bit
    // hasBall          1bit
    // foulLevel        2bit
    // model            4bit
    //
    //////////////////////////////////////////////////            
    writer.PutChar(m_Angle << 4 | m_State);
    writer.PutChar(m_NameVisible << 3 | m_HasBall << 2 | m_FoulLevel);
    writer.PutChar(m_Model);
}

void PlayerProcess::InitVariables()
{
    m_Angle         = 0;
    m_State         = 0;
    m_NameVisible   = false;
    m_HasBall       = false;
    m_FoulLevel     = false;
}

void PlayerProcess::Output(ofstream& writer)
{
    Output(writer, m_Id);
}

void PlayerProcess::Output(ofstream& writer, int id)
{
    MoveableProcess::Output(writer, id);

    // byte angle = GetPlayerAngleIndex(this.Angle);
    // byte state = GetClientStateIndex(this.State);                      

    //////////////////////////////////////////////////
    //
    // angle            4bit
    // state            4bit
    // nameVisible      1bit
    // hasBall          1bit
    // foulLevel        2bit
    // model            4bit
    //
    //////////////////////////////////////////////////            
    writer.put(m_Angle << 4 | m_State);
    writer.put(m_NameVisible << 3 | m_HasBall << 2 | m_FoulLevel);
}

void PlayerProcess::Read(ifstream& reader)
{
    ReadInvoke(reader);
}

void PlayerProcess::Read(CUtlBuffer& reader)
{
    ReadInvoke(reader);
}

void PlayerProcess::ReadInvoke(ifstream& reader)
{
    MoveableProcess::ReadInvoke(reader);

    int value       = reader.get() & 0xff;
    m_Angle         = value >> 4 & 0xf;
    m_State         = value & 0xf;

    value           = reader.get() & 0xff;
    m_NameVisible   = (bool)((value >> 3) & 0x1f);
    m_HasBall       = (bool)(value & 0x4);
    m_FoulLevel     = value & 0x3;
}

void PlayerProcess::ReadInvoke(CUtlBuffer& reader)
{
    MoveableProcess::ReadInvoke(reader);

    int value       = reader.GetChar() & 0xff;
    m_Angle         = value >> 4 & 0xf;
    m_State         = value & 0xf;

    value           = reader.GetChar() & 0xff;
    m_NameVisible   = (bool)((value >> 3) & 0x1f);
    m_HasBall       = (bool)(value & 0x4);
    m_FoulLevel     = value & 0x3;
}
