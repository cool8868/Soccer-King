/********************************************************************************
 * 文件名：ICreateMatch.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ICREATEMATCH_H__
#define __ICREATEMATCH_H__

#include "../Match/TransferEntity.h"

class IInitMatch 
{
public:

    virtual ~IInitMatch() {}

public:

    /// Initializes a match.
    virtual void InitMatch(int homeId, int awayId, int matchType, double time) = 0;

    virtual void InitMatch(TransferMatchEntity* entity) = 0;
};

#endif //__ICREATEMATCH_H__
