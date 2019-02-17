/********************************************************************************
 * 文件名：MoveableProcess.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "MoveableProcess.h"

string MoveableProcess::ToString()
{
    return String::Format("Round:%d, Coordinate:%s, Acceleration:%d\n", m_Round, m_Coordinate.c_str(), m_Acceleration);
}

void MoveableProcess::Output(CUtlBuffer& writer, int id)
{
    Process::Output(writer, id);

    writer.PutChar(m_Coordinate2.X);
    writer.PutChar(m_Coordinate2.Y);
}

void MoveableProcess::Output(ofstream& writer, int id)
{
    Process::Output(writer, id);

    writer.put(m_Coordinate2.X);
    writer.put(m_Coordinate2.Y);
}

void MoveableProcess::ReadInvoke(ifstream& reader)
{
    //外部已经读取了m_id和round，这里不需要再读取了

    m_Coordinate2.X     = reader.get() & 0xff;
    m_Coordinate2.Y     = reader.get() & 0xff;
}

void MoveableProcess::ReadInvoke(CUtlBuffer& reader)
{
    //外部已经读取了m_id和round，这里不需要再读取了

    m_Coordinate2.X     = reader.GetChar() & 0xff;
    m_Coordinate2.Y     = reader.GetChar() & 0xff;
}

