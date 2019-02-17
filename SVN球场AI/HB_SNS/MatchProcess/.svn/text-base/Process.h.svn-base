/********************************************************************************
 * 文件名：Process.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PROCESS_H__
#define __PROCESS_H__

#include "../Interface/IProcess.h"
#include "../Interface/IBinaryOutput.h"
#include "../Interface/IBinaryReadIn.h"

/// 表示了当前回合的结果
class Process : public IProcess, public IBinaryOutput, public IBinaryReadIn
{
public:

    Process() : m_Round(0) {}

    int     Round() { return m_Round; }
    void    SetRound(int round) { m_Round = round; }
    void    IncRound() { m_Round++; }

    void    Output(CUtlBuffer& writer);
    void    Output(ofstream& writer);

protected:

    virtual void Output(CUtlBuffer& writer, int id);
    virtual void Output(ofstream& writer, int id);
    virtual void ReadInvoke(ifstream& reader);

protected:

    /// 表示了回合数
    int m_Round;
};

#endif //__PROCESS_H__
