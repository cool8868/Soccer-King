/********************************************************************************
 * 文件名：IDefenceStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IDEFENCESTATUS_H__
#define __IDEFENCESTATUS_H__

#include "../../../common/common.h"

class IPlayer;

/// 表示了球员防守时的状态
class IDefenceStatus
{
public:

    virtual ~IDefenceStatus() {}

public:

    virtual vector<IPlayer*>&   Defenders() = 0;

    virtual IPlayer*            DefenceTarget() = 0;
    virtual void                SetDefenceTarget(IPlayer* player) = 0;

    virtual IPlayer*            ClosestDefender() = 0;
    virtual void                SetClosestDefender(IPlayer* player) = 0;

    /// 找出最近的防守人
    virtual IPlayer*            GetClosestDefender() = 0;
};

#endif //__IDEFENCESTATUS_H__
