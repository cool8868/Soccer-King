/********************************************************************************
 * 文件名：PlayerAttribute.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PLAYERATTRIBUTE_H__
#define __PLAYERATTRIBUTE_H__

#include "../../Interface/Player/Property/IPlayerAttribute.h"

/// 表示了球员的天生属性接口
class PlayerAttribute: public IPlayerAttribute
{
public:

    PlayerAttribute();

    unsigned int    PID() { return m_PID; }
    void            SetPID(unsigned int pid) { m_PID = pid; }

    int             CardLevel() { return m_CardLevel; }
    void            SetCardLevel(int cardLevel) { m_CardLevel = cardLevel; }

    int             Strengthen() { return m_Strengthen; }
    void            SetStrengthen(int strength) { m_Strengthen = strength; }

    Position        GetPosition() { return m_Position; }
    void            SetPosition(Position pos) { m_Position = pos; }

    int             HeadStyle() { return m_HeadStyle; }
    void            SetHeadStyle(int style) { m_HeadStyle = style; }

    int             BodyStyle() { return m_BodyStyle; }
    void            SetBodyStyle(int style) { m_BodyStyle = style; }

    string          FirstName() { return m_FirstName; }
    void            SetFirstName(string firstName) { m_FirstName = firstName; }

    string          FamilyName() { return m_FamilyName; }
    void            SetFamilyName(string familyName) { m_FamilyName = familyName; }

    int             Width();
    void            SetWidth(int width) { m_Width = width; }

    int             DefenceRange();
    void            SetDefenceRange(int range) { m_DefenceRange = range; }

    int             SightRange();
    void            SetSightRange(int range) { m_SightRange = range; }

    Region          MoveRegion() { return m_MoveRegion; }
    void            SetMoveRegion(Region region) { m_MoveRegion = region; }

private:

    //初始化变量
    void        InitVariables();

private:

    /// 表示了球员在卡库中的编号
    unsigned int    m_PID;

    /// 表示了卡的级别
    int             m_CardLevel;

    /// 表示了卡的强化级别
    int             m_Strengthen;

    /// 表示了球员的场上位置
    Position        m_Position;

    /// 表示了球员的头造型
    int             m_HeadStyle;

    /// 表示了球员的身体造型
    int             m_BodyStyle;

    /// 表示了球员的名字
    string          m_FirstName;

    /// 表示了球员的姓
    string          m_FamilyName;

    /// 表示了球员的宽度
    int             m_Width;

    /// 表示了球员的防守距离
    int             m_DefenceRange;

    /// 表示了球员的视野距离
    int             m_SightRange;

    /// 表示了球员的可移动区域
    Region          m_MoveRegion;
};

#endif //__PLAYERATTRIBUTE_H__
