/********************************************************************************
 * 文件名：Creature.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __CREATURE_H__
#define __CREATURE_H__

#include "../Buff/AbsBuff.h"
#include "../Exception/MyException.h"
#include "../common/Delegate/Delegate.h"

class AddDebuffEventArgs;
class RemoveDebuffEventArgs;
class AddBuffEventArgs;
class RemoveBuffEventArgs;

/// Represents the baseclass of the manager and player.
class Creature
{
public:

    Creature();

    virtual ~Creature() {}

public:

    /// Represents the buff bar.
    vector<AbsBuff*>&               BuffBar() { return m_BuffBar; }

    vector<AbsBuff*>&               DebuffBar() { return m_DebuffBar; }

    void                            AddBuff(AbsBuff* buff);

    void                            AddDebuff(AbsBuff* debuff);

protected:

    /// Represents the event that invokes on add a new debuff.
    Functor<AddDebuffEventArgs>*    OnAddDebuff() { return m_OnAddDebuff; }
    void                            SetOnAddDebuff(Functor<AddDebuffEventArgs>* value) { m_OnAddDebuff =  value; }

    /// Represents the event that invokes on remove a debuff.
    Functor<RemoveDebuffEventArgs>* OnRemoveDebuff() { return m_OnRemoveDebuff; }
    void                            SetOnRemoveDebuff(Functor<RemoveDebuffEventArgs>* value) { m_OnRemoveDebuff = value; }

    /// Represents the event that invokes on add a new buff.
    Functor<AddBuffEventArgs>*      OnAddBuff() { return m_OnAddBuff; }
    void                            SetOnAddBuff(Functor<AddBuffEventArgs>* value) { m_OnAddBuff = value; }

    /// Represents the event that invokes on remove a buff.
    Functor<RemoveBuffEventArgs>*   OnRemoveBuff() { return m_OnRemoveBuff; }
    void                            SetOnRemoveBuff(Functor<RemoveBuffEventArgs>* value) { m_OnRemoveBuff = value; }

    /// Refresh the player's all buffs.
    void                            RefreshBuff();

    /// Refresh the player's all debuffs.
    void                            RefreshDebuff();

    /// Validate the creature immunities to add a debuff.
    virtual bool                    ValidateImmunityAddDebuff() = 0;

protected:

    const int                           m_MAX_BUFF_COUNT;
    vector<AbsBuff*>                    m_BuffBar;
    vector<AbsBuff*>                    m_DebuffBar;
    Functor<AddDebuffEventArgs>*        m_OnAddDebuff;
    Functor<RemoveDebuffEventArgs>*     m_OnRemoveDebuff;
    Functor<AddBuffEventArgs>*          m_OnAddBuff;
    Functor<RemoveBuffEventArgs>*       m_OnRemoveBuff;

private:

    void InitVariables();
};

/// Represents the <see cref="EventArgs"/> of the AddDebuff event.
class AddDebuffEventArgs
{
public:

    AddDebuffEventArgs(AbsBuff* buff) : m_Debuff(buff)
    {

    }

    virtual ~AddDebuffEventArgs() {}

    /// Represents the debuff that adding.
    AbsBuff*    GetDebuff() { return m_Debuff; }
    void        SetDebuff(AbsBuff* value) { m_Debuff = value; }

private:

    AbsBuff*    m_Debuff;
};

/// Represents the <see cref="EventArgs"/> of the RemoveDebuff event.
class RemoveDebuffEventArgs
{
public:

    RemoveDebuffEventArgs(AbsBuff* buff) : m_Debuff(buff)
    {

    }

    virtual ~RemoveDebuffEventArgs() {}

    AbsBuff*    GetDebuff() { return m_Debuff; }
    void        SetDebuff(AbsBuff* value) { m_Debuff = value; }

private:

    AbsBuff*    m_Debuff;
};

/// Represents the <see cref="EventArgs"/> of AddBuff event.
class AddBuffEventArgs
{
public:

    AddBuffEventArgs(AbsBuff* buff) : m_Buff(buff)
    {

    }

    virtual ~AddBuffEventArgs() {}

    AbsBuff*    GetBuff() { return m_Buff; }
    void        SetBuff(AbsBuff* value) { m_Buff = value; }

private:

    AbsBuff*    m_Buff;
};

/// Represents the <see cref="EventArgs"/> of the RemoveBuff event.
class RemoveBuffEventArgs
{
public:

    RemoveBuffEventArgs(AbsBuff* buff) : m_Buff(buff)
    {

    }

    virtual ~RemoveBuffEventArgs() {}

    AbsBuff*    GetBuff() { return m_Buff; }
    void        SetBuff(AbsBuff* value) { m_Buff = value; }

private:

    AbsBuff*    m_Buff;
};

/// Represents the error buff type exception.
class ErrorBuffTypeException: public MyException
{
public:

    /// 错误的Buff类型
    ErrorBuffTypeException(string message)
    {
        m_err = "ErrorBuffTypeException:";
        m_err += message;
    }
};

#endif //__CREATURE_H__
