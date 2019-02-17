/********************************************************************************
 * 文件名：AbsBuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ABSBUFF_H__
#define __ABSBUFF_H__

#include "../common/common.h"
#include "../common/Enum/BuffType.h"

/// 表示了BUFF和DEBUFF的基类
class AbsBuff
{
public:

    /// 初始化一个新的BUFF或DEBUFF
    /// <param name="triggerId">Represents the trigger man's id.</param>
    /// <param name="time">Represents the buff's lasting time.(round)</param>
    /// <param name="type">Represents the buff's type.</param>
    AbsBuff(int triggerId, int time, BuffType type);

    virtual ~AbsBuff() {}

public:

    int             Id() { return m_Id; }

    int             TriggerId() { return m_TriggerId; }

    int             Time() { return m_Time; }
    void            SetTime(int vl) { m_Time = vl; }

    int             Last() { return m_Last; }

    bool            IsBuff() { return m_Isbuff; }
    void            SetIsBuff(bool vl) { m_Isbuff = vl; }

    BuffType        Type() { return m_Type; }

    vector<int>&    PropertyId() { return m_VectorPropertyId; }
    void            SetPropertyId(vector<int>& vecIds);

    double          Rate() { return m_Rate; }
    void            SetRate(double vl) { m_Rate = vl; }

    /// 表示了时间的变化
    virtual void    TimeLapse();

protected:

    /// 表示了BUFF的唯一标示符
    int             m_Id;

    /// 表示了触发人的ID
    int             m_TriggerId;

    /// 表示了BUFF的类型
    BuffType        m_Type;

    /// 表示了是否是BUFF还是DEBUFF
    bool            m_Isbuff;

    /// 表示了剩余时间
    int             m_Time;

    /// 表示了总剩余时间
    int             m_Last;

    /// 表示了要变化的属性ID集合
    vector<int>     m_VectorPropertyId;

    /// 表示了属性变化比率
    /// <example>0 ~ 100</example>
    double          m_Rate;

private:

    void            InitVariables();
};


#endif //__ABSBUFF_H__
