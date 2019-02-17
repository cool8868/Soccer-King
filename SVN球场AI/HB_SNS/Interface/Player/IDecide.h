/********************************************************************************
 * 文件名：IDecide.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IDECIDE_H__
#define __IDECIDE_H__

class IDecide
{
public:

    virtual ~IDecide() {}

public:

    /// Player to action.
    virtual void Action() = 0;

    /// Decide.
    virtual void Decide() = 0;

    /// Quick Decide.
    virtual void QuickDecide() = 0;
};

/// Defines the methods that notify decide is end.
class IDecideEnd
{
public:

    virtual ~IDecideEnd() {}

public:

    /// Decide is over.
    virtual void DecideEnd() = 0;
};

#endif //__IDECIDE_H__

