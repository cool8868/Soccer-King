/********************************************************************************
 * 文件名：PassOutboundEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PASSOUTBOUNDEFFECTOR_H__
#define __PASSOUTBOUNDEFFECTOR_H__

/// 表示了将球直接传出界的特殊效果
class PassOutboundEffector : public ISpecialEffector
{
public:

    void Effect(IPlayer* player, Special* skill)
    {
        IMatch* match = player->GetMatch();

        match->Save();

        player->Status()->SetHasball(true);
        match->GetFootball()->MoveTo(player->Current());

        match->Status()->IncRound();
        match->RoundInit();

        double x = player->Current().X;
        double y = (player->Current().Y < Defines_Pitch.MAX_HEIGHT - player->Current().Y) ? -20 : Defines_Pitch.MAX_HEIGHT + 20;

        player->Status()->SetState(LongPassState::Instance());
        match->GetFootball()->Kick(Coordinate(x, y), 50, 45, player);

        match->Save();

        foreach (IPlayer* p, player->GetManager()->Opponent()->Players())
        {
            if (p->Status()->Enable())
            {
                p->Status()->SetHasball(true);
                break;
            }
        }
    }

    string ToString()
    {
        return "PassOutboundEffector";
    }
};

#endif //__PASSOUTBOUNDEFFECTOR_H__
