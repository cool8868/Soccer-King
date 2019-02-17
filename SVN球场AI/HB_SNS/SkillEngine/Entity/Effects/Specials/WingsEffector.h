/********************************************************************************
 * 文件名：WingsEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WINGSEFFECTOR_H__
#define __WINGSEFFECTOR_H__

/// 梦幻双翼
class WingsEffector : public ISpecialEffector
{
public:

    WingsEffector()
    {
        InitVariables();
    }

public:

    /// Effects the wings skill.
    void Effect(IPlayer* player, Special* skill)
    {
        vector<IPlayer*> forwards = player->GetManager()->GetPlayersByPosition(Position_Forward);
        vector<int> proArray(m_Procache.size());

        for (size_t i = 0; i < m_Procache.size(); i++)
        {
            proArray[i] = m_Procache[i];
        }

        vector<double> valueArray(forwards.size());
        for (size_t i = 0; i < m_Procache.size(); i++)
        {
            for (size_t m = 0; m < forwards.size(); m++)
            {
                valueArray[m] = forwards[m]->CurrProperty()[proArray[i]];
            }

            double max = Common::GetDoubleMaxQuick(valueArray, forwards.size());
            for (size_t m = 0; m < forwards.size(); m++)
            {
                double rate = 0;
                if (skill->Value() == "1")
                {
                    rate = 0.8;
                }

                if (skill->Value() == "2")
                {
                    rate = 0.85;
                }

                if (skill->Value() == "3")
                {
                    rate = 0.9;
                }

                if (forwards[m]->CurrProperty()[proArray[i]] != max)
                {
                    forwards[m]->CurrProperty()[proArray[i]] = max * rate;
                }

                if (forwards[m]->RawProperty()[proArray[i]] != max)
                {
                    forwards[m]->RawProperty()[proArray[i]] = max * rate;
                }
            }
        }
    }

    string ToString()
    {
        return "WingsEffector";
    }

private:

    vector<int> m_Procache;

    void InitVariables();
};

void WingsEffector::InitVariables()
{
    //初始化
    m_Procache.clear();
    m_Procache += PlayerProperty_Speed, PlayerProperty_Shooting, PlayerProperty_FreeKick;
}

#endif //__WINGSEFFECTOR_H__

