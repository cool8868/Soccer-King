/********************************************************************************
 * 文件名：WillEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WILLEFFECTOR_H__
#define __WILLEFFECTOR_H__

/// 意志效果触发器
class WillEffector
{
public:

    virtual ~WillEffector() {}

public:

    /// 触发一个意志的效果
    void Effect(IPlayer* player, IWillRawSkill* skill)
    {
        singleton_default<PropertyChangesEffector>::instance().Effect(player, skill->PropertyChanges());
    }
};

#endif //__WILLEFFECTOR_H__
