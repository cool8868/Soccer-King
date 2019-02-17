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
#ifndef __WILLFACADE_H__
#define __WILLFACADE_H__

class WillFacade : public IRequestInitialize
{
public:

    /// 初始化意志
    void Initialize();

    /// 获取意志原数据
    IRawSkill* GetWill(string id);

    /// 验证意志是否能够触发
    /// <param name="player">球员</param>
    /// <param name="will">意志</param>
    /// <returns>是否能够触发</returns>
    bool IsTriggered(IPlayer* player, IWillRawSkill* will);

    /// 触发效果
    /// <param name="player">球员</param>
    /// <param name="will">意志</param>
    void Effect(IPlayer* player, IWillRawSkill* will);
};

#endif //__WILLFACADE_H__

