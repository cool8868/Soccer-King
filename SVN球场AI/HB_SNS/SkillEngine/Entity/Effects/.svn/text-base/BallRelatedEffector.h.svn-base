/********************************************************************************
 * 文件名：BallRelatedEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __BALLRELATEDEFFECTOR_H__
#define __BALLRELATEDEFFECTOR_H__

class BallRelatedEffector : public IEffect
{
public:

    BallRelatedEffector()
        : IN_AIR(0)
        , HIDE(1)
    {

    }

    /// Effect a skill
    void Effect(IPlayer* player, ISkillPart* skill)
    {
        if (skill == NULL)
        {
            return;
        }

        BallRelatedSkillPart* skillPart = PointerCastSafe(BallRelatedSkillPart, skill);

        foreach (BallRelated* related, skillPart->BallRelateds())
        {
            if (related->Action() == IN_AIR)
            {
                player->GetMatch()->GetFootball()->ForceInAir();
            }

            if (related->Action() == HIDE)
            {
                player->GetMatch()->GetFootball()->Hide(related->Last());
            }
        }
    }

private:

    const int IN_AIR;

    const int HIDE;
};

#endif //__BALLRELATEDEFFECTOR_H__
