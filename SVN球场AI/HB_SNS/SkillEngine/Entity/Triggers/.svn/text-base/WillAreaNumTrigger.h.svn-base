/********************************************************************************
 * 文件名：WillAreaNumTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WILLAREANUMTRIGGER_H__
#define __WILLAREANUMTRIGGER_H__

#include "../../../Interface/Skill/ITrigger.h"
#include "../../../Interface/Player/IPlayer.h"

/// 在某些区域的某方人数达到一定条件时触发
class WillAreaNumTrigger : public ITrigger
{
public:

    WillAreaNumTrigger();

    WillAreaNumTrigger(WillAreaNumTrigger& rf);

public:

    int                     Area() { return m_Area; }
    void                    SetArea(int vl) { m_Area = vl; }

    int                     GetSide() { return m_Side; }
    void                    SetSide(int vl) { m_Side = vl; }

    int                     Position() { return m_Position; }
    void                    SetPosition(int vl) { m_Position = vl; }

    int                     Num() { return m_Num; }
    void                    SetNum(int vl) { m_Num = vl; }

    int                     Condition() { return m_Condition; }
    void                    SetCondition(int vl) { m_Condition = vl; }

    /// 判断意志是否能够触发
    bool                    IsSkillTriggered(IPlayer* player);

    WillAreaNumTrigger*     Clone() { return new WillAreaNumTrigger(*this); }

    bool                    IsKindOf(string name) { return typeid(*this).name() == name; }

    void                    SetAttribute(xml_node& node);

protected:

    /// 区域
    /// 0 - 本方半场
    /// 1 - 对方半场
    int         m_Area;

    /// 0 - 双方
    /// 1 - 本方
    /// 2 - 对方
    int         m_Side;

    /// 位置
    /// 0 - 所有
    /// 1 - 门将
    /// 2 - 后卫
    /// 3 - 中场
    /// 4 - 前锋
    int         m_Position;

    /// 人数
    int         m_Num;

    /// 判定方式
    /// 0 - 大于
    /// 1 - 小于
    /// 2 - 等于
    int         m_Condition;

private:

    void        InitVariables();
};

#endif //__WILLAREANUMTRIGGER_H__
