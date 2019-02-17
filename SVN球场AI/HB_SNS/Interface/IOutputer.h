/********************************************************************************
 * 文件名：IOutputer.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IOUTPUTER_H__
#define __IOUTPUTER_H__

class Process;

/// 表示了一个需要输出至前台的物体需要的接口
class IOutputer 
{
public:

    virtual ~IOutputer() {}

public:

    /// 表示了该物体的每回合信息
    virtual vector<Process*>&   Processes() = 0;
};

#endif //__IOUTPUTER_H__
