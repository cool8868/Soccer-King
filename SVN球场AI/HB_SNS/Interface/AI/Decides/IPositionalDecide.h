/********************************************************************************
 * 文件名：IPositionalDecide.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IPOSITIONALDECIDE_H__
#define __IPOSITIONALDECIDE_H__

/// Decide the moving target.
class IPositionalDecide
{
public:

    ~IPositionalDecide() {}

public:

    /// Decides the moving target.
    virtual Coordinate DecideTarget(IPlayer* player) = 0;
};

#endif //__IPOSITIONALDECIDE_H__
