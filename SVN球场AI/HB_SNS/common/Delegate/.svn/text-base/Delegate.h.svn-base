/********************************************************************************
 * 文件名：Delegate.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DELEGATE_H__
#define __DELEGATE_H__

template<class T>
class Functor
{
public:

    virtual void Invoke(T&) = 0;
};

template <class T, class R>
class Delegate : public Functor<R>
{
protected:

    typedef void (T::*pfnHandle)(R&);

    const pfnHandle m_pfn;

    T* const m_pThis;

public:

    Delegate(T* const pThis, const pfnHandle pfn)
        : m_pThis(pThis), m_pfn(pfn)
    {
        if (m_pThis == NULL || m_pfn == NULL)
        {
            throw;
        } 
    }

    virtual void Invoke(R& p1)
    {
        return (m_pThis->*m_pfn)(p1);
    }
};


#endif //__DELEGATE_H__
