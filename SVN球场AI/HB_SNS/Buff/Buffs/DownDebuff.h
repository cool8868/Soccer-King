﻿/********************************************************************************
 * 文件名：DownDebuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DOWNDEBUFF_H__
#define __DOWNDEBUFF_H__

/// 表示了球员倒地异常状态
class DownDebuff : public DisableDebuff
{
public:

    DownDebuff(int triggerId, int time)
        : DisableDebuff(triggerId, time, DebuffType_DownDebuff)
        , MID(ModelType_DownEffect)
    {

    }

    /// Player to make a action.
    void Action(IPlayer* player)
    {
        player->Status()->SetState(IdleState::Instance());
        player->Status()->State()->Action(player);
    }

    /// Represents the debuff's model id.
    const ModelType MID;
};

#endif //__DOWNDEBUFF_H__
