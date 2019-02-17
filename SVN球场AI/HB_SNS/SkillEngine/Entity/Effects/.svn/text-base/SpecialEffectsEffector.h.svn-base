/********************************************************************************
 * 文件名：SpecialEffectsEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SPECIALEFFECTSEFFECTOR_H__
#define __SPECIALEFFECTSEFFECTOR_H__

#include "../../../Interface/Skill/Entity/Effects/ISpecialEffector.h"

class SpecialEffectsEffector : public IEffect
{
public:

    SpecialEffectsEffector()
    {
        m_Cache.clear();
        m_Cache[singleton_default<AntiOffsideEffector>::instance().ToString()]          = singleton_default<AntiOffsideEffector>::instance();
        m_Cache[singleton_default<ChangeStateEffector>::instance().ToString()]          = singleton_default<ChangeStateEffector>::instance();
        m_Cache[singleton_default<ChangeShootRegionEffector>::instance().ToString()]    = singleton_default<ChangeShootRegionEffector>::instance();
        m_Cache[singleton_default<MadmanEffector>::instance().ToString()]               = singleton_default<MadmanEffector>::instance();
        m_Cache[singleton_default<PassOutboundEffector>::instance().ToString()]         = singleton_default<PassOutboundEffector>::Instance();
        m_Cache[singleton_default<WingsEffector>::instance().ToString()]                = singleton_default<WingsEffector>::instance();
        m_Cache[singleton_default<PositionExchangeEffector>::instance().ToString()]     = singleton_default<PositionExchangeEffector>::Instance();
    }

    void Effect(IPlayer* player, ISkillPart* skill)
    {
        if (skill == NULL)
        {
            return;
        }

        SpecialEffectsSkillPart* specials = PointerCastSafe(SpecialEffectsSkillPart, skill);

        if (specials == NULL)
        {
            throw ArgumentException("Invoke the SpecialEffectsEffector to Effect a skill with error skill part. Argument need SpecialEffectsSkillPart but is NULL");
        }

        foreach (Special* special, specials->Specials())
        {
            m_Cache[special->Name()]->Effect(player, special);
        }
    }

private:

    map<string, ISpecialEffector*>  m_Cache;
};

#endif //__SPECIALEFFECTSEFFECTOR_H__
