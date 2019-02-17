/********************************************************************************
 * 文件名：MadmanEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MADMANEFFECTOR_H__
#define __MADMANEFFECTOR_H__

/// value : 对自己的属性变化, 对对方的属性变化
class MadmanEffector : public ISpecialEffector
{
public:

    void Effect(IPlayer* player, Special* skill)
    {
        vector<int> values = Common::ConvertStringToByteArray(skill->Value());

        if (values.size() != 2)
        {
            throw ConfigurationErrorsException("The Manager Skill's special effect(madman effect) config file error: Less than 2 value parts.");
        }

        player->GetManager()->Status()->MadmanStatus()->SetSelfUpgradeRate(values[0]);
    }

    string ToString()
    {
        return "MadmanEffector";
    }
};

#endif //__MADMANEFFECTOR_H__
