/********************************************************************************
 * 文件名：IShoot.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ISHOOT_H__
#define __ISHOOT_H__

/// 表示了球员的射门动作
class IShoot 
{
public:

    virtual ~IShoot() {}

public:

    /// 射门
    virtual void    Shoot() = 0;

    /// 球员发动一次大力抽射
    virtual void    VolleyShoot() = 0;

    /// 球员发动一次直接任意球射门
    virtual void    FreeKickShoot() = 0;
};

#endif //__ISHOOT_H__
