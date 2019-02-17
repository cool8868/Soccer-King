/********************************************************************************
 * 文件名：ModelEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MODELEFFECTOR_H__
#define __MODELEFFECTOR_H__

class ModelEffector : IEffect
{
public:

    void Effect(IPlayer* player, ISkillPart* skill)
    {
        if (skill == NULL)
        {
            return;
        }

        ModelsSkillPart* skillPart = PointerCastSafe(ModelsSkillPart, skill);

        foreach (ModelRecord* model, skillPart->Models())
        {
            vector<IPlayer*> targets = singleton_default<TargetSelector>::instance().GetTarget(model->Target(), model->TargetPosition(), player);

            foreach (IPlayer* t, targets)
            {
                // +++ tony:
                t.Status.ModelStatus.Mid = model.Mid;
                t.Status.ModelStatus.IsHoldBall = false;

                if (model->Last() < 0)
                {
                    t.Status.ModelStatus.RemainTime = player.Match.TotalRound;
                    t.Status.ModelStatus.IsHoldBall = true;
                    continue;
                }

                if (model.Last == 0)
                {
                    t.Status.ModelStatus.RemainTime = 1;
                    continue;
                }

                t.Status.ModelStatus.RemainTime = Utility.ConvertTimeToRound(model.Last, player.Match.TotalRound);
            }
        }
    }
};

#endif //__MODELEFFECTOR_H__

