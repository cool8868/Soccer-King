/********************************************************************************
 * 文件名：DisplacementsEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DISPLACEMENTSEFFECTOR_H__
#define __DISPLACEMENTSEFFECTOR_H__

class DisplacementsEffector : public IEffect, public IGetSkillTarget 
{        
public:

    DisplacementsEffector()
    {
        m_Displacements.clear();

        m_Displacements[0]      = MoveDisplacement();
        m_Displacements[1]      = SkillDisplacement();
    }

public:

    void Effect(IPlayer* player, ISkillPart* skill) 
    {
        if (skill == NULL) 
        {
            return;
        }

        DisplacementsSkillPart* displacements = PointerCastSafe(DisplacementsSkillPart, skill);

        foreach (Displacement* displacement, displacements->Displacements()) 
        {
            m_Displacements[displacement->Type()]->Effect(displacement, player);
        }
    }

    vector<IPlayer*> GetSkillTargets(IPlayer* player, ISkillPart* part) 
    {
        DisplacementsSkillPart* displacements = PointerCastSafe(DisplacementsSkillPart, part);
        vector<IPlayer*> targets(4);
        targets.clear();

        if (displacements != NULL) 
        {
            foreach (Displacement* displacement, displacements->Displacements())
            {
                Common::AddRange(targets, singleton_default<TargetSelector>::instance().GetTarget(displacement->Target(), displacement->TargetPosition(), player));
            }
        }

        return targets;
    }

private:
    
    map<int, IDisplacement*>    m_Displacements;
};

class IDisplacement 
{
public:

    virtual ~IDisplacement() {}

public:

    /// Effect a displacement record.
    virtual void Effect(Displacement* displacement, IPlayer* player) = 0;
};

#endif //__DISPLACEMENTSEFFECTOR_H__
