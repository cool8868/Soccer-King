/********************************************************************************
 * 文件名：WillAreaTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WILLAREATRIGGER_H__
#define __WILLAREATRIGGER_H__

#include "../../../Interface/Skill/ITrigger.h"

#include "../../../common/Utility.h"

/// 某些球员在某些区域触发
class WillAreaTrigger : public ITrigger
{
public:

    WillAreaTrigger();

    WillAreaTrigger(WillAreaTrigger& rf);

public:

    int                 Target() { return m_Target; }
    void                SetTarget(int vl) { m_Target = vl; }

    vector<int>&        TargetPosition() { return m_TargetPosition; }
    void                SetTargetPosition(vector<int>& vl) { m_TargetPosition = vl; }
    void                SetTargetPosition(string vl) { m_TargetPosition = Common::TransToValueVector<int>(vl, ","); }

    int                 Area() { return m_Area; }
    void                SetArea(int vl) { m_Area = vl; }

    /// 判断该意志是否能够触发
    bool                IsSkillTriggered(IPlayer* player);

    WillAreaTrigger*    Clone() { return new WillAreaTrigger(*this); }  

    bool                IsKindOf(string name) { return typeid(*this).name() == name; }

    void                SetAttribute(xml_node& node);

protected:

    /// 选择球员
    int                 m_Target;
    
    /// 该球员必须满足的区域
    vector<int>         m_TargetPosition;

    /// 必须满足的区域
    /// 0 - 本方半场
    /// 1 - 对方半场
    int                 m_Area;

private:

    void            InitVariables();
};

#endif //__WILLAREATRIGGER_H__
