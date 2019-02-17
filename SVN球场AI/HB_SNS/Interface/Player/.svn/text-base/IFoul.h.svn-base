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
#ifndef __IFOUL_H__
#define __IFOUL_H__

/// Represents the interface of the foul.
class IFoul
{
public:

    virtual ~IFoul() {}

public:

    /// Foul
    /// <param name="level">
    /// foul's level.
    /// 0 - Normal
    /// 1 - Yellow Card
    /// 2 - Red Card
    virtual void Foul(int level) = 0;

    /// Injured
    virtual void Injured() = 0;
};

#endif //__IFOUL_H__
