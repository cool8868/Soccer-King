/********************************************************************************
 * 文件名：IShortPass.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ISHORTPASS_H__
#define __ISHORTPASS_H__

/// Represents the action of the player to pass ball to other.
class IShortPass
{
public:

    virtual ~IShortPass() {}

public:

    /// Decide a target to pass.
    virtual void DecidePassTarget() = 0;

    /// Player to pass the ball to passtarget.
    virtual void ShortPass() = 0;

    /// 判定一个坐标是否能传球
    /// <param name="c"><see cref="Coordinate"/></param>
    /// <returns>Enable to pass.</returns>
    virtual bool IsCoordinateEnablePass(Coordinate c) = 0;
};

#endif //__ISHORTPASS_H__

