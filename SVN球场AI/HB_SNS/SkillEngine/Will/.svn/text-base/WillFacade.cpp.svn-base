/********************************************************************************
 * 文件名：WillFacade.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "WillFacade.h"

/// 初始化意志
void WillFacade::Initialize()
{
    WillCache::Instance().Clear();
    singleton_default<WillBuilder>::instance().Initialize();

    vector<xml_node>& wills = singleton_default<WillConfigCache>::instance().GetAllWills();

    int succ = 0;
    foreach (xml_node& will, wills)
    {
        try
        {
            if (will == NULL)
            {
                continue;
            }
            string id       = will.attribute("id").value();
            IRawSkill* w    = singleton_default<WillBuilder>::instance().BuildSkill(id);
            
            WillCache::Instance()->Insert(id, w);
            succ++;
        }
        catch (SkillErrorException& ex)
        {
            LogHelper::Insert(&ex, "Initialize Will throw exceptions.", LogType_Error);
        }
    }

    LogHelper::Insert(lexical_cast<string>(succ) + "/" + lexical_cast<string>(wills.size()) + " wills has loaded.", LogType_Info);
}

IRawSkill* WillFacade::GetWill(string id)
{
    try
    {
        return WillCache::Instance()->GetWill(id).Clone();
    }
    catch (MyException& ex)
    {
        throw SkillErrorException("WillFacade class's GetSkill method causes exception.", ex);
    }
}

bool WillFacade::IsTriggered(IPlayer* player, IWillRawSkill* will)
{
    return singleton_default<WillTrigger>::instance().IsSkillTrigger(player, will);
}

void WillFacade::Effect(IPlayer* player, IWillRawSkill* will)
{
    singleton_default<WillEffector>::instance().Effect(player, will);
}
