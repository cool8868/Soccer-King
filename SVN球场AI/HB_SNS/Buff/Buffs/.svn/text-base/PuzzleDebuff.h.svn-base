/********************************************************************************
 * 文件名：PuzzleDebuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PUZZLEDEBUFF_H__
#define __PUZZLEDEBUFF_H__

/// 表示了球员的困惑异常状态
class PuzzleDebuff : public DisableDebuff 
{
public:

    /// Initialize a new abnormal debuff.
    PuzzleDebuff(int triggerId, int time)
        : DisableDebuff(triggerId, time, DebuffType_PuzzleDebuff) 
        , MID(ModelType_PuzzleEffect)
    {

    }

public:

    /// Player to make a action.
    void Action(IPlayer* player) 
    {
        player->Status()->SetState(ChaceState::Instance());
        const int offset = Defines_Player.PUZZLE_RANGE;

        int x = RandomHelper::GetInt32(int(player->Current().X - offset), int(player->Current().X + offset));
        int y = RandomHelper::GetInt32(int(player->Current().Y - offset), int(player->Current().Y + offset));
        
        player->SetTarget(x, y);
        player->Status()->State()->Action(player);
    }

    const int MID;
};

#endif //__PUZZLEDEBUFF_H__

