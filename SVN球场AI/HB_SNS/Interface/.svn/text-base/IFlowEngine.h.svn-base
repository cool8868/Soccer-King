/********************************************************************************
 * 文件名：IFlowEngine.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IFLOWENGINE_H__
#define __IFLOWENGINE_H__

class IFlowEngine 
{
public:

    virtual ~IFlowEngine() {}

public:

    /// 创建比赛
    virtual IMatch*     CreateMatch(int homeId, int awayId, int matchType, double time) = 0;

    /// 创建Debug模式的比赛
    virtual IMatch*     CreateMatchForDebug(int homeId, int awayId, int matchType, double time) = 0;

    /// 通过外部数据创建比赛
    virtual IMatch*     CreateMatch(TransferMatchEntity* match) = 0;

    /// 通过外部数据创建Debug模式的比赛
    virtual IMatch*     CreateMatchForDebug(TransferMatchEntity* match) = 0;

    /// 读取本地比赛文件创建比赛
    virtual IMatch*     CreateMatchFromFile(string fileName) = 0;

    /// 获取比赛对象
    virtual IMatch*     GetMatch() = 0;

    /// Debug模式的单步循环
    virtual void        MainLoopForDebug() = 0;
};

#endif //__IFLOWENGINE_H__
