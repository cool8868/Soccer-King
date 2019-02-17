/********************************************************************************
 * 文件名：IStopball.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ISTOPBALL_H__
#define __ISTOPBALL_H__

/// Represents the player to stop the ball.
class IStopball 
{
public:

    virtual ~IStopball() {}

public:

    /// Player to stop ball.
    virtual void Stopball() = 0;
};

#endif //__ISTOPBALL_H__
