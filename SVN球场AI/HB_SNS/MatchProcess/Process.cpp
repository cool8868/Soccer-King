/********************************************************************************
 * 文件名：Process.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "Process.h"
#include "../common/Utility.h"

/// 将当前对象的内容填充入二进制流中
void Process::Output(CUtlBuffer& writer)
{
    writer.PutChar(m_Round);
    writer.PutChar(m_Round >> 8);
}

void Process::Output(ofstream& writer)
{
    writer.put(m_Round);
    writer.put(m_Round >> 8);
}

/// 将当前对象的数据填充入二进制流中
void Process::Output(CUtlBuffer& writer, int id)
{
    writer.PutChar(id << 5 | m_Round >> 8);
    writer.PutChar(m_Round);
}

void Process::Output(ofstream& writer, int id)
{
    writer.put(id << 5 | m_Round >> 8);
    writer.put(m_Round);
}

void Process::ReadInvoke(ifstream& reader)
{

}
