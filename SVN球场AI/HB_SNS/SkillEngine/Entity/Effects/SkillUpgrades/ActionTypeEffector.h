/********************************************************************************
 * 文件名：ActionTypeEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __ACTIONTYPEEFFECTOR_H__
#define __ACTIONTYPEEFFECTOR_H__

class ActionTypeEffector : public ISkillUpgradeEffector 
{
public:

    /// Effects the skill upgrade effect.
    void Effect(IPlayer* player, SkillUpgrade* upgrade) 
    {
        vector<IPlayer*> targetPlayers = singleton_default<TargetSelector>::instance().GetTarget(upgrade->Target(), upgrade->TargetPosition(), player);
        vector<IActionSkill> targetSkills(4);
        targetSkills.clear();

        foreach (IPlayer* p, targetPlayers) 
        {
            // +++ tony:
            foreach (var skill in p.SkillReferences) {
                if (upgrade.SkillType[0] == "*") {
                    targetSkills.Add(skill);
                    continue;
                }

                foreach (var type in upgrade.SkillType) {
                    if ((byte)skill.Class == byte.Parse(type)) {
                        targetSkills.Add(skill);
                        break;
                    }
                }
            }
        }

        Singleton<SkillUpgradesEffector>.Instance.Effect(targetSkills, upgrade);
    }
};

#endif //__ACTIONTYPEEFFECTOR_H__
