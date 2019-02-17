/********************************************************************************
 * 文件名：WillTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WILLTRIGGER_H__
#define __WILLTRIGGER_H__

class WillTrigger
{
public:

    virtual ~WillTrigger() {}

public:

    /// 验证这个意志是否能够触发
    //+++ tony: 没有抛出异常的地方，catch暂时不删除
    bool IsSkillTrigger(IPlayer* player, IWillRawSkill* will)
    {
        try
        {
            WillTriggersSkillPart* skillPart = PointerCastSafe(WillTriggersSkillPart, will->Triggers());

            foreach (ITrigger* trigger, skillPart->Triggers())
            {
                if (!trigger->IsSkillTriggered(player))
                {
                    return false;
                }
            }

            return true;
        }
        catch (MyException& ex)
        {
            throw SkillErrorException("WillFacade class's IsSkillTrigger method causes exception.", ex);
        }
    }
};

#endif //__WILLTRIGGER_H__

