/********************************************************************************
 * 文件名：InertiaDebuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __INERTIADEBUFF_H__
#define __INERTIADEBUFF_H__

/// 表示了球员的惯性异常状态
class InertiaDebuff : public DisableDebuff
{
public:

    /// Initialize a new abnormal debuff.
    InertiaDebuff(int triggerId, int time)
        : DisableDebuff(triggerId, time, DebuffType_InertiaDebuff)
        , MID(ModelType_InertiaEffect)
    {

    }

public:

    /// Player to make a action.
    void Action(IPlayer* player)
    {
        if (MATH::FloatEqual(player->Status()->DestinationDistance(), 0.0))
        {
            player->Status()->SetState(IdleState::Instance());
        }
        else
        {
            double x = player->Current().X + cos(player->Status()->Angle() * MATH::PI / 180) * 50;
            double y = player->Current().Y + sin(player->Status()->Angle() * MATH::PI / 180) * 50;

            player->SetTarget(x, y);
            player->Status()->SetState(ChaceState::Instance());
            player->DecreaseSpeed();
        }

        player->Status()->State()->Action(player);
    }

protected:

    const int MID;
};

#endif //__INERTIADEBUFF_H__
