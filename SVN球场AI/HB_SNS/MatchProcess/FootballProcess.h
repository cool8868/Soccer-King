/********************************************************************************
 * 文件名：FootballProcess.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FOOTBALLPROCESS_H__
#define __FOOTBALLPROCESS_H__

#include "MoveableProcess.h"
#include "../Interface/IFootballProcess.h"

/// 表示了足球的回合数据
class FootballProcess : public MoveableProcess, public IFootballProcess
{
public:

    FootballProcess();

public:

    //////////////////////////////////////////////////////////////////////////
    int         Angle() { return m_Angle; }
    void        SetAngle(int angle) { m_Angle = angle; }

    string      Destination() { return m_Destination; }
    void        SetDestination(string destination) { m_Destination = destination; }

    bool        IsInAir() { return m_IsInAir; }
    void        SetIsInAir(bool inAir) { m_IsInAir = inAir; }
    //////////////////////////////////////////////////////////////////////////
    string      GetCoordinate() { return MoveableProcess::GetCoordinate(); }
    void        SetCoordinate(string coord) { MoveableProcess::SetCoordinate(coord); }

    Coordinate  GetCoordinate2() { return MoveableProcess::GetCoordinate2(); }
    void        SetCoordinate2(Coordinate coord) { MoveableProcess::SetCoordinate2(coord); }

    int         Acceleration() { return MoveableProcess::Acceleration(); }
    void        SetAcceleration(int acc) { MoveableProcess::SetAcceleration(acc); }

    //////////////////////////////////////////////////////////////////////////
    Coordinate  Destination2() { return m_Destination2; }
    void        SetDestination2(Coordinate coord) { m_Destination2 = coord; }

    bool        Visible() { return m_Visible; }
    void        SetVisible(bool visible) { m_Visible = visible; }

    void        Output(CUtlBuffer& writer);
    void        Output(ofstream& writer);

    void        Read(ifstream& reader);
    void        Read(CUtlBuffer& reader);

private:

    //初始化变量
    void        InitVariables();

private:

    /// 表示了回合数
    int m_Round;

    /// 表示了足球的角度
    int m_Angle;

    /// 表示了足球的目标点
    string m_Destination;

    /// 表示了足球是否可见
    bool m_Visible;

    /// 表示了足球是否是在空中
    bool m_IsInAir;

    //////////////////////////////////////////////////////////////////////////
    /// 表示了足球的目标点（被写入在二进制流中）
    Coordinate m_Destination2;

private:

    const int m_Id;   // id
};

#endif //__FOOTBALLPROCESS_H__
