/********************************************************************************
 * 文件名：IDisturb.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IDISTURB_H__
#define __IDISTURB_H__

/// Represents the interface of the disturb.
class IDisturb 
{
public:

    virtual ~IDisturb() {}

public:

    /// Validate current player has into any defender's defence range.
    /// Only the ball handler will decrease attribute-points, 
    /// off-ball player has no disturb effect.
    virtual void ValidateDisturbState() = 0;
};

#endif //__IDISTURB_H__
