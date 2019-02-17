/********************************************************************************
 * 文件名：LocalWorkflowAdapter.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __LOCALWORKFLOWADAPTER_H__
#define __LOCALWORKFLOWADAPTER_H__

#include "../Interface/IMatch.h"
#include "../Interface/IFlowEngine.h"

class LocalWorkflowAdapter: public IFlowEngine
{
public:

    LocalWorkflowAdapter();

    ~LocalWorkflowAdapter();

public:

    /// Create a match.
    IMatch* CreateMatch(int homeId, int awayId, int matchType, double time);

    IMatch* CreateMatchForDebug(int homeId, int awayId, int matchType, double time);

    IMatch* CreateMatch(TransferMatchEntity* match);

    IMatch* CreateMatchForDebug(TransferMatchEntity* match);

    IMatch* CreateMatchFromFile(string fileName);

    IMatch* GetMatch() { return m_Match; }

    void MainLoopForDebug();

private:

    /// 游戏的主函数
    IMatch* MainLoop(IMatch* match);


private:

    IMatch* m_Match;
};

#endif //__LOCALWORKFLOWADAPTER_H__
