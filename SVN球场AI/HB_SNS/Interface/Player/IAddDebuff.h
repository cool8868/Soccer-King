/********************************************************************************
 * 文件名：IAddDebuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IADDDEBUFF_H__
#define __IADDDEBUFF_H__

/// Represents the methods that to add buffs to player.
class IAddBuff
{
public:

    virtual ~IAddBuff() {}

public:

    /// Add the finishing type buff.
    virtual void AddFinishing() = 0;
};

/// Represents the methods that to add debuffs to player.
class IAddDebuff
{
public:

    virtual ~IAddDebuff() {}

public:

    /// Adds a inertia type debuff to a player.
    virtual void AddInertia() = 0;

    /// Adds a inertia type debuff to a player with the lasting time.
    /// <param name="last"></param>
    virtual void AddInertia(int last) = 0;

    /// Adds a fall down type debuff to a player.
    /// <param name="triggerId">The skill trigger's id.</param>
    /// <param name="last">Represents the debuff's lasting time.</param>
    virtual void AddDownDebuff(int triggerId, int last) = 0;

    /// Adds a puzzle type debuff to a player.
    /// <param name="triggerId">Represents the triggerman's id.</param>
    /// <param name="last">Represents the debuff's lasting time.</param>
    virtual void AddPuzzleDebuff(int triggerId, int last) = 0;

    /// Adds a stun type debuff to a player.
    /// <param name="triggerId">Represents the triggerman's id.</param>
    /// <param name="level">Represents the debuff's level.</param>
    virtual void AddStunDebuff(int triggerId, int level) = 0;

    /// Adds a out of hand debuff to a player.
    /// <param name="triggerId">Represents the triggerman's id.</param>
    /// <param name="level">Represents the debuff's level.</param>
    virtual void AddOutOfHandDebuff(int triggerId, int level) = 0;

    /// Adds a freeze debuff.
    /// <param name="triggerId">Represents the triggerman's id.</param>
    /// <param name="last">Represents the debuff's lasting time.</param>
    virtual void AddFreezeDebuff(int triggerId, int last) = 0;
};

#endif //__IADDDEBUFF_H__
