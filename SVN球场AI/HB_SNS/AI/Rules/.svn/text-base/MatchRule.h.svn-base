/********************************************************************************
 * 文件名：MatchRule.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MATCHRULE_H__
#define __MATCHRULE_H__

class MatchRule
{
public:
    
    /// 获取离球最近的本方球员
    static IPlayer* GetClosestPlayerFromBallInMySide(IManager* manager)
    {
        Coordinate point = manager->GetMatch()->GetFootball()->Current();

        double              array[MyDefine_MAX_PLAYER_COUNT - 1] = {};          // 距离数组
        vector<IPlayer*>    targetArray(Defines_Match.MAX_PLAYER_COUNT - 1);    // 目标的数组
        targetArray.clear();

        int count = 0;
        for (size_t i = 0; i < manager->Players().size(); i++)
        {
            if (manager->Players()[i]->BuildinAttribute()->GetPosition() == Position_Goalkeeper) continue;
            if (manager->Players()[i]->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None) continue;
            if (manager->Players()[i]->Status()->Enable())
            {
                targetArray.push_back(manager->Players()[i]);
                array[count] = manager->Players()[i]->Current().SimpleDistance(point);
                count++;
            }
        }

        if (count == 0)
        {
            return NULL;
        }

        return targetArray[Common::GetDoubleMinIndexQuick(array, count)];
    }

    /// 获取本方某个属性最高的球员
    static IPlayer* GetHighestPropertyPlayer(IManager* manager, int property)
    {
        double              array[MyDefine_MAX_PLAYER_COUNT - 1] = {};          // 属性值数组
        vector<IPlayer*>    targetArray(Defines_Match.MAX_PLAYER_COUNT - 1);    // 球员数组
        targetArray.clear();

        int count = 0;
        for (size_t i = 0; i < manager->Players().size(); i++)
        {
            if (manager->Players()[i]->BuildinAttribute()->GetPosition() == Position_Goalkeeper) continue;
            if (manager->Players()[i]->Status()->GetDebuffStatus()->GetDebuffType() != DebuffType_None) continue;

            if (manager->Players()[i]->Status()->Enable())
            {
                targetArray.push_back(manager->Players()[i]);
                array[count++] = manager->Players()[i]->CurrProperty()[property];                    
            }
        }

        if (targetArray.size() == 0) // 如果没有满足条件的球员则返回空
        {
            return NULL;
        }

        return targetArray[Common::GetDoubleMaxIndexQuick(array, count)];
    }
};

#endif //__MATCHRULE_H__
