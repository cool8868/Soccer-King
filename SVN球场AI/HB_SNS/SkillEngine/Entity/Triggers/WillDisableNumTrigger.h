/********************************************************************************
 * 文件名：WillDisableNumTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WILLDISABLENUMTRIGGER_H__
#define __WILLDISABLENUMTRIGGER_H__

#include "../../../common/common.h"
#include "../../../common/Utility.h"

#include "../../../Interface/Skill/ITrigger.h"
#include "../../../Interface/Player/IPlayer.h"
#include "../../../Interface/IMatch.h"
#include "../../../Interface/Player/Status/IPlayerStatus.h"

/// 当本方或对方因某种原因球员下场达到要求的人数时触发
/// 第一次达成目标时计算
class WillDisableNumTrigger : public ITrigger
{
public:
    
    WillDisableNumTrigger();

    WillDisableNumTrigger(WillDisableNumTrigger& rf);

public:

    int                     GetSide() { return m_Side; }
    void                    SetSide(int vl) { m_Side = vl; }

    int                     Condition() { return m_Condition; }
    void                    SetCondition(int vl) { m_Condition = vl; }

    int                     Num() { return m_Num; }
    void                    SetNum(int vl) { m_Num = vl; }

    /// 判断这个意识是否可以触发
    bool                    IsSkillTriggered(IPlayer* player);

    WillDisableNumTrigger*  Clone() { return new WillDisableNumTrigger(*this); }

    bool                    IsKindOf(string name) { return typeid(*this).name() == name; }

    void                    SetAttribute(xml_node& node);

protected:

    /// 0 - 双方
    /// 1 - 本方
    /// 2 - 对方
    int             m_Side;

    /// 0 - 任意
    /// 1 - 红牌
    /// 2 - 黄牌
    int             m_Condition;

    /// 人数
    int             m_Num;

private:

    void            InitVariables();
};

#endif //__WILLDISABLENUMTRIGGER_H__
