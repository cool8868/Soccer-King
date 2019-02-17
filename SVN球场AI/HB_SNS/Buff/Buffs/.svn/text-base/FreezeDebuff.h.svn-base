/********************************************************************************
 * 文件名：FreezeDebuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FREEZEDEBUFF_H__
#define __FREEZEDEBUFF_H__

/// 表示了静止异常状态
class FreezeDebuff : public DisableDebuff
{
public:

    /// Initialize a new abnormal debuff.
    FreezeDebuff(int triggerId, int time)
        : DisableDebuff(triggerId, time, DebuffType_FreezeDebuff)
        , MID(ModelType_FreezeEffect)
    {
    }

public:

    /// Player to make a action.
    void Action(IPlayer* player)
    {
        player->Status()->SetState(IdleState::Instance());
        player->Status()->State()->Action(player);
    }

protected:

    const int MID;
};

#endif //__FREEZEDEBUFF_H__
