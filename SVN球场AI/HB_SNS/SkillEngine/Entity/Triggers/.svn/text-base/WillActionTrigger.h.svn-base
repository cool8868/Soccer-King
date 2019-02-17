/********************************************************************************
 * 文件名：WillActionTrigger.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __WILLACTIONTRIGGER_H__
#define __WILLACTIONTRIGGER_H__

#include "../../../Interface/Skill/ITrigger.h"

/// 当球员满足行动条件时触发
class WillActionTrigger : public ITrigger
{
public:

    WillActionTrigger();

    WillActionTrigger(WillActionTrigger& rf);

public:

    int                 Target() { return m_Target; }
    void                SetTarget(int vl) { m_Target = vl; }

    vector<int>&        TargetPosition() { return m_TargetPosition; }
    void                SetTargetPosition(vector<int>& vl) { m_TargetPosition = vl; }
    void                SetTargetPosition(string vl) { m_TargetPosition = Common::TransToStringVector(vl, ","); }

    int                 IsSuccess() { return m_IsSuccess; }
    void                SetIsSuccess(int vl) { m_IsSuccess = vl; }

    string              State() { return m_States; }
    void                SetState(string vl);

    vector<string>&     TriggerStates() { return m_TriggerStates; }
    void                SetTriggerStates(vector<string>& vl) { m_TriggerStates = vl; }

    /// 判断该意志是否能够触发
    bool                IsSkillTriggered(IPlayer* player);

    WillActionTrigger*  Clone() { return new WillActionTrigger(*this); }

    bool                IsKindOf(string name) { return typeid(*this).name() == name; }

    void                SetAttribute(xml_node& node);

protected:

    /// 表示需要满足条件的球员
    int             m_Target;

    /// 表示了需要满足条件的球员的位置
    vector<int>     m_TargetPosition;

    /// 表示了是否需要该动作成功
    int             m_IsSuccess;

    /// 表示了触发的状态
    vector<string>  m_TriggerStates;

    /// 从配置导入的状态
    string          m_States;

private:

    //初始化变量
    void            InitVariables();
};

#endif //__WILLACTIONTRIGGER_H__
