/********************************************************************************
 * 文件名：ChangeShootRegionEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __CHANGESHOOTREGIONEFFECTOR_H__
#define __CHANGESHOOTREGIONEFFECTOR_H__

/// Represents a special effector that will change shoot region.
class ChangeShootRegionEffector : public ISpecialEffector
{
public:

    /// Effects a special effect that will change the player's shoot region.
    /// skill.value ->
    /// <![CDATA[更改后的区域(以主队场地为准)|y1<=y<=y2 || y3<=y <= y4|targetPosition]]>
    /// <example>0,0,0,0|33,44,33,44|0</example>
    void Effect(IPlayer* player, Special* skill)
    {
        vector<string> values = Common::TransToStringVector(skill->Value(), "|");

        if (values.size() != 3)
        {
            throw ConfigurationErrorsException("Skill config file's speical effect part error. SkillId:" + skill->Name());
        }

        Region region = (player->Side() == Side_Home) ? Region::ParseByStr(values[0]) : Region::ParseByStr(values[0]).Mirror();
        vector<int> targetPositions = Common::ConvertStringToByteArray(values[2]);
        vector<int> coordinates     = Common::ConvertStringToByteArray(values[1]);

        vector<IPlayer*> targets = singleton_default<TargetSelector>::instance().GetTarget(3, targetPositions, player, coordinates);

        foreach (IPlayer* p, targets)
        {
            p->Status()->GetShootStatus()->SetShootRegion(region);
        }
    }

    string ToString()
    {
        return "ChangeShootRegionEffector";
    }
};

#endif //__CHANGESHOOTREGIONEFFECTOR_H__
