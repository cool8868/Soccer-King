/********************************************************************************
 * 文件名：ISight.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ISIGHT_H__
#define __ISIGHT_H__

class IPlayer;

/// Represents the contracts of the player's sight.
class ISight 
{
public:

    virtual ~ISight() {}

public:

    /// Represents the player in the sight.
    virtual vector<IPlayer*>&   InSightPlayers() = 0;

    /// Refresh player's sight.
    virtual void                RefreshSight() = 0;
};

#endif //__ISIGHT_H__
