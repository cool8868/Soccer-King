/********************************************************************************
 * 文件名：PassProcess.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "PassProcess.h"

PassProcess::PassProcess()
    : PlayerProcess(4)
{
    InitVariables();
}

void PassProcess::InitVariables()
{
    m_IsPassFail    = false;
}

void PassProcess::Output(CUtlBuffer& writer)
{
    PlayerProcess::Output(writer, m_Id);
}

void PassProcess::Output(ofstream& writer)
{
    PlayerProcess::Output(writer, m_Id);
}

void PassProcess::Read(ifstream& reader)
{
    PlayerProcess::ReadInvoke(reader);
}

void PassProcess::Read(CUtlBuffer& reader)
{
    PlayerProcess::ReadInvoke(reader);
}

